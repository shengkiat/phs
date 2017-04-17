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

        public PHSEvent GetEventByID(int ID)
        {
           
            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                return unitOfWork.Events.Get(null,null,includeProperties: "Modalities").FirstOrDefault();
            }
        }

        public bool NewEvent(PHSEvent eventModel)
        {
            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                eventModel.CreateDate = DateTime.Now;
                eventModel.IsActive = true;
                unitOfWork.Events.Add(eventModel);
                unitOfWork.Complete();
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
                var eventToUpdate =  unitOfWork.Events.Get(eventModel.ID);

               

                foreach (var newModality in eventModel.Modalities)
                {

                    Modality modality = new Modality();
                    modality.Name = newModality.Name;

                    unitOfWork.Modalities.Add(modality);

                    eventToUpdate.Modalities.Add(modality);

                }



                eventToUpdate.Title = eventModel.Title;
                eventToUpdate.Venue = eventModel.Venue;
                eventToUpdate.StartDT = eventModel.StartDT;
                eventToUpdate.EndDT = eventModel.EndDT;

               // eventToUpdate.UpdateDT = DateTime.Now;

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
