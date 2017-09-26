using PHS.Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using PHS.DB;
using PHS.Repository;
using PHS.Repository.Context;
using System.IO;
using OfficeOpenXml;
using PHS.DB.ViewModels.Form;
using PHS.Business.Extensions;
using System.Transactions;
using System.Web.Mvc;
using PHS.Common;
using static PHS.Common.Constants;
using PHS.DB.ViewModels;
using System.Web;
using System.Collections;

namespace PHS.Business.Implementation
{
    public class FormManager : BaseFormManager, IFormManager, IManagerFactoryBase<IFormManager>
    {
        public IFormManager Create()
        {
            return new FormManager();
        }

        [System.Obsolete("To be deprecated since use by formImport")]
        public List<Template> FindAllTemplates()
        {
            List<Template> templates = new List<Template>();
            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                templates = unitOfWork.FormRepository.GetBaseTemplates();

                if (templates != null)
                {
                    return templates;
                }
            }

            return templates;

        }

        public List<FormViewModel> FindAllFormsByDes()
        {
            using (var unitOfWork = CreateUnitOfWork())
            {
                var forms = unitOfWork.FormRepository.GetForms().OrderByDescending(f => f.DateAdded).ToList();

                return forms;
            }
        }

        public string DeleteFormAndTemplate(int formId)
        {
            string result = null;
            using (var unitOfWork = CreateUnitOfWork())
            {
                var form = unitOfWork.FormRepository.GetForm(formId);

                if (form != null)
                {

                    IEnumerable<Modality> modalities = unitOfWork.Modalities.GetModalityByFormID(formId);

                    if (modalities != null && !modalities.Any())
                    {
                        List<TemplateViewModel> templates = null;

                        templates = FindAllTemplatesByFormId(formId);

                        foreach (var templateView in templates)
                        {
                            templateView.Entries = HasSubmissions(templateView).ToList();
                            if (templateView.Entries.Any())
                            {
                                result = "Unable to delete - Templates must have no entries to be able to be deleted";
                            }
                        }

                        if (result.IsNullOrEmpty())
                        {
                            try
                            {
                                using (TransactionScope scope = new TransactionScope())
                                {
                                    foreach (var templateView in templates)
                                    {
                                        unitOfWork.FormRepository.DeleteTemplate(templateView.TemplateID.Value);
                                    }

                                    unitOfWork.FormRepository.DeleteForm(formId);

                                    unitOfWork.Complete();
                                    scope.Complete();

                                    result = "success";
                                }


                            }
                            catch
                            {
                                result = "Unable to delete form - there is an error deleting the form and template";
                            }
                        }
                    }

                    else
                    {
                        result = "Unable to delete - Form is already attached to a modality";
                    }

                    
                }

                else
                {
                    result = "Unable to delete form - invalid id";
                }
            }

            return result;
        }

        public List<TemplateViewModel> FindAllTemplatesByFormId(int formId)
        {
            using (var unitOfWork = CreateUnitOfWork())
            {
                var form = unitOfWork.FormRepository.GetForm(formId);
                if (form == null)
                {
                    throw new Exception("Invalid id");
                }

                var templates = unitOfWork.FormRepository.GetTemplates(formId).OrderByDescending(f => f.DateAdded).ToList();

                return templates;
            }
        }

        public FormViewModel FindFormToEdit(int formID)
        {
            using (var unitOfWork = CreateUnitOfWork())
            {
                Form form = unitOfWork.FormRepository.GetForm(formID);

                if (form == null)
                {
                    return null;
                }

                FormViewModel model1 = FormViewModel.CreateFromObject(form);

                return model1;
            }
        }

        public TemplateViewModel FindTemplateToEdit(int templateID)
        {
            using (var unitOfWork = CreateUnitOfWork())
            {
                Template template = unitOfWork.FormRepository.GetTemplate(templateID);

                if (template == null)
                {
                    return null;
                }

                Constants.TemplateMode Mode = Constants.TemplateMode.EDIT;

                var templateView = TemplateViewModel.CreateFromObject(template);
                templateView.Entries = HasSubmissions(templateView).ToList();

                if (templateView.Entries.Any())
                {
                    var templates = FindAllTemplatesByFormId(template.FormID);
                    if (templates.Count() == template.Version)
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            template = unitOfWork.FormRepository.CopyTemplate(template);

                            unitOfWork.Complete();
                            scope.Complete();
                        }
                    }

                    else
                    {
                        Mode = Constants.TemplateMode.READONLY;
                    }
                }

                TemplateViewModel model1 = TemplateViewModel.CreateFromObject(template);
                model1.Mode = Mode;

                return model1;
            }
        }

        public void UpdateTemplate(TemplateViewModel model, FormCollection collection, IDictionary<string, string> Fields)
        {
            using (var unitOfWork = CreateUnitOfWork())
            {
                var template = FindTemplate(model.TemplateID.Value);

                using (TransactionScope scope = new TransactionScope())
                {
                    // first update the form metadata
                    unitOfWork.FormRepository.UpdateTemplate(model, template);

                    // then if fields were passed in, update them
                    if (Fields.Count() > 0)
                    {
                        foreach (var kvp in Fields)
                        {

                            var domId = Convert.ToInt32(kvp.Key);
                            if (domId >= 0)
                            {
                                var fieldType = collection.FormFieldValue(domId, "FieldType");
                                var fieldId = collection.FormFieldValue(domId, "Id");
                                var minAge = collection.FormFieldValue(domId, "MinimumAge").IsInt(18);
                                var maxAge = collection.FormFieldValue(domId, "MaximumAge").IsInt(100);

                                if (minAge >= maxAge)
                                {
                                    minAge = 18;
                                    maxAge = 100;
                                }

                                var fieldTypeEnum = (Constants.TemplateFieldType)Enum.Parse(typeof(Constants.TemplateFieldType), fieldType.ToUpper());

                                var fieldView = new TemplateFieldViewModel
                                {
                                    DomId = Convert.ToInt32(domId),
                                    FieldType = fieldTypeEnum,
                                    MaxCharacters = collection.FormFieldValue(domId, "MaxCharacters").IsInt(),
                                    Text = collection.FormFieldValue(domId, "Text"),
                                    Label = collection.FormFieldValue(domId, "Label"),
                                    IsRequired = collection.FormFieldValue(domId, "IsRequired").IsBool(),
                                    Options = collection.FormFieldValue(domId, "Options"),
                                    SelectedOption = collection.FormFieldValue(domId, "SelectedOption"),
                                    AddOthersOption = collection.FormFieldValue(domId, "AddOthersOption").IsBool(),
                                    OthersOption = collection.FormFieldValue(domId, "OthersOption"),
                                    OthersPlaceHolder = collection.FormFieldValue(domId, "OthersPlaceHolder"),
                                    HoverText = collection.FormFieldValue(domId, "HoverText"),
                                    Hint = collection.FormFieldValue(domId, "Hint"),
                                    MinimumAge = minAge,
                                    MaximumAge = maxAge,
                                    HelpText = collection.FormFieldValue(domId, "HelpText"),
                                    SubLabel = collection.FormFieldValue(domId, "SubLabel"),
                                    Size = collection.FormFieldValue(domId, "Size"),
                                    Columns = collection.FormFieldValue(domId, "Columns").IsInt(20),
                                    Rows = collection.FormFieldValue(domId, "Columns").IsInt(2),
                                    Validation = collection.FormFieldValue(domId, "Validation"),
                                    Order = collection.FormFieldValue(domId, "Order").IsInt(1),
                                    MaxFileSize = collection.FormFieldValue(domId, "MaxFileSize").IsInt(5000),
                                    MinFileSize = collection.FormFieldValue(domId, "MinFileSize").IsInt(5),
                                    ValidFileExtensions = collection.FormFieldValue(domId, "ValidExtensions"),
                                    ImageBase64 = collection.FormFieldValue(domId, "ImageBase64"),
                                    MatrixColumn = collection.FormFieldValue(domId, "MatrixColumn"),
                                    MatrixRow = collection.FormFieldValue(domId, "MatrixRow"),
                                    PreRegistrationFieldName = collection.FormFieldValue(domId, "PreRegistrationFieldName"),
                                    RegistrationFieldName = collection.FormFieldValue(domId, "RegistrationFieldName"),
                                    SummaryFieldName = collection.FormFieldValue(domId, "SummaryFieldName"),
                                    SummaryType = collection.FormFieldValue(domId, "SummaryType"),
                                    ConditionCriteria = collection.FormFieldValue(domId, "ConditionCriteria"),
                                    ConditionOptions = collection.FormFieldValue(domId, "ConditionOptions"),
                                };

                                var conditionTemplateFieldID = collection.FormFieldValue(domId, "ConditionTemplateFieldID");

                                if (!string.IsNullOrEmpty(conditionTemplateFieldID))
                                {
                                    fieldView.ConditionTemplateFieldID = Convert.ToInt32(conditionTemplateFieldID);
                                }

                                var standardReferenceID = collection.FormFieldValue(domId, "StandardReferenceID");

                                if (!string.IsNullOrEmpty(standardReferenceID))
                                {
                                    fieldView.StandardReferenceID = Convert.ToInt32(standardReferenceID);
                                }

                                if (!fieldId.IsNullOrEmpty() && fieldId.IsInteger())
                                {
                                    fieldView.TemplateFieldID = Convert.ToInt32(fieldId);
                                }

                                unitOfWork.FormRepository.UpdateTemplateField(template, fieldView);

                            }
                        }
                    }

                    unitOfWork.Complete();
                    scope.Complete();
                }
            }
        }

        public string DeleteTemplate(int templateID)
        {
            string result = null;
            using (var unitOfWork = CreateUnitOfWork())
            {
                var template = FindTemplate(templateID);

                if (template != null)
                {
                    var templateView = TemplateViewModel.CreateFromObject(template);

                    var templates = FindAllTemplatesByFormId(template.FormID);
                    if (templates.Count() == 1)
                    {
                        result = "Unable to delete template when there is only one remains";
                    }

                    else
                    {
                        templateView.Entries = HasSubmissions(templateView).ToList();

                        if (!templateView.Entries.Any())
                        {
                            try
                            {
                                using (TransactionScope scope = new TransactionScope())
                                {
                                    unitOfWork.FormRepository.DeleteTemplate(templateID);

                                    unitOfWork.Complete();
                                    scope.Complete();

                                    result = "success";
                                }
                            }
                            catch
                            {
                                result = "Unable to delete template - there is an error deleting the template";
                            }
                        }

                        else
                        {
                            result = "Unable to delete template - Template must have no entries to be able to be deleted";
                        }
                    }


                }

                else
                {
                    result = "Unable to delete template - invalid id";
                }
            }

            return result;
        }

        public void DeleteTemplateField(int templateFieldID)
        {
            using (var unitOfWork = CreateUnitOfWork())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    unitOfWork.FormRepository.DeleteTemplateField(templateFieldID);

                    unitOfWork.Complete();
                    scope.Complete();
                }
            }
        }

        public IEnumerable<TemplateFieldValueViewModel> HasSubmissions(TemplateViewModel model)
        {
            using (var unitOfWork = CreateUnitOfWork())
            {
                var fieldValues = unitOfWork.FormRepository.GetTemplateFieldValuesByTemplate(model.TemplateID.Value);
                var values = fieldValues
                             .Select((fv) =>
                             {
                                 return TemplateFieldValueViewModel.CreateFromObject(fv);
                             })
                             .OrderBy(f => f.FieldOrder)
                             .ThenByDescending(f => f.DateAdded);

                return values;
            }
        }

        public Template CreateNewFormAndTemplate(FormViewModel formViewModel)
        {
            Template template = null;
            using (var unitOfWork = CreateUnitOfWork())
            {
                if (!formViewModel.IsPublic)
                {
                    formViewModel.Slug = null;
                    formViewModel.PublicFormType = null;
                }

                else
                {
                    formViewModel.InternalFormType = null;

                    Form form = unitOfWork.FormRepository.GetForm(formViewModel.Slug);
                    if (form != null)
                    {
                        throw new Exception("Slug has already being used. Please use another.");
                    }
                }

                using (TransactionScope scope = new TransactionScope())
                {
                    var form = unitOfWork.FormRepository.CreateNewForm(formViewModel);
                    template = unitOfWork.FormRepository.CreateNewTemplate(form.FormID);

                    unitOfWork.Complete();
                    scope.Complete();
                }
            }

            return template;

        }

        public string EditForm(FormViewModel formViewModel)
        {
            string result = null;
            using (var unitOfWork = CreateUnitOfWork())
            {
                Form form = unitOfWork.FormRepository.GetForm(formViewModel.FormID.Value);
                if (form != null)
                {
                    if (!formViewModel.IsPublic)
                    {
                        formViewModel.Slug = null;
                        formViewModel.PublicFormType = null;
                    }

                    else
                    {
                        formViewModel.InternalFormType = null;

                        Form form1 = unitOfWork.FormRepository.GetForm(formViewModel.Slug);
                        if (form1 != null && form1.FormID != formViewModel.FormID)
                        {
                            throw new Exception("Slug has already being used. Please use another.");
                        }
                    }

                    using (TransactionScope scope = new TransactionScope())
                    {
                        unitOfWork.FormRepository.UpdateForm(formViewModel, form);

                        unitOfWork.Complete();
                        scope.Complete();

                        result = "success";
                    }
                }

                else
                {
                    result = "Unable to edit form - invalid id";
                }
            }

            return result;
        }

        
        

        public string FindSaveValue(string entryId, int fieldID)
        {
            try
            {
                using (var unitOfWork = CreateUnitOfWork())
                {
                    var guid = Guid.Parse(entryId);
                    var value = unitOfWork.TemplateFieldValues.Find(u => u.EntryId.Equals(guid) && u.TemplateFieldID == fieldID);

                    return value.First().Value;
                }
            }
            catch
            {
                return "";
            }

        }

        public List<ModalityForm> FindModalityForm(int modalityID)
        {
            IEnumerable<Form> formList;
            Modality modality;
            Boolean isPublicFacing = false;
            
            try
            {
                using (var unitOfWork = CreateUnitOfWork())
                {                    
                    var forms = unitOfWork.FormRepository.GetAll().Where(f => f.IsActive == true);
                    formList = (IEnumerable<Form>) forms;
                    modality = unitOfWork.Modalities.GetModalityByID(modalityID); 

                    if(modality.Position.Equals(99) && modality.Status.Equals("Public"))
                    {
                        isPublicFacing = true; 
                    }
                }
            }
            catch
            {
                return null;
            }

            List<ModalityForm> modalityFormList = new List<ModalityForm>(); 

            foreach (Form form in formList)
            {
                ModalityForm modalityForm = new ModalityForm();
                Boolean isSelected = false; 
                for(int j = 0; j < modality.Forms.Count; j++)
                {
                    if (modality.Forms.ElementAt(j).FormID == form.FormID)
                    {
                        isSelected = true;
                        break;
                    }
                }

                modalityForm.FormName = form.Title;
                modalityForm.FormID = form.FormID;
                modalityForm.IsSelected = isSelected;

                if(isPublicFacing)
                {

                    modalityForm.publicURL = HttpContext.Current.Request.Url.AbsolutePath;


                }

                modalityFormList.Add(modalityForm); 
            }

            return modalityFormList; 
        }

        public void AddModalityForm(int formID, int modalityID, int eventID)
        {
            Form form; 
            Modality modality;
            try
            {
                using (var unitOfWork = CreateUnitOfWork())
                {
                    form = unitOfWork.FormRepository.Get(formID);
                    modality = unitOfWork.Modalities.GetModalityByID(modalityID);
                    modality.Forms.Add(form);

                    if (modality.Position.Equals(99) && modality.Status.Equals("Public"))
                    {
                        AddToPublicFacing(formID, eventID, "PRE-REGISTRATION");
                    }

                    using (TransactionScope scope = new TransactionScope())
                    {
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                }
            }
            catch
            {
               
            }
        }

        public void RemoveModalityForm(int formID, int modalityID, int eventID)
        {
            Form form;
            Modality modality;
            try
            {
                using (var unitOfWork = CreateUnitOfWork())
                {
                    form = unitOfWork.FormRepository.Get(formID);
                    modality = unitOfWork.Modalities.GetModalityByID(modalityID);

                    modality.Forms.Remove(form);

                    if (modality.Position.Equals(99) && modality.Status.Equals("Public"))
                    {
                        RemoveFromPublicFacing(formID);
                    }

                    using (TransactionScope scope = new TransactionScope())
                    {
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                }
            }
            catch
            {

            }
        }

        public void AddToPublicFacing(int formID, int eventID, string publicFormType)
        {
            Form form;
            try
            {
                using (var unitOfWork = CreateUnitOfWork())
                {
                    form = unitOfWork.FormRepository.Get(formID);

                    PHSEvent phsEvent = unitOfWork.Events.Get(eventID);

                    string publicURL = phsEvent.Title.Replace(" ", "") + "_" + form.Title.Replace(" ", "").Substring(0, 6); 

                    form.IsPublic = true;
                    form.PublicFormType = publicFormType;
                    form.Slug = publicURL;
                                


                    using (TransactionScope scope = new TransactionScope())
                    {
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                }
            }
            catch
            {

            }
        }

        public void RemoveFromPublicFacing(int formID)
        {
            Form form;
            try
            {
                using (var unitOfWork = CreateUnitOfWork())
                {
                    form = unitOfWork.FormRepository.Get(formID);
                    form.IsPublic = false;
                    form.PublicFormType = null;
                    form.Slug = null; 

                    using (TransactionScope scope = new TransactionScope())
                    {
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                }
            }
            catch
            {

            }
        }

        public TemplateFieldViewModel FindTemplateField(int templateFieldID)
        {
            using (var unitOfWork = CreateUnitOfWork())
            {
                TemplateField templateField = unitOfWork.FormRepository.GetTemplateField(templateFieldID);

                if (templateField == null)
                {
                    return null;
                }

                return TemplateFieldViewModel.CreateFromObject(templateField);
            }
        }
    }
}
