using PHS.DB;
using PHS.Repository.Interface;
using PHS.Repository.Repository.Core;
using System.Data.Entity;


namespace PHS.Repository.Repository
{
    public class FormFieldValueRepository : Repository<TemplateFieldValue>, IFormFieldValueRepository
    {
        public FormFieldValueRepository(DbContext context) : base(context)
        {
        }
    }
}
