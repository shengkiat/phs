using PHS.DB;
using PHS.Repository.Interface.Core;
using System.Collections.Generic;

namespace PHS.Repository.Interface
{
    public interface IEventRepository : IRepository<PHSEvent>
    {
        PHSEvent GetEvent(int id);
        IEnumerable<PHSEvent> GetAllActiveEvents();
    }

}
