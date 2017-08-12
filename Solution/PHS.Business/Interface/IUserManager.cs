using PHS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.Business.Interface
{
    public interface IUserManager : IDisposable
    {

        PHSUser IsAuthenticated(string userName, string password, out string message);

        bool ChangePassword(PHSUser user, string oldPass, string newPass, string newPassConfirm, out string message);

        IList<PHSUser> GetAllUsers(out string message);

        PHSUser GetUserByID(int userID, out string message);

        PHSUser GetUserByUserName(string userName, out string message);

        IList<PHSUser> GetUsersByUserName(string userName, out string message);

        IList<PHSUser> GetUsersByFullName(string fullName, out string message);

        bool AddUser(PHSUser loginUser, PHSUser user, out string message);

        bool UpdateUser(PHSUser loginUser, PHSUser user, out string message);

        bool DeleteUser(int userID, out string message);
    }
}
