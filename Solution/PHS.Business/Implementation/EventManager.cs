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
    public class EventManager : BaseManager, IEventManager, IManagerFactoryBase<IEventManager>
    {
        public EventManager() : base(null)
        {
        }

        public EventManager(PHSUser loginUser) : base(loginUser)
        {
        }

        public IEventManager Create(PHSUser loginUser)
        {
            return new EventManager(loginUser);
        }

        public IEnumerable<PHSEvent> GetAllEvents()
        {
            using (var unitOfWork = CreateUnitOfWork())
            {
                return unitOfWork.Events.GetAll();
            }
        }

        public PHSEvent GetEventByID(int ID, out string message)
        {
            message = string.Empty;

            try
            {
                using (var unitOfWork = CreateUnitOfWork())
                {
                    var phsEvent = unitOfWork.Events.GetEvent(ID);

                    if (phsEvent == null)
                    {
                        message = "Event Not Found";
                        return null;
                    }

                    message = string.Empty;
                    return phsEvent;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue("Event by ID");
                return null;
            }
        }

        public bool NewEvent(PHSEvent eventModel, out string message)
        {
            message = string.Empty;

            validateEvent(eventModel, out message);

            if(message != string.Empty)
            {
                return false;
            }

            using (var unitOfWork = CreateUnitOfWork())
            {

                foreach (var newModality in eventModel.Modalities)
                {
                    newModality.IsActive = true;
                }

                eventModel.CreatedDateTime = DateTime.Now;
                eventModel.IsActive = true;
                unitOfWork.Events.Add(eventModel);

                using (TransactionScope scope = new TransactionScope())
                {
                    unitOfWork.Complete();
                    scope.Complete();
                }

                return true;
            }
        }

        public bool UpdateEvent(PHSEvent eventModel)
        {
            if (eventModel == null)
            {
                return false;
            }

            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                var eventToUpdate =  unitOfWork.Events.GetEvent(eventModel.PHSEventID);

                //foreach (var newModality in eventModel.Modalities)
                //{

                //    Modality modality = new Modality();
                //    modality.Name = newModality.Name;

                //    unitOfWork.Modalities.Add(modality);

                //    eventToUpdate.Modalities.Add(modality);

                //}

                eventToUpdate.Title = eventModel.Title;
                eventToUpdate.Venue = eventModel.Venue;
                eventToUpdate.StartDT = eventModel.StartDT;
                eventToUpdate.EndDT = eventModel.EndDT;
                eventToUpdate.UpdatedDateTime = DateTime.Now;

                using (TransactionScope scope = new TransactionScope())
                {
                    unitOfWork.Complete();
                    scope.Complete();
                }

                return true;
            }
        }

        public bool DeActiveEvent(PHSEvent eventModel)
        {
            if (eventModel == null)
            {
                return false;
            }

            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                var eventToUpdate = unitOfWork.Events.GetEvent(eventModel.PHSEventID);

                eventToUpdate.IsActive = false;
                eventToUpdate.UpdatedDateTime = DateTime.Now;

                using (TransactionScope scope = new TransactionScope())
                {
                    unitOfWork.Complete();
                    scope.Complete();
                }

                return true;
            }
        }

        public bool DeleteEvent(int eventid, out string message)
        {
            message = string.Empty;
            try
            {
                using (var unitOfWork = new UnitOfWork(new PHSContext()))
                {
                    var phsEvent = unitOfWork.Events.GetEvent(eventid);

                    if (phsEvent == null)
                    {
                        message = "Invalid Event Id";
                        return false;
                    }

                    if (phsEvent.Participants != null && phsEvent.Participants.Count >0)
                    {
                        message = "Can't delete event with partients!";
                        return false;
                    }

                    using (TransactionScope scope = new TransactionScope())
                    {
                        List<Modality> modalitiesToDelete = new List<Modality>();

                        foreach (var modality in phsEvent.Modalities)
                        {
                            Modality modalityToDelete = unitOfWork.Modalities.Get(modality.ModalityID);
                            modalitiesToDelete.Add(modalityToDelete);
                        }

                        //remove EventModality
                        foreach (Modality m in modalitiesToDelete)
                        {
                            phsEvent.Modalities.Remove(m);
                        }

                        //remove Modality
                        unitOfWork.Modalities.RemoveRange(modalitiesToDelete);

                        //remove event
                        unitOfWork.Events.Remove(phsEvent);

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
                message = "Operation failed during deleting Event";
                return false;
            }
        }

        public bool DeleteEventModality(int modalityid, int eventid, out string message)
        {
            message = string.Empty;
            try
            {
                using (var unitOfWork = new UnitOfWork(new PHSContext()))
                {
                    var phsEvent = unitOfWork.Events.GetEvent(eventid);

                    if (phsEvent == null)
                    {
                        message = "Invalid Event Id";
                        return false;
                    }

                    Modality modalityToDelete = unitOfWork.Modalities.Get(modalityid);

                    if (modalityToDelete != null)
                    {

                        using (TransactionScope scope = new TransactionScope())
                        {
                            phsEvent.Modalities.Remove(modalityToDelete);
                            unitOfWork.Modalities.Remove(modalityToDelete);

                            unitOfWork.Complete();
                            scope.Complete();
                        }
                    }
                    message = string.Empty;
                    return true;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = "Operation failed during deleting Modality";
                return false;
            }
        }

        public bool EventTitleExists(string eventTitle, out string message)
        {
            if (string.IsNullOrEmpty(eventTitle))
            {
                message = Constants.ValueIsEmpty("Event Title");
                return true;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new PHSContext()))
                {
                    var phsEvent = unitOfWork.Events.Find(e => e.Title.Equals(eventTitle, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                    if (phsEvent != null)
                    {
                        message = Constants.ValueAlreadyExists(eventTitle);
                        return true;
                    }
                }
                message = string.Empty;
                return false;
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue("Event Title");
                return true;
            }
        }

        private void validateEvent(PHSEvent eventModel, out string message)
        {
            if (eventModel == null)
            {
                message = Constants.PleaseFillInAllRequiredFields();
                return;
            }

            if ((string.IsNullOrEmpty(eventModel.Title) || string.IsNullOrEmpty(eventModel.Title.Trim()))
                ||
                (string.IsNullOrEmpty(eventModel.Venue) || string.IsNullOrEmpty(eventModel.Venue.Trim()))
                )
            {
                message = Constants.PleaseFillInAllRequiredFields();
                return;
            }

            if (EventTitleExists(eventModel.Title, out message))
            {
                return;
            }

            if (eventModel.StartDT == null || eventModel.EndDT == null)
            {
                message = Constants.PleaseFillInAllRequiredFields();
                return;
            }

            if (eventModel.StartDT <= DateTime.Today || eventModel.EndDT <= DateTime.Today)
            {
                message = "Input Date must larger than today";
                return;
            }

        }

    }
}
