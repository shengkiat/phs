using PHS.Business.Extensions;
using PHS.Business.Interface;
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
    public class PublicFormManager : BaseFormManager, IPublicFormManager, IManagerFactoryBase<IPublicFormManager>
    {
        public IPublicFormManager Create()
        {
            return new PublicFormManager();
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
            string result = null;
            IList<string> errors = Enumerable.Empty<string>().ToList();
            //var formObj = this._formRepo.GetByPrimaryKey(model.Id.Value);

            var template = FindTemplate(model.TemplateID.Value);

            var templateView = TemplateViewModel.CreateFromObject(template, Constants.TemplateFieldMode.INPUT);
            templateView.AssignInputValues(formCollection);
            // this.InsertValuesIntoTempData(SubmitFields, formCollection);

            if (templateView.Fields.Any())
            {
                // first validate fields
                foreach (var field in templateView.Fields)
                {
                    if (!field.SubmittedValueIsValid(formCollection))
                    {
                        field.SetFieldErrors();
                        errors.Add(field.Errors);
                    }

                    var value = field.SubmittedValue(formCollection);
                    if (field.IsRequired && value.IsNullOrEmpty())
                    {
                        field.Errors = "{0} is a required field".FormatWith(field.Label);
                        errors.Add(field.Errors);
                    }
                };

                if (errors.Count == 0)
                {
                    //then insert values
                    var entryId = Guid.NewGuid();

                    using (var unitOfWork = CreateUnitOfWork())
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            IDictionary<string, object> values = new Dictionary<string, object>();

                            foreach (var field in templateView.Fields)
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

                                if (!string.IsNullOrEmpty(field.PreRegistrationFieldName))
                                {
                                    values.Add(field.PreRegistrationFieldName, value);
                                }


                                unitOfWork.FormRepository.InsertTemplateFieldValue(field, value, entryId);
                            }

                            if (Public_Form_Type_PreRegistration.Equals(template.Form.PublicFormType))
                            {
                                PreRegistration preRegistration = new PreRegistration();

                                preRegistration.EntryId = entryId;
                                preRegistration.CreatedDateTime = DateTime.Now;

                                preRegistration.Citizenship = getStringValue(values, PreRegistration_Field_Name_Citizenship);
                                preRegistration.ContactNumber = getStringValue(values, PreRegistration_Field_Name_ContactNumber);
                                preRegistration.DateOfBirth = getDateTimeValue(values, PreRegistration_Field_Name_DateOfBirth);
                                preRegistration.Nric = getStringValue(values, PreRegistration_Field_Name_Nric);
                                preRegistration.PreferedTime = getStringValue(values, PreRegistration_Field_Name_PreferedTime);
                                preRegistration.Race = getStringValue(values, PreRegistration_Field_Name_Race);
                                preRegistration.Salutation = getStringValue(values, PreRegistration_Field_Name_Salutation);
                                preRegistration.Address = getStringValue(values, PreRegistration_Field_Name_Address);
                                preRegistration.Language = getStringValue(values, PreRegistration_Field_Name_Language);
                                preRegistration.FullName = getStringValue(values, PreRegistration_Field_Name_FullName);
                                preRegistration.Gender = getStringValue(values, PreRegistration_Field_Name_Gender);

                                unitOfWork.PreRegistrations.Add(preRegistration);
                            }


                            unitOfWork.Complete();
                            scope.Complete();

                            result = "success";
                        }
                    }
                }
            }

            if (errors.Count > 0)
            {
                result = errors.ToUnorderedList();
            }


            return result;
        }

        private string getStringValue(IDictionary<string, object> values, string key)
        {
            string result = string.Empty;
            if (values.ContainsKey(key) && values[key] != null)
            {
                result = values[key].ToString();
            }

            return result;
        }

        private Nullable<System.DateTime> getDateTimeValue(IDictionary<string, object> values, string key)
        {
            Nullable<System.DateTime> result = null;
            if (values.ContainsKey(key) && values[key] != null)
            {
                result = Convert.ToDateTime(values[key].ToString());
            }

            return result;
        }



    }
}
