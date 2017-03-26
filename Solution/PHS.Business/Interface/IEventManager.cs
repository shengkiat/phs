using PHS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.Business.Interface
{
    public interface IEventManager
    {
        IEnumerable<@event> GetAllEvents();
        @event GetEventByID(int ID);
        bool NewEvent(@event eventModel);
        bool UpdateEvent(@event eventModel);
    }
}
