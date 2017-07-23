using PHS.Repository.Interface.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PHS.DB;
using PHS.DB.ViewModels.Form;
using System.Web.Mvc;

namespace PHS.Business.Implementation.FillIn
{
    class InternalFormFillIn : BaseFormFillIn
    {
        public InternalFormFillIn(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        protected override void HandleAdditionalInsert(TemplateViewModel templateView, Template template, FormCollection formCollection, Guid entryId)
        {
            throw new NotImplementedException();
        }
    }
}
