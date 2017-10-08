using PHS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.Business.Implementation
{
    public class BaseFormManager : BaseManager
    {
        public BaseFormManager(PHSUser loginUser) : base(loginUser)
        {
        }

        public Template FindTemplate(int templateID)
        {
            Template template = null;
            using (var unitOfWork = CreateUnitOfWork())
            {
                template = unitOfWork.FormRepository.GetTemplate(templateID);
            }

            return template;
        }

        public Template FindLatestTemplate(int formId)
        {
            using (var unitOfWork = CreateUnitOfWork())
            {
                Form form = unitOfWork.FormRepository.GetForm(formId);
                if (form != null)
                {
                    return form.Templates.Where(t => t.IsActive == true).OrderByDescending(f => f.Version).First();
                }
                return null;
            }
        }
    }
}
