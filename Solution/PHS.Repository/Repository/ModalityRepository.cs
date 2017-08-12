using PHS.DB;
using PHS.Repository.Interface;
using PHS.Repository.Repository.Core;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PHS.Repository.Repository
{
    public class ModalityRepository : Repository<Modality>, IModalityRepository
    {
        public ModalityRepository(DbContext context) : base(context)
        {
        }

        public Modality GetModalityByID(int id)
        {
            return dbContext.Set<Modality>().Where(u => u.ModalityID == id).Include(x => x.Forms).FirstOrDefault();
        }

        public Modality GetActiveModalityByID(int id)
        {
            return dbContext.Set<Modality>().Where(u => u.ModalityID == id && u.IsActive == true).Include(x => x.Forms).FirstOrDefault();
        }

        public IEnumerable<Modality> GetModalityByFormID(int formId)
        {
            return dbContext.Set<Modality>().Where(u => u.Forms.Any(f => f.FormID == formId)).Include(x => x.Forms);
        }

    }
}
