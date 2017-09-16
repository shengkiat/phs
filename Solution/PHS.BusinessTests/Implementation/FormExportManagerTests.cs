﻿using Effort;
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
        public void CreateFormEntriesDataTableTest_BirthdayPicker()
        {
            Template template;
            TemplateViewModel templateViewModel;
            CreateTemplateAndField(new FormViewModel(), Constants.TemplateFieldType.BIRTHDAYPICKER, "enter your birthday", out template, out templateViewModel);

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

            Assert.AreEqual(2, result.Columns.Count);
            DataColumn column = result.Columns[0];
            Assert.AreEqual("enter your birthday", column.ColumnName);

            Assert.AreEqual(1, result.Rows.Count);
            DataRow row = result.Rows[0];
            Assert.AreEqual("18 Jul 2017", row["enter your birthday"]);
        }

        [TestMethod()]
        public void CreateFormEntriesDataTableTest_Bmi()
        {
            Template template;
            TemplateViewModel templateViewModel;
            CreateTemplateAndField(new FormViewModel(), Constants.TemplateFieldType.BMI, "bmi entered please", out template, out templateViewModel);

            templateViewModel = _formManager.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(templateViewModel.Fields);
            Assert.AreEqual(1, templateViewModel.Fields.Count);

            templateViewModel.Entries = _formManager.HasSubmissions(templateViewModel).ToList();
            Assert.AreEqual(0, templateViewModel.Entries.Count);

            FormCollection submissionCollection = new FormCollection();
            submissionCollection.Add("SubmitFields[1].Weight", "83");
            submissionCollection.Add("SubmitFields[1].Height", "170");

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

            Assert.AreEqual(4, result.Columns.Count);
            Assert.AreEqual("Weight", result.Columns[0].ColumnName);
            Assert.AreEqual("Height", result.Columns[1].ColumnName);
            Assert.AreEqual("BMI", result.Columns[2].ColumnName);

            Assert.AreEqual(1, result.Rows.Count);
            Assert.AreEqual("83", result.Rows[0]["Weight"]);
            Assert.AreEqual("170", result.Rows[0]["Height"]);
            Assert.AreEqual("28.72", result.Rows[0]["BMI"]);
        }

        [TestMethod()]
        public void CreateFormEntriesDataTableTest_RemoveWYSIWYG()
        {
            Template template;
            TemplateViewModel templateViewModel;
            CreateTemplateAndField(new FormViewModel(), Constants.TemplateFieldType.TEXTBOX, "<p>hello</p><p>this is for <b>testing </b>mah</p>", out template, out templateViewModel);

            templateViewModel = _formManager.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(templateViewModel.Fields);
            Assert.AreEqual(1, templateViewModel.Fields.Count);

            templateViewModel.Entries = _formManager.HasSubmissions(templateViewModel).ToList();
            Assert.AreEqual(0, templateViewModel.Entries.Count);

            fillin(templateViewModel, "SubmitFields[1].TextBox", "HelloTest");

            FormExportViewModel model = new FormExportViewModel()
            {
                FormID = 1
            };

            DataTable result = _target.CreateFormEntriesDataTable(model);
            Assert.IsNotNull(result);

            Assert.AreEqual(2, result.Columns.Count);
            DataColumn column = result.Columns[0];
            Assert.AreEqual("hellothis is for testing mah", column.ColumnName);

            Assert.AreEqual(1, result.Rows.Count);
            DataRow row = result.Rows[0];
            Assert.AreEqual("HelloTest", row["hellothis is for testing mah"]);
        }

        [TestMethod()]
        public void CreateFormEntriesDataTableTest_Sorting()
        {
            Template template;
            TemplateViewModel templateViewModel;
            CreateTemplateAndField(new FormViewModel(), Constants.TemplateFieldType.TEXTBOX, "this is for testing", out template, out templateViewModel);

            templateViewModel = _formManager.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(templateViewModel.Fields);
            Assert.AreEqual(1, templateViewModel.Fields.Count);

            templateViewModel.Entries = _formManager.HasSubmissions(templateViewModel).ToList();
            Assert.AreEqual(0, templateViewModel.Entries.Count);

            fillin(templateViewModel, "SubmitFields[1].TextBox", "ABC HelloTest");
            fillin(templateViewModel, "SubmitFields[1].TextBox", "ZXY HelloTest");
            fillin(templateViewModel, "SubmitFields[1].TextBox", "HHH HelloTest");

            SortFieldViewModel sortFieldViewModel = new SortFieldViewModel()
            {
                FieldLabel = "this is for testing",
                SortOrder = "DESC"
            };

            var SortFieldViewModels = new List<SortFieldViewModel>();
            SortFieldViewModels.Add(sortFieldViewModel);

            FormExportViewModel model = new FormExportViewModel()
            {
                FormID = 1,
                SortFields = SortFieldViewModels
            };

            DataTable result = _target.CreateFormEntriesDataTable(model);
            Assert.IsNotNull(result);

            Assert.AreEqual(2, result.Columns.Count);
            DataColumn column = result.Columns[0];
            Assert.AreEqual("this is for testing", column.ColumnName);

            Assert.AreEqual(3, result.Rows.Count);
            DataRow row = result.Rows[0];
            Assert.AreEqual("ZXY HelloTest", row["this is for testing"]);
        }

        [TestMethod()]
        public void CreateFormEntriesDataTableTest_Filtering()
        {
            Template template;
            TemplateViewModel templateViewModel;
            CreateTemplateAndField(new FormViewModel(), Constants.TemplateFieldType.TEXTBOX, "this is for testing", out template, out templateViewModel);

            templateViewModel = _formManager.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(templateViewModel.Fields);
            Assert.AreEqual(1, templateViewModel.Fields.Count);

            templateViewModel.Entries = _formManager.HasSubmissions(templateViewModel).ToList();
            Assert.AreEqual(0, templateViewModel.Entries.Count);

            fillin(templateViewModel, "SubmitFields[1].TextBox", "ABC HelloTest");
            fillin(templateViewModel, "SubmitFields[1].TextBox", "ZXY HelloTest");
            fillin(templateViewModel, "SubmitFields[1].TextBox", "HHH HelloTest");

            Dictionary<string, string> criteriaValue = new Dictionary<string, string>();
            criteriaValue.Add("this is for testing", "ABC");

            CriteriaFieldViewModel criteriaFieldViewModel = new CriteriaFieldViewModel()
            {
                FieldLabel = "this is for testing",
                CriteriaLogic = "contains",
                CriteriaValue = criteriaValue
            };

            var criteriaFields = new List<CriteriaFieldViewModel>();
            criteriaFields.Add(criteriaFieldViewModel);

            FormExportViewModel model = new FormExportViewModel()
            {
                FormID = 1,
                CriteriaFields = criteriaFields
            };

            DataTable result = _target.CreateFormEntriesDataTable(model);
            Assert.IsNotNull(result);

            Assert.AreEqual(2, result.Columns.Count);
            DataColumn column = result.Columns[0];
            Assert.AreEqual("this is for testing", column.ColumnName);

            Assert.AreEqual(1, result.Rows.Count);
            DataRow row = result.Rows[0];
            Assert.AreEqual("ABC HelloTest", row["this is for testing"]);
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
            CeateFieldForm(1, Constants.TemplateFieldType.TEXTBOX.ToString(), "Click to edit test", out fieldCollection, out fields);

            _formManager.UpdateTemplate(templateViewModel, fieldCollection, fields);
        }

        private void CreateTemplateAndField(FormViewModel formViewModel, Constants.TemplateFieldType fieldType, string label, out Template template, out TemplateViewModel templateViewModel)
        {

            template = _formManager.CreateNewFormAndTemplate(formViewModel);
            Assert.IsNotNull(template);

            templateViewModel = _formManager.FindTemplateToEdit(template.TemplateID);

            FormCollection fieldCollection;
            IDictionary<string, string> fields;
            CeateFieldForm(1, fieldType.ToString(), label, out fieldCollection, out fields);

            _formManager.UpdateTemplate(templateViewModel, fieldCollection, fields);
        }

        private FormCollection CeateFieldForm(int id, string fieldType, string label, out FormCollection fieldCollection, out IDictionary<string, string> fields)
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
            fieldCollection.Add("Fields[1].Label", label);
            fieldCollection.Add("Fields[1].HoverText", "");
            fieldCollection.Add("Fields[1].SubLabel", "");
            fieldCollection.Add("Fields[1].HelpText", "");
            fieldCollection.Add("Fields[1].Hint", "");

            fields = new System.Collections.Generic.Dictionary<string, string>();
            fields.Add("1", "1");

            return fieldCollection;
        }

        private void fillin(TemplateViewModel templateViewModel, string inputText, string value)
        {

            FormCollection submissionCollection = new FormCollection();
            submissionCollection.Add(inputText, value);

            IDictionary<string, string> submissionFields = new System.Collections.Generic.Dictionary<string, string>();
            submissionFields.Add("1", "1");

            string fillinResult = _formAccessManager.FillIn(submissionFields, templateViewModel, submissionCollection);
            Assert.AreEqual(fillinResult, "success");
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