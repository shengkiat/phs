using PHS.Business.Extensions;
using PHS.Business.Implementation.FillIn;
using PHS.Business.Interface;
using PHS.Business.ViewModel.ParticipantJourney;
using PHS.Common;
using PHS.DB;
using PHS.DB.ViewModels.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Mvc;
using static PHS.Common.Constants;

namespace PHS.Business.Implementation
{
    public class FormAccessManager : BaseFormManager, IFormAccessManager, IManagerFactoryBase<IFormAccessManager>
    {
        public IFormAccessManager Create()
        {
            return new FormAccessManager();
        }

        public Template FindPublicTemplate(string slug)
        {
            using (var unitOfWork = CreateUnitOfWork())
            {
                Form form = unitOfWork.FormRepository.GetPublicForm(slug);
                if (form != null)
                {
                    return form.Templates.Where(t => t.IsActive == true).OrderByDescending(f => f.Version).First();
                }
                return null;
            }
        }

        public Template FindPreRegistrationForm()
        {
            using (var unitOfWork = CreateUnitOfWork())
            {
                Form form = unitOfWork.FormRepository.GetPreRegistrationForm();
                if (form != null)
                {
                    return form.Templates.Where(t => t.IsActive == true).OrderByDescending(f => f.Version).First();
                }
                return null;
            }
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

        public string FillIn(IDictionary<string, string> SubmitFields, TemplateViewModel model, FormCollection formCollection)
        {
            
            var template = FindTemplate(model.TemplateID.Value);

            using (var unitOfWork = CreateUnitOfWork())
            {
                using (var fillIn = new PublicFormFillIn(unitOfWork))
                {
                    return fillIn.FillIn(SubmitFields, template, formCollection);
                }
            }
        }

        
    }
}
