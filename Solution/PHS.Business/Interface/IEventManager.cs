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
        IEnumerable<PHSEvent> GetAllEvents();
        PHSEvent GetEventByID(int ID, out string message);
        bool NewEvent(PHSEvent eventModel);
        bool UpdateEvent(PHSEvent eventModel);
    }
}
