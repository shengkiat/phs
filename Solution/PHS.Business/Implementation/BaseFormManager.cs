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
        public Template FindTemplate(int templateID)
        {
            Template template = null;
            using (var unitOfWork = CreateUnitOfWork())
            {
                template = unitOfWork.FormRepository.GetTemplate(templateID);
            }

            return template;
        }
    }
}
