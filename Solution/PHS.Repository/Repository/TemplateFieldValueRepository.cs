using PHS.DB;
using PHS.Repository.Interface;
using PHS.Repository.Repository.Core;
using System.Data.Entity;


namespace PHS.Repository.Repository
{
    public class TemplateFieldValueRepository : Repository<TemplateFieldValue>, ITemplateFieldValueRepository
    {
        public TemplateFieldValueRepository(DbContext context) : base(context)
        {
        }
    }
}
