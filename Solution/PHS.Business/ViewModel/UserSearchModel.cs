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
        [Display(Name = "User id")]
        public string UserId { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        public IList<Person> persons { get; set; }
        //public IPagedList<Product> SearchResults { get; set; }
        public string SearchButton { get; set; }
    }
}
