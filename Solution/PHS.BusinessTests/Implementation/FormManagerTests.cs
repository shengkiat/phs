using Microsoft.VisualStudio.TestTools.UnitTesting;
using PHS.Business.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PHS.Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using PHS.DB;
using PHS.Repository;
using PHS.Repository.Context;
using System.IO;
using PHS.DB.ViewModels.Forms;
using PHS.Business.Extensions;
using System.Transactions;
using System.Web.Mvc;
using PHS.Common;
using PHS.BusinessTests.Implementation;
using PHS.Repository.Interface.Core;
using PHS.BusinessTests;
using Effort;

namespace PHS.Business.Implementation.Tests
{
    [TestClass()]
    public class FormManagerTests
    {
        private FormManager _target;
        private IUnitOfWork _unitOfWork;
        private PHSContext _context;

        [TestMethod()]
        public void CreateNewFormAndTemplate_CreateSuccess()
        {
            FormViewModel formViewModel = new FormViewModel();

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
            formViewModel.Slug = "slug1";
            formViewModel.IsPublic = true;
            formViewModel.PublicFormType = "TEST";

            Template templateOne = _target.CreateNewFormAndTemplate(formViewModel);
            Assert.IsNotNull(templateOne);
            Assert.IsNotNull(templateOne.Form);
            Assert.IsNotNull(templateOne.Form.FormID);

            formViewModel = new FormViewModel();
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

            Template template = _target.CreateNewFormAndTemplate(formViewModel);
            Assert.IsNotNull(template);

            Template postExecuteResult = _target.FindTemplate(template.TemplateID);
            Assert.IsNotNull(postExecuteResult);
        }

        [TestMethod()]
        public void UpdateTemplate_ShouldHaveFieldRecordsAfterSave()
        {
            FormViewModel formViewModel = new FormViewModel();

            Template template = _target.CreateNewFormAndTemplate(formViewModel);
            Assert.IsNotNull(template);

            TemplateViewModel templateViewModel = _target.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(templateViewModel.Fields);
            Assert.AreEqual(0, templateViewModel.Fields.Count);

            FormCollection collection = new FormCollection();
            //collection.Add("SubmitFields[1].TextBox", "SubmitFields[1].TextBox");
            collection.Add("Fields[1].FieldType", "TEXTBOX");
            collection.Add("Fields[1].MaxCharacters", "200");
            collection.Add("Fields[1].IsRequired", "false");
            collection.Add("Fields[1].AddOthersOption", "false");
            collection.Add("Fields[1].MinimumAge", "18");
            collection.Add("Fields[1].MaximumAge", "100");
            collection.Add("Fields[1].Text", "");
            collection.Add("Fields[1].Label", "Click to edit");
            collection.Add("Fields[1].HoverText", "");
            collection.Add("Fields[1].SubLabel", "");
            collection.Add("Fields[1].HelpText", "");
            collection.Add("Fields[1].Hint", "");


            IDictionary<string, string> Fields = new System.Collections.Generic.Dictionary<string, string>();
            Fields.Add("1", "1");

            _target.UpdateTemplate(templateViewModel, collection, Fields);

            templateViewModel = _target.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(templateViewModel.Fields);
            Assert.AreEqual(1, templateViewModel.Fields.Count);
        }

         [TestMethod()]
         public void FillIn_ShouldHaveRecordAfterCreate()
         {
            FormViewModel formViewModel = new FormViewModel();

            Template template = _target.CreateNewFormAndTemplate(formViewModel);
            Assert.IsNotNull(template);

            TemplateViewModel templateViewModel = _target.FindTemplateToEdit(template.TemplateID);

            FormCollection fieldCollection = new FormCollection();
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

            IDictionary<string, string> Fields = new System.Collections.Generic.Dictionary<string, string>();
            Fields.Add("1", "1");

            _target.UpdateTemplate(templateViewModel, fieldCollection, Fields);

            templateViewModel = _target.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(templateViewModel.Fields);
            Assert.AreEqual(1, templateViewModel.Fields.Count);

            templateViewModel.Entries = _target.HasSubmissions(templateViewModel).ToList();
            Assert.AreEqual(0, templateViewModel.Entries.Count);

            FormCollection submissionCollection = new FormCollection();
            submissionCollection.Add("SubmitFields[1].TextBox", "HelloTest");

            IDictionary<string, string> submissionFields = new System.Collections.Generic.Dictionary<string, string>();
            submissionFields.Add("1", "1");

            string result = _target.FillIn(submissionFields, templateViewModel, submissionCollection);
            Assert.AreEqual(result, "success");

            templateViewModel.Entries = _target.HasSubmissions(templateViewModel).ToList();

            Assert.AreEqual(1, templateViewModel.Entries.Count);
        }


        //test for Constants.TemplateMode.READONLY scenario
        //test for FindTemplateToEdit to do copyTemplate
        [TestMethod()]
        public void FindTemplateToEdit_ShouldHaveRecordAfterCreate()
        {
            FormViewModel formViewModel = new FormViewModel();

            Template template = _target.CreateNewFormAndTemplate(formViewModel);
            Assert.IsNotNull(template);

            TemplateViewModel postExecuteResult = _target.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(postExecuteResult);
            Assert.AreEqual(Constants.TemplateMode.EDIT, postExecuteResult.Mode);
        }

        [TestMethod()]
        public void FindTemplateToEdit_CopyToNewTemplate()
        {
            FormViewModel formViewModel = new FormViewModel();

            Template template = _target.CreateNewFormAndTemplate(formViewModel);
            Assert.IsNotNull(template);



            TemplateViewModel postExecuteResult = _target.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(postExecuteResult);
            Assert.AreEqual(Constants.TemplateMode.EDIT, postExecuteResult.Mode);
        }


        [TestMethod()]
        public void DeleteTemplate_UnableToDeleteBecauseOfOnlyVersion()
        {
            FormViewModel formViewModel = new FormViewModel();

            Template template = _target.CreateNewFormAndTemplate(formViewModel);
            Assert.IsNotNull(template);
            Assert.IsNotNull(template.Form);

            string deleteResult = _target.DeleteTemplate(template.TemplateID);
            Assert.AreEqual(deleteResult, "Unable to delete template when there is only one remains");

            Template templateResult = _target.FindTemplate(template.TemplateID);
            Assert.IsNotNull(templateResult);
        }


        //test for able to delete when there is two version
        //test for unable to delete when there is form submission
        [TestMethod()]
        public void DeleteTemplate_ReturnError()
        {
            string deleteResult = _target.DeleteTemplate(-1);
            Assert.AreEqual(deleteResult, "Unable to delete template - invalid id");
        }


        [TestMethod()]
        public void DeleteFormAndTemplate_DeleteSuccess()
        {
            FormViewModel formViewModel = new FormViewModel();

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

        //test for unable to delete when there is form submission
        [TestMethod()]
        public void DeleteFormAndTemplate_ReturnError()
        {
            string deleteResult = _target.DeleteFormAndTemplate(-1);
            Assert.AreEqual(deleteResult, "Unable to delete form - invalid id");
        }

        [TestMethod()]
        public void FindAllFormsByDes_ShouldHaveNoRecordAfterDelete()
        {
            List<FormViewModel> preExecuteResult = _target.FindAllFormsByDes();
            Assert.IsNotNull(preExecuteResult);
            Assert.AreEqual(preExecuteResult.Count(), 0);

            FormViewModel formViewModel = new FormViewModel();

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

            Template template = _target.CreateNewFormAndTemplate(formViewModel);
            Assert.IsNotNull(template);

            string deleteResult = _target.DeleteFormAndTemplate(template.Form.FormID);
            Assert.AreEqual(deleteResult, "success");

            List<TemplateViewModel> postExecuteResult = _target.FindAllTemplatesByFormId(template.Form.FormID);
            Assert.IsNotNull(postExecuteResult);
            Assert.AreEqual(postExecuteResult.Count(), 0);
        }

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

            Template template = _target.CreateNewFormAndTemplate(formViewModel);
            Assert.IsNotNull(template);

            Template postExecuteResult = _target.FindPublicTemplate(slug);
            Assert.IsNotNull(postExecuteResult);
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

            Template template = _target.CreateNewFormAndTemplate(formViewModel);
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

            Template template = _target.CreateNewFormAndTemplate(formViewModel);
            Assert.IsNotNull(template);

            Template postExecuteResult = _target.FindPreRegistrationForm();
            Assert.IsNotNull(postExecuteResult);
        }

        [TestInitialize]
        public void SetupTest()
        {
            var connection = DbConnectionFactory.CreateTransient();
            _context = new PHSContext(connection);
            _unitOfWork = new MockUnitOfWork(_context);

            _target = new MockFormManager(_unitOfWork);
        }

        [TestCleanup]
        public void CleanupTest()
        {
            // dispose of the database and connection
            _context.Dispose();
            _unitOfWork.Dispose();
            _target.Dispose();

            _unitOfWork = null;
            _context = null;
            _target = null;
        }
    }
}