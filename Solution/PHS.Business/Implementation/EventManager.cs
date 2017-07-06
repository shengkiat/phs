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
        public IEventManager Create()
        {
            return new EventManager();
        }

        public IEnumerable<PHSEvent> GetAllEvents()
        {
            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                return unitOfWork.Events.GetAll();
            }
        }

        public PHSEvent GetEventByID(int ID, out string message)
        {
            message = string.Empty;

            try
            {
                using (var unitOfWork = new UnitOfWork(new PHSContext()))
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

        public bool NewEvent(PHSEvent eventModel)
        {
            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                eventModel.CreatedDateTime = DateTime.Now;
                eventModel.IsActive = true;
                unitOfWork.Events.Add(eventModel);

                //foreach (var newModality in eventModel.Modalities)
                //{

                //    Modality modalityDB = new Modality();
                //    Util.CopyNonNullProperty(newModality, modalityDB);
                //    modalityDB.IsActive = true;
                //    modalityDB.PHSEvents.Add(eventModel);

                //    unitOfWork.Modalities.Add(modalityDB);

                //}

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













    }
}
