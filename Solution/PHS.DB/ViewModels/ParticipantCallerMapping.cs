using PHS.DB.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PHS.DB
{
    [TrackChangesAttribute]
    [MetadataType(typeof(ParticipantCallerMappingMetadata))]
    public partial class ParticipantCallerMapping
    {
        public class ParticipantCallerMappingMetadata
        {
            public int ParticipantCallerMappingID { get; set; }
            public int ParticipantID { get; set; }
            public int FollowUpGroupID { get; set; }
            public string PhaseIFollowUpVolunteer { get; set; }
            public string PhaseIFollowUpVolunteerCallStatus { get; set; }

            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public Nullable<System.DateTime> PhaseIFollowUpVolunteerCallDateTime { get; set; }
            public string PhaseIFollowUpVolunteerCallRemark { get; set; }
            public string PhaseICommitteeMember { get; set; }
            public string PhaseICommitteeMemberCallStatus { get; set; }

            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public Nullable<System.DateTime> PhaseICommitteeMemberCallDateTime { get; set; }

            public string PhaseICommitteeMemberCallRemark { get; set; }
            public string PhaseIIFollowUpVolunteer { get; set; }
            public string PhaseIIFollowUpVolunteerCallStatus { get; set; }

            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public Nullable<System.DateTime> PhaseIIFollowUpVolunteerCallDateTime { get; set; }

            public string PhaseIIFollowUpVolunteerCallRemark { get; set; }
            public string PhaseIICommitteeMember { get; set; }
            public string PhaseIICommitteeMemberCallStatus { get; set; }

            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public Nullable<System.DateTime> PhaseIICommitteeMemberCallDateTime { get; set; }

            public string PhaseIICommitteeMemberCallRemark { get; set; }
        }
    }
}
