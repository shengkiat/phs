using Effort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PHS.Business.Implementation;
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
    public class PublicFormManagerTests
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
            CreateDefaultTemplateAndField(new FormViewModel(), out template, out templateViewModel);

            Template postExecuteResult = _target.FindLatestTemplate(1);
            Assert.IsNotNull(postExecuteResult);
            Assert.AreEqual(1, postExecuteResult.Version);
        }

        [TestMethod()]
        public void FindLatestTemplate_ShouldGetLatestVersion()
        {
            Template template;
            TemplateViewModel templateViewModel;
            CreateDefaultTemplateAndField(new FormViewModel(), out template, out templateViewModel);

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
            CreateDefaultTemplateAndField(new FormViewModel(), out template, out templateViewModel);

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
            CreateDefaultTemplateAndField(new FormViewModel(), out template, out templateViewModel);

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
            FormCollection fieldCollection;
            IDictionary<string, string> fields;
            CeateFieldForm(1, out fieldCollection, out fields);

            _formManager.UpdateTemplate(templateViewModel, fieldCollection, fields);
        }

        private FormCollection CeateFieldForm(int id, out FormCollection fieldCollection, out IDictionary<string, string> fields)
        {
            fieldCollection = new FormCollection();

            //collection.Add("SubmitFields[1].TextBox", "SubmitFields[1].TextBox");
            fieldCollection.Add("Fields[1].FieldType", "TEXTBOX");
            fieldCollection.Add("Fields[1].MaxCharacters", "200");
            fieldCollection.Add("Fields[1].IsRequired", "false");
            fieldCollection.Add("Fields[1].AddOthersOption", "false");
            fieldCollection.Add("Fields[1].MinimumAge", "18");
            fieldCollection.Add("Fields[1].MaximumAge", "100");
            fieldCollection.Add("Fields[1].Text", "");
            fieldCollection.Add("Fields[1].Label", "Click to edit");
            fieldCollection.Add("Fields[1].HoverText", "");
            fieldCollection.Add("Fields[1].SubLabel", "");
            fieldCollection.Add("Fields[1].HelpText", "");
            fieldCollection.Add("Fields[1].Hint", "");

            fields = new System.Collections.Generic.Dictionary<string, string>();
            fields.Add("1", "1");

            return fieldCollection;
        }

        private class MockPublicFormManager : FormAccessManager
        {
            private IUnitOfWork _unitOfWork;

            public MockPublicFormManager(IUnitOfWork unitOfWork)
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
