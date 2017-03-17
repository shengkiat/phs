﻿using PHS.Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            if (user == null || user.Sid == 0 || string.IsNullOrEmpty(user.Username))
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
                    unitOfWork.Persons.Get(user.Sid).Password = newPassHash;
                    unitOfWork.Persons.Get(user.Sid).UpdateDT = DateTime.Now;
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
                message = "Please enter Username and Password" ;
                return null;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new PHSContext()))
                {
                    var user = unitOfWork.Persons.Find(u => u.Username.Equals(userName, StringComparison.CurrentCultureIgnoreCase) && u.IsActive && !u.DeleteDT.HasValue).FirstOrDefault();

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
                            case Constants.User_Role_Student_Code:
                                authenticatedUser = user;
                                message = string.Empty;
                                break;
                            case Constants.User_Role_Instructor_Code:
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
                        message = "Invalid UserName";
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
            Person person = null;
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(userName.Trim()) )
            {
                message = "Username is empty!";
                return null;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new PHSContext()))
                {
                    var user = unitOfWork.Persons.Find(u => u.Username.Equals(userName, StringComparison.CurrentCultureIgnoreCase) && u.IsActive && !u.DeleteDT.HasValue).FirstOrDefault();

                    if (user != null)
                    {
                        message = string.Empty;
                        person = user;

                        return person;
                    }
                    else
                    {
                        message = "Username is not found!";
                        return person;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue("Person by User Name");
                return person;
            }
        }
    }
    
}
