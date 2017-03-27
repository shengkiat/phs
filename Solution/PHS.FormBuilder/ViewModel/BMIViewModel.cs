using System;
using System.ComponentModel.DataAnnotations;

namespace PHS.FormBuilder.ViewModel
{
    public class BMIViewModel
    {
        [Display(Name = "Weight")]
        public virtual string Weight { get; set; }

        [Display(Name = "Height")]
        public virtual string Height { get; set; }

        [Display(Name = "BMI")]
        public virtual string BodyMassIndex
        {
            get
            {
               return (Convert.ToDouble(Weight) / ((Convert.ToDouble(Height) / 100) * ((Convert.ToDouble(Height) / 100)))).ToString("0.##");

            }
         }

    }
}
