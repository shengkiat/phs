﻿using Foolproof;
using PHS.DB.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace PHS.DB
{
    [TrackChangesAttribute]
    [MetadataType(typeof(ReferenceRangeMetadata))]
    public partial class ReferenceRange
    {
        public class ReferenceRangeMetadata
        {
            public int ReferenceRangeID { get; set; }

            [Required(ErrorMessage = "Please enter Title")]
            [Display(Name = "Title")]
            public string Title { get; set; }

            [Required(ErrorMessage = "Please enter Minimum value")]
            [Display(Name = "Minimum Value")]
            public double MinimumValue { get; set; }

            [Required(ErrorMessage = "Please enter Maximum value")]
            [GreaterThan("MinimumValue")]
            [Display(Name = "Maximum Value")]
            public double MaximumValue { get; set; }

            [Required(ErrorMessage = "Please enter Result string")]
            [Display(Name = "Result String")]
            public string Result { get; set; }
            public int StandardReferenceID { get; set; }
        }
    }
}