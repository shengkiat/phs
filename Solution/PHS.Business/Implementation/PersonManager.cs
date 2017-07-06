﻿using PHS.Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using PHS.DB;
using PHS.Repository;
using PHS.Repository.Context;
using PHS.Business.Common;
using PHS.Common;

namespace PHS.Business.Implementation
{
    public class PersonManager : BaseManager, IPersonManager, IManagerFactoryBase<IPersonManager>
    {
        public bool ChangePassword(Person user, string oldPass, string newPass, string newPassConfirm, out string message)
        {
            if (user == null || user.PersonID == 0 || string.IsNullOrEmpty(user.Username))
            {
                message = "Cannot find user";
                return false;
            }
            if (string.IsNullOrEmpty(oldPass))
            {
                message = "Please Enter Old Password";
                return false;
            }
            if (string.IsNullOrEmpty(newPass) || string.IsNullOrEmpty(newPass.Trim()))
            {
                message = "Please Enter New Password";
                return false;
            }
            if (string.IsNullOrEmpty(newPassConfirm) || string.IsNullOrEmpty(newPassConfirm.Trim()))
            {
                message = "Please Enter Confirmed New Password";
                return false;
            }
            if (!newPass.Trim().Equals(newPassConfirm, StringComparison.CurrentCultureIgnoreCase))
            {
                message = "Please confirm new password";
                return false;
            }
            if (!PasswordManager.IsPasswordComplex(newPass))
            {
                message = "Password must be a combination of at least 1 digit, 1 upper case letter, 1 lower case letter, 1 symbol and length of at least 8";
                return false;
            }
            var existingUser = IsAuthenticated(user.Username, oldPass, out message);
            if (existingUser == null)
            {
                message = "Invalid Password";
                return false;
            }
            string newPassHash = PasswordManager.CreateHash(newPass, user.PasswordSalt);

            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                try
                {
                    unitOfWork.Persons.Get(user.PersonID).Password = newPassHash;
                    unitOfWork.Persons.Get(user.PersonID).UsingTempPW = false;
                    unitOfWork.Persons.Get(user.PersonID).UpdatedDateTime = DateTime.Now;
                    unitOfWork.Complete();
                    return true;
                }
                catch (Exception ex)
                {
                    ExceptionLog(ex);
                    message = "Operation failed during saving Password. Please contact system admin";
                    return false;
                }
            }
        }

        public IPersonManager Create()
        {
            return new PersonManager();
        }

        public Person IsAuthenticated(Person user, out string message)
        {
            throw new NotImplementedException();
        }

        public Person IsAuthenticated(string userName, string password, out string message)
        {
            Person authenticatedUser = null;
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(userName.Trim()) || string.IsNullOrEmpty(password.Trim()))
            {
                message = "Please enter Username and Password";
                return null;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new PHSContext()))
                {
                    var user = unitOfWork.Persons.Find(u => u.Username.Equals(userName, StringComparison.CurrentCultureIgnoreCase) && u.IsActive && !u.DeleteStatus).FirstOrDefault();

                    if (user != null)
                    {
                        if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.PasswordSalt))
                        {
                            message = "User data is corrupted. Please contact system admin";
                            return null;
                        }

                        if (!PasswordManager.ValidatePassword(password, user.Password, user.PasswordSalt))
                        {
                            message = "Invalid Username or Password";
                            return authenticatedUser;
                        }

                        switch (user.Role)
                        {
                            case Constants.User_Role_Doctor_Code:
                                authenticatedUser = user;
                                message = string.Empty;
                                break;
                            case Constants.User_Role_Volunteer_Code:
                                authenticatedUser = user;
                                message = string.Empty;
                                break;

                            case Constants.User_Role_Admin_Code:
                                authenticatedUser = user;
                                message = string.Empty;
                                break;

                            default:
                                message = "Invalid Username or Password";
                                return authenticatedUser;
                        }

                    }
                    else
                    {
                        message = "Invalid Username or Password";
                        return authenticatedUser;
                    }
                }
                return authenticatedUser;
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = "Operation failed during authenticating: " + userName + ". Please contact system admin";
                return authenticatedUser;
            }
        }

        #region Search
        public IList<Person> GetAllPersons(out string message)
        {
            message = string.Empty;
            List<Person> list = new List<Person>();

            try
            {
                using (var unitOfWork = new UnitOfWork(new PHSContext()))
                {
                    var persons = unitOfWork.Persons.GetAll();
                    if (persons == null || persons.Count() == 0)
                    {
                        message = Constants.ThereIsNoValueFound("User");
                        return null;
                    }

                    foreach (var person in persons)
                    {
                        if (!person.DeleteStatus)
                        {
                            list.Add(person);
                        }
                    }
                    if (list.Count == 0)
                    {
                        message = Constants.ThereIsNoValueFound("User");
                        return null;
                    }
                    message = string.Empty;
                    return list;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue("User");
                return null;
            }
        }

        public Person GetPersonByPersonSid(int personSid, out string message)
        {
            message = string.Empty;
            try
            {
                using (var unitOfWork = new UnitOfWork(new PHSContext()))
                {
                    var person = unitOfWork.Persons.Get(personSid);

                    if (person == null)
                    {
                        message = "Invalid User Id";
                        return null;
                    }

                    message = string.Empty;
                    return person;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue("Person by personSid");
                return null;
            }
        }

        public Person GetPersonByUserName(string userName, out string message)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(userName.Trim()))
            {
                message = "User Id is empty!";
                return null;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new PHSContext()))
                {
                    var users = unitOfWork.Persons.Find(u => u.Username.Equals(userName, StringComparison.CurrentCultureIgnoreCase) && !u.DeleteStatus);

                    if (users != null && users.Any())
                    {
                        message = string.Empty;
                        return users.ToList()[0];
                    }
                    else
                    {
                        message = "User not found!";
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue("Person by Full Name");
                return null;
            }
        }

        public IList<Person> GetPersonsByUserID(string userId, out string message)
        {
            IList<Person> persons = null;
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(userId.Trim()))
            {
                message = "User ID is empty!";
                return null;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new PHSContext()))
                {
                    var users = unitOfWork.Persons.Find(u => u.Username.Contains(userId) && !u.DeleteStatus);

                    if (users != null && users.Any())
                    {
                        message = string.Empty;
                        persons = users.ToList();

                        return persons;
                    }
                    else
                    {
                        message = "User not found!";
                        return persons;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue("Person by Full Name");
                return persons;
            }
        }

        public IList<Person> GetPersonsByFullName(string fullName, out string message)
        {
            IList<Person> persons = null;
            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(fullName.Trim()))
            {
                message = "Name is empty!";
                return null;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new PHSContext()))
                {
                    var users = unitOfWork.Persons.Find(u => u.FullName.Contains(fullName) && !u.DeleteStatus);

                    if (users != null && users.Any())
                    {
                        message = string.Empty;
                        persons = users.ToList();

                        return persons;
                    }
                    else
                    {
                        message = "User not found!";
                        return persons;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue("Person by Full Name");
                return persons;
            }
        }
        #endregion

        /*
         *Will check username
         *If username exists, will return null and username already exists message 
        */
        public Person AddPerson(Person loginPerson, Person person, out string message)
        {
            message = string.Empty;
            if (person == null)
            {
                message = Constants.PleaseFillInAllRequiredFields();
                return null;
            }
            if (string.IsNullOrEmpty(person.Username) || string.IsNullOrEmpty(person.Username.Trim()))
            {
                message = Constants.PleaseFillInAllRequiredFields();
                return null;
            }
            if (UserNameExists(person.Username, out message))
            {
                return null;
            }
            if (string.IsNullOrEmpty(person.FullName) || string.IsNullOrEmpty(person.FullName.Trim()))
            {
                message = Constants.PleaseFillInAllRequiredFields();
                //message = Constants.PleaseEnterValue(Constants.FullName);
                return null;
            }
            var hashedUser = GenerateHashedUser(person, out message);
            if (hashedUser == null)
            {
                return null;
            }
            person = hashedUser;
            try
            {
                using (var unitOfWork = new UnitOfWork(new PHSContext(loginPerson)))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        person.CreatedDateTime = DateTime.Now;
                        person.CreatedBy = loginPerson.Username;
                        person.UsingTempPW = true;
                        unitOfWork.Persons.Add(person);
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                    message = string.Empty;
                    return person;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringAddingValue("Person");
                return null;
            }
        }

        /// <summary>
        /// Will NOT check whether username exists
        /// Username is not allowed to be changed
        /// </summary>
        /// <param name="loginPerson">Person performing the update</param>
        /// <param name="person">Person to be updated</param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool UpdatePerson(Person loginPerson, Person person, out string message)
        {
            message = string.Empty;
            if (person == null)
            {
                message = Constants.PleaseFillInAllRequiredFields();
                return false;
            }
            if (string.IsNullOrEmpty(person.Username) || string.IsNullOrEmpty(person.Username.Trim()))
            {
                message = Constants.PleaseFillInAllRequiredFields();
                return false;
            }
            if (string.IsNullOrEmpty(person.FullName) || string.IsNullOrEmpty(person.FullName.Trim()))
            {
                message = Constants.PleaseFillInAllRequiredFields();
                return false;
            }
            if (string.IsNullOrEmpty(person.Role) || string.IsNullOrEmpty(person.Role.Trim()))
            {
                message = Constants.PleaseFillInAllRequiredFields();
                return false;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new PHSContext(loginPerson)))
                {
                    var personToUpdate = unitOfWork.Persons.Get(person.PersonID);
                    Util.CopyNonNullProperty(person, personToUpdate);
                    personToUpdate.UpdatedDateTime = DateTime.Now;
                    personToUpdate.UpdatedBy = loginPerson.Username;
                    using (TransactionScope scope = new TransactionScope())
                    {
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                }
                message = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringUpdatingValue("Person");
                return false;
            }
        }

        public bool DeletePerson(Person loginPerson,int personid, out string message)
        {
            message = string.Empty;
            try
            {
                using (var unitOfWork = new UnitOfWork(new PHSContext(loginPerson)))
                {
                    var person = unitOfWork.Persons.Get(personid);

                    if (person == null)
                    {
                        message = "Invalid User Id";
                        return false;
                    }
                    person.UpdatedDateTime = DateTime.Now;
                    person.UpdatedBy = loginPerson.Username;
                    person.DeleteStatus = true;
                    using (TransactionScope scope = new TransactionScope())
                    {
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                    message = string.Empty;
                    return true;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = "Operation failed during deleting user";
                return false;
            }
        }
        public bool UserNameExists(string userName, out string message)
        {
            if (string.IsNullOrEmpty(userName))
            {
                message = Constants.ValueIsEmpty("User Id");
                return true;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new PHSContext()))
                {
                    var user = unitOfWork.Persons.Find(u => u.Username.Equals(userName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                    if (user != null)
                    {
                        message = Constants.ValueAlreadyExists(userName);
                        return true;
                    }
                }
                message = string.Empty;
                return false;
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue("User Id");
                return true;
            }
        }

        public Person GenerateHashedUser(Person person, out string message)
        {
            if (person == null)
            {
                message = Constants.ValueIsEmpty("User");
                return null;
            }
            if (string.IsNullOrEmpty(person.Username))
            {
                message = Constants.ValueIsEmpty("User Id");
                return null;
            }
            if (string.IsNullOrEmpty(person.Password))
            {
                message = Constants.ValueIsEmpty("Password");
                return null;
            }

            try
            {
                person.PasswordSalt = PasswordManager.GenerateSalt();
                person.Password = PasswordManager.CreateHash(person.Password, person.PasswordSalt);
                message = string.Empty;
                return person;
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringAddingValue("Person");
                return null;
            }

        }
    }
}
