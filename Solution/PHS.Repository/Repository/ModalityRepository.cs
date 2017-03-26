using PHS.DB;
using PHS.Repository.Interface;
using PHS.Repository.Repository.Core;
using System.Data.Entity;

namespace PHS.Repository.Repository
{
    public class ModalityRepository : Repository<Modality>, IModalityRepository
    {
        public ModalityRepository(DbContext context) : base(context)
        {
        }
    }
}
