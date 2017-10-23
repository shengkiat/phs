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
        public IList<FollowUpMgmtViewModel> RetrieveScreeningEventParticipants(int? phsEventId)
        {
            using (var unitOfWork = CreateUnitOfWork())
            {
                //FollowUpMgmtViewModel result = new FollowUpMgmtViewModel();
                //result.PHSEvents = unitOfWork.Events.GetAllActiveEvents().ToList();

                var result = new List<FollowUpMgmtViewModel>();

                var followupgroups = unitOfWork.FollowUpConfigurations.GetFollowUpConfiguration(1).FollowUpGroups;
                if (followupgroups.Count > 0)
                {
                    foreach (var item in followupgroups)
                    {
                        var fugroup = new FollowUpMgmtViewModel();
                        fugroup.FollowUpGroup = item;
                        fugroup.Participants = unitOfWork.Participants.findparticipants().ToList();
                        result.Add(fugroup);
                    }
                }
                if (phsEventId.HasValue)
                {
                    //result.PHSEventId = phsEventId.Value;
                    //result.Participants = unitOfWork.Participants.FindParticipants(u => u.ParticipantJourneyModalities.Any(e => e.PHSEventID == phsEventId.Value && e.ModalityID == 1 && e.FormID == 8)).ToList();
                    //result.Participants = unitOfWork.Participants.findparticipants().ToList();
                    //return dbContext.Set<FollowUpConfiguration>().Where(u => u.FollowUpConfigurationID == id).Include(b => b.FollowUpGroups).FirstOrDefault();

                }

                return result;
            }
        }
    }
}
