
using PHS.DB;
using PHS.DB.ViewModels.Forms;
using PHS.Repository.Interface.Core;
using System;
using System.Collections.Generic;

namespace PHS.Repository.Interface
{
    public interface IFormRepository : IRepository<Form>
    {
        Form GetForm(int key);
        Form GetPublicForm(string slug);
        Template GetTemplate(int key);
        void UpdateTemplateField(Template template1, TemplateFieldViewModel fieldView);
        Form CreateNewForm(string title);
        void UpdateForm(string title, Form form1);
        Template CreateNewTemplate(string title, int formId);
        Template CopyTemplate(Template template1);
        void UpdateTemplate(TemplateViewModel model, Template template1);
        void DeleteTemplateField(int id);
        IEnumerable<TemplateFieldValueViewModel> GetTemplateFieldValuesByForm(TemplateViewModel model);
        List<TemplateFieldValue> GetTemplateFieldValuesByTemplate(int templateId);
        void InsertTemplateFieldValue(TemplateFieldViewModel field, string value, Guid entryId, string userId = "");
        void DeleteEntries(IEnumerable<string> selectedEntries);
        List<FormViewModel> GetForms();
        List<TemplateViewModel> GetTemplates(int formId);
        List<Template> GetBaseTemplates();
        Template GetPreRegistrationForm(int year = -1);
        void DeleteForm(int formId);
        void DeleteForm(Form form);
        void DeleteTemplate(int templateId);
        void DeleteTemplate(Template template1);
        int DeleteTemplates(int olderThanInDays);
        int DeleteSubmissions(int olderThanInDays);
    }
}
