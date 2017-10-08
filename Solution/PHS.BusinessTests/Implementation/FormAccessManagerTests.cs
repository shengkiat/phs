using Effort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PHS.Business.Implementation;
using PHS.Common;
using PHS.DB;
using PHS.DB.ViewModels.Form;
using PHS.Repository.Context;
using PHS.Repository.Interface.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PHS.BusinessTests.Implementation
{
    [TestClass()]
    public class FormAccessManagerTests
    {
        private FormManager _formManager;
        private FormAccessManager _target;
        private IUnitOfWork _unitOfWork;
        private PHSContext _context;

        [TestMethod()]
        public void FindPublicTemplate_ShouldHaveRecordAfterCreate()
        {
            string slug = "test";
            Template preExecuteResult = _target.FindPublicTemplate(slug);
            Assert.IsNull(preExecuteResult);

            FormViewModel formViewModel = new FormViewModel();
            formViewModel.Title = "Required Form Name";
            formViewModel.Slug = slug;
            formViewModel.IsPublic = true;
            formViewModel.PublicFormType = "TEST";

            Template template = _formManager.CreateNewFormAndTemplate(formViewModel);
            Assert.IsNotNull(template);

            Template postExecuteResult = _target.FindPublicTemplate(slug);
            Assert.IsNotNull(postExecuteResult);
        }

        [TestMethod()]
        public void FindPublicTemplate_ShouldGetLatestVersion()
        {
            string slug = "test";
            
            FormViewModel formViewModel = new FormViewModel();
            formViewModel.Title = "Required Form Name";
            formViewModel.Slug = slug;
            formViewModel.IsPublic = true;
            formViewModel.PublicFormType = "TEST";

            Template template;
            TemplateViewModel templateViewModel;
            CreateDefaultTemplateAndField(formViewModel, out template, out templateViewModel);

            Template preExecuteResult = _target.FindPublicTemplate(slug);
            Assert.IsNotNull(preExecuteResult);
            Assert.AreEqual(1, preExecuteResult.Version);

            templateViewModel = _formManager.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(templateViewModel.Fields);
            Assert.AreEqual(1, templateViewModel.Fields.Count);
            Assert.AreEqual(template.TemplateID, templateViewModel.TemplateID);

            templateViewModel.Entries = _formManager.HasSubmissions(templateViewModel).ToList();
            Assert.AreEqual(0, templateViewModel.Entries.Count);

            FormCollection submissionCollection = new FormCollection();
            submissionCollection.Add("SubmitFields[1].TextBox", "HelloTest");

            IDictionary<string, string> submissionFields = new System.Collections.Generic.Dictionary<string, string>();
            submissionFields.Add("1", "1");

            string result = _target.FillIn(submissionFields, templateViewModel, submissionCollection);
            Assert.AreEqual(result, "success");

            TemplateViewModel executeResult = _formManager.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(executeResult);

            Template postExecuteResult = _target.FindPublicTemplate(slug);
            Assert.IsNotNull(postExecuteResult);
            Assert.AreEqual(2, postExecuteResult.Version);
        }

        [TestMethod()]
        public void FindPublicTemplate_ShouldGetActiveVersionAfterDelete()
        {
            string slug = "test";

            FormViewModel formViewModel = new FormViewModel();
            formViewModel.Title = "Required Form Name";
            formViewModel.Slug = slug;
            formViewModel.IsPublic = true;
            formViewModel.PublicFormType = "TEST";

            Template template;
            TemplateViewModel templateViewModel;
            CreateDefaultTemplateAndField(formViewModel, out template, out templateViewModel);

            Template preExecuteResult = _target.FindPublicTemplate(slug);
            Assert.IsNotNull(preExecuteResult);
            Assert.AreEqual(1, preExecuteResult.Version);

            templateViewModel = _formManager.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(templateViewModel.Fields);
            Assert.AreEqual(1, templateViewModel.Fields.Count);
            Assert.AreEqual(template.TemplateID, templateViewModel.TemplateID);

            templateViewModel.Entries = _formManager.HasSubmissions(templateViewModel).ToList();
            Assert.AreEqual(0, templateViewModel.Entries.Count);

            FormCollection submissionCollection = new FormCollection();
            submissionCollection.Add("SubmitFields[1].TextBox", "HelloTest");

            IDictionary<string, string> submissionFields = new System.Collections.Generic.Dictionary<string, string>();
            submissionFields.Add("1", "1");

            string result = _target.FillIn(submissionFields, templateViewModel, submissionCollection);
            Assert.AreEqual(result, "success");

            TemplateViewModel executeResult = _formManager.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(executeResult);

            Template postExecuteResult = _target.FindPublicTemplate(slug);
            Assert.IsNotNull(postExecuteResult);
            Assert.AreEqual(2, postExecuteResult.Version);

            _formManager.DeleteTemplate(executeResult.TemplateID.Value);

            postExecuteResult = _target.FindPublicTemplate(slug);
            Assert.IsNotNull(postExecuteResult);
            Assert.AreEqual(1, postExecuteResult.Version);
        }

        [TestMethod()]
        public void FindPublicTemplate_ShouldHaveNoRecordAfterCreateNonPublic()
        {
            string slug = "test";
            Template preExecuteResult = _target.FindPublicTemplate(slug);
            Assert.IsNull(preExecuteResult);

            FormViewModel formViewModel = new FormViewModel();
            formViewModel.Title = "Required Form Name";
            formViewModel.IsPublic = false;
            formViewModel.Slug = slug;

            Template template = _formManager.CreateNewFormAndTemplate(formViewModel);
            Assert.IsNotNull(template);

            Template postExecuteResult = _target.FindPublicTemplate(slug);
            Assert.IsNull(postExecuteResult);
        }

        [TestMethod()]
        public void FindPreRegistrationForm_ShouldHaveRecord()
        {
            Template preExecuteResult = _target.FindPreRegistrationForm();
            Assert.IsNull(preExecuteResult);

            FormViewModel formViewModel = new FormViewModel();
            formViewModel.Title = "Required Form Name";
            formViewModel.IsPublic = true;
            formViewModel.PublicFormType = "PRE-REGISTRATION";
            formViewModel.Slug = "TEST";

            Template template = _formManager.CreateNewFormAndTemplate(formViewModel);
            Assert.IsNotNull(template);

            Template postExecuteResult = _target.FindPreRegistrationForm();
            Assert.IsNotNull(postExecuteResult);
        }

        [TestMethod()]
        public void FindPreRegistrationForm_ShouldGetLatestVersion()
        {
            FormViewModel formViewModel = new FormViewModel();
            formViewModel.Title = "Required Form Name";
            formViewModel.IsPublic = true;
            formViewModel.PublicFormType = "PRE-REGISTRATION";
            formViewModel.Slug = "TEST";

            Template template;
            TemplateViewModel templateViewModel;
            CreateDefaultTemplateAndField(formViewModel, out template, out templateViewModel);

            Template preExecuteResult = _target.FindPreRegistrationForm();
            Assert.IsNotNull(preExecuteResult);
            Assert.AreEqual(1, preExecuteResult.Version);

            templateViewModel = _formManager.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(templateViewModel.Fields);
            Assert.AreEqual(1, templateViewModel.Fields.Count);
            Assert.AreEqual(template.TemplateID, templateViewModel.TemplateID);

            templateViewModel.Entries = _formManager.HasSubmissions(templateViewModel).ToList();
            Assert.AreEqual(0, templateViewModel.Entries.Count);

            FormCollection submissionCollection = new FormCollection();
            submissionCollection.Add("SubmitFields[1].TextBox", "HelloTest");

            IDictionary<string, string> submissionFields = new System.Collections.Generic.Dictionary<string, string>();
            submissionFields.Add("1", "1");

            string result = _target.FillIn(submissionFields, templateViewModel, submissionCollection);
            Assert.AreEqual(result, "success");

            TemplateViewModel executeResult = _formManager.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(executeResult);

            Template postExecuteResult = _target.FindPreRegistrationForm();
            Assert.IsNotNull(postExecuteResult);
            Assert.AreEqual(2, postExecuteResult.Version);
        }

        [TestMethod()]
        public void FindPreRegistrationForm_ShouldGetActiveVersionAfterDelete()
        {
            FormViewModel formViewModel = new FormViewModel();
            formViewModel.Title = "Required Form Name";
            formViewModel.IsPublic = true;
            formViewModel.PublicFormType = "PRE-REGISTRATION";
            formViewModel.Slug = "TEST";

            Template template;
            TemplateViewModel templateViewModel;
            CreateDefaultTemplateAndField(formViewModel, out template, out templateViewModel);

            Template preExecuteResult = _target.FindPreRegistrationForm();
            Assert.IsNotNull(preExecuteResult);
            Assert.AreEqual(1, preExecuteResult.Version);

            templateViewModel = _formManager.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(templateViewModel.Fields);
            Assert.AreEqual(1, templateViewModel.Fields.Count);
            Assert.AreEqual(template.TemplateID, templateViewModel.TemplateID);

            templateViewModel.Entries = _formManager.HasSubmissions(templateViewModel).ToList();
            Assert.AreEqual(0, templateViewModel.Entries.Count);

            FormCollection submissionCollection = new FormCollection();
            submissionCollection.Add("SubmitFields[1].TextBox", "HelloTest");

            IDictionary<string, string> submissionFields = new System.Collections.Generic.Dictionary<string, string>();
            submissionFields.Add("1", "1");

            string result = _target.FillIn(submissionFields, templateViewModel, submissionCollection);
            Assert.AreEqual(result, "success");

            TemplateViewModel executeResult = _formManager.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(executeResult);

            Template postExecuteResult = _target.FindPreRegistrationForm();
            Assert.IsNotNull(postExecuteResult);
            Assert.AreEqual(2, postExecuteResult.Version);

            _formManager.DeleteTemplate(executeResult.TemplateID.Value);

            postExecuteResult = _target.FindPreRegistrationForm();
            Assert.IsNotNull(postExecuteResult);
            Assert.AreEqual(1, postExecuteResult.Version);
        }

        [TestMethod()]
        public void FindLatestTemplate_ShouldHaveRecordAfterCreate()
        {
            Template preExecuteResult = _target.FindLatestTemplate(1);
            Assert.IsNull(preExecuteResult);

            Template template;
            TemplateViewModel templateViewModel;
            FormViewModel formViewModel = new FormViewModel();
            formViewModel.Title = "Required Form Name";
            CreateDefaultTemplateAndField(formViewModel, out template, out templateViewModel);

            Template postExecuteResult = _target.FindLatestTemplate(1);
            Assert.IsNotNull(postExecuteResult);
            Assert.AreEqual(1, postExecuteResult.Version);
        }

        [TestMethod()]
        public void FindLatestTemplate_ShouldGetLatestVersion()
        {
            Template template;
            TemplateViewModel templateViewModel;
            FormViewModel formViewModel = new FormViewModel();
            formViewModel.Title = "Required Form Name";
            CreateDefaultTemplateAndField(formViewModel, out template, out templateViewModel);

            Template preExecuteResult = _target.FindLatestTemplate(1);
            Assert.IsNotNull(preExecuteResult);
            Assert.AreEqual(1, preExecuteResult.Version);

            templateViewModel = _formManager.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(templateViewModel.Fields);
            Assert.AreEqual(1, templateViewModel.Fields.Count);
            Assert.AreEqual(template.TemplateID, templateViewModel.TemplateID);

            templateViewModel.Entries = _formManager.HasSubmissions(templateViewModel).ToList();
            Assert.AreEqual(0, templateViewModel.Entries.Count);

            FormCollection submissionCollection = new FormCollection();
            submissionCollection.Add("SubmitFields[1].TextBox", "HelloTest");

            IDictionary<string, string> submissionFields = new System.Collections.Generic.Dictionary<string, string>();
            submissionFields.Add("1", "1");

            string result = _target.FillIn(submissionFields, templateViewModel, submissionCollection);
            Assert.AreEqual(result, "success");

            TemplateViewModel executeResult = _formManager.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(executeResult);

            Template postExecuteResult = _target.FindLatestTemplate(1);
            Assert.IsNotNull(postExecuteResult);
            Assert.AreEqual(2, postExecuteResult.Version);
        }

        [TestMethod()]
        public void FindLatestTemplate_ShouldGetActiveVersionAfterDelete()
        {
            Template template;
            TemplateViewModel templateViewModel;
            FormViewModel formViewModel = new FormViewModel();
            formViewModel.Title = "Required Form Name";
            CreateDefaultTemplateAndField(formViewModel, out template, out templateViewModel);

            Template preExecuteResult = _target.FindLatestTemplate(1);
            Assert.IsNotNull(preExecuteResult);
            Assert.AreEqual(1, preExecuteResult.Version);

            templateViewModel = _formManager.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(templateViewModel.Fields);
            Assert.AreEqual(1, templateViewModel.Fields.Count);
            Assert.AreEqual(template.TemplateID, templateViewModel.TemplateID);

            templateViewModel.Entries = _formManager.HasSubmissions(templateViewModel).ToList();
            Assert.AreEqual(0, templateViewModel.Entries.Count);

            FormCollection submissionCollection = new FormCollection();
            submissionCollection.Add("SubmitFields[1].TextBox", "HelloTest");

            IDictionary<string, string> submissionFields = new System.Collections.Generic.Dictionary<string, string>();
            submissionFields.Add("1", "1");

            string result = _target.FillIn(submissionFields, templateViewModel, submissionCollection);
            Assert.AreEqual(result, "success");

            TemplateViewModel executeResult = _formManager.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(executeResult);

            Template postExecuteResult = _target.FindLatestTemplate(1);
            Assert.IsNotNull(postExecuteResult);
            Assert.AreEqual(2, postExecuteResult.Version);

            _formManager.DeleteTemplate(executeResult.TemplateID.Value);

            postExecuteResult = _target.FindLatestTemplate(1);
            Assert.IsNotNull(postExecuteResult);
            Assert.AreEqual(1, postExecuteResult.Version);
        }

        [TestMethod()]
        public void FillIn_SuccessWithHasSubmissions()
        {
            Template template;
            TemplateViewModel templateViewModel;
            FormViewModel formViewModel = new FormViewModel();
            formViewModel.Title = "Required Form Name";
            CreateDefaultTemplateAndField(formViewModel, out template, out templateViewModel);

            templateViewModel = _formManager.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(templateViewModel.Fields);
            Assert.AreEqual(1, templateViewModel.Fields.Count);

            templateViewModel.Entries = _formManager.HasSubmissions(templateViewModel).ToList();
            Assert.AreEqual(0, templateViewModel.Entries.Count);

            FormCollection submissionCollection = new FormCollection();
            submissionCollection.Add("SubmitFields[1].TextBox", "HelloTest");

            IDictionary<string, string> submissionFields = new System.Collections.Generic.Dictionary<string, string>();
            submissionFields.Add("1", "1");

            string result = _target.FillIn(submissionFields, templateViewModel, submissionCollection);
            Assert.AreEqual(result, "success");

            templateViewModel.Entries = _formManager.HasSubmissions(templateViewModel).ToList();

            Assert.AreEqual(1, templateViewModel.Entries.Count);

            Assert.AreEqual(0, _unitOfWork.PreRegistrations.GetAll().Count());
        }

        [TestMethod()]
        public void FillIn_SuccessWithBirthdayPickerSingleDigitMonth()
        {
            Template template;
            TemplateViewModel templateViewModel;
            FormViewModel formViewModel = new FormViewModel();
            formViewModel.Title = "Required Form Name";
            CreateTemplateAndField(formViewModel, Constants.TemplateFieldType.BIRTHDAYPICKER, out template, out templateViewModel);

            templateViewModel = _formManager.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(templateViewModel.Fields);
            Assert.AreEqual(1, templateViewModel.Fields.Count);

            templateViewModel.Entries = _formManager.HasSubmissions(templateViewModel).ToList();
            Assert.AreEqual(0, templateViewModel.Entries.Count);

            FormCollection submissionCollection = new FormCollection();
            submissionCollection.Add("SubmitFields[1].Day", "18");
            submissionCollection.Add("SubmitFields[1].Month", "7");
            submissionCollection.Add("SubmitFields[1].Year", "2017");

            IDictionary<string, string> submissionFields = new System.Collections.Generic.Dictionary<string, string>();
            submissionFields.Add("1", "1");

            string result = _target.FillIn(submissionFields, templateViewModel, submissionCollection);
            Assert.AreEqual(result, "success");

            templateViewModel.Entries = _formManager.HasSubmissions(templateViewModel).ToList();

            Assert.AreEqual(0, _unitOfWork.PreRegistrations.GetAll().Count());

            Assert.AreEqual(1, templateViewModel.Entries.Count);

            TemplateFieldValueViewModel templateFieldValue = templateViewModel.Entries.FirstOrDefault();
            Assert.AreEqual("18/7/2017 12:00:00 AM", templateFieldValue.Value);
        }

        [TestMethod()]
        public void FillIn_SuccessWithBirthdayPickerDoubleDigitMonth()
        {
            Template template;
            TemplateViewModel templateViewModel;
            FormViewModel formViewModel = new FormViewModel();
            formViewModel.Title = "Required Form Name";
            CreateTemplateAndField(formViewModel, Constants.TemplateFieldType.BIRTHDAYPICKER, out template, out templateViewModel);

            templateViewModel = _formManager.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(templateViewModel.Fields);
            Assert.AreEqual(1, templateViewModel.Fields.Count);

            templateViewModel.Entries = _formManager.HasSubmissions(templateViewModel).ToList();
            Assert.AreEqual(0, templateViewModel.Entries.Count);

            FormCollection submissionCollection = new FormCollection();
            submissionCollection.Add("SubmitFields[1].Day", "09");
            submissionCollection.Add("SubmitFields[1].Month", "11");
            submissionCollection.Add("SubmitFields[1].Year", "2017");

            IDictionary<string, string> submissionFields = new System.Collections.Generic.Dictionary<string, string>();
            submissionFields.Add("1", "1");

            string result = _target.FillIn(submissionFields, templateViewModel, submissionCollection);
            Assert.AreEqual(result, "success");

            templateViewModel.Entries = _formManager.HasSubmissions(templateViewModel).ToList();

            Assert.AreEqual(0, _unitOfWork.PreRegistrations.GetAll().Count());

            Assert.AreEqual(1, templateViewModel.Entries.Count);

            TemplateFieldValueViewModel templateFieldValue = templateViewModel.Entries.FirstOrDefault();
            Assert.AreEqual("09/11/2017 12:00:00 AM", templateFieldValue.Value);
        }

        [TestMethod()]
        public void FillIn_SuccessWithCheckbox()
        {
            FormViewModel formViewModel = new FormViewModel();
            formViewModel.Title = "Required Form Name";
            Template template = _formManager.CreateNewFormAndTemplate(formViewModel);
            Assert.IsNotNull(template);

            TemplateViewModel templateViewModel = _formManager.FindTemplateToEdit(template.TemplateID);

            FormCollection fieldCollection = new FormCollection();
            IDictionary<string, string> fields;
            CeateFieldForm(1, Constants.TemplateFieldType.CHECKBOX.ToString(), fieldCollection, out fields);
            fieldCollection.Add("Fields[" + 1 + "].Options", "Test1,1234|Test423|helloworld");

            _formManager.UpdateTemplate(templateViewModel, fieldCollection, fields);

            templateViewModel = _formManager.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(templateViewModel.Fields);
            Assert.AreEqual(1, templateViewModel.Fields.Count);

            templateViewModel.Entries = _formManager.HasSubmissions(templateViewModel).ToList();
            Assert.AreEqual(0, templateViewModel.Entries.Count);

            FormCollection submissionCollection = new FormCollection();
            submissionCollection.Add("SubmitFields[1].CheckBox", "Test1,1234,helloworld");

            IDictionary<string, string> submissionFields = new System.Collections.Generic.Dictionary<string, string>();
            submissionFields.Add("1", "1");

            string result = _target.FillIn(submissionFields, templateViewModel, submissionCollection);
            Assert.AreEqual(result, "success");

            templateViewModel.Entries = _formManager.HasSubmissions(templateViewModel).ToList();

            Assert.AreEqual(0, _unitOfWork.PreRegistrations.GetAll().Count());

            Assert.AreEqual(1, templateViewModel.Entries.Count);

            TemplateFieldValueViewModel templateFieldValue = templateViewModel.Entries.FirstOrDefault();
            Assert.AreEqual("Test1,1234|helloworld", templateFieldValue.Value);
        }

        [TestMethod()]
        public void FillIn_ErrorWithRequiredField()
        {
            FormViewModel formViewModel = new FormViewModel();
            formViewModel.Title = "Required Form Name";
            Template template = _formManager.CreateNewFormAndTemplate(formViewModel);
            Assert.IsNotNull(template);

            TemplateViewModel templateViewModel = _formManager.FindTemplateToEdit(template.TemplateID);

            FormCollection fieldCollection = new FormCollection();
            IDictionary<string, string> fields;
            CeateFieldForm(1, Constants.TemplateFieldType.TEXTBOX.ToString(), fieldCollection, out fields);

            fieldCollection.Set("Fields[" + 1 + "].IsRequired", "true");

            _formManager.UpdateTemplate(templateViewModel, fieldCollection, fields);

            templateViewModel = _formManager.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(templateViewModel.Fields);
            Assert.AreEqual(1, templateViewModel.Fields.Count);

            templateViewModel.Entries = _formManager.HasSubmissions(templateViewModel).ToList();
            Assert.AreEqual(0, templateViewModel.Entries.Count);

            FormCollection submissionCollection = new FormCollection();
            submissionCollection.Add("SubmitFields[1].TextBox", null);

            IDictionary<string, string> submissionFields = new System.Collections.Generic.Dictionary<string, string>();
            submissionFields.Add("1", "1");

            string result = _target.FillIn(submissionFields, templateViewModel, submissionCollection);
            Assert.AreEqual("<ul><li>Click to edit is a required field</li></ul>", result);

            templateViewModel.Entries = _formManager.HasSubmissions(templateViewModel).ToList();

            Assert.AreEqual(0, templateViewModel.Entries.Count);
        }

        [TestMethod()]
        public void FillIn_NotSelectedConditionFieldWithRequiredShouldBeSave()
        {
            FormViewModel formViewModel = new FormViewModel();
            formViewModel.Title = "Required Form Name";
            Template template = _formManager.CreateNewFormAndTemplate(formViewModel);
            Assert.IsNotNull(template);

            TemplateViewModel templateViewModel = _formManager.FindTemplateToEdit(template.TemplateID);

            FormCollection fieldCollection = new FormCollection();
            IDictionary<string, string> fields;

            CeateFieldForm(1, Constants.TemplateFieldType.RADIOBUTTON.ToString(), fieldCollection, out fields);
            fieldCollection.Set("Fields[" + 1 + "].Label", "Radio Button");
            fieldCollection.Set("Fields[" + 1 + "].IsRequired", "true");
            fieldCollection.Add("Fields[" + 1 + "].Options", "Yes,No");

            _formManager.UpdateTemplate(templateViewModel, fieldCollection, fields);

            CeateFieldForm(2, Constants.TemplateFieldType.TEXTBOX.ToString(), fieldCollection, out fields);
            fieldCollection.Set("Fields[" + 2 + "].Label", "Radio Button Yes");
            fieldCollection.Set("Fields[" + 2 + "].IsRequired", "true");
            fieldCollection.Add("Fields[" + 2 + "].ConditionTemplateFieldID", "1");
            fieldCollection.Add("Fields[" + 2 + "].ConditionCriteria", "==");
            fieldCollection.Add("Fields[" + 2 + "].ConditionOptions", "Yes");

            _formManager.UpdateTemplate(templateViewModel, fieldCollection, fields);

            CeateFieldForm(3, Constants.TemplateFieldType.TEXTBOX.ToString(), fieldCollection, out fields);
            fieldCollection.Set("Fields[" + 3 + "].Label", "Radio Button No");
            fieldCollection.Set("Fields[" + 3 + "].IsRequired", "true");
            fieldCollection.Add("Fields[" + 3 + "].ConditionTemplateFieldID", "1");
            fieldCollection.Add("Fields[" + 3 + "].ConditionCriteria", "==");
            fieldCollection.Add("Fields[" + 3 + "].ConditionOptions", "No");

            _formManager.UpdateTemplate(templateViewModel, fieldCollection, fields);

            templateViewModel = _formManager.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(templateViewModel.Fields);
            Assert.AreEqual(3, templateViewModel.Fields.Count);

            templateViewModel.Entries = _formManager.HasSubmissions(templateViewModel).ToList();
            Assert.AreEqual(0, templateViewModel.Entries.Count);

            FormCollection submissionCollection = new FormCollection();
            submissionCollection.Add("SubmitFields[1].RadioButton", "Yes");
            submissionCollection.Add("SubmitFields[2].TextBox", "Testing 123");
            submissionCollection.Add("SubmitFields[3].TextBox", "not saved 123");

            IDictionary<string, string> submissionFields = new System.Collections.Generic.Dictionary<string, string>();
            submissionFields.Add("1", "1");
            submissionFields.Add("2", "3");
            submissionFields.Add("3", "3");

            string result = _target.FillIn(submissionFields, templateViewModel, submissionCollection);
            Assert.AreEqual("success", result);

            templateViewModel.Entries = _formManager.HasSubmissions(templateViewModel).ToList();

            Assert.AreEqual(0, _unitOfWork.PreRegistrations.GetAll().Count());

            Assert.AreEqual(2, templateViewModel.Entries.Count);

            Assert.AreEqual("Yes", templateViewModel.Entries[0].Value);
            Assert.AreEqual("Testing 123", templateViewModel.Entries[1].Value);
        }

        [TestMethod()]
        public void FillIn_NotSelectedConditionFieldWithSubComponentRequiredShouldBeSave()
        {
            FormViewModel formViewModel = new FormViewModel();
            formViewModel.Title = "Required Form Name";
            Template template = _formManager.CreateNewFormAndTemplate(formViewModel);
            Assert.IsNotNull(template);

            TemplateViewModel templateViewModel = _formManager.FindTemplateToEdit(template.TemplateID);

            FormCollection fieldCollection = new FormCollection();
            IDictionary<string, string> fields;

            CeateFieldForm(1, Constants.TemplateFieldType.RADIOBUTTON.ToString(), fieldCollection, out fields);
            fieldCollection.Set("Fields[" + 1 + "].Label", "Radio Button");
            fieldCollection.Set("Fields[" + 1 + "].IsRequired", "true");
            fieldCollection.Add("Fields[" + 1 + "].Options", "Yes,No");

            _formManager.UpdateTemplate(templateViewModel, fieldCollection, fields);

            CeateFieldForm(2, Constants.TemplateFieldType.RADIOBUTTON.ToString(), fieldCollection, out fields);
            fieldCollection.Set("Fields[" + 2 + "].Label", "Radio Button Yes");
            fieldCollection.Set("Fields[" + 2 + "].IsRequired", "true");
            fieldCollection.Add("Fields[" + 2 + "].ConditionTemplateFieldID", "1");
            fieldCollection.Add("Fields[" + 2 + "].ConditionCriteria", "==");
            fieldCollection.Add("Fields[" + 2 + "].ConditionOptions", "Yes");
            fieldCollection.Add("Fields[" + 1 + "].Options", "Yes,No");

            _formManager.UpdateTemplate(templateViewModel, fieldCollection, fields);

            CeateFieldForm(3, Constants.TemplateFieldType.TEXTBOX.ToString(), fieldCollection, out fields);
            fieldCollection.Set("Fields[" + 3 + "].Label", "Radio Button No");
            fieldCollection.Set("Fields[" + 3 + "].IsRequired", "true");
            fieldCollection.Add("Fields[" + 3 + "].ConditionTemplateFieldID", "1");
            fieldCollection.Add("Fields[" + 3 + "].ConditionCriteria", "==");
            fieldCollection.Add("Fields[" + 3 + "].ConditionOptions", "No");

            _formManager.UpdateTemplate(templateViewModel, fieldCollection, fields);

            CeateFieldForm(4, Constants.TemplateFieldType.TEXTBOX.ToString(), fieldCollection, out fields);
            fieldCollection.Set("Fields[" + 4 + "].Label", "Radio Button Yes Yes");
            fieldCollection.Set("Fields[" + 4 + "].IsRequired", "true");
            fieldCollection.Add("Fields[" + 4 + "].ConditionTemplateFieldID", "2");
            fieldCollection.Add("Fields[" + 4 + "].ConditionCriteria", "==");
            fieldCollection.Add("Fields[" + 4 + "].ConditionOptions", "Yes");

            _formManager.UpdateTemplate(templateViewModel, fieldCollection, fields);

            CeateFieldForm(5, Constants.TemplateFieldType.TEXTBOX.ToString(), fieldCollection, out fields);
            fieldCollection.Set("Fields[" + 5 + "].Label", "Radio Button Yes No");
            fieldCollection.Set("Fields[" + 5 + "].IsRequired", "true");
            fieldCollection.Add("Fields[" + 5 + "].ConditionTemplateFieldID", "2");
            fieldCollection.Add("Fields[" + 5 + "].ConditionCriteria", "==");
            fieldCollection.Add("Fields[" + 5 + "].ConditionOptions", "No");

            _formManager.UpdateTemplate(templateViewModel, fieldCollection, fields);

            templateViewModel = _formManager.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(templateViewModel.Fields);
            Assert.AreEqual(5, templateViewModel.Fields.Count);

            templateViewModel.Entries = _formManager.HasSubmissions(templateViewModel).ToList();
            Assert.AreEqual(0, templateViewModel.Entries.Count);

            FormCollection submissionCollection = new FormCollection();
            submissionCollection.Add("SubmitFields[1].RadioButton", "Yes");
            submissionCollection.Add("SubmitFields[2].RadioButton", "Yes");
            submissionCollection.Add("SubmitFields[3].TextBox", "i not supposed to be saved 1");
            submissionCollection.Add("SubmitFields[4].TextBox", "Testing 123");
            submissionCollection.Add("SubmitFields[5].TextBox", "i not supposed to be saved 2");

            IDictionary<string, string> submissionFields = new System.Collections.Generic.Dictionary<string, string>();
            submissionFields.Add("1", "1");
            submissionFields.Add("2", "3");
            submissionFields.Add("3", "3");
            submissionFields.Add("4", "4");
            submissionFields.Add("5", "5");

            string result = _target.FillIn(submissionFields, templateViewModel, submissionCollection);
            Assert.AreEqual("success", result);

            templateViewModel.Entries = _formManager.HasSubmissions(templateViewModel).ToList();

            Assert.AreEqual(0, _unitOfWork.PreRegistrations.GetAll().Count());

            Assert.AreEqual(3, templateViewModel.Entries.Count);

            Assert.AreEqual("Yes", templateViewModel.Entries[0].Value);
            Assert.AreEqual("Yes", templateViewModel.Entries[1].Value);
            Assert.AreEqual("Testing 123", templateViewModel.Entries[2].Value);
        }

        [TestMethod()]
        public void FillIn_SuccessForPreRegistrationForm()
        {
            FormViewModel formViewModel = new FormViewModel()
            {
                IsPublic = true,
                PublicFormType = Constants.Public_Form_Type_PreRegistration,
                Slug = "prereg",
                Title = "PreReg"
            };

            Assert.AreEqual(0, _unitOfWork.PreRegistrations.GetAll().Count());

            Template template = _formManager.CreateNewFormAndTemplate(formViewModel);
            Assert.IsNotNull(template);

            TemplateViewModel templateViewModel = _formManager.FindTemplateToEdit(template.TemplateID);

            FormCollection fieldCollection = new FormCollection();
            IDictionary<string, string> fields;
            CeateFieldForm(1, Constants.TemplateFieldType.TEXTBOX.ToString(), fieldCollection, out fields);

            fieldCollection.Add("Fields[1].PreRegistrationFieldName", Constants.PreRegistration_Field_Name_FullName);

            _formManager.UpdateTemplate(templateViewModel, fieldCollection, fields);

            templateViewModel = _formManager.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(templateViewModel.Fields);
            Assert.AreEqual(1, templateViewModel.Fields.Count);

            templateViewModel.Entries = _formManager.HasSubmissions(templateViewModel).ToList();
            Assert.AreEqual(0, templateViewModel.Entries.Count);

            FormCollection submissionCollection = new FormCollection();
            submissionCollection.Add("SubmitFields[1].TextBox", "HelloTest");

            IDictionary<string, string> submissionFields = new System.Collections.Generic.Dictionary<string, string>();
            submissionFields.Add("1", "1");

            string result = _target.FillIn(submissionFields, templateViewModel, submissionCollection);
            Assert.AreEqual(result, "success");

            templateViewModel.Entries = _formManager.HasSubmissions(templateViewModel).ToList();

            Assert.AreEqual(1, templateViewModel.Entries.Count);

            IEnumerable<PreRegistration> preRegistrations = _unitOfWork.PreRegistrations.GetAll();
            Assert.AreEqual(1, preRegistrations.Count());
            var preRegistrationResult = preRegistrations.FirstOrDefault();
            Assert.AreEqual("HelloTest", preRegistrationResult.FullName);
        }

        [TestMethod()]
        public void GetReferenceRange_MinValueSuccess()
        {
            StandardReference standardReference = new StandardReference()
            {
                Title = "Test"
            };

            ReferenceRange referenceRangeOne = new ReferenceRange()
            {
                Title = "Underweight",
                MinimumValue = 0,
                MaximumValue = 18.4,
                Result = "NORMAL"
            };

            _unitOfWork.StandardReferences.Add(standardReference);

            _unitOfWork.Complete();

            standardReference.ReferenceRanges.Add(referenceRangeOne);

            _unitOfWork.Complete();

            string message = string.Empty;
            ReferenceRange result = _target.GetReferenceRange(1, 0, out message);
            Assert.IsNotNull(result);
            Assert.AreEqual("Underweight", result.Title);
        }

        [TestMethod()]
        public void GetReferenceRange_MaximumValueSuccess()
        {
            StandardReference standardReference = new StandardReference()
            {
                Title = "Test"
            };

            ReferenceRange referenceRangeOne = new ReferenceRange()
            {
                Title = "Underweight",
                MinimumValue = 0,
                MaximumValue = 18.4,
                Result = "NORMAL"
            };

            ReferenceRange referenceRangetwo = new ReferenceRange()
            {
                Title = "Normal",
                MinimumValue = 18.5,
                MaximumValue = 24.9,
                Result = "NORMAL"
            };

            _unitOfWork.StandardReferences.Add(standardReference);

            _unitOfWork.Complete();

            standardReference.ReferenceRanges.Add(referenceRangeOne);
            standardReference.ReferenceRanges.Add(referenceRangetwo);

            _unitOfWork.Complete();

            string message = string.Empty;
            ReferenceRange result = _target.GetReferenceRange(1, 18.4, out message);
            Assert.IsNotNull(result);
            Assert.AreEqual("Underweight", result.Title);
        }

        [TestMethod()]
        public void GetReferenceRange_NoMatch()
        {
            StandardReference standardReference = new StandardReference()
            {
                Title = "Test"
            };

            ReferenceRange referenceRangeOne = new ReferenceRange()
            {
                Title = "Underweight",
                MinimumValue = 0,
                MaximumValue = 18.4,
                Result = "NORMAL"
            };

            ReferenceRange referenceRangetwo = new ReferenceRange()
            {
                Title = "Normal",
                MinimumValue = 18.5,
                MaximumValue = 24.9,
                Result = "NORMAL"
            };

            _unitOfWork.StandardReferences.Add(standardReference);

            _unitOfWork.Complete();

            standardReference.ReferenceRanges.Add(referenceRangeOne);
            standardReference.ReferenceRanges.Add(referenceRangetwo);

            _unitOfWork.Complete();

            string message = string.Empty;
            ReferenceRange result = _target.GetReferenceRange(1, 25.0, out message);
            Assert.IsNull(result);
            Assert.AreEqual("Unable to find reference range", message);
        }

        [TestInitialize]
        public void SetupTest()
        {
            var connection = DbConnectionFactory.CreateTransient();
            _context = new PHSContext(connection);
            _unitOfWork = new MockUnitOfWork(_context);

            _formManager = new MockFormManager(_unitOfWork);
            _target = new MockPublicFormManager(_unitOfWork);
        }

        [TestCleanup]
        public void CleanupTest()
        {
            // dispose of the database and connection
            _context.Dispose();
            _unitOfWork.Dispose();
            _formManager.Dispose();
            _target.Dispose();

            _unitOfWork = null;
            _context = null;
            _formManager = null;
            _target = null;
        }
        private void CreateDefaultTemplateAndField(FormViewModel formViewModel, out Template template, out TemplateViewModel templateViewModel)
        {

            template = _formManager.CreateNewFormAndTemplate(formViewModel);
            Assert.IsNotNull(template);

            templateViewModel = _formManager.FindTemplateToEdit(template.TemplateID);

            FormCollection fieldCollection = new FormCollection();
            IDictionary<string, string> fields;
            CeateFieldForm(1, Constants.TemplateFieldType.TEXTBOX.ToString(), fieldCollection, out fields);

            _formManager.UpdateTemplate(templateViewModel, fieldCollection, fields);
        }

        private void CreateTemplateAndField(FormViewModel formViewModel, Constants.TemplateFieldType fieldType, out Template template, out TemplateViewModel templateViewModel)
        {

            template = _formManager.CreateNewFormAndTemplate(formViewModel);
            Assert.IsNotNull(template);

            templateViewModel = _formManager.FindTemplateToEdit(template.TemplateID);

            FormCollection fieldCollection = new FormCollection();
            IDictionary<string, string> fields;
            CeateFieldForm(1, fieldType.ToString(), fieldCollection, out fields);

            _formManager.UpdateTemplate(templateViewModel, fieldCollection, fields);
        }

        private FormCollection CeateFieldForm(int id, string fieldType, FormCollection fieldCollection, out IDictionary<string, string> fields)
        {
            fieldCollection.Add("Fields[" + id + "].FieldType", fieldType);
            fieldCollection.Add("Fields[" + id + "].MaxCharacters", "200");
            fieldCollection.Add("Fields[" + id + "].IsRequired", "false");
            fieldCollection.Add("Fields[" + id + "].AddOthersOption", "false");
            fieldCollection.Add("Fields[" + id + "].MinimumAge", "18");
            fieldCollection.Add("Fields[" + id + "].MaximumAge", "100");
            fieldCollection.Add("Fields[" + id + "].Text", "");
            fieldCollection.Add("Fields[" + id + "].Label", "Click to edit");
            fieldCollection.Add("Fields[" + id + "].HoverText", "");
            fieldCollection.Add("Fields[" + id + "].SubLabel", "");
            fieldCollection.Add("Fields[" + id + "].HelpText", "");
            fieldCollection.Add("Fields[" + id + "].Hint", "");
            fieldCollection.Add("Fields[" + id + "].Order", "" + id);

            fields = new System.Collections.Generic.Dictionary<string, string>();
            fields.Add("" + id, "" + id);

            return fieldCollection;
        }

        private class MockPublicFormManager : FormAccessManager
        {
            private IUnitOfWork _unitOfWork;

            public MockPublicFormManager(IUnitOfWork unitOfWork) : base(null)
            {
                _unitOfWork = unitOfWork;
            }

            protected override IUnitOfWork CreateUnitOfWork()
            {
                return _unitOfWork;
            }
        }

        private class MockFormManager : FormManager
        {
            private IUnitOfWork _unitOfWork;

            public MockFormManager(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            protected override IUnitOfWork CreateUnitOfWork()
            {
                return _unitOfWork;
            }
        }
    }
}
