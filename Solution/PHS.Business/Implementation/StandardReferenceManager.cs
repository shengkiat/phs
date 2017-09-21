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
    public class StandardReferenceManager : BaseManager, IStandardReferenceManager, IManagerFactoryBase<IStandardReferenceManager>
    {
        public IStandardReferenceManager Create()
        {
            return new StandardReferenceManager();
        }

        public IList<StandardReference> GetAllStandardReferences(out string message)
        {
            message = string.Empty;
            try
            {
                using (var unitOfWork = CreateUnitOfWork())
                {
                    var standardreferences = unitOfWork.StandardReferences.GetAll().ToList();
                    message = string.Empty;
                    return standardreferences;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue("Standard Reference");
                return null;
            }
        }
        public StandardReference GetStandardReferenceByID(int? id, out string message)
        {
            message = string.Empty;
            try
            {
                using (var unitOfWork = CreateUnitOfWork())
                {
                    var standardReference = unitOfWork.StandardReferences.GetStandardReference(id);

                    if (standardReference == null)
                    {
                        message = "Invalid Standard Reference ID";
                        return null;
                    }

                    message = string.Empty;
                    return standardReference;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue("Standard Reference by ID");
                return null;
            }
        }

        public StandardReference AddStandardReference(StandardReference standardReference, out string message)
        {
            message = string.Empty;
            if (standardReference == null)
            {
                message = Constants.PleaseFillInAllRequiredFields();
                return null;
            }
            if (string.IsNullOrEmpty(standardReference.Title) || string.IsNullOrEmpty(standardReference.Title.Trim()))
            {
                message = Constants.PleaseFillInAllRequiredFields();
                return null;
            }
            if (StandardReferenceExists(standardReference.Title, standardReference.StandardReferenceID, out message))
            {
                message = "Standard Reference already exists";
                return null;
            }

            try
            {
                using (var unitOfWork = CreateUnitOfWork())
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        unitOfWork.StandardReferences.Add(standardReference);
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                    message = string.Empty;
                    return standardReference;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringAddingValue("Standard Reference");
                return null;
            }
        }

        public bool UpdateStandardReference(StandardReference standardReference, out string message)
        {
            message = string.Empty;
            if (standardReference == null)
            {
                message = Constants.PleaseFillInAllRequiredFields();
                return false;
            }
            if (string.IsNullOrEmpty(standardReference.Title) || string.IsNullOrEmpty(standardReference.Title.Trim()))
            {
                message = Constants.PleaseFillInAllRequiredFields();
                return false;
            }
            if (StandardReferenceExists(standardReference.Title, standardReference.StandardReferenceID, out message))
            {
                message = "Standard Reference Title already exists";
                return false;
            }

            try
            {
                using (var unitOfWork = CreateUnitOfWork())
                {
                    var standardReferenceToUpdate = unitOfWork.StandardReferences.GetStandardReference(standardReference.StandardReferenceID);
                    Util.CopyNonNullProperty(standardReference, standardReferenceToUpdate);
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
                message = Constants.OperationFailedDuringUpdatingValue("Standard Reference");
                return false;
            }
        }

        public bool DeleteStandardReference(int id, out string message)
        {
            message = string.Empty;
            try
            {
                using (var unitOfWork = CreateUnitOfWork())
                {
                    var standardReference = unitOfWork.StandardReferences.GetStandardReference(id);

                    if (standardReference == null)
                    {
                        message = "Standard Reference not found";
                        return false;
                    }
                    if (standardReference.TemplateFields.Any())
                    {
                        message = "Standard Reference linked to template.";
                        return false;
                    }
                    using (TransactionScope scope = new TransactionScope())
                    {
                        List<ReferenceRange> referenceRangesToDelete = new List<ReferenceRange>();

                        foreach (var referenceRange in standardReference.ReferenceRanges)
                        {
                            ReferenceRange referenceRangeToDelete = unitOfWork.ReferenceRanges.Get(referenceRange.ReferenceRangeID);
                            referenceRangesToDelete.Add(referenceRangeToDelete);
                        }

                        //remove Reference Range from Standard reference
                        foreach (ReferenceRange rr in referenceRangesToDelete)
                        {
                            standardReference.ReferenceRanges.Remove(rr);
                        }

                        //remove Reference Range
                        unitOfWork.ReferenceRanges.RemoveRange(referenceRangesToDelete);

                        //remove Standard Reference
                        unitOfWork.StandardReferences.Remove(standardReference);

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
                message = "Operation failed during deleting Standard Reference";
                return false;
            }
        }

        public bool StandardReferenceExists(string title, int standardReferenceID, out string message)
        {
            if (string.IsNullOrEmpty(title))
            {
                message = Constants.ValueIsEmpty("Standard Reference ID");
                return true;
            }
            try
            {
                using (var unitOfWork = CreateUnitOfWork())
                {
                    var standardReference = unitOfWork.StandardReferences.Find(u => u.Title.Equals(title, StringComparison.CurrentCultureIgnoreCase) && u.StandardReferenceID != standardReferenceID).FirstOrDefault();
                    if (standardReference != null)
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
                message = Constants.OperationFailedDuringRetrievingValue("Standard Reference Title");
                return true;
            }
        }

        public ReferenceRange GetReferenceRange(int standardReferenceID, string value, out string message)
        {
            message = string.Empty;
            try
            {
                using (var unitOfWork = CreateUnitOfWork())
                {
                    var standardReference = unitOfWork.StandardReferences.GetStandardReference(standardReferenceID);

                    if (standardReference == null)
                    {
                        message = "Invalid Standard Reference ID";
                        return null;
                    }
                    ReferenceRange referenceRange = null;

                    if (standardReference.DataType == "Number") {
                        double inputValue;
                        if(double.TryParse(value, out inputValue)) {
                            referenceRange = standardReference.ReferenceRanges.Where(r => r.MinimumValue <= inputValue && r.MaximumValue >= inputValue).FirstOrDefault();
                        }
                        else {
                            message = "Incompatible Standard reference Data type";
                            return null;
                        }
                    }
                    else if(standardReference.DataType == "String")
                    {
                        referenceRange = standardReference.ReferenceRanges.Where(r => r.StringValue == value).FirstOrDefault();
                    }
                    return referenceRange;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue("GetReferenceRange");
                return null;
            }
        }
    }
}
