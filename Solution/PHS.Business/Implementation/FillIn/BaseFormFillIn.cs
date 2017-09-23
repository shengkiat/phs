using Microsoft.Win32.SafeHandles;
using PHS.Business.Extensions;
using PHS.Common;
using PHS.DB;
using PHS.DB.ViewModels.Form;
using PHS.Repository.Interface.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Mvc;

namespace PHS.Business.Implementation.FillIn
{
    abstract class BaseFormFillIn : IDisposable
    {
        // Flag: Has Dispose already been called?
        bool disposed = false;
        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        public BaseFormFillIn(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public string FillIn(IDictionary<string, string> SubmitFields, Template Template, FormCollection formCollection)
        {
            string result = null;
            IList<string> errors = Enumerable.Empty<string>().ToList();

            var templateView = TemplateViewModel.CreateFromObject(Template, Constants.TemplateFieldMode.INPUT);
            templateView.AssignInputValues(formCollection);

            if (templateView.Fields.Any())
            {
                IDictionary<int, string> submissionFields = new System.Collections.Generic.Dictionary<int, string>();

                // first validate fields
                foreach (var field in templateView.Fields.OrderBy(f=>f.TemplateFieldID))
                {
                    if (!field.SubmittedValueIsValid(formCollection))
                    {
                        field.SetFieldErrors();
                        errors.Add(field.Errors);
                    }

                    var value = field.SubmittedValue(formCollection);
                    if (field.IsRequired && value.IsNullOrEmpty())
                    {
                        if (field.ConditionTemplateFieldID.HasValue)
                        {
                            if (isConditionalFieldRequired(field, submissionFields))
                            {
                                field.Errors = "{0} is a required field".FormatWith(field.Label);
                                errors.Add(field.Errors);
                            }
                        }

                        else
                        {
                            field.Errors = "{0} is a required field".FormatWith(field.Label);
                            errors.Add(field.Errors);
                        }
                    }

                    else
                    {
                        submissionFields.Add(field.TemplateFieldID.Value, value);
                    }
                };

                if (errors.Count == 0)
                {
                    //then insert values
                    var entryId = Guid.NewGuid();

                    using (TransactionScope scope = new TransactionScope())
                    {
                        foreach (var field in templateView.Fields.OrderBy(f => f.TemplateFieldID))
                        {
                            var value = field.SubmittedValue(formCollection);

                            //if it's a file, save it to hard drive
                            if (field.FieldType == Constants.TemplateFieldType.FILEPICKER && !string.IsNullOrEmpty(value))
                            {
                                //var file = Request.Files[field.SubmittedFieldName()];
                                //var fileValueObject = value.GetFileValueFromJsonObject();

                                //if (fileValueObject != null)
                                //{
                                //    if (UtilityHelper.UseCloudStorage())
                                //    {
                                //        this.SaveImageToCloud(file, fileValueObject.SaveName);
                                //    }
                                //    else
                                //    {
                                //        file.SaveAs(Path.Combine(HostingEnvironment.MapPath(fileValueObject.SavePath), fileValueObject.SaveName));
                                //    }
                                //}
                            }
                            HandleTemplateFieldValue(field, value, entryId);
                        }

                        HandleAdditionalInsert(templateView, Template, formCollection, entryId);

                        UnitOfWork.Complete();
                        scope.Complete();

                        result = "success";
                    }
                    
                }
            }

            if (errors.Count > 0)
            {
                result = errors.ToUnorderedList();
            }


            return result;
        }

        private bool isConditionalFieldRequired(TemplateFieldViewModel field, IDictionary<int, string> submissionFields)
        {
            bool result = false;
            if (submissionFields.ContainsKey(field.ConditionTemplateFieldID.Value))
            {
                string enteredValue = submissionFields[field.ConditionTemplateFieldID.Value];
                if (field.ConditionCriteria.Equals("=="))
                {
                    if (field.ConditionOptions.Contains(enteredValue))
                    {
                        result = true;
                    }

                    else
                    {
                        result = false;
                    }
                }

                else if (field.ConditionCriteria.Equals("!="))
                {
                    if (field.ConditionOptions.Contains(enteredValue))
                    {
                        result = false;
                    }

                    else
                    {
                        result = true;
                    }
                }
            }

            return result;
        }

        protected string getStringValue(IDictionary<string, object> values, string key)
        {
            string result = string.Empty;
            if (values.ContainsKey(key) && values[key] != null)
            {
                result = values[key].ToString();
            }

            return result;
        }

        protected Nullable<System.DateTime> getDateTimeValue(IDictionary<string, object> values, string key)
        {
            Nullable<System.DateTime> result = null;
            if (values.ContainsKey(key) && values[key] != null)
            {
                string dateValue = values[key].ToString();
                if (!string.IsNullOrEmpty(dateValue))
                {
                    result = Convert.ToDateTime(dateValue);
                }
            }

            return result;
        }

        protected virtual void HandleTemplateFieldValue(TemplateFieldViewModel field, string value, Guid entryId)
        {
            UnitOfWork.FormRepository.InsertTemplateFieldValue(field, value, entryId);
        }

        protected abstract void HandleAdditionalInsert(TemplateViewModel templateView, Template template, FormCollection formCollection, Guid entryId);

        protected IUnitOfWork UnitOfWork { get; private set; }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}
