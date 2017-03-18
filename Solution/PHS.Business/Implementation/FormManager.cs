using PHS.Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PHS.DB;
using PHS.Repository;
using PHS.Repository.Context;

namespace PHS.Business.Implementation
{
    public class FormManager : BaseManager, IFormManager, IManagerFactoryBase<IFormManager>
    {
        public IFormManager Create()
        {
            return new FormManager();
        }

        public List<form> FindAllForms()
        {
            List<form> forms = new List<form>();
            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                 forms = unitOfWork.formRepository.GetBaseForms();

                if (forms != null)
                {
                    return forms;
                }
            }

            return forms;

        }

        public form FindForm(int formID)
        {
            form form = new form();
            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                 form = unitOfWork.formRepository.GetForm(formID);

                if (form != null)
                {
                    return form;
                }
            }

            return form;

        }



    }
}
