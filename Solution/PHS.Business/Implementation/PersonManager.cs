using PHS.Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PHS.DB;
using PHS.Repository;
using PHS.Repository.Context;
using PHS.Business.Common;

namespace PHS.Business.Implementation
{
    public class PersonManager : BaseManager, IPersonManager, IManagerFactoryBase<IPersonManager>
    {
        public bool ChangePassword(Person user, string oldPass, string newPass, string newPassConfirm, out string message)
        {
            throw new NotImplementedException();
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
    }
}
