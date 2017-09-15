using Effort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PHS.Business.Implementation;
using PHS.Business.ViewModel.FormExport;
using PHS.BusinessTests;
using PHS.Common;
using PHS.DB;
using PHS.DB.ViewModels.Form;
using PHS.Repository.Context;
using PHS.Repository.Interface.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PHS.Business.Implementation.Tests
{
    [TestClass()]
    public class FormExportManagerTests
    {
        private FormManager _formManager;
        private FormAccessManager _formAccessManager;
        private FormExportManager _target;
        private IUnitOfWork _unitOfWork;
        private PHSContext _context;

        [TestMethod()]
        public void CreateFormEntriesDataTableTest()
        {
            Template template;
            TemplateViewModel templateViewModel;
            CreateTemplateAndField(new FormViewModel(), Constants.TemplateFieldType.BIRTHDAYPICKER, out template, out templateViewModel);

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

            string fillinResult = _formAccessManager.FillIn(submissionFields, templateViewModel, submissionCollection);
            Assert.AreEqual(fillinResult, "success");

            FormExportViewModel model = new FormExportViewModel()
            {
                FormID = 1
            };



            DataTable result = _target.CreateFormEntriesDataTable(model);
            Assert.IsNotNull(result);
        }

        [TestInitialize]
        public void SetupTest()
        {
            var connection = DbConnectionFactory.CreateTransient();
            _context = new PHSContext(connection);
            _unitOfWork = new MockUnitOfWork(_context);

            _formManager = new MockFormManager(_unitOfWork);
            _formAccessManager = new MockFormAccessManager(_unitOfWork);
            _target = new MockFormExportManager(_unitOfWork);
        }

        [TestCleanup]
        public void CleanupTest()
        {
            // dispose of the database and connection
            _context.Dispose();
            _unitOfWork.Dispose();
            _formManager.Dispose();
            _formAccessManager.Dispose();
            _target.Dispose();

            _unitOfWork = null;
            _context = null;
            _formManager = null;
            _formAccessManager = null;
            _target = null;
        }

        private void CreateDefaultTemplateAndField(FormViewModel formViewModel, out Template template, out TemplateViewModel templateViewModel)
        {

            template = _formManager.CreateNewFormAndTemplate(formViewModel);
            Assert.IsNotNull(template);

            templateViewModel = _formManager.FindTemplateToEdit(template.TemplateID);

            FormCollection fieldCollection;
            IDictionary<string, string> fields;
            CeateFieldForm(1, Constants.TemplateFieldType.TEXTBOX.ToString(), out fieldCollection, out fields);

            _formManager.UpdateTemplate(templateViewModel, fieldCollection, fields);
        }

        private void CreateTemplateAndField(FormViewModel formViewModel, Constants.TemplateFieldType fieldType, out Template template, out TemplateViewModel templateViewModel)
        {

            template = _formManager.CreateNewFormAndTemplate(formViewModel);
            Assert.IsNotNull(template);

            templateViewModel = _formManager.FindTemplateToEdit(template.TemplateID);

            FormCollection fieldCollection;
            IDictionary<string, string> fields;
            CeateFieldForm(1, fieldType.ToString(), out fieldCollection, out fields);

            _formManager.UpdateTemplate(templateViewModel, fieldCollection, fields);
        }

        private FormCollection CeateFieldForm(int id, string fieldType, out FormCollection fieldCollection, out IDictionary<string, string> fields)
        {
            fieldCollection = new FormCollection();

            //collection.Add("SubmitFields[1].TextBox", "SubmitFields[1].TextBox");
            fieldCollection.Add("Fields[1].FieldType", fieldType);
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

        private class MockFormExportManager : FormExportManager
        {
            private IUnitOfWork _unitOfWork;

            public MockFormExportManager(IUnitOfWork unitOfWork)
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

        private class MockFormAccessManager : FormAccessManager
        {
            private IUnitOfWork _unitOfWork;

            public MockFormAccessManager(IUnitOfWork unitOfWork)
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