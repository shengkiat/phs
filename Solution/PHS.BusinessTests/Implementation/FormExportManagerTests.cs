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
        public void AddNewSortEntries_Versioning()
        {
            Template template;
            TemplateViewModel templateViewModel;
            CreateTemplateAndField(new FormViewModel(), Constants.TemplateFieldType.TEXTBOX, "this is for testing", out template, out templateViewModel);

            templateViewModel = _formManager.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(templateViewModel.Fields);
            Assert.AreEqual(1, templateViewModel.Fields.Count);

            templateViewModel.Entries = _formManager.HasSubmissions(templateViewModel).ToList();
            Assert.AreEqual(0, templateViewModel.Entries.Count);

            fillin("1", templateViewModel, "SubmitFields[1].TextBox", "ABC HelloTest");

            UpdateByAddingTemplateField(template, 2, Constants.TemplateFieldType.TEXTBOX, "this is for another testing", out templateViewModel);

            SortFieldViewModel result = _target.AddNewSortEntries(1);
            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.SortFields.Count);
            Assert.AreEqual("this is for testing", result.SortFields[0].Text);
            Assert.AreEqual("1: this is for testing", result.SortFields[1].Text);
            Assert.AreEqual("this is for another testing", result.SortFields[2].Text);
        }

        [TestMethod()]
        public void AddNewCriteriaEntries_Versioning()
        {
            Template template;
            TemplateViewModel templateViewModel;
            CreateTemplateAndField(new FormViewModel(), Constants.TemplateFieldType.TEXTBOX, "this is for testing", out template, out templateViewModel);

            templateViewModel = _formManager.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(templateViewModel.Fields);
            Assert.AreEqual(1, templateViewModel.Fields.Count);

            templateViewModel.Entries = _formManager.HasSubmissions(templateViewModel).ToList();
            Assert.AreEqual(0, templateViewModel.Entries.Count);

            fillin("1", templateViewModel, "SubmitFields[1].TextBox", "ABC HelloTest");

            UpdateByAddingTemplateField(template, 2, Constants.TemplateFieldType.TEXTBOX, "this is for another testing", out templateViewModel);

            CriteriaFieldViewModel result = _target.AddNewCriteriaEntries(1);
            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.FieldLabels.Count);
            Assert.AreEqual("this is for testing", result.FieldLabels[0].Text);
            Assert.AreEqual("1: this is for testing", result.FieldLabels[1].Text);
            Assert.AreEqual("this is for another testing", result.FieldLabels[2].Text);
        }

        [TestMethod()]
        public void CreateFormEntriesDataTableTest_Address()
        {
            Template template;
            TemplateViewModel templateViewModel;
            CreateTemplateAndField(new FormViewModel(), Constants.TemplateFieldType.ADDRESS, "Address", out template, out templateViewModel);

            templateViewModel = _formManager.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(templateViewModel.Fields);
            Assert.AreEqual(1, templateViewModel.Fields.Count);

            templateViewModel.Entries = _formManager.HasSubmissions(templateViewModel).ToList();
            Assert.AreEqual(0, templateViewModel.Entries.Count);

            FormCollection submissionCollection = new FormCollection();
            submissionCollection.Add("SubmitFields[1].Blk", "777");
            submissionCollection.Add("SubmitFields[1].Unit", "04-55");
            submissionCollection.Add("SubmitFields[1].StreetAddress", "NUS ISS");
            submissionCollection.Add("SubmitFields[1].ZipCode", "123456");

            IDictionary<string, string> submissionFields = new System.Collections.Generic.Dictionary<string, string>();
            submissionFields.Add("1", "1");

            string fillinResult = _formAccessManager.FillIn(submissionFields, templateViewModel, submissionCollection);
            Assert.AreEqual(fillinResult, "success");

            FormExportViewModel model = new FormExportViewModel()
            {
                FormID = 1
            };

            var result = _target.CreateFormEntriesDataTable(model).ValuesDataTable;
            Assert.IsNotNull(result);

            Assert.AreEqual(6, result.Columns.Count);
            Assert.AreEqual("Nric", result.Columns[0].ColumnName);
            Assert.AreEqual("Blk", result.Columns[1].ColumnName);
            Assert.AreEqual("Unit", result.Columns[2].ColumnName);
            Assert.AreEqual("Street Address", result.Columns[3].ColumnName);
            Assert.AreEqual("ZipCode", result.Columns[4].ColumnName);

            Assert.AreEqual(1, result.Rows.Count);
            Assert.AreEqual("", result.Rows[0]["Nric"]);
            Assert.AreEqual("777", result.Rows[0]["Blk"]);
            Assert.AreEqual("04-55", result.Rows[0]["Unit"]);
            Assert.AreEqual("NUS ISS", result.Rows[0]["Street Address"]);
            Assert.AreEqual("123456", result.Rows[0]["ZipCode"]);
        }

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

            DataTable result = _target.CreateFormEntriesDataTable(model).ValuesDataTable;
            Assert.IsNotNull(result);

            Assert.AreEqual(3, result.Columns.Count);
            Assert.AreEqual("Nric", result.Columns[0].ColumnName);
            DataColumn column = result.Columns[1];
            Assert.AreEqual("enter your birthday", column.ColumnName);

            Assert.AreEqual(1, result.Rows.Count);
            DataRow row = result.Rows[0];
            Assert.AreEqual("", row["Nric"]);
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

            DataTable result = _target.CreateFormEntriesDataTable(model).ValuesDataTable;
            Assert.IsNotNull(result);

            Assert.AreEqual(5, result.Columns.Count);
            Assert.AreEqual("Nric", result.Columns[0].ColumnName);
            Assert.AreEqual("Weight", result.Columns[1].ColumnName);
            Assert.AreEqual("Height", result.Columns[2].ColumnName);
            Assert.AreEqual("BMI", result.Columns[3].ColumnName);

            Assert.AreEqual(1, result.Rows.Count);
            Assert.AreEqual("", result.Rows[0]["Nric"]);
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

            fillin("1", templateViewModel, "SubmitFields[1].TextBox", "HelloTest");

            FormExportViewModel model = new FormExportViewModel()
            {
                FormID = 1
            };

            DataTable result = _target.CreateFormEntriesDataTable(model).ValuesDataTable;
            Assert.IsNotNull(result);

            Assert.AreEqual(3, result.Columns.Count);
            DataColumn column = result.Columns[1];
            Assert.AreEqual("hellothis is for testing mah", column.ColumnName);

            Assert.AreEqual(1, result.Rows.Count);
            DataRow row = result.Rows[0];
            Assert.AreEqual("HelloTest", row["hellothis is for testing mah"]);
        }

        [TestMethod()]
        public void CreateFormEntriesDataTableTest_PhlebotomyForm()
        {
            Template template;
            TemplateViewModel templateViewModel;
            FormViewModel formViewModel = new FormViewModel()
            {
                InternalFormType = Constants.Internal_Form_Type_Phlebotomy
            };

            CreateTemplateAndField(formViewModel, Constants.TemplateFieldType.TEXTBOX, "Textbox", out template, out templateViewModel);

            templateViewModel = _formManager.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(templateViewModel.Fields);
            Assert.AreEqual(1, templateViewModel.Fields.Count);

            templateViewModel.Entries = _formManager.HasSubmissions(templateViewModel).ToList();
            Assert.AreEqual(0, templateViewModel.Entries.Count);

            FormCollection submissionCollection = new FormCollection();
            submissionCollection.Add("SubmitFields[1].TextBox", "Not important");

            IDictionary<string, string> submissionFields = new System.Collections.Generic.Dictionary<string, string>();
            submissionFields.Add("1", "1");

            string fillinResult = _formAccessManager.FillIn(submissionFields, templateViewModel, submissionCollection);
            Assert.AreEqual(fillinResult, "success");

            templateViewModel.Entries = _formManager.HasSubmissions(templateViewModel).ToList();
            Assert.AreEqual(1, templateViewModel.Entries.Count);

            var entryId = templateViewModel.Entries.FirstOrDefault().EntryId;
            Assert.AreNotEqual(Guid.Empty, entryId);

            PHSEvent phsEvent = new PHSEvent()
            {
                Title = "Test 15",
                Venue = "Test",
                StartDT = DateTime.Now.AddDays(-200),
                EndDT = DateTime.Now.AddDays(199),
                IsActive = false
            };

            Modality modality = new Modality()
            {
                Name = "Test Modality"
            };

            Participant participant = new Participant()
            {
                Nric = "S8250369B",
                DateOfBirth = new DateTime(1988, 11, 22),
                Gender = "Male",
                FullName = "Tester 123"
            };

            _unitOfWork.Participants.Add(participant);

            _unitOfWork.Events.Add(phsEvent);

            participant.PHSEvents.Add(phsEvent);

            _unitOfWork.Participants.Add(participant);

            phsEvent.Modalities.Add(modality);

            _unitOfWork.Complete();

            ParticipantJourneyModality journeyModality = new ParticipantJourneyModality()
            {
                ParticipantID = 1,
                PHSEventID = 1,
                ModalityID = 1,
                FormID = 1,
                TemplateID = 1,
                EntryId = new Guid(entryId)
            };

            _unitOfWork.ParticipantJourneyModalities.Add(journeyModality);

            _unitOfWork.Complete();

            FormExportViewModel model = new FormExportViewModel()
            {
                FormID = 1
            };

            var result = _target.CreateFormEntriesDataTable(model).ValuesDataTable;
            Assert.IsNotNull(result);

            Assert.AreEqual(5, result.Columns.Count);
            Assert.AreEqual("Nric", result.Columns[0].ColumnName);
            Assert.AreEqual("Name", result.Columns[1].ColumnName);
            Assert.AreEqual("DOB", result.Columns[2].ColumnName);
            Assert.AreEqual("Sex", result.Columns[3].ColumnName);

            Assert.AreEqual(1, result.Rows.Count);
            Assert.AreEqual("S8250369B", result.Rows[0]["Nric"]);
            Assert.AreEqual("Tester 123", result.Rows[0]["Name"]);
            Assert.AreEqual("22 Nov 1988", result.Rows[0]["DOB"]);
            Assert.AreEqual("Male", result.Rows[0]["Sex"]);
        }

        [TestMethod()]
        public void CreateFormEntriesDataTableTest_SimpleFormWithParticipantJourney()
        {
            Template template;
            TemplateViewModel templateViewModel;
            FormViewModel formViewModel = new FormViewModel();

            CreateTemplateAndField(formViewModel, Constants.TemplateFieldType.TEXTBOX, "Textbox 12345", out template, out templateViewModel);

            templateViewModel = _formManager.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(templateViewModel.Fields);
            Assert.AreEqual(1, templateViewModel.Fields.Count);

            templateViewModel.Entries = _formManager.HasSubmissions(templateViewModel).ToList();
            Assert.AreEqual(0, templateViewModel.Entries.Count);

            FormCollection submissionCollection = new FormCollection();
            submissionCollection.Add("SubmitFields[1].TextBox", "Very important");

            IDictionary<string, string> submissionFields = new System.Collections.Generic.Dictionary<string, string>();
            submissionFields.Add("1", "1");

            string fillinResult = _formAccessManager.FillIn(submissionFields, templateViewModel, submissionCollection);
            Assert.AreEqual(fillinResult, "success");

            templateViewModel.Entries = _formManager.HasSubmissions(templateViewModel).ToList();
            Assert.AreEqual(1, templateViewModel.Entries.Count);

            var entryId = templateViewModel.Entries.FirstOrDefault().EntryId;
            Assert.AreNotEqual(Guid.Empty, entryId);

            PHSEvent phsEvent = new PHSEvent()
            {
                Title = "Test 15",
                Venue = "Test",
                StartDT = DateTime.Now.AddDays(-200),
                EndDT = DateTime.Now.AddDays(199),
                IsActive = false
            };

            Modality modality = new Modality()
            {
                Name = "Test Modality"
            };

            Participant participant = new Participant()
            {
                Nric = "S8250369B",
                DateOfBirth = new DateTime(1988, 11, 22),
                Gender = "Male",
                FullName = "Tester 123"
            };

            _unitOfWork.Participants.Add(participant);

            _unitOfWork.Events.Add(phsEvent);

            participant.PHSEvents.Add(phsEvent);

            _unitOfWork.Participants.Add(participant);

            phsEvent.Modalities.Add(modality);

            _unitOfWork.Complete();

            ParticipantJourneyModality journeyModality = new ParticipantJourneyModality()
            {
                ParticipantID = 1,
                PHSEventID = 1,
                ModalityID = 1,
                FormID = 1,
                TemplateID = 1,
                EntryId = new Guid(entryId)
            };

            _unitOfWork.ParticipantJourneyModalities.Add(journeyModality);

            _unitOfWork.Complete();

            FormExportViewModel model = new FormExportViewModel()
            {
                FormID = 1
            };

            var result = _target.CreateFormEntriesDataTable(model).ValuesDataTable;
            Assert.IsNotNull(result);

            Assert.AreEqual(3, result.Columns.Count);
            Assert.AreEqual("Nric", result.Columns[0].ColumnName);
            Assert.AreEqual("Textbox 12345", result.Columns[1].ColumnName);

            Assert.AreEqual(1, result.Rows.Count);
            Assert.AreEqual("S8250369B", result.Rows[0]["Nric"]);
            Assert.AreEqual("Very important", result.Rows[0]["Textbox 12345"]);
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

            fillin("1", templateViewModel, "SubmitFields[1].TextBox", "ABC HelloTest");
            fillin("1", templateViewModel, "SubmitFields[1].TextBox", "ZXY HelloTest");
            fillin("1", templateViewModel, "SubmitFields[1].TextBox", "HHH HelloTest");

            SortFieldViewModel sortFieldViewModel = new SortFieldViewModel()
            {
                TemplateFieldID = "1",
                SortOrder = "DESC"
            };

            var SortFieldViewModels = new List<SortFieldViewModel>();
            SortFieldViewModels.Add(sortFieldViewModel);

            FormExportViewModel model = new FormExportViewModel()
            {
                FormID = 1,
                SortFields = SortFieldViewModels
            };

            DataTable result = _target.CreateFormEntriesDataTable(model).ValuesDataTable;
            Assert.IsNotNull(result);

            Assert.AreEqual(3, result.Columns.Count);
            DataColumn column = result.Columns[1];
            Assert.AreEqual("this is for testing", column.ColumnName);

            Assert.AreEqual(3, result.Rows.Count);
            DataRow row = result.Rows[0];
            Assert.AreEqual("ZXY HelloTest", row["this is for testing"]);
        }

        [TestMethod()]
        public void CreateFormEntriesDataTableTest_SortingWithSubmittedOn()
        {
            Template template;
            TemplateViewModel templateViewModel;
            CreateTemplateAndField(new FormViewModel(), Constants.TemplateFieldType.TEXTBOX, "this is for testing", out template, out templateViewModel);

            templateViewModel = _formManager.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(templateViewModel.Fields);
            Assert.AreEqual(1, templateViewModel.Fields.Count);

            templateViewModel.Entries = _formManager.HasSubmissions(templateViewModel).ToList();
            Assert.AreEqual(0, templateViewModel.Entries.Count);

            fillin("1", templateViewModel, "SubmitFields[1].TextBox", "ABC HelloTest");
            fillin("1", templateViewModel, "SubmitFields[1].TextBox", "ZXY HelloTest");
            fillin("1", templateViewModel, "SubmitFields[1].TextBox", "HHH HelloTest");

            SortFieldViewModel sortFieldViewModel = new SortFieldViewModel()
            {
                TemplateFieldID = Constants.Export_SubmittedOn,
                SortOrder = "DESC"
            };

            var SortFieldViewModels = new List<SortFieldViewModel>();
            SortFieldViewModels.Add(sortFieldViewModel);

            FormExportViewModel model = new FormExportViewModel()
            {
                FormID = 1,
                SortFields = SortFieldViewModels
            };

            DataTable result = _target.CreateFormEntriesDataTable(model).ValuesDataTable;
            Assert.IsNotNull(result);

            Assert.AreEqual(3, result.Columns.Count);
            DataColumn column = result.Columns[1];
            Assert.AreEqual("this is for testing", column.ColumnName);

            Assert.AreEqual(3, result.Rows.Count);
            Assert.AreEqual("HHH HelloTest", result.Rows[0]["this is for testing"]);
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

            fillin("1", templateViewModel, "SubmitFields[1].TextBox", "ABC HelloTest");
            fillin("1", templateViewModel, "SubmitFields[1].TextBox", "ZXY HelloTest");
            fillin("1", templateViewModel, "SubmitFields[1].TextBox", "HHH HelloTest");

            Dictionary<string, string> criteriaValue = new Dictionary<string, string>();
            criteriaValue.Add("1", "ABC");

            CriteriaFieldViewModel criteriaFieldViewModel = new CriteriaFieldViewModel()
            {
                TemplateFieldID = "1",
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

            DataTable result = _target.CreateFormEntriesDataTable(model).ValuesDataTable;
            Assert.IsNotNull(result);

            Assert.AreEqual(3, result.Columns.Count);
            Assert.AreEqual("Nric", result.Columns[0].ColumnName);

            DataColumn column = result.Columns[1];
            Assert.AreEqual("this is for testing", column.ColumnName);

            Assert.AreEqual(1, result.Rows.Count);
            DataRow row = result.Rows[0];
            Assert.AreEqual("", row["Nric"]);
            Assert.AreEqual("ABC HelloTest", row["this is for testing"]);
        }

        [TestMethod()]
        public void CreateFormEntriesDataTableTest_FilteringWithSubmittedOn()
        {
            Template template;
            TemplateViewModel templateViewModel;
            CreateTemplateAndField(new FormViewModel(), Constants.TemplateFieldType.TEXTBOX, "this is for testing", out template, out templateViewModel);

            templateViewModel = _formManager.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(templateViewModel.Fields);
            Assert.AreEqual(1, templateViewModel.Fields.Count);

            templateViewModel.Entries = _formManager.HasSubmissions(templateViewModel).ToList();
            Assert.AreEqual(0, templateViewModel.Entries.Count);

            fillin("1", templateViewModel, "SubmitFields[1].TextBox", "ABC HelloTest");
            fillin("1", templateViewModel, "SubmitFields[1].TextBox", "ZXY HelloTest");
            fillin("1", templateViewModel, "SubmitFields[1].TextBox", "HHH HelloTest");

            Dictionary<string, string> criteriaValue = new Dictionary<string, string>();
            criteriaValue.Add(Constants.Export_SubmittedOn, "2017-01-24 10:55");

            CriteriaFieldViewModel criteriaFieldViewModel = new CriteriaFieldViewModel()
            {
                TemplateFieldID = Constants.Export_SubmittedOn,
                CriteriaLogic = "gte",
                CriteriaValue = criteriaValue
            };

            var criteriaFields = new List<CriteriaFieldViewModel>();
            criteriaFields.Add(criteriaFieldViewModel);

            FormExportViewModel model = new FormExportViewModel()
            {
                FormID = 1,
                CriteriaFields = criteriaFields
            };

            DataTable result = _target.CreateFormEntriesDataTable(model).ValuesDataTable;
            Assert.IsNotNull(result);

            Assert.AreEqual(3, result.Columns.Count);
            DataColumn column = result.Columns[1];
            Assert.AreEqual("this is for testing", column.ColumnName);

            Assert.AreEqual(3, result.Rows.Count);
        }

        [TestMethod()]
        public void CreateFormEntriesDataTableTest_FilteringWithQuoteValue()
        {
            Template template;
            TemplateViewModel templateViewModel;
            CreateTemplateAndField(new FormViewModel(), Constants.TemplateFieldType.TEXTBOX, "this is for testing", out template, out templateViewModel);

            templateViewModel = _formManager.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(templateViewModel.Fields);
            Assert.AreEqual(1, templateViewModel.Fields.Count);

            templateViewModel.Entries = _formManager.HasSubmissions(templateViewModel).ToList();
            Assert.AreEqual(0, templateViewModel.Entries.Count);

            fillin("1", templateViewModel, "SubmitFields[1].TextBox", "ABC's HelloTest");
            fillin("1", templateViewModel, "SubmitFields[1].TextBox", "ZXY's HelloTest");
            fillin("1", templateViewModel, "SubmitFields[1].TextBox", "HHH's HelloTest");

            Dictionary<string, string> criteriaValue = new Dictionary<string, string>();
            criteriaValue.Add("1", "ABC's");

            CriteriaFieldViewModel criteriaFieldViewModel = new CriteriaFieldViewModel()
            {
                TemplateFieldID = "1",
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

            DataTable result = _target.CreateFormEntriesDataTable(model).ValuesDataTable;
            Assert.IsNotNull(result);

            Assert.AreEqual(3, result.Columns.Count);
            Assert.AreEqual("Nric", result.Columns[0].ColumnName);

            DataColumn column = result.Columns[1];
            Assert.AreEqual("this is for testing", column.ColumnName);

            Assert.AreEqual(1, result.Rows.Count);
            DataRow row = result.Rows[0];
            Assert.AreEqual("", row["Nric"]);
            Assert.AreEqual("ABC's HelloTest", row["this is for testing"]);
        }


        [TestMethod()]
        public void CreateFormEntriesDataTableTest_VersionWithBasicComponent()
        {
            Template template;
            TemplateViewModel templateViewModel;
            CreateTemplateAndField(new FormViewModel(), Constants.TemplateFieldType.TEXTBOX, "this is for testing", out template, out templateViewModel);

            templateViewModel = _formManager.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(templateViewModel.Fields);
            Assert.AreEqual(1, templateViewModel.Fields.Count);

            templateViewModel.Entries = _formManager.HasSubmissions(templateViewModel).ToList();
            Assert.AreEqual(0, templateViewModel.Entries.Count);

            fillin("1", templateViewModel, "SubmitFields[1].TextBox", "ABC HelloTest");

            UpdateByAddingTemplateField(template, 2, Constants.TemplateFieldType.TEXTBOX, "this is for another testing", out templateViewModel);

            FormCollection submissionCollection = new FormCollection();
            submissionCollection.Add("SubmitFields[1].TextBox", "i am second");
            submissionCollection.Add("SubmitFields[2].TextBox", "i am second HelloTest");

            IDictionary<string, string> submissionFields = new System.Collections.Generic.Dictionary<string, string>();
            submissionFields.Add("1", "1");
            submissionFields.Add("2", "2");

            string fillinResult = _formAccessManager.FillIn(submissionFields, templateViewModel, submissionCollection);
            Assert.AreEqual(fillinResult, "success");

            FormExportViewModel model = new FormExportViewModel()
            {
                FormID = 1
            };

            DataTable result = _target.CreateFormEntriesDataTable(model).ValuesDataTable;
            Assert.IsNotNull(result);

            Assert.AreEqual(5, result.Columns.Count);
            Assert.AreEqual("Nric", result.Columns[0].ColumnName);
            Assert.AreEqual("this is for testing", result.Columns[1].ColumnName);
            Assert.AreEqual("1: this is for testing", result.Columns[2].ColumnName);
            Assert.AreEqual("this is for another testing", result.Columns[3].ColumnName);

            Assert.AreEqual(2, result.Rows.Count);
            Assert.AreEqual("", result.Rows[0]["Nric"]);
            Assert.AreEqual("ABC HelloTest", result.Rows[0]["this is for testing"]);

            Assert.AreEqual("", result.Rows[1]["Nric"]);
            Assert.AreEqual("i am second", result.Rows[1]["1: this is for testing"]);
            Assert.AreEqual("i am second HelloTest", result.Rows[1]["this is for another testing"]);
        }

        [TestMethod()]
        public void CreateFormEntriesDataTableTest_VersionWithAdvancedComponent()
        {
            Template template;
            TemplateViewModel templateViewModel;
            CreateTemplateAndField(new FormViewModel(), Constants.TemplateFieldType.BMI, "bmi entered please", out template, out templateViewModel);

            templateViewModel = _formManager.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(templateViewModel.Fields);
            Assert.AreEqual(1, templateViewModel.Fields.Count);

            templateViewModel.Entries = _formManager.HasSubmissions(templateViewModel).ToList();
            Assert.AreEqual(0, templateViewModel.Entries.Count);

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

            UpdateByAddingTemplateField(template, 2, Constants.TemplateFieldType.TEXTBOX, "this is for another testing", out templateViewModel);

            submissionCollection = new FormCollection();
            submissionCollection.Add("SubmitFields[1].Weight", "120");
            submissionCollection.Add("SubmitFields[1].Height", "190");
            submissionCollection.Add("SubmitFields[2].TextBox", "i am second HelloTest");

            submissionFields = new System.Collections.Generic.Dictionary<string, string>();
            submissionFields.Add("1", "1");
            submissionFields.Add("2", "2");

            fillinResult = _formAccessManager.FillIn(submissionFields, templateViewModel, submissionCollection);
            Assert.AreEqual(fillinResult, "success");

            FormExportViewModel model = new FormExportViewModel()
            {
                FormID = 1
            };

            DataTable result = _target.CreateFormEntriesDataTable(model).ValuesDataTable;
            Assert.IsNotNull(result);

            Assert.AreEqual(9, result.Columns.Count);
            Assert.AreEqual("Nric", result.Columns[0].ColumnName);
            Assert.AreEqual("Weight", result.Columns[1].ColumnName);
            Assert.AreEqual("Height", result.Columns[2].ColumnName);
            Assert.AreEqual("BMI", result.Columns[3].ColumnName);
            Assert.AreEqual("3: Weight", result.Columns[4].ColumnName);
            Assert.AreEqual("3: Height", result.Columns[5].ColumnName);
            Assert.AreEqual("3: BMI", result.Columns[6].ColumnName);
            Assert.AreEqual("this is for another testing", result.Columns[7].ColumnName);

            Assert.AreEqual(2, result.Rows.Count);
            Assert.AreEqual("", result.Rows[0]["Nric"]);
            Assert.AreEqual("83", result.Rows[0]["Weight"]);
            Assert.AreEqual("170", result.Rows[0]["Height"]);
            Assert.AreEqual("28.72", result.Rows[0]["BMI"]);

            Assert.AreEqual("", result.Rows[1]["Nric"]);
            Assert.AreEqual("120", result.Rows[1]["3: Weight"]);
            Assert.AreEqual("190", result.Rows[1]["3: Height"]);
            Assert.AreEqual("33.24", result.Rows[1]["3: BMI"]);
            Assert.AreEqual("i am second HelloTest", result.Rows[1]["this is for another testing"]);
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

        private void UpdateByAddingTemplateField(Template template, int id, Constants.TemplateFieldType fieldType, string label, out TemplateViewModel templateViewModel)
        {
            templateViewModel = _formManager.FindTemplateToEdit(template.TemplateID);

            FormCollection fieldCollection;
            IDictionary<string, string> fields;
            CeateFieldForm(id, fieldType.ToString(), label, out fieldCollection, out fields);

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

            fieldCollection.Add("Fields[" + id + "].FieldType", fieldType);
            fieldCollection.Add("Fields[" + id + "].MaxCharacters", "200");
            fieldCollection.Add("Fields[" + id + "].IsRequired", "false");
            fieldCollection.Add("Fields[" + id + "].AddOthersOption", "false");
            fieldCollection.Add("Fields[" + id + "].MinimumAge", "18");
            fieldCollection.Add("Fields[" + id + "].MaximumAge", "100");
            fieldCollection.Add("Fields[" + id + "].Text", "");
            fieldCollection.Add("Fields[" + id + "].Label", label);
            fieldCollection.Add("Fields[" + id + "].HoverText", "");
            fieldCollection.Add("Fields[" + id + "].SubLabel", "");
            fieldCollection.Add("Fields[" + id + "].HelpText", "");
            fieldCollection.Add("Fields[" + id + "].Hint", "");

            fields = new System.Collections.Generic.Dictionary<string, string>();
            fields.Add("" + id, "" + id);

            return fieldCollection;
        }

        private void fillin(string id, TemplateViewModel templateViewModel, string inputText, string value)
        {

            FormCollection submissionCollection = new FormCollection();
            submissionCollection.Add(inputText, value);

            IDictionary<string, string> submissionFields = new System.Collections.Generic.Dictionary<string, string>();
            submissionFields.Add(id, id);

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