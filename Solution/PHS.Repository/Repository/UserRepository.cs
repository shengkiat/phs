using PHS.DB;
using PHS.Repository.Context;
using PHS.Repository.Interface;
using PHS.Repository.Repository.Core;

namespace PHS.Repository.Repository
{
    public class UserRepository : Repository<PHSUser>, IUserRepository
    {
        public UserRepository(PHSContext context) : base(context)
        {
        }
    }
}

