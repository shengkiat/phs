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

namespace PHS.Business.Implementation
{
    public class EventManager : BaseManager, IEventManager, IManagerFactoryBase<IEventManager>
    {
        public IEventManager Create()
        {
            return new EventManager();
        }

        public IEnumerable<@event> GetAllEvents()
        {
            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                return unitOfWork.Events.GetAll();
            }
        }

        public @event GetEventByID(int ID)
        {
            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                return unitOfWork.Events.Get(ID);
            }
        }


        public bool NewEvent(@event eventModel)
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















    }
}
