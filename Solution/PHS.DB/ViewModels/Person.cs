using System;
using System.ComponentModel.DataAnnotations;

namespace PHS.DB
{
    [MetadataType(typeof(PersonMetadata))]
    public partial class Person
    {
        public class PersonMetadata
        {
            [Required(ErrorMessage = "Please enter User Name")]
            [Display(Name = "User Name")]
            public string Username { get; set; }

            [Required(ErrorMessage = "Please enter Full Name")]
            [Display(Name = "Full Name")]
            public string FullName { get; set; }

            [Display(Name = "Is Active")]
            public bool IsActive { get; set; }

            [Required(ErrorMessage = "Please enter Password")]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Display(Name = "Contact Number")]
            public string ContactNumber { get; set; }

            [Display(Name = "Created Date")]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
            public DateTime CreateDT { get; set; }

            [Display(Name = "Updated Date")]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", NullDisplayText = "-")]
            public DateTime? UpdateDT { get; set; }

            [Display(Name = "Deleted Date")]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", NullDisplayText = "-")]
            public DateTime? DeleteDT { get; set; }
        }
    }
}
