using Effort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PHS.Business.Implementation;
using PHS.BusinessTests;
using PHS.DB;
using PHS.Repository.Context;
using PHS.Repository.Interface.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.Business.Implementation.Tests
{
    [TestClass()]
    public class FormImportManagerTests
    {
        private FormManager _formManager;
        private FormAccessManager _formAccessManager;
        private FormImportManager _target;
        private IUnitOfWork _unitOfWork;
        private PHSContext _context;

        [TestMethod()]
        public void RetrieveAllFormsTest()
        {
            int formId = 1;
            Guid entryId = new Guid();

            PHSEvent phsEvent = new PHSEvent()
            {
                Title = "Test 15",
                Venue = "Test",
                StartDT = DateTime.Now.AddDays(-200),
                EndDT = DateTime.Now.AddDays(-199),
                IsActive = false
            };

            Modality modality = new Modality()
            {
                Name = "Test Modality"
            };

            Form form = new Form
            {
                Title = "Test form",
                DateAdded = new DateTime()
            };

            _unitOfWork.Events.Add(phsEvent);

            modality.Forms.Add(form);

            phsEvent.Modalities.Add(modality);

            _unitOfWork.Complete();

            string message = string.Empty;
            var result = _target.RetrieveAllForms(1, out message);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Forms.Count);
        }

        [TestInitialize]
        public void SetupTest()
        {
            var connection = DbConnectionFactory.CreateTransient();
            _context = new PHSContext(connection);
            _unitOfWork = new MockUnitOfWork(_context);

            _target = new MockFormImportManager(_unitOfWork);
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

        private class MockFormImportManager : FormImportManager
        {
            private IUnitOfWork _unitOfWork;

            public MockFormImportManager(IUnitOfWork unitOfWork) : base(null)
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