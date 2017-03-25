using PHS.DB;
using PHS.Repository.Interface;
using PHS.Repository.Repository.Core;
using System.Data.Entity;

namespace PHS.Repository.Repository
{
    public class EventRepository : Repository<@event>, IEventRepository
    {
        public EventRepository(DbContext context) : base(context)
        {
        }
    }
}
