using PHS.Business.Interface;
using PHS.Business.ViewModel.FollowUp;
using PHS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.Business.Implementation
{
    public class FollowUpManager : BaseManager, IFollowUpManager, IManagerFactoryBase<IFollowUpManager>
    {
        public FollowUpManager() : base(null)
        {
        }

        public FollowUpManager(PHSUser loginUser) : base(loginUser)
        {
        }

        public IFollowUpManager Create(PHSUser loginUser)
        {
            return new FollowUpManager(loginUser);
        }
        public IList<FollowUpMgmtViewModel> GetParticipantsByFollowUpConfiguration(int? followupconfigurationid)
        {
            using (var unitOfWork = CreateUnitOfWork())
            {
                var result = new List<FollowUpMgmtViewModel>();
                var finalgroupparticipants = unitOfWork.Participants.FindParticipants(p => p.PHSEvents.Any(e => e.PHSEventID == 3));
                var followupgroups = unitOfWork.FollowUpConfigurations.GetFollowUpConfiguration(followupconfigurationid.Value).FollowUpGroups;
                if (followupgroups.Count > 0)
                {
                    foreach (var item in followupgroups)
                    {
                        var participantsbygroup = unitOfWork.Participants.SearchParticipants(item.Filter);
                        var fugroup = new FollowUpMgmtViewModel();
                        fugroup.FollowUpGroup = item;
                        fugroup.Participants = finalgroupparticipants.Intersect(participantsbygroup).ToList();
                        finalgroupparticipants = finalgroupparticipants.Except(participantsbygroup);
                        result.Add(fugroup);
                    }
                    var endfugroup = new FollowUpMgmtViewModel();
                    endfugroup.FollowUpGroup = new FollowUpGroup();
                    endfugroup.Participants = finalgroupparticipants.ToList();
                    result.Add(endfugroup);
                }

                return result;
            }
        }
    }
}
