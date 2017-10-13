using PHS.DB;
using System.Linq;
using PHS.Repository.Interface;
using PHS.Repository.Repository.Core;
using System.Data.Entity;
using System.Collections.Generic;
using System;

namespace PHS.Repository.Repository
{
    public class EventRepository : Repository<PHSEvent>, IEventRepository
    {
        public EventRepository(DbContext datacontext) : base(datacontext)
        {
        }

        public PHSEvent GetEvent(int id)
        {
            return dbContext.Set<PHSEvent>().Where(u => u.PHSEventID == id && u.IsActive == true).Include(x => x.Modalities).Include(x => x.Participants).FirstOrDefault();
        }

        public PHSEvent GetEventWithModalityForm(int id)
        {
            return dbContext.Set<PHSEvent>().Where(u => u.PHSEventID == id && u.IsActive == true).Include(x => x.Participants).Include(x => x.Modalities.Select(y => y.Forms.Select(z => z.Templates))).FirstOrDefault();
        }

        public IEnumerable<PHSEvent> GetAllActiveEvents()
        {
            DateTime currentTime = DateTime.Now;
            return GetAll().Where(e => e.IsActive == true && currentTime.Ticks > e.StartDT.Ticks && currentTime.Ticks < e.EndDT.Ticks);
        }
    }
}
