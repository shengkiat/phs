using PHS.DB;
using PHS.Repository.Context;
using PHS.Repository.Interface;
using PHS.Repository.Repository.Core;

namespace PHS.Repository.Repository
{
    public class SummaryRepository : Repository<Summary>, ISummaryRepository
    {
        public SummaryRepository(PHSContext context) : base(context)
        {
        }
    }
}
