
using PHS.DB;
using PHS.DB.ViewModels.Form;
using PHS.Repository.Interface.Core;
using System;
using System.Collections.Generic;

namespace PHS.Repository.Interface
{
    public interface IFormRepository : IRepository<Form>
    {
        Form GetForm(int key);
        Form GetForm(string slug);
        Form GetPublicForm(string slug);
        Template GetTemplate(int key);
        TemplateField GetTemplateField(int templateFieldId);
        void UpdateTemplateField(Template template1, TemplateFieldViewModel fieldView);
        Form CreateNewForm(FormViewModel formViewModel);
        void UpdateForm(FormViewModel formViewModel, Form form1);
        Template CreateNewTemplate(int formId);
        Template CopyTemplate(Template template1);
        void UpdateTemplate(TemplateViewModel model, Template template1);
        void DeleteTemplateField(int id);
        IEnumerable<TemplateFieldValueViewModel> GetTemplateFieldValuesByForm(TemplateViewModel model);
        List<TemplateFieldValue> GetTemplateFieldValuesByTemplate(int templateId);
        void InsertTemplateFieldValue(TemplateFieldViewModel field, string value, Guid entryId, string userId = "");
        TemplateFieldValue GetTemplateFieldValue(TemplateFieldViewModel field, Guid entryId);
        void UpdateTemplateFieldValue(TemplateFieldValue fieldValue, TemplateFieldViewModel field, string value, string userId = "");
        void DeleteEntries(IEnumerable<string> selectedEntries);
        List<FormViewModel> GetForms();
        List<TemplateViewModel> GetTemplates(int formId);
        List<Template> GetBaseTemplates();
        Form GetPreRegistrationForm(int year = -1);
        void DeleteForm(int formId);
        void DeleteForm(Form form);
        void DeleteTemplate(int templateId);
        void DeleteTemplate(Template template1);
        int DeleteTemplates(int olderThanInDays);
        int DeleteSubmissions(int olderThanInDays);
    }
}
