using PHS.Business.Interface;
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
using System.Security;

namespace PHS.Business.Implementation
{
    public class UserManager : BaseManager, IUserManager, IManagerFactoryBase<IUserManager>
    {
        public IUserManager Create()
        {
            return new UserManager();
        }

        public bool ChangePassword(PHSUser user, string oldPass, string newPass, string newPassConfirm, out string message)
        {
            if (user == null || user.PHSUserID == 0 || string.IsNullOrEmpty(user.Username))
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

            SecureString newPassHash = PasswordManager.CreateHash(newPass, user.PasswordSalt);

            using (var unitOfWork = CreateUnitOfWork())
            {
                try
                {
                    unitOfWork.Users.Get(user.PHSUserID).Password = PasswordManager.SecureStringToString(newPassHash);
                    unitOfWork.Users.Get(user.PHSUserID).UsingTempPW = false;
                    unitOfWork.Users.Get(user.PHSUserID).UpdatedDateTime = DateTime.Now;
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

        public PHSUser IsAuthenticated(string userName, string password, out string message)
        {
            PHSUser authenticatedUser = null;
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(userName.Trim()) || string.IsNullOrEmpty(password.Trim()))
            {
                message = "Please enter Username and Password";
                return null;
            }
            try
            {
                using (var unitOfWork = CreateUnitOfWork())
                {
                    var user = unitOfWork.Users.Find(u => u.Username.Equals(userName, StringComparison.CurrentCultureIgnoreCase) && !u.DeleteStatus).FirstOrDefault();

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

                        if (!GetCurrentActiveStatus(user))
                        {
                            message = "Account is inactive. Please contact system admin";
                            return authenticatedUser;
                        }

                        switch (user.Role)
                        {
                            case Constants.User_Role_Doctor_Code:
                                
                            case Constants.User_Role_Volunteer_Code:
                                
                            case Constants.User_Role_CommitteeMember_Code:
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
        public IList<PHSUser> GetAllUsers(out string message)
        {
            message = string.Empty;
            List<PHSUser> list = new List<PHSUser>();

            try
            {
                using (var unitOfWork = CreateUnitOfWork())
                {
                    var users = unitOfWork.Users.GetAll().Where(e => e.DeleteStatus == false);
                    if (users == null || users.Count() == 0)
                    {
                        message = Constants.ThereIsNoValueFound("User");
                        return null;
                    }

                    foreach (var user in users)
                    {
                        user.IsActive = GetCurrentActiveStatus(user);
                        list.Add(user);
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

        public PHSUser GetUserByID(int userID, out string message)
        {
            message = string.Empty;
            try
            {
                using (var unitOfWork = CreateUnitOfWork())
                {
                    var user = unitOfWork.Users.Find(u => u.PHSUserID.Equals(userID) && !u.DeleteStatus);

                    if (user != null && user.Any())
                    {
                        message = string.Empty;
                        return user.FirstOrDefault();
                    }
                    else
                    {
                        message = "User not found.";
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue("User by ID");
                return null;
            }
        }

        public PHSUser GetUserByUserName(string userName, out string message)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(userName.Trim()))
            {
                message = "User Name is empty.";
                return null;
            }
            try
            {
                using (var unitOfWork = CreateUnitOfWork())
                {
                    var users = unitOfWork.Users.Find(u => u.Username.Equals(userName, StringComparison.CurrentCultureIgnoreCase) && !u.DeleteStatus);

                    if (users != null && users.Any())
                    {
                        message = string.Empty;
                        return users.FirstOrDefault();
                    }
                    else
                    {
                        message = "User not found.";
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue("User by Full Username");
                return null;
            }
        }

        public IList<PHSUser> GetUsersByUserName(string userName, out string message)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(userName.Trim()))
            {
                message = "User Name is empty.";
                return null;
            }
            try
            {
                using (var unitOfWork = CreateUnitOfWork())
                {
                    var users = unitOfWork.Users.Find(u => u.Username.Contains(userName) && !u.DeleteStatus);

                    if (users != null && users.Any())
                    {
                        message = string.Empty;
                        return users.ToList();
                    }
                    else
                    {
                        message = "User not found.";
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue("User by Username");
                return null;
            }
        }

        public IList<PHSUser> GetUsersByFullName(string fullName, out string message)
        {
            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(fullName.Trim()))
            {
                message = "Name is empty.";
                return null;
            }
            try
            {
                using (var unitOfWork = CreateUnitOfWork())
                {
                    var users = unitOfWork.Users.Find(u => u.FullName.Contains(fullName) && !u.DeleteStatus);

                    if (users != null && users.Any())
                    {
                        message = string.Empty;
                        return users.ToList();
                    }
                    else
                    {
                        message = "User not found.";
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue("User by Full Name");
                return null;
            }
        }
        #endregion

        /*
         *Will check username
         *If username exists, will return null and username already exists message 
        */
        public bool AddUser(PHSUser loginUser, PHSUser user, out string message)
        {
            message = string.Empty;
            if (user == null)
            {
                message = Constants.PleaseFillInAllRequiredFields();
                return false;
            }
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Username.Trim()))
            {
                message = Constants.PleaseFillInAllRequiredFields();
                return false;
            }
            if (UserNameExists(user.Username, user.PHSUserID, out message))
            {
                message = "Username already exists";
                return false;
            }
            if (string.IsNullOrEmpty(user.FullName) || string.IsNullOrEmpty(user.FullName.Trim()))
            {
                message = Constants.PleaseFillInAllRequiredFields();
                return false;
            }
            if (string.IsNullOrEmpty(user.Role) || string.IsNullOrEmpty(user.Role.Trim()))
            {
                message = Constants.PleaseFillInAllRequiredFields();
                return false;
            }

            var hashedUser = GenerateHashedUser(user, out message);
            if (hashedUser == null)
            {
                return false;
            }
            user = hashedUser;
            try
            {
                using (var unitOfWork = CreateUnitOfWork())
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        user.CreatedDateTime = DateTime.Now;
                        user.CreatedBy = loginUser.Username;
                        user.UsingTempPW = true;
                        unitOfWork.Users.Add(user);
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
                message = Constants.OperationFailedDuringAddingValue("User");
                return false;
            }
        }

        public bool UpdateUser(PHSUser loginUser, PHSUser user, out string message)
        {
            message = string.Empty;
            if (user == null)
            {
                message = Constants.PleaseFillInAllRequiredFields();
                return false;
            }
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Username.Trim()))
            {
                message = Constants.PleaseFillInAllRequiredFields();
                return false;
            }
            if (UserNameExists(user.Username, user.PHSUserID, out message))
            {
                message = "Username already exists";
                return false;
            }
            if (string.IsNullOrEmpty(user.FullName) || string.IsNullOrEmpty(user.FullName.Trim()))
            {
                message = Constants.PleaseFillInAllRequiredFields();
                return false;
            }
            if (string.IsNullOrEmpty(user.Role) || string.IsNullOrEmpty(user.Role.Trim()))
            {
                message = Constants.PleaseFillInAllRequiredFields();
                return false;
            }
            try
            {
                using (var unitOfWork = CreateUnitOfWork())
                {
                    var userToUpdate = unitOfWork.Users.Get(user.PHSUserID);
                    Util.CopyNonNullProperty(user, userToUpdate);
                    userToUpdate.UpdatedDateTime = DateTime.Now;
                    userToUpdate.UpdatedBy = loginUser.Username;
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
                message = Constants.OperationFailedDuringUpdatingValue("User");
                return false;
            }
        }

        public bool DeleteUser(int userID, out string message)
        {
            message = string.Empty;
            try
            {
                using (var unitOfWork = CreateUnitOfWork())
                {
                    var user = unitOfWork.Users.Get(userID);

                    if (user == null)
                    {
                        message = "User not found.";
                        return false;
                    }
                    user.DeleteStatus = true;
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
                message = Constants.OperationFailedDuringDeletingValue("User");
                return false;
            }
        }

        public bool ResetPassword(PHSUser loginUser, String[] selectedusers, string tempPW, out string message)
        {
            message = string.Empty;

            if (selectedusers == null || selectedusers.Length == 0)
            {
                message = "No Selection made!";
                return false;
            }

            try
            {
                using (var unitOfWork = CreateUnitOfWork())
                {
                    using (TransactionScope scope = new TransactionScope())
                    {

                        foreach (var username in selectedusers)
                        {
                            var user = GetUserByUserName(username.ToString(), out message);
                            SecureString newPassHash = PasswordManager.CreateHash(tempPW, user.PasswordSalt);
                            user.Password = PasswordManager.SecureStringToString(newPassHash);
                            user.UsingTempPW = true;
                            if (!UpdateUser(loginUser, user, out message))
                            {
                                return false;
                            }
                        }

                        unitOfWork.Complete();
                        scope.Complete();
                    }
                }

                return true;
            }
            catch(Exception ex)
            {
                ExceptionLog(ex);
                message = "Operation failed during reset Password.";
                return false;
            }

        }

        public bool UserNameExists(string userName, int userID, out string message)
        {
            if (string.IsNullOrEmpty(userName))
            {
                message = Constants.ValueIsEmpty("User Id");
                return true;
            }
            try
            {
                using (var unitOfWork = CreateUnitOfWork())
                {
                    var user = unitOfWork.Users.Find(u => u.Username.Equals(userName, StringComparison.CurrentCultureIgnoreCase) && u.PHSUserID != userID).FirstOrDefault();
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
                message = Constants.OperationFailedDuringRetrievingValue("User username");
                return true;
            }
        }

        public PHSUser GenerateHashedUser(PHSUser user, out string message)
        {
            if (user == null)
            {
                message = Constants.ValueIsEmpty("User");
                return null;
            }
            if (string.IsNullOrEmpty(user.Username))
            {
                message = Constants.ValueIsEmpty("User Id");
                return null;
            }
            if (string.IsNullOrEmpty(user.Password))
            {
                message = Constants.ValueIsEmpty("Password");
                return null;
            }

            try
            {
                user.PasswordSalt = PasswordManager.GenerateSalt();
                user.Password = PasswordManager.SecureStringToString(PasswordManager.CreateHash(user.Password, user.PasswordSalt));
                message = string.Empty;
                return user;
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringAddingValue("User");
                return null;
            }

        }

        public bool GetCurrentActiveStatus(PHSUser user)
        {
            if (user == null)
            {
                return false;
            }

            if(user.IsActive)
            {
                if(DateTime.Now >= user.EffectiveStartDate && DateTime.Now <= user.EffectiveEndDate)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
