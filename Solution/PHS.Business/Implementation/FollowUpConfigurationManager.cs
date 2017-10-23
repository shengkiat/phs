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

                    if (fuConfiguration.PHSEvent != null)
                    {
                        message = "Can't delete deployed Follow-up configuration!";
                        return false;
                    }

                    using (TransactionScope scope = new TransactionScope())
                    {
                        //List<FollowUpGroup> fuGroupsToDelete = new List<FollowUpGroup>();

                        //foreach (var fuGroup in fuConfiguration.FollowUpGroups)
                        //{
                        //    Modality modalityToDelete = unitOfWork.Modalities.Get(modality.ModalityID);
                        //    modalitiesToDelete.Add(modalityToDelete);
                        //}

                        ////remove EventModality
                        //foreach (Modality m in modalitiesToDelete)
                        //{
                        //    phsEvent.Modalities.Remove(m);
                        //}

                        ////remove Modality
                        //unitOfWork.Modalities.RemoveRange(modalitiesToDelete);

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
