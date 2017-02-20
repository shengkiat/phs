using PHS.DB;
using PHS.Repository.Interface;
using PHS.Repository.Repository.Core;
using System.Data.Entity;

namespace PHS.Repository.Repository
{
    public class MasterAddressRepository : Repository<MasterAddress>, IMasterAddressRepository
    {
        public MasterAddressRepository(DbContext context) : base(context)
        {
        }
    }
}
