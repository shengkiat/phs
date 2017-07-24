using PHS.DB;
using PHS.Repository.Context;
using PHS.Repository.Interface;
using PHS.Repository.Repository.Core;

namespace PHS.Repository.Repository
{
    public class ReferenceRangeRepository : Repository<ReferenceRange>, IReferenceRangeRepository
    {
        public ReferenceRangeRepository(PHSContext context) : base(context)
        {
        }
    }
}

