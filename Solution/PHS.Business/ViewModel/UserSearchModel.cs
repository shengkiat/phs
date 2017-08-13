using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using PHS.DB;

namespace PHS.Business.ViewModel
{
    public class UserSearchModel
    {
        public string SearchBy { get; set; }
        public string Content { get; set; }
        public IList<PHSUser> users { get; set; }
        public string SearchButton { get; set; }
    }
}
