using PHS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.Business.Interface
{
    public interface IPersonManager : IDisposable
    {

        Person IsAuthenticated(string userName, string password, out string message);
        Person IsAuthenticated(Person user, out string message);

        bool ChangePassword(Person user, string oldPass, string newPass, string newPassConfirm, out string message);




    }
}
