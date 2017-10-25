using PHS.Business.Interface;
using PHS.Business.ViewModel.FollowUp;
using PHS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

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
        public IList<FollowUpGroup> GetParticipantsByFollowUpConfiguration(int? followupconfigurationid)
        {
            using (var unitOfWork = CreateUnitOfWork())
            {
                var result = new List<FollowUpGroup>();
                var followupconfig = unitOfWork.FollowUpConfigurations.GetFollowUpConfiguration(followupconfigurationid.Value);
                if (followupconfig != null)
                {
                    //if (followupconfig.Deploy)
                    //{
                    //    result = followupconfig.FollowUpGroups.ToList();
                    //}
                    //else
                    {

                        var eventid = unitOfWork.FollowUpConfigurations.GetFollowUpConfiguration(followupconfigurationid.Value).PHSEventID;
                        var finalgroupparticipants = unitOfWork.Participants.FindParticipants(p => p.PHSEvents.Any(e => e.PHSEventID == eventid));
                        var followupgroups = followupconfig.FollowUpGroups;
                        if (followupgroups.Count > 0)
                        {
                            foreach (var item in followupgroups)
                            {
                                var participantCallerMappingList = new List<ParticipantCallerMapping>();
                                var participantsbygroup = unitOfWork.Participants.SearchParticipants(item.Filter);
                                participantsbygroup = finalgroupparticipants.Intersect(participantsbygroup).ToList();
                                finalgroupparticipants = finalgroupparticipants.Except(participantsbygroup);
                                var participantcallermapping = new ParticipantCallerMapping();
                                foreach (var participant in participantsbygroup)
                                {
                                    participantcallermapping.Participant = participant;
                                    participantCallerMappingList.Add(participantcallermapping);

                                }
                                item.ParticipantCallerMappings = participantCallerMappingList;
                                result.Add(item);
                            }
                            var endfugroup = new FollowUpGroup();
                            var endparticipantCallerMappingList = new List<ParticipantCallerMapping>();
                            foreach (var participant in finalgroupparticipants.ToList())
                            {
                                var endparticipantcallermapping = new ParticipantCallerMapping();
                                endparticipantcallermapping.Participant = participant;
                                endparticipantCallerMappingList.Add(endparticipantcallermapping);

                            }
                            endfugroup.ParticipantCallerMappings = endparticipantCallerMappingList;
                            result.Add(endfugroup);
                        }
                    }
                }

                return result;
            }
        }

        public bool DeployFollowUpConfiguration(int followupconfigurationid, out string message)
        {
            message = string.Empty;
            using (var unitOfWork = CreateUnitOfWork())
            {
                var followupconfiguration = unitOfWork.FollowUpConfigurations.GetFollowUpConfiguration(followupconfigurationid);
                if(followupconfiguration == null)
                {
                    message = "Follow-up configuration does not exist!";
                    return false;
                }
                if (followupconfiguration.Deploy)
                {
                    message = "Follow-up configuration is already deployed!";
                    return false;
                }
                else
                {
                    var modelToUpdate = unitOfWork.FollowUpConfigurations.GetFollowUpConfiguration(followupconfigurationid);
                    modelToUpdate.Deploy = true;

                    using (TransactionScope scope = new TransactionScope())
                    {
                        unitOfWork.Complete();
                        scope.Complete();
                    }

                    return true;
                }
            }
        }

        public bool PrintHealthReportByFollowUpGroup(int followgroupid, out string message)
        {
            message = string.Empty;
            using (var unitOfWork = CreateUnitOfWork())
            {
                var followupgroup = unitOfWork.FollowUpGroups.Find(u => u.FollowUpGroupID == followgroupid).FirstOrDefault();
                if (followupgroup == null)
                {
                    message = "Follow-up group does not exist!";
                    return false;
                }
                var followupconfig = unitOfWork.FollowUpConfigurations.Find(u => u.FollowUpConfigurationID == followupgroup.FollowUpConfigurationID).FirstOrDefault();

                if (!followupgroup.FollowUpConfiguration.Deploy)
                {
                    message = "Follow-up configuration is not deployed!";
                    return false;
                }
                var printmodellist = new List<FollowUpMgmtViewModel>();

                if (followupgroup.ParticipantCallerMappings.Count > 0)
                {
                    foreach( var participantcallermapping in followupgroup.ParticipantCallerMappings)
                    {
                        var printmodel = new FollowUpMgmtViewModel();
                        printmodel.Participant = participantcallermapping.Participant;

                        //TODO: get values from followup modality forms
                        printmodel.BMIvalueStdRefPair = Tuple.Create("22", "normal");
                        printmodel.BPValueStdRefPair = Tuple.Create("120/80", "normal");
                        printmodel.BloodTestResult = "Satisfactory";
                        printmodel.OverAllResult = "satisfactory";

                        printmodellist.Add(printmodel);
                    }
                }

                return true;
            }
        }
    }
}
