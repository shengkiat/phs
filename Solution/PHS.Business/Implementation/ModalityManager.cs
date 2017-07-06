using PHS.Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PHS.DB;
using PHS.Repository;
using PHS.Repository.Context;
using System.Transactions;
using PHS.Business.Common;
using PHS.Business.ViewModel.Event;
using PHS.Common;

namespace PHS.Business.Implementation
{
    public class ModalityManager : BaseManager, IModalityManager, IManagerFactoryBase<IModalityManager>
    {
        public IModalityManager Create()
        {
            return new ModalityManager();
        }

        public IEnumerable<Modality> GetAllModalities(int EventID)
        {
            throw new NotImplementedException();
        }

        public Modality GetModalityByID(int ID, out string message)
        {
            throw new NotImplementedException();
        }

        public bool NewModality(ModalityEventViewModel modalityEventView, out string message)
        {
            message = string.Empty;
            Modality modality = new Modality();

            Util.CopyNonNullProperty(modalityEventView, modality);
            modality.IsActive = true;
            modality.IsVisible = true;
            modality.IsMandatory = false;

            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                PHSEvent phsEvent = unitOfWork.Events.GetEvent(modalityEventView.EventID);
                int count = phsEvent.Modalities.Count;
                modality.Position= count;

                phsEvent.Modalities.Add(modality);

                using (TransactionScope scope = new TransactionScope())
                {
                    unitOfWork.Complete();
                    scope.Complete();
                }

                return true;
            }

        }

        public bool UpdateModality(Modality modalityModel)
        {
            throw new NotImplementedException();
        }
    }
}
