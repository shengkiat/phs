using PHS.DB;
using PHS.Repository.Context;
using PHS.Repository.Interface;
using PHS.Repository.Repository.Core;

namespace PHS.Repository.Repository
{
    public class FollowUpGroupRepository : Repository<FollowUpGroup>, IFollowUpGroupRepository
    {
        public FollowUpGroupRepository(PHSContext context) : base(context)
        {
        }
    }
}


