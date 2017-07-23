using PHS.DB;
using System.Linq;
using PHS.Repository.Interface;
using PHS.Repository.Repository.Core;
using System.Data.Entity;

namespace PHS.Repository.Repository
{
    public class StandardReferenceRepository : Repository<StandardReference>, IStandardReferenceRepository
    {
        public StandardReferenceRepository(DbContext context) : base(context)
        {
        }

        public StandardReference GetStandardReference(int? id)
        {
            return dbContext.Set<StandardReference>().Where(u => u.StandardReferenceID == id).Include(x => x.ReferenceRanges).FirstOrDefault();
        }
    }
}
