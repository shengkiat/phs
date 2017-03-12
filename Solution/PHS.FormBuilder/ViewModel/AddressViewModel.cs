using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PHS.FormBuilder.ViewModels
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

        public virtual string State { get; set; }

        [Display(Name = "Zip Code")]
        public virtual string ZipCode { get; set; }

        public virtual string Country { get; set; }

        public string Longitude { get; set; }

        public string Latitude { get; set; }


        public static AddressViewModel Initialize()
        {
            return new AddressViewModel();
        }

    }
}