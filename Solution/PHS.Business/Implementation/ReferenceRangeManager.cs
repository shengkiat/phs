using PHS.Business.Common;
using PHS.Business.Interface;
using PHS.Common;
using PHS.DB;
using PHS.Repository;
using PHS.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace PHS.Business.Implementation
{
    public class ReferenceRangeManager : BaseManager, IReferenceRangeManager, IManagerFactoryBase<IReferenceRangeManager>
    {
        public IReferenceRangeManager Create()
        {
            return new ReferenceRangeManager();
        }

       
        public ReferenceRange GetReferenceRangeByID(int id, out string message)
        {
            message = string.Empty;
            try
            {
                using (var unitOfWork = CreateUnitOfWork())
                {
                    var referenceRange = unitOfWork.ReferenceRanges.Get(id);

                    if (referenceRange == null)
                    {
                        message = "Invalid Reference Range ID";
                        return null;
                    }

                    message = string.Empty;
                    return referenceRange;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue("Reference Range by ID");
                return null;
            }
        }

        public ReferenceRange AddReferenceRange(ReferenceRange referenceRange, out string message)
        {
            message = string.Empty;
            if (referenceRange == null)
            {
                message = Constants.PleaseFillInAllRequiredFields();
                return null;
            }
            if (string.IsNullOrEmpty(referenceRange.Title) || string.IsNullOrEmpty(referenceRange.Title.Trim()))
            {
                message = Constants.PleaseFillInAllRequiredFields();
                return null;
            }
            if (ReferenceRangeExists(referenceRange.Title, referenceRange.ReferenceRangeID, out message))
            {
                message = "Reference Range already exists";
                return null;
            }

            try
            {
                using (var unitOfWork = CreateUnitOfWork())
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        unitOfWork.ReferenceRanges.Add(referenceRange);
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                    message = string.Empty;
                    return referenceRange;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringAddingValue("Reference Range");
                return null;
            }
        }

        public bool UpdateReferenceRange(ReferenceRange referenceRange, out string message)
        {
            message = string.Empty;
            if (referenceRange == null)
            {
                message = Constants.PleaseFillInAllRequiredFields();
                return false;
            }
            if (string.IsNullOrEmpty(referenceRange.Title) || string.IsNullOrEmpty(referenceRange.Title.Trim()))
            {
                message = Constants.PleaseFillInAllRequiredFields();
                return false;
            }
            if (ReferenceRangeExists(referenceRange.Title, referenceRange.ReferenceRangeID, out message))
            {
                message = "Reference Range Title already exists";
                return false;
            }

            try
            {
                using (var unitOfWork = CreateUnitOfWork())
                {
                    var referenceRangeToUpdate = unitOfWork.ReferenceRanges.Get(referenceRange.ReferenceRangeID);
                    Util.CopyNonNullProperty(referenceRange, referenceRangeToUpdate);
                    using (TransactionScope scope = new TransactionScope())
                    {
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                }
                message = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringUpdatingValue("Reference Range");
                return false;
            }
        }

        public bool DeleteReferenceRange(int id, out string message)
        {
            message = string.Empty;
            try
            {
                using (var unitOfWork = CreateUnitOfWork())
                {
                    var referenceRange = unitOfWork.ReferenceRanges.Get(id);

                    if (referenceRange == null)
                    {
                        message = "Invalid Reference Range ID";
                        return false;
                    }
                    using (TransactionScope scope = new TransactionScope())
                    {
                        //remove Reference Range
                        unitOfWork.ReferenceRanges.Remove(referenceRange);
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                    message = string.Empty;
                    return true;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = "Operation failed during deleting Reference Range";
                return false;
            }
        }

        public bool ReferenceRangeExists(string title, int referenceRangeID, out string message)
        {
            if (string.IsNullOrEmpty(title))
            {
                message = Constants.ValueIsEmpty("Reference Range ID");
                return true;
            }
            try
            {
                using (var unitOfWork = CreateUnitOfWork())
                {
                    var referenceRange = unitOfWork.ReferenceRanges.Find(u => u.Title.Equals(title, StringComparison.CurrentCultureIgnoreCase) && u.ReferenceRangeID != referenceRangeID).FirstOrDefault();
                    if (referenceRange != null)
                    {
                        message = Constants.ValueAlreadyExists(title);
                        return true;
                    }
                }
                message = string.Empty;
                return false;
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue("Reference Range Title");
                return true;
            }
        }
    }
}
