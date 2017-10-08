using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using PHS.DB;
using PHS.Repository.Context;
using PHS.DB.ViewModels.Form;
using System.Web.Mvc;
using PHS.Common;
using PHS.Repository.Interface.Core;
using PHS.BusinessTests;
using Effort;

namespace PHS.Business.Implementation.Tests
{
    [TestClass()]
    public class FormManagerTests
    {
        private FormAccessManager _publicFormManager;
        private FormManager _target;
        private IUnitOfWork _unitOfWork;
        private PHSContext _context;

        [TestMethod()]
        public void CreateNewFormAndTemplate_CreateSuccess()
        {
            FormViewModel formViewModel = new FormViewModel();
            formViewModel.Title = "Required Form Name";

            Template template = _target.CreateNewFormAndTemplate(formViewModel);
            Assert.IsNotNull(template);
            Assert.IsNotNull(template.TemplateID);
            Assert.IsNotNull(template.Form);
            Assert.IsNotNull(template.Form.FormID);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception),
            "Slug has already being used. Please use another.")]
        public void CreateNewFormAndTemplate_UnableToEditBecauseSlugExists()
        {
            FormViewModel formViewModel = new FormViewModel();
            formViewModel.Slug = "slug1";
            formViewModel.IsPublic = true;
            formViewModel.PublicFormType = "TEST";
            formViewModel.Title = "Required Form Name";

            Template template = _target.CreateNewFormAndTemplate(formViewModel);
            Assert.IsNotNull(template);
            Assert.IsNotNull(template.Form);
            Assert.IsNotNull(template.Form.FormID);

            formViewModel = new FormViewModel();
            formViewModel.Slug = "slug1";
            formViewModel.IsPublic = true;
            formViewModel.PublicFormType = "HELLO";

            _target.CreateNewFormAndTemplate(formViewModel);
            Assert.Fail("Expecting exception");
        }

        [TestMethod()]
        public void EditForm_EditSuccess()
        {
            FormViewModel formViewModel = new FormViewModel();
            formViewModel.Title = "Required Form Name";

            Template template = _target.CreateNewFormAndTemplate(formViewModel);
            Assert.IsNotNull(template);
            Assert.IsNotNull(template.Form);
            Assert.IsNotNull(template.Form.FormID);

            formViewModel.FormID = template.Form.FormID;

            string result = _target.EditForm(formViewModel);
            Assert.AreEqual("success", result);
        }

        [TestMethod()]
        public void EditForm_ReturnError()
        {
            FormViewModel formViewModel = new FormViewModel();
            formViewModel.FormID = -1;

            string result = _target.EditForm(formViewModel);
            Assert.AreEqual("Unable to edit form - invalid id", result);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception),
            "Slug has already being used. Please use another.")]
        public void EditForm_UnableToEditBecauseSlugExists()
        {
            FormViewModel formViewModel = new FormViewModel();
            formViewModel.Title = "Required Form Name";
            formViewModel.Slug = "slug1";
            formViewModel.IsPublic = true;
            formViewModel.PublicFormType = "TEST";

            Template templateOne = _target.CreateNewFormAndTemplate(formViewModel);
            Assert.IsNotNull(templateOne);
            Assert.IsNotNull(templateOne.Form);
            Assert.IsNotNull(templateOne.Form.FormID);

            formViewModel = new FormViewModel();
            formViewModel.Title = "Required Form Name 2";
            formViewModel.Slug = "slug2";
            formViewModel.IsPublic = true;
            formViewModel.PublicFormType = "TEST";

            Template templateTwo = _target.CreateNewFormAndTemplate(formViewModel);
            Assert.IsNotNull(templateTwo);
            Assert.IsNotNull(templateTwo.Form);
            Assert.IsNotNull(templateTwo.Form.FormID);

            formViewModel.FormID = templateTwo.Form.FormID;
            formViewModel.Slug = "slug1";

            _target.EditForm(formViewModel);
            Assert.Fail("Expecting exception");
        }

        [TestMethod()]
        public void FindAllFormsByDes_ShouldHaveRecordAfterCreate()
        {
            List<FormViewModel> preExecuteResult = _target.FindAllFormsByDes();
            Assert.IsNotNull(preExecuteResult);
            Assert.AreEqual(preExecuteResult.Count(), 0);

            FormViewModel formViewModel = new FormViewModel();
            formViewModel.Title = "Required Form Name";

            Template template = _target.CreateNewFormAndTemplate(formViewModel);
            Assert.IsNotNull(template);

            List<FormViewModel> postExecuteResult = _target.FindAllFormsByDes();
            Assert.IsNotNull(postExecuteResult);
            Assert.AreEqual(postExecuteResult.Count(), 1);
        }

        [TestMethod()]
        public void FindAllTemplatesByFormId_ShouldHaveRecordAfterCreate()
        {
            List<FormViewModel> preExecuteResult = _target.FindAllFormsByDes();
            Assert.IsNotNull(preExecuteResult);
            Assert.AreEqual(preExecuteResult.Count(), 0);

            FormViewModel formViewModel = new FormViewModel();
            formViewModel.Title = "Required Form Name";

            Template template = _target.CreateNewFormAndTemplate(formViewModel);
            Assert.IsNotNull(template);

            List<TemplateViewModel> postExecuteResult = _target.FindAllTemplatesByFormId(template.Form.FormID);
            Assert.IsNotNull(postExecuteResult);
            Assert.AreEqual(postExecuteResult.Count(), 1);
        }

        [TestMethod()]
        public void FindTemplate_ShouldHaveRecordAfterCreate()
        {
            FormViewModel formViewModel = new FormViewModel();
            formViewModel.Title = "Required Form Name";

            Template template = _target.CreateNewFormAndTemplate(formViewModel);
            Assert.IsNotNull(template);

            Template postExecuteResult = _target.FindTemplate(template.TemplateID);
            Assert.IsNotNull(postExecuteResult);
        }

        [TestMethod()]
        public void UpdateTemplate_ShouldHaveFieldRecordsAfterSave()
        {
            FormViewModel formViewModel = new FormViewModel();
            formViewModel.Title = "Required Form Name";

            Template template = _target.CreateNewFormAndTemplate(formViewModel);
            Assert.IsNotNull(template);

            TemplateViewModel templateViewModel = _target.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(templateViewModel.Fields);
            Assert.AreEqual(0, templateViewModel.Fields.Count);

            FormCollection fieldCollection;
            IDictionary<string, string> fields;
            CeateFieldForm(1, out fieldCollection, out fields);

            _target.UpdateTemplate(templateViewModel, fieldCollection, fields);

            templateViewModel = _target.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(templateViewModel.Fields);
            Assert.AreEqual(1, templateViewModel.Fields.Count);
        }

        [TestMethod()]
        public void FindTemplateToEdit_ShouldHaveRecordAfterCreate()
        {
            FormViewModel formViewModel = new FormViewModel();
            formViewModel.Title = "Required Form Name";

            Template template = _target.CreateNewFormAndTemplate(formViewModel);
            Assert.IsNotNull(template);

            TemplateViewModel postExecuteResult = _target.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(postExecuteResult);
            Assert.AreEqual(Constants.TemplateMode.EDIT, postExecuteResult.Mode);
        }

        [TestMethod()]
        public void FindTemplateToEdit_CopyToNewTemplate()
        {
            Template template;
            TemplateViewModel templateViewModel;
            CreateDefaultTemplateAndField(out template, out templateViewModel);

            templateViewModel = _target.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(templateViewModel.Fields);
            Assert.AreEqual(1, templateViewModel.Fields.Count);
            Assert.AreEqual(template.TemplateID, templateViewModel.TemplateID);

            templateViewModel.Entries = _target.HasSubmissions(templateViewModel).ToList();
            Assert.AreEqual(0, templateViewModel.Entries.Count);

            FormCollection submissionCollection = new FormCollection();
            submissionCollection.Add("SubmitFields[1].TextBox", "HelloTest");

            IDictionary<string, string> submissionFields = new System.Collections.Generic.Dictionary<string, string>();
            submissionFields.Add("1", "1");

            string result = _publicFormManager.FillIn(submissionFields, templateViewModel, submissionCollection);
            Assert.AreEqual(result, "success");

            TemplateViewModel postExecuteResult = _target.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(postExecuteResult);
            Assert.AreEqual(Constants.TemplateMode.EDIT, postExecuteResult.Mode);
            Assert.AreNotEqual(template.TemplateID, postExecuteResult.TemplateID);
        }

        [TestMethod()]
        public void FindTemplateToEdit_CopyToNewTemplateWithConditionalFields()
        {
            FormViewModel formViewModel = new FormViewModel();
            formViewModel.Title = "Required Form Name";

            Template template = _target.CreateNewFormAndTemplate(formViewModel);
            Assert.IsNotNull(template);

            TemplateViewModel templateViewModel = _target.FindTemplateToEdit(template.TemplateID);
            FormCollection fieldCollection;
            IDictionary<string, string> fields;
            CeateFieldForm(1, out fieldCollection, out fields);

            _target.UpdateTemplate(templateViewModel, fieldCollection, fields);

            CeateFieldForm(2, out fieldCollection, out fields);
            fieldCollection.Set("Fields[" + 2 + "].Label", "Test");
            fieldCollection.Add("Fields[" + 2 + "].ConditionTemplateFieldID", "1");
            fieldCollection.Add("Fields[" + 2 + "].ConditionCriteria", "==");
            fieldCollection.Add("Fields[" + 2 + "].ConditionOptions", "Yes");

            _target.UpdateTemplate(templateViewModel, fieldCollection, fields);

            templateViewModel = _target.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(templateViewModel.Fields);
            Assert.AreEqual(2, templateViewModel.Fields.Count);
            Assert.AreEqual(template.TemplateID, templateViewModel.TemplateID);
            Assert.AreEqual("==", templateViewModel.Fields[1].ConditionCriteria);
            Assert.AreEqual("Yes", templateViewModel.Fields[1].ConditionOptions);
            Assert.AreEqual(1, templateViewModel.Fields[1].ConditionTemplateFieldID);

            templateViewModel.Entries = _target.HasSubmissions(templateViewModel).ToList();
            Assert.AreEqual(0, templateViewModel.Entries.Count);

            FormCollection submissionCollection = new FormCollection();
            submissionCollection.Add("SubmitFields[1].TextBox", "HelloTest");

            IDictionary<string, string> submissionFields = new System.Collections.Generic.Dictionary<string, string>();
            submissionFields.Add("1", "1");

            string result = _publicFormManager.FillIn(submissionFields, templateViewModel, submissionCollection);
            Assert.AreEqual(result, "success");

            TemplateViewModel postExecuteResult = _target.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(postExecuteResult);
            Assert.AreEqual(Constants.TemplateMode.EDIT, postExecuteResult.Mode);
            Assert.AreNotEqual(template.TemplateID, postExecuteResult.TemplateID);

            Assert.IsNotNull(postExecuteResult.Fields);
            Assert.AreEqual(2, postExecuteResult.Fields.Count);
            Assert.AreEqual("==", postExecuteResult.Fields[1].ConditionCriteria);
            Assert.AreEqual("Yes", postExecuteResult.Fields[1].ConditionOptions);
            Assert.AreEqual(3, postExecuteResult.Fields[1].ConditionTemplateFieldID);
        }

        [TestMethod()]
        public void FindTemplateToEdit_ReadOnlyTemplateWhenViewOldTemplate()
        {
            Template template;
            TemplateViewModel templateViewModel;
            CreateDefaultTemplateAndField(out template, out templateViewModel);

            templateViewModel = _target.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(templateViewModel.Fields);
            Assert.AreEqual(1, templateViewModel.Fields.Count);
            Assert.AreEqual(template.TemplateID, templateViewModel.TemplateID);

            templateViewModel.Entries = _target.HasSubmissions(templateViewModel).ToList();
            Assert.AreEqual(0, templateViewModel.Entries.Count);

            FormCollection submissionCollection = new FormCollection();
            submissionCollection.Add("SubmitFields[1].TextBox", "HelloTest");

            IDictionary<string, string> submissionFields = new System.Collections.Generic.Dictionary<string, string>();
            submissionFields.Add("1", "1");

            string result = _publicFormManager.FillIn(submissionFields, templateViewModel, submissionCollection);
            Assert.AreEqual(result, "success");

            _target.FindTemplateToEdit(template.TemplateID); //copyTemplate

            TemplateViewModel postExecuteResult = _target.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(postExecuteResult);
            Assert.AreEqual(Constants.TemplateMode.READONLY, postExecuteResult.Mode);
            Assert.AreEqual(template.TemplateID, postExecuteResult.TemplateID);
        }


        [TestMethod()]
        public void DeleteTemplate_UnableToDeleteBecauseOfOnlyVersion()
        {
            FormViewModel formViewModel = new FormViewModel();
            formViewModel.Title = "Required Form Name";

            Template template = _target.CreateNewFormAndTemplate(formViewModel);
            Assert.IsNotNull(template);
            Assert.IsNotNull(template.Form);

            string deleteResult = _target.DeleteTemplate(template.TemplateID);
            Assert.AreEqual(deleteResult, "Unable to delete template when there is only one remains");

            Template templateResult = _target.FindTemplate(template.TemplateID);
            Assert.IsNotNull(templateResult);
        }

        [TestMethod()]
        public void DeleteTemplate_ReturnError()
        {
            string deleteResult = _target.DeleteTemplate(-1);
            Assert.AreEqual(deleteResult, "Unable to delete template - invalid id");
        }

        [TestMethod()]
        public void DeleteTemplate_DeleteNewVersionAfterCopy()
        {
            Template template;
            TemplateViewModel templateViewModel;
            CreateDefaultTemplateAndField(out template, out templateViewModel);

            templateViewModel = _target.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(templateViewModel.Fields);
            Assert.AreEqual(1, templateViewModel.Fields.Count);
            Assert.AreEqual(template.TemplateID, templateViewModel.TemplateID);

            templateViewModel.Entries = _target.HasSubmissions(templateViewModel).ToList();
            Assert.AreEqual(0, templateViewModel.Entries.Count);

            FormCollection submissionCollection = new FormCollection();
            submissionCollection.Add("SubmitFields[1].TextBox", "HelloTest");

            IDictionary<string, string> submissionFields = new System.Collections.Generic.Dictionary<string, string>();
            submissionFields.Add("1", "1");

            string result = _publicFormManager.FillIn(submissionFields, templateViewModel, submissionCollection);
            Assert.AreEqual(result, "success");

            TemplateViewModel newTemplate = _target.FindTemplateToEdit(template.TemplateID); //copyTemplate

            string deleteResult = _target.DeleteTemplate(newTemplate.TemplateID.Value);
            Assert.AreEqual(deleteResult, "success");
        }

        [TestMethod()]
        public void DeleteTemplate_UnableToDeleteWhenSubmission()
        {
            Template template;
            TemplateViewModel templateViewModel;
            CreateDefaultTemplateAndField(out template, out templateViewModel);

            templateViewModel = _target.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(templateViewModel.Fields);
            Assert.AreEqual(1, templateViewModel.Fields.Count);
            Assert.AreEqual(template.TemplateID, templateViewModel.TemplateID);

            templateViewModel.Entries = _target.HasSubmissions(templateViewModel).ToList();
            Assert.AreEqual(0, templateViewModel.Entries.Count);

            FormCollection submissionCollection = new FormCollection();
            submissionCollection.Add("SubmitFields[1].TextBox", "HelloTest");

            IDictionary<string, string> submissionFields = new System.Collections.Generic.Dictionary<string, string>();
            submissionFields.Add("1", "1");

            string result = _publicFormManager.FillIn(submissionFields, templateViewModel, submissionCollection);
            Assert.AreEqual(result, "success");

            _target.FindTemplateToEdit(template.TemplateID); //copyTemplate

            string deleteResult = _target.DeleteTemplate(templateViewModel.TemplateID.Value);
            Assert.AreEqual(deleteResult, "Unable to delete template - Template must have no entries to be able to be deleted");
        }


        [TestMethod()]
        public void DeleteFormAndTemplate_DeleteSuccess()
        {
            FormViewModel formViewModel = new FormViewModel();
            formViewModel.Title = "Required Form Name";

            Template template = _target.CreateNewFormAndTemplate(formViewModel);
            Assert.IsNotNull(template);
            Assert.IsNotNull(template.Form);

            string deleteResult = _target.DeleteFormAndTemplate(template.Form.FormID);
            Assert.AreEqual(deleteResult, "success");

            FormViewModel formViewResult = _target.FindFormToEdit(template.Form.FormID);
            Assert.IsNull(formViewResult);

            Template templateResult = _target.FindTemplate(template.TemplateID);
            Assert.IsNull(templateResult);
        }

        [TestMethod()]
        public void DeleteFormAndTemplate_ReturnInvalidIdError()
        {
            string deleteResult = _target.DeleteFormAndTemplate(-1);
            Assert.AreEqual(deleteResult, "Unable to delete form - invalid id");
        }

        [TestMethod()]
        public void DeleteFormAndTemplate_UnableToDeleteWhenSubmission()
        {
            Template template;
            TemplateViewModel templateViewModel;
            CreateDefaultTemplateAndField(out template, out templateViewModel);

            templateViewModel = _target.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(templateViewModel.Fields);
            Assert.AreEqual(1, templateViewModel.Fields.Count);
            Assert.AreEqual(template.TemplateID, templateViewModel.TemplateID);

            templateViewModel.Entries = _target.HasSubmissions(templateViewModel).ToList();
            Assert.AreEqual(0, templateViewModel.Entries.Count);

            FormCollection submissionCollection = new FormCollection();
            submissionCollection.Add("SubmitFields[1].TextBox", "HelloTest");

            IDictionary<string, string> submissionFields = new System.Collections.Generic.Dictionary<string, string>();
            submissionFields.Add("1", "1");

            string result = _publicFormManager.FillIn(submissionFields, templateViewModel, submissionCollection);
            Assert.AreEqual(result, "success");

            string deleteResult = _target.DeleteFormAndTemplate(template.FormID);
            Assert.AreEqual(deleteResult, "Unable to delete - Templates must have no entries to be able to be deleted");
        }

        [TestMethod()]
        public void DeleteFormAndTemplate_UnableToDeleteWhenAttachedToModality()
        {
            Template template;
            TemplateViewModel templateViewModel;
            CreateDefaultTemplateAndField(out template, out templateViewModel);

            PHSEvent phsEvent = new PHSEvent()
            {
                Title = "Test",
                Venue = "Test",
                StartDT = DateTime.Now.AddDays(-1),
                EndDT = DateTime.Now.AddDays(1),
                IsActive = true
            };

            Modality modality = new Modality()
            {
                Name = "Test Modality",
                IsMandatory = true
            };

            Form form = _unitOfWork.FormRepository.Find(f=>f.FormID == template.FormID).FirstOrDefault();

            Assert.IsNotNull(form);

            modality.Forms.Add(form);

            phsEvent.Modalities.Add(modality);

            _unitOfWork.Events.Add(phsEvent);

            _unitOfWork.Complete();

            string deleteResult = _target.DeleteFormAndTemplate(template.FormID);
            Assert.AreEqual("Unable to delete - Form is already attached to a modality", deleteResult);
        }

        [TestMethod()]
        public void FindAllFormsByDes_ShouldHaveNoRecordAfterDelete()
        {
            List<FormViewModel> preExecuteResult = _target.FindAllFormsByDes();
            Assert.IsNotNull(preExecuteResult);
            Assert.AreEqual(preExecuteResult.Count(), 0);

            FormViewModel formViewModel = new FormViewModel();
            formViewModel.Title = "Required Form Name";

            Template template = _target.CreateNewFormAndTemplate(formViewModel);
            Assert.IsNotNull(template);

            string deleteResult = _target.DeleteFormAndTemplate(template.Form.FormID);
            Assert.AreEqual(deleteResult, "success");

            List<FormViewModel> postExecuteResult = _target.FindAllFormsByDes();
            Assert.IsNotNull(postExecuteResult);
            Assert.AreEqual(postExecuteResult.Count(), 0);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception),
            "Invalid id")]
        public void FindAllTemplatesByFormId_ThrowExceptionForInvalidId()
        {
            _target.FindAllTemplatesByFormId(-1);
            Assert.Fail("Expecting exception");    
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception),
            "Invalid id")]
        public void FindAllTemplatesByFormId_ThrowExceptionForInvalidIdAfterDelete()
        {
            List<FormViewModel> preExecuteResult = _target.FindAllFormsByDes();
            Assert.IsNotNull(preExecuteResult);
            Assert.AreEqual(preExecuteResult.Count(), 0);

            FormViewModel formViewModel = new FormViewModel();
            formViewModel.Title = "Required Form Name";

            Template template = _target.CreateNewFormAndTemplate(formViewModel);
            Assert.IsNotNull(template);

            string deleteResult = _target.DeleteFormAndTemplate(template.Form.FormID);
            Assert.AreEqual(deleteResult, "success");

            List<TemplateViewModel> postExecuteResult = _target.FindAllTemplatesByFormId(template.Form.FormID);
            Assert.IsNotNull(postExecuteResult);
            Assert.AreEqual(postExecuteResult.Count(), 0);
        }

        [TestMethod()]
        public void FindTemplateField_Success()
        {
            Template template;
            TemplateViewModel templateViewModel;
            CreateDefaultTemplateAndField(out template, out templateViewModel);

            var postExecuteResult = _target.FindTemplateField(1);
            Assert.IsNotNull(postExecuteResult);
        }


        [TestInitialize]
        public void SetupTest()
        {
            var connection = DbConnectionFactory.CreateTransient();
            _context = new PHSContext(connection);
            _unitOfWork = new MockUnitOfWork(_context);

            _target = new MockFormManager(_unitOfWork);
            _publicFormManager = new MockPublicFormManager(_unitOfWork);
        }

        [TestCleanup]
        public void CleanupTest()
        {
            // dispose of the database and connection
            _context.Dispose();
            _unitOfWork.Dispose();
            _publicFormManager.Dispose();
            _target.Dispose();

            _unitOfWork = null;
            _context = null;
            _publicFormManager = null;
            _target = null;
        }

        

        private void CreateDefaultTemplateAndField(out Template template, out TemplateViewModel templateViewModel)
        {
            FormViewModel formViewModel = new FormViewModel();
            formViewModel.Title = "Required Form Name";

            template = _target.CreateNewFormAndTemplate(formViewModel);
            Assert.IsNotNull(template);

            templateViewModel = _target.FindTemplateToEdit(template.TemplateID);
            FormCollection fieldCollection;
            IDictionary<string, string> fields;
            CeateFieldForm(1, out fieldCollection, out fields);

            _target.UpdateTemplate(templateViewModel, fieldCollection, fields);
        }

        private FormCollection CeateFieldForm(int id, out FormCollection fieldCollection, out IDictionary<string, string> fields)
        {
            fieldCollection = new FormCollection();

            //collection.Add("SubmitFields[1].TextBox", "SubmitFields[1].TextBox");
            fieldCollection.Add("Fields[" + id + "].FieldType", "TEXTBOX");
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

            fields = new System.Collections.Generic.Dictionary<string, string>();
            fields.Add("" + id, "" + id);

            return fieldCollection;
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

    }
}