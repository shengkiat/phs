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
        }

        [TestMethod()]
        public void FindAllFormsByDes_ShouldHaveRecordsAfterCreate()
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
        public void DeleteFormAndTemplate()
        {
            FormViewModel formViewModel = new FormViewModel();

            Template template = _target.CreateNewFormAndTemplate(formViewModel);
            Assert.IsNotNull(template);
            Assert.IsNotNull(template.Form);

            string deleteResult = _target.DeleteFormAndTemplate(template.Form.FormID);
            Assert.AreEqual(deleteResult, "success");

            FormViewModel result = _target.FindFormToEdit(template.Form.FormID);
            Assert.IsNull(result);
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