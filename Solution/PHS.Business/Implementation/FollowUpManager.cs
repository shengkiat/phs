using OfficeOpenXml;
using PHS.Business.Common;
using PHS.Business.Interface;
using PHS.Business.ViewModel.FollowUp;
using PHS.Common;
using PHS.DB;
using System;
using System.Collections.Generic;
using System.IO;
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
                                if (map.PhaseIFollowUpVolunteer == loginuser.FullName)
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

        public IList<FollowUpGroup> GetParticipantsByFollowUpConfiguration(int followupconfigurationid, out string message)
        {
            message = string.Empty;
            var result = new List<FollowUpGroup>();
            var followupconfig = GetFollowUpConfiguration(followupconfigurationid);
            if (followupconfig == null)
            {
                message = "Follow-up configuration does not exist!";
                return result;
            }

            if (followupconfig.Deploy)
            {
                result = followupconfig.FollowUpGroups.ToList();
                return result;
            }
          
            var followupgroups = followupconfig.FollowUpGroups;
            if(followupgroups.Count <= 0)
            {
                message = "No follow-up groups found.";
                return result;
            }

            var eventid = followupconfig.PHSEventID;
            var finalgroupparticipants = GetAllParticipants(eventid);
            foreach (var item in followupgroups)
            {
                using (var unitOfWork = CreateUnitOfWork())
                {
                    var participantCallerMappingList = new List<ParticipantCallerMapping>();
                    //testing
                    //var participantsbygroup = unitOfWork.Participants.FindParticipants(p => p.Language == "Mandarin" && p.PHSEvents.Any(e => e.PHSEventID == eventid));

                    /*BMI : "3#19#35#36#470#==#25.0"*/ /*"BP : 3#19#35#36#466#==#100"*/
                    var participantsbygroup = unitOfWork.Participants.SearchParticipants(item.Filter).ToList();
                    //var participantsbygroup = SearchParticipants(item.Filter);

                    //avoid overlapping participants
                    var intersectresult = finalgroupparticipants.Where(a => participantsbygroup.Select(b => b.ParticipantID).Contains(a.ParticipantID)).ToList();
                    participantsbygroup = intersectresult;
                    var exceptresult = finalgroupparticipants.Where(a => !participantsbygroup.Select(b => b.ParticipantID).Contains(a.ParticipantID)).ToList();
                    finalgroupparticipants = exceptresult;

                    var participantcallermapping = new ParticipantCallerMapping();
                    foreach (var participant in participantsbygroup)
                    {
                        participantcallermapping.Participant = participant;
                        participantCallerMappingList.Add(participantcallermapping);

                    }
                    item.ParticipantCallerMappings = participantCallerMappingList;
                    result.Add(item);
                }
            }
            var endfugroup = new FollowUpGroup();
            var endparticipantCallerMappingList = new List<ParticipantCallerMapping>();
            foreach (var participant in finalgroupparticipants.ToList())
            {
                var endparticipantcallermapping = new ParticipantCallerMapping();
                endparticipantcallermapping.Participant = participant;
                endparticipantCallerMappingList.Add(endparticipantcallermapping);

            }
            endfugroup.Title = "No Group Participants";
            endfugroup.ParticipantCallerMappings = endparticipantCallerMappingList;
            result.Add(endfugroup);
                
            return result;
        }

        private IList<Participant> SearchParticipants(string searchstring)
        {
            //searchstring = "3#1#1#1#80#==#Male#AND#3#19#35#36#470#>#22#AND#3#19#35#36#470#>#22";
            searchstring = "3#19#35#36#470#==#25#AND";
            IList<Participant> resultParticipants = new List<Participant>();
            var splitstring = searchstring.Split(new[] { "AND" }, StringSplitOptions.None);

            foreach (var s in splitstring)
            {
                var validsearchstring = s;
                if (s != "" && searchstring.Split('#').Length > 7)
                {
                    using (var unitOfWork = CreateUnitOfWork())
                    {
                        var result = unitOfWork.Participants.SearchParticipants(s);
                        if (resultParticipants.Count == 0)
                            resultParticipants = result.ToList();
                        else
                            resultParticipants = resultParticipants.Intersect(result).ToList();
                    }
                }
            }
            return resultParticipants;

        }
        public bool DeployFollowUpConfiguration(int followupconfigurationid, out string message)
        {
            message = string.Empty;
            var result = new List<FollowUpGroup>();
            var followupconfiguration = GetFollowUpConfiguration(followupconfigurationid);
            if (followupconfiguration == null)
            {
                message = "No follow-up configuration found.";
                return false;
            }

            if (followupconfiguration.Deploy)
            {
                message = "Follow-up configuration is already deployed!";
                return false;
            }

            var followupgroups = followupconfiguration.FollowUpGroups;
            if (followupgroups.Count() <= 0)
            {
                message = "No follow-up groups found.";
                return false;
            }

            using (var unitOfWork = CreateUnitOfWork())
            {

                var modelToUpdate = unitOfWork.FollowUpConfigurations.GetFollowUpConfiguration(followupconfigurationid);
                modelToUpdate.Deploy = true;
                //modelToUpdate.DateTime = DateTime.Now;

                using (TransactionScope scope = new TransactionScope())
                {
                    unitOfWork.Complete();
                    scope.Complete();
                }
            }
            var finalgroupparticipants = GetAllParticipants(followupconfiguration.PHSEventID);
            foreach (var item in followupgroups)
            {
                using (var unitOfWork = CreateUnitOfWork())
                {
                    //var participantsbygroup = unitOfWork.Participants.FindParticipants(p => p.Language == "Mandarin" && p.PHSEvents.Any(e => e.PHSEventID == eventid));
                    //var participantsbygroup = SearchParticipants(item.Filter);
                    var participantsbygroup = unitOfWork.Participants.SearchParticipants(item.Filter/*"3#19#35#36#470#==#25.0"*/).ToList();

                    //avoid overlapping participants
                    var intersectresult = finalgroupparticipants.Where(a => participantsbygroup.Select(b => b.ParticipantID).Contains(a.ParticipantID)).ToList();
                    participantsbygroup = intersectresult;
                    var exceptresult = finalgroupparticipants.Where(a => !participantsbygroup.Select(b => b.ParticipantID).Contains(a.ParticipantID)).ToList();
                    finalgroupparticipants = exceptresult;

                    var participantcallermapping = new ParticipantCallerMapping();
                    foreach (var participant in participantsbygroup)
                    {
                        participantcallermapping.FollowUpGroupID = item.FollowUpGroupID;
                        participantcallermapping.Participant = participant;
                        unitOfWork.ParticipantCallerMappings.Add(participantcallermapping);
                    }
                    using (TransactionScope scope = new TransactionScope())
                    {
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                }
            }
            //var endfugroup = new FollowUpGroup();
            //foreach (var participant in finalgroupparticipants.ToList())
            //{
            //    var endparticipantcallermapping = new ParticipantCallerMapping();
            //    endparticipantcallermapping.Participant = participant;

            //}
            return true;
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
                        printmodel.BMIStandardReferenceResult = "normal";
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

        public List<ParticipantCallerMapping> ImportCaller(byte[] data, int followgroupid, out string message)
        {
            message = string.Empty;
            var followupgroup = GetFollowUpGroupByID(followgroupid);
            if (followupgroup == null)
            {
                message = "Follow-up group does not exist!";
            }

            if (!followupgroup.FollowUpConfiguration.Deploy)
            {
                message = "Follow-up configuration is not deployed!";
            }


            List<string> phaseOnevolunteers = new List<string>();
            List<string> phaseOneCommMembers = new List<string>();

            using (MemoryStream ms = new MemoryStream(data))
            using (ExcelPackage package = new ExcelPackage(ms))
            {
                if (package.Workbook.Worksheets.Count == 0)
                {
                    message = "Invalid File.";
                }

                else
                {
                    foreach (ExcelWorksheet worksheet in package.Workbook.Worksheets)
                    {
                        if (!worksheet.Cells[1, 1].Value.Equals("Phase-1 Follow-up Volunteer")
                            || !worksheet.Cells[1, 2].Value.Equals("Phase-1 Committee Member")
                            || !worksheet.Cells[1, 3].Value.Equals("Phase-2 Follow-up Volunteer")
                            || !worksheet.Cells[1, 4].Value.Equals("Phase-2 Committee Member"))
                        {
                            message = "Invalid File.";
                        }

                        else
                        {
                            phaseOnevolunteers = GetColumnList(worksheet, 1);
                            phaseOneCommMembers = GetColumnList(worksheet, 2);
                        }
                    }
                }
            }

            var volunteerscount = phaseOnevolunteers.Count;
            var commmembercount = phaseOneCommMembers.Count;

            if (volunteerscount == 0 || commmembercount == 0)
            {
                message = "No Volunteers/Comm Members found.";
            }

            foreach (var volunteer in phaseOnevolunteers)
            {
                if (!ValidCaller(volunteer))
                    message = "Volunteer " + volunteer + "not a valid user";
            }

            foreach (var commmember in phaseOneCommMembers)
            {
                if (!ValidCaller(commmember))
                    message = "Commitee Member " + commmember + "not a valid user";
            }

            var numberofparticipant = followupgroup.ParticipantCallerMappings.Count;

            if (numberofparticipant <= volunteerscount)
            {
                for (var count = 0; count < numberofparticipant; count++)
                {
                    var participantcallermapping = followupgroup.ParticipantCallerMappings.ElementAt(count);
                    try
                    {
                        using (var unitOfWork = CreateUnitOfWork())
                        {
                            var toupdate = unitOfWork.ParticipantCallerMappings.Get(participantcallermapping.ParticipantCallerMappingID);
                            Util.CopyNonNullProperty(participantcallermapping, toupdate);
                            toupdate.PhaseIFollowUpVolunteer = phaseOnevolunteers[count];
                            using (TransactionScope scope = new TransactionScope())
                            {
                                unitOfWork.Complete();
                                scope.Complete();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionLog(ex);
                        message = Constants.OperationFailedDuringUpdatingValue("ParticipantCallerMappings");
                    }
                }
            }
            else
            {
                var ratio = numberofparticipant / volunteerscount;
                int iCaller = 0;
                for (var count = 1; count <= numberofparticipant; count++)
                {
                    var participantcallermapping = followupgroup.ParticipantCallerMappings.ElementAt(count-1);
                    try
                    {
                        using (var unitOfWork = CreateUnitOfWork())
                        {
                            var toupdate = unitOfWork.ParticipantCallerMappings.Get(participantcallermapping.ParticipantCallerMappingID);
                            Util.CopyNonNullProperty(participantcallermapping, toupdate);
                            toupdate.PhaseIFollowUpVolunteer = phaseOnevolunteers[iCaller];
                            using (TransactionScope scope = new TransactionScope())
                            {
                                unitOfWork.Complete();
                                scope.Complete();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionLog(ex);
                        message = Constants.OperationFailedDuringUpdatingValue("ParticipantCallerMappings");
                    }
                    if (count % ratio == 0 && iCaller+1 < volunteerscount)
                    {
                        ++iCaller;
                    }
                }
            }

            if (numberofparticipant <= commmembercount)
            {
                for (var count = 0; count < numberofparticipant; count++)
                {
                    var participantcallermapping = followupgroup.ParticipantCallerMappings.ElementAt(count);
                    try
                    {
                        using (var unitOfWork = CreateUnitOfWork())
                        {
                            var toupdate = unitOfWork.ParticipantCallerMappings.Get(participantcallermapping.ParticipantCallerMappingID);
                            Util.CopyNonNullProperty(participantcallermapping, toupdate);
                            toupdate.PhaseICommitteeMember = phaseOneCommMembers[count];
                            using (TransactionScope scope = new TransactionScope())
                            {
                                unitOfWork.Complete();
                                scope.Complete();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionLog(ex);
                        message = Constants.OperationFailedDuringUpdatingValue("ParticipantCallerMappings");
                    }
                }
            }
            else
            {
                var ratio = numberofparticipant / commmembercount;
                int icommmember = 0;
                for (var count = 1; count <= numberofparticipant; count++)
                {
                    var participantcallermapping = followupgroup.ParticipantCallerMappings.ElementAt(count - 1);
                    try
                    {
                        using (var unitOfWork = CreateUnitOfWork())
                        {
                            var toupdate = unitOfWork.ParticipantCallerMappings.Get(participantcallermapping.ParticipantCallerMappingID);
                            Util.CopyNonNullProperty(participantcallermapping, toupdate);
                            toupdate.PhaseICommitteeMember = phaseOnevolunteers[icommmember];
                            using (TransactionScope scope = new TransactionScope())
                            {
                                unitOfWork.Complete();
                                scope.Complete();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionLog(ex);
                        message = Constants.OperationFailedDuringUpdatingValue("ParticipantCallerMappings");
                    }
                    if (count % ratio == 0 && icommmember + 1 < commmembercount)
                    {
                        ++icommmember;
                    }
                }
            }
            message = "success";
            return followupgroup.ParticipantCallerMappings.ToList();
        }

        private List<string> GetColumnList(ExcelWorksheet worksheet, int x)
        {
            List<string> values = new List<string>();
            for (int row = worksheet.Dimension.Start.Row + 1; row <= worksheet.Dimension.End.Row; row++)
            {
                if (worksheet.Cells[row, x].Value == null)
                {
                    continue;
                }

                values.Add(worksheet.Cells[row, x].Value.ToString());
            }

            return values;
        }


        private FollowUpGroup GetFollowUpGroupByID(int followgroupid)
        {
            using (var unitOfWork = CreateUnitOfWork())
            {
                return unitOfWork.FollowUpGroups.GetFollowUpGroup(followgroupid);
            }
        }

        private bool ValidCaller(string caller)
        {
            using (var unitOfWork = CreateUnitOfWork())
            {
                var user = unitOfWork.Users.Find(u => u.FullName == caller);
                if (user != null)
                    return true;
            }

            return false;
        }
       
        private FollowUpConfiguration GetFollowUpConfiguration(int followupconfigurationid)
        {
            using (var unitOfWork = CreateUnitOfWork())
            {
                return unitOfWork.FollowUpConfigurations.GetFollowUpConfiguration(followupconfigurationid);

            }
        }
        private IList<Participant> GetAllParticipants(int eventid)
        {
            using (var unitOfWork = CreateUnitOfWork())
            {
                return unitOfWork.Participants.FindParticipants(p => p.PHSEvents.Any(e => e.PHSEventID == eventid)).ToList();
            }
        }
    }
}
