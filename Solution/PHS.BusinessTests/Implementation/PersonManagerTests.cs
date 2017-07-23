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
    class PersonManagerTests
    {
        private PersonManager _target;
        private IUnitOfWork _unitOfWork;
        private PHSContext _context;

        [TestMethod()]
        public void GetAllPersons_ShouldHaveRecordAfterCreate()
        {
            string message = string.Empty;
            IList<Person> preExecuteResult = _target.GetAllPersons(out message);
            Assert.IsNotNull(preExecuteResult);
            Assert.AreEqual(preExecuteResult.Count(), 0);

            Person person = new Person();
            Person loginperson = new Person();

            Person personcreated = _target.AddPerson(loginperson, person, out message);
            Assert.IsNotNull(personcreated);

            IList<Person> postExecuteResult = _target.GetAllPersons(out message);
            Assert.IsNotNull(postExecuteResult);
            Assert.AreEqual(postExecuteResult.Count(), 1);
        }
    }
}
