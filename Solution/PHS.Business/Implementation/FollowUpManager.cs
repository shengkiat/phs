using PHS.Business.Interface;
using PHS.Business.ViewModel.FollowUp;
using PHS.Common;
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

        public IList<FollowUpGroup> GetParticipantsByLoginUser(int eventid, PHSUser loginuser, out string message)
        {
            message = string.Empty;
            using (var unitOfWork = CreateUnitOfWork())
            {
                var result = new List<FollowUpGroup>();
                var followupconfig = unitOfWork.FollowUpConfigurations.GetDeployedFollowUpConfiguration(eventid);
                if (followupconfig == null)
                {
                    message = "No follow-up configuration is deployed!!";
                    return result;
                }
                if (loginuser.Role == Constants.User_Role_CommitteeMember_Code)
                {
                    return followupconfig.FollowUpGroups.ToList();
                }
                else if (loginuser.Role == Constants.User_Role_FollowUpVolunteer_Code)
                {

                    var followupgroups = followupconfig.FollowUpGroups;

                    if (followupgroups.Count > 0)
                    {
                        foreach (var item in followupgroups)
                        {
                            var resultfollowupgroup = new FollowUpGroup();
                            var resultparticipantcallmaplist = new List<ParticipantCallerMapping>();
                            foreach (var map in item.ParticipantCallerMappings)
                            {
                                if (map.FollowUpVolunteer == loginuser.FullName)
                                {
                                    resultparticipantcallmaplist.Add(map);
                                }
                            }
                            resultfollowupgroup.ParticipantCallerMappings = resultparticipantcallmaplist;
                            result.Add(resultfollowupgroup);
                        }
                    }
                    else
                    {
                        message = "No follow-up group present!!";
                    }
                }
                return result;
            }
        }

        public IList<FollowUpGroup> GetParticipantsByFollowUpConfiguration(int followupconfigurationid)
        {
            using (var unitOfWork = CreateUnitOfWork())
            {
                var result = new List<FollowUpGroup>();
                var followupconfig = unitOfWork.FollowUpConfigurations.GetFollowUpConfiguration(followupconfigurationid);
                if (followupconfig != null)
                {
                    if (followupconfig.Deploy)
                    {
                        result = followupconfig.FollowUpGroups.ToList();
                    }
                    else
                    {

                        var eventid = unitOfWork.FollowUpConfigurations.GetFollowUpConfiguration(followupconfigurationid).PHSEventID;
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
                if (followupconfiguration == null)
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


                    var eventid = unitOfWork.FollowUpConfigurations.GetFollowUpConfiguration(followupconfigurationid).PHSEventID;
                    var finalgroupparticipants = unitOfWork.Participants.FindParticipants(p => p.PHSEvents.Any(e => e.PHSEventID == eventid));
                    var followupgroups = followupconfiguration.FollowUpGroups;
                    if (followupgroups.Count > 0)
                    {
                        foreach (var item in followupgroups)
                        {
                            var participantsbygroup = unitOfWork.Participants.SearchParticipants(item.Filter);
                            participantsbygroup = finalgroupparticipants.Intersect(participantsbygroup).ToList();
                            finalgroupparticipants = finalgroupparticipants.Except(participantsbygroup);
                            var participantcallermapping = new ParticipantCallerMapping();
                            foreach (var participant in participantsbygroup)
                            {
                                participantcallermapping.FollowUpGroupID = item.FollowUpGroupID;
                                participantcallermapping.Participant = participant;
                                unitOfWork.ParticipantCallerMappings.Add(participantcallermapping);
                            }
                        }
                        //var endfugroup = new FollowUpGroup();
                        //foreach (var participant in finalgroupparticipants.ToList())
                        //{
                        //    var endparticipantcallermapping = new ParticipantCallerMapping();
                        //    endparticipantcallermapping.Participant = participant;

                        //}
                    }
                           

                    using (TransactionScope scope = new TransactionScope())
                    {
                        unitOfWork.Complete();
                        scope.Complete();
                    }

                    return true;
                }
            }
        }

        public List<FollowUpMgmtViewModel> PrintHealthReportByFollowUpGroup(int followgroupid, out string message)
        {
            message = string.Empty;
            using (var unitOfWork = CreateUnitOfWork())
            {
                var followupgroup = unitOfWork.FollowUpGroups.GetFollowUpGroup(followgroupid);
                if (followupgroup == null)
                {
                    message = "Follow-up group does not exist!";
                }
                var followupconfig = unitOfWork.FollowUpConfigurations.Find(u => u.FollowUpConfigurationID == followupgroup.FollowUpConfigurationID).FirstOrDefault();

                if (!followupgroup.FollowUpConfiguration.Deploy)
                {
                    message = "Follow-up configuration is not deployed!";
                }

                var printmodellist = new List<FollowUpMgmtViewModel>();

                if (followupgroup.ParticipantCallerMappings.Count > 0)
                {
                    foreach (var participantcallermapping in followupgroup.ParticipantCallerMappings)
                    {
                        var printmodel = new FollowUpMgmtViewModel();
                        printmodel.Participant = participantcallermapping.Participant;

                        //TODO: get values from followup modality forms
                        printmodel.Weight = "55";
                        printmodel.Height = "159";
                        printmodel.BMIValue = "22";
                        printmodel.BMIStandardReferenceResult ="normal";
                        printmodel.BPValue = "120/80";
                        printmodel.BPStandardReferenceResult = "normal";
                        printmodel.BloodTestResult = "Satisfactory";
                        printmodel.OverAllResult = "satisfactory";

                        printmodellist.Add(printmodel);
                    }
                }

                message = "success";
                return printmodellist;
            }
        }
    }
}
