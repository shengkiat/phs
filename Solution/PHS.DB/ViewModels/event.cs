using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PHS.DB
{
    [MetadataType(typeof(EventMetadata))]
    public partial class PHSEvent
    {
        public class EventMetadata
        {

            public int PHSEventID { get; set; }

            [Required(ErrorMessage = "Please enter the title for the event")]
            [Display(Name = "Title")]
            public string Title { get; set; }

            [Required(ErrorMessage = "Please enter the start date")]
            [Display(Name = "Start Date")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public System.DateTime StartDT { get; set; }

            [Required(ErrorMessage = "Please enter the end date")]
            [Display(Name = "End Date")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public System.DateTime EndDT { get; set; }

            [Required(ErrorMessage = "Please enter the event venue")]
            [Display(Name = "Venue")]
            public string Venue { get; set; }
        }
    }
}
