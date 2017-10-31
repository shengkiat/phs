using PHS.Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using PHS.DB;
using PHS.Repository;
using PHS.Repository.Context;
using PHS.Business.Common;
using PHS.Common;
using PHS.Repository.Repository;

namespace PHS.Business.Implementation
{
    public class FollowUpConfigurationManager : BaseManager, IFollowUpConfigurationManager, IManagerFactoryBase<IFollowUpConfigurationManager>
    {
        public FollowUpConfigurationManager() : base(null)
        {
        }

        public FollowUpConfigurationManager(PHSUser loginUser) : base(loginUser)
        {
        }

        public IFollowUpConfigurationManager Create(PHSUser loginUser)
        {
            return new FollowUpConfigurationManager(loginUser);
        }

        public IEnumerable<FollowUpConfiguration> GetAllFUConfiguration()
        {
            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                return unitOfWork.FollowUpConfigurations.GetAllFollowUpConfigurations();
            }
        }

        public IEnumerable<FollowUpConfiguration> GetAllFUConfigurationByEventID(int eventid)
        {
            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                return unitOfWork.FollowUpConfigurations.GetAllFollowUpConfigurationsByEventID(eventid);
            }
        }
        public FollowUpConfiguration GetFUConfigurationByID(int id, out string message)
        {
            message = string.Empty;

            try
            {
                using (var unitOfWork = new UnitOfWork(new PHSContext()))
                {
                    var followupConfiguration = unitOfWork.FollowUpConfigurations.GetFollowUpConfiguration(id);

                    if (followupConfiguration == null)
                    {
                        message = "Follow-up Configuration Not Found";
                        return null;
                    }

                    message = string.Empty;
                    return followupConfiguration;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue("Follow-up Configuration by ID");
                return null;
            }
        }
        public bool NewFollowUpConfiguration(FollowUpConfiguration model, out string message)
        {
            message = string.Empty;

            validateFollowUpConfiguration(model, out message);

            if (message != string.Empty)
            {
                return false;
            }

            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                unitOfWork.FollowUpConfigurations.Add(model);

                using (TransactionScope scope = new TransactionScope())
                {
                    unitOfWork.Complete();
                    scope.Complete();
                }

                return true;
            }
        }

        public FollowUpGroup AddFollowUpGroup(FollowUpGroup followupgroup, out string message)
        {
            message = string.Empty;
            if (followupgroup == null)
            {
                message = Constants.PleaseFillInAllRequiredFields();
                return null;
            }
            if (string.IsNullOrEmpty(followupgroup.Title) || string.IsNullOrEmpty(followupgroup.Title.Trim()))
            {
                message = Constants.PleaseFillInAllRequiredFields();
                return null;
            }

            try
            {
                using (var unitOfWork = CreateUnitOfWork())
                {
                    unitOfWork.FollowUpGroups.Add(followupgroup);

                    using (TransactionScope scope = new TransactionScope())
                    {
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                    message = string.Empty;
                    return followupgroup;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringAddingValue("Follow Up Group");
                return null;
            }
        }
        public bool UpdateFollowUpConfiguration(FollowUpConfiguration model)
        {
            if (model == null)
            {
                return false;
            }

            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                var modelToUpdate = unitOfWork.FollowUpConfigurations.GetFollowUpConfiguration(model.FollowUpConfigurationID);
                modelToUpdate.Title = model.Title;

                using (TransactionScope scope = new TransactionScope())
                {
                    unitOfWork.Complete();
                    scope.Complete();
                }

                return true;
            }
        }
        public bool DeleteFollowUpConfiguration(int id, out string message)
        {
            message = string.Empty;
            try
            {
                using (var unitOfWork = new UnitOfWork(new PHSContext()))
                {
                    var fuConfiguration = unitOfWork.FollowUpConfigurations.GetFollowUpConfiguration(id);

                    if (fuConfiguration == null)
                    {
                        message = "Invalid Follow-up coniguration Id";
                        return false;
                    }

                    if (fuConfiguration.Deploy == true)
                    {
                        message = "Can't delete deployed Follow-up configuration!";
                        return false;
                    }

                    using (TransactionScope scope = new TransactionScope())
                    {
                        List<FollowUpGroup> fuGroupsToDelete = new List<FollowUpGroup>();
                        fuGroupsToDelete = unitOfWork.FollowUpConfigurations.Get(id).FollowUpGroups.ToList();

                        foreach (FollowUpGroup fu in fuGroupsToDelete)
                        {
                            fuConfiguration.FollowUpGroups.Remove(fu);
                        }

                        //remove followup groups
                        unitOfWork.FollowUpGroups.RemoveRange(fuGroupsToDelete);

                        //remove event
                        unitOfWork.FollowUpConfigurations.Remove(fuConfiguration);

                        unitOfWork.Complete();
                        scope.Complete();
                    }
                    message = string.Empty;
                    return true;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = "Operation failed during deleting Follow-up configuration";
                return false;
            }
        }

        public IList<Modality> GetTeleHealthModalitiesByID(int configid, out string message)
        {
            message = string.Empty;
            IList<Modality> modalities = new List<Modality>();
            using (var followupconfigmanager = new FollowUpConfigurationManager())
            {
                var phseventid = followupconfigmanager.GetFUConfigurationByID(configid, out message).PHSEventID;

                using (var eventmanager = new EventManager())
                {
                    var phsevent = eventmanager.GetEventByID(phseventid, out message);
                    foreach (var item in phsevent.Modalities)
                    {
                        //if (item.Name == "Post Event" || item.Name == "Telehealth")
                        modalities.Add(item);
                    }
                }
            }
            return modalities;
        }

        private void validateFollowUpConfiguration(FollowUpConfiguration model, out string message)
        {
            if (model == null)
            {
                message = Constants.PleaseFillInAllRequiredFields();
                return;
            }

            if ((string.IsNullOrEmpty(model.Title) || string.IsNullOrEmpty(model.Title.Trim())))
            {
                message = Constants.PleaseFillInAllRequiredFields();
                return;
            }

            if (FollowUpConfigurationTitleExists(model.Title, out message))
            {
                return;
            }

        }

        private bool FollowUpConfigurationTitleExists(string fuConfigurationTitle, out string message)
        {
            if (string.IsNullOrEmpty(fuConfigurationTitle))
            {
                message = Constants.ValueIsEmpty("FollowUpConfiguration Title");
                return true;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new PHSContext()))
                {
                    var fuConfiguration = unitOfWork.FollowUpConfigurations.Find(e => e.Title.Equals(fuConfigurationTitle, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                    if (fuConfiguration != null)
                    {
                        message = Constants.ValueAlreadyExists(fuConfigurationTitle);
                        return true;
                    }
                }
                message = string.Empty;
                return false;
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue("FollowUpConfiguration Title");
                return true;
            }
        }
    }
}
