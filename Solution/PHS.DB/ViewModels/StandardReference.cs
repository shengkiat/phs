using PHS.DB.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace PHS.DB
{
    [TrackChangesAttribute]
    [MetadataType(typeof(StandardReferenceMetadata))]
    public partial class StandardReference
    {
        public class StandardReferenceMetadata
        {
            public int StandardReferenceID { get; set; }

            [Required(ErrorMessage = "Please enter Title")]
            [Display(Name = "Title")]
            public string Title { get; set; }
        }
    }
}