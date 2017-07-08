
using System.ComponentModel.DataAnnotations;

namespace PHS.DB.ViewModels.Form
{
    public class AddressViewModel
    {
        public int? Id { get; set; }

        [Display(Name = "Blk")]
        public virtual string Blk { get; set; }

        [Display(Name = "Unit")]
        public virtual string Unit { get; set; }

        [Display(Name = "StreetAddress")]
        public virtual string StreetAddress { get; set; }

        [Display(Name = "Zip Code")]
        public virtual string ZipCode { get; set; }

        public static AddressViewModel Initialize()
        {
            return new AddressViewModel();
        }

    }
}