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
using System.IO;

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
            message = string.Empty;

            try
            {
                using (var unitOfWork = new UnitOfWork(new PHSContext()))
                {
                    var modality = unitOfWork.Modalities.Get(ID);

                    if (modality == null)
                    {
                        message = "Modality Not Found";
                        return null;
                    }

                    message = string.Empty;
                    return modality;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue("Modality by ID");
                return null;
            }
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

        public bool UpdateModality(ModalityEventViewModel modalityEventView, out string message)
        {
            message = string.Empty;

            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                var modalityToUpdate = unitOfWork.Modalities.Get(modalityEventView.ModalityID);
                modalityToUpdate.Name = modalityEventView.Name;
                if (modalityEventView.IconPath != null && modalityEventView.IconPath.Length > 0) {
                    modalityToUpdate.IconPath = modalityEventView.IconPath;
                }

                using (TransactionScope scope = new TransactionScope())
                {
                    unitOfWork.Complete();
                    scope.Complete();
                }

                return true;
            }

        }

        //public bool DeleteModality(int modalityid, int eventid, out string message)
        //{
        //    message = string.Empty;
        //    try
        //    {
        //        using (var unitOfWork = new UnitOfWork(new PHSContext()))
        //        {
        //            Modality modality = GetModalityByID(modalityid, out message);

        //            if (modality == null)
        //            {
        //                message = "Invalid Modality Id";
        //                return false;
        //            }

        //            using (TransactionScope scope = new TransactionScope())
        //            {
        //                unitOfWork.Modalities.Remove(modality);

        //                unitOfWork.Complete();
        //                scope.Complete();
        //            }
        //            message = string.Empty;
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionLog(ex);
        //        message = "Operation failed during deleting Modality";
        //        return false;
        //    }
        //}
    }
}
