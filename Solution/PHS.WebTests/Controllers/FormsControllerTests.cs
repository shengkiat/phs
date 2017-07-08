﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using PHS.DB.ViewModels.Form;
using System.Collections.Generic;

namespace PHS.Web.Controllers.Tests
{
    [TestClass()]
    public class FormsControllerTests
    {
        [TestMethod()]
        public void GenerateSorting_EmptyRecord()
        {
            FormController target = new FormController();
            PrivateObject obj = new PrivateObject(target);

            List<SortFieldViewModel> sortFields = new List<SortFieldViewModel>();

            var retVal = obj.Invoke("GenerateSorting", new object[] { sortFields });
            
            Assert.AreEqual(retVal, "");
        }

        [TestMethod()]
        public void GenerateSorting_OneRecord()
        {
            FormController target = new FormController();
            PrivateObject obj = new PrivateObject(target);

            List<SortFieldViewModel> sortFields = new List<SortFieldViewModel>();

            SortFieldViewModel record = new SortFieldViewModel();
            record.FieldLabel = "TEST COL";
            record.SortOrder = "ASC";
            sortFields.Add(record);

            var retVal = obj.Invoke("GenerateSorting", new object[] { sortFields });

            Assert.AreEqual("TEST COL ASC", retVal);
        }

        [TestMethod()]
        public void GenerateSorting_MultipleRecords()
        {
            FormController target = new FormController();
            PrivateObject obj = new PrivateObject(target);

            List<SortFieldViewModel> sortFields = new List<SortFieldViewModel>();

            SortFieldViewModel recordOne = new SortFieldViewModel();
            recordOne.FieldLabel = "TEST COL";
            recordOne.SortOrder = "ASC";
            sortFields.Add(recordOne);

            SortFieldViewModel recordTwo = new SortFieldViewModel();
            recordTwo.FieldLabel = "TWO COL";
            recordTwo.SortOrder = "DESC";
            sortFields.Add(recordTwo);

            var retVal = obj.Invoke("GenerateSorting", new object[] { sortFields });

            Assert.AreEqual("TEST COL ASC, TWO COL DESC", retVal);
        }

        [TestMethod()]
        public void GenerateFlitering_EmptyRecord()
        {
            FormController target = new FormController();
            PrivateObject obj = new PrivateObject(target);

            List<CriteriaFieldViewModel> fields = new List<CriteriaFieldViewModel>();

            var retVal = obj.Invoke("GenerateFlitering", new object[] { fields });

            Assert.AreEqual(retVal, "");
        }

        [TestMethod()]
        public void GenerateFlitering_OneRecordUsingSimpleMapping()
        {
            FormController target = new FormController();
            PrivateObject obj = new PrivateObject(target);

            List<CriteriaFieldViewModel> fields = new List<CriteriaFieldViewModel>();
            CriteriaFieldViewModel record = new CriteriaFieldViewModel();
            record.CriteriaValue = new Dictionary<string, string>();

            record.FieldLabel = "TWO COL";
            record.CriteriaLogic = "eq";
            record.CriteriaValue[record.FieldLabel] = "TEST";
            fields.Add(record);

            var retVal = obj.Invoke("GenerateFlitering", new object[] { fields });

            Assert.AreEqual("[TWO COL] = 'TEST'", retVal);
        }

        [TestMethod()]
        public void GenerateFlitering_OneRecordWithSubRecordUsingSimpleMapping()
        {
            FormController target = new FormController();
            PrivateObject obj = new PrivateObject(target);

            List<CriteriaFieldViewModel> fields = new List<CriteriaFieldViewModel>();
            CriteriaFieldViewModel record = new CriteriaFieldViewModel();
            record.CriteriaValue = new Dictionary<string, string>();
            record.CriteriaSubFields = new List<CriteriaSubFieldViewModel>();

            record.FieldLabel = "TWO COL";
            record.CriteriaLogic = "neq";
            record.CriteriaValue[record.FieldLabel] = "FIRSTCOL";

            CriteriaSubFieldViewModel subRecord = new CriteriaSubFieldViewModel();
            subRecord.CriteriaValue = new Dictionary<string, string>();

            subRecord.OperatorLogic = "and";
            subRecord.CriteriaLogic = "gt";
            subRecord.CriteriaValue[record.FieldLabel] = "SECCOL";

            record.CriteriaSubFields.Add(subRecord);

            fields.Add(record);

            var retVal = obj.Invoke("GenerateFlitering", new object[] { fields });

            Assert.AreEqual("[TWO COL] <> 'FIRSTCOL' and [TWO COL] > 'SECCOL'", retVal);
        }

        [TestMethod()]
        public void GenerateFlitering_MultipleRecordUsingSimpleMapping()
        {
            FormController target = new FormController();
            PrivateObject obj = new PrivateObject(target);

            List<CriteriaFieldViewModel> fields = new List<CriteriaFieldViewModel>();
            CriteriaFieldViewModel recordOne = new CriteriaFieldViewModel();
            recordOne.CriteriaValue = new Dictionary<string, string>();

            recordOne.FieldLabel = "FIRST COL";
            recordOne.CriteriaLogic = "gte";
            recordOne.CriteriaValue[recordOne.FieldLabel] = "FIRSTVAL";

            fields.Add(recordOne);

            CriteriaFieldViewModel recordTwo = new CriteriaFieldViewModel();
            recordTwo.CriteriaValue = new Dictionary<string, string>();

            recordTwo.FieldLabel = "SEC COL";
            recordTwo.CriteriaLogic = "lt";
            recordTwo.CriteriaValue[recordTwo.FieldLabel] = "SECVAL";

            fields.Add(recordTwo);

            CriteriaFieldViewModel recordThree = new CriteriaFieldViewModel();
            recordThree.CriteriaValue = new Dictionary<string, string>();

            recordThree.FieldLabel = "THIRD COL";
            recordThree.CriteriaLogic = "lte";
            recordThree.CriteriaValue[recordThree.FieldLabel] = "THIRDVAL";

            fields.Add(recordThree);

            var retVal = obj.Invoke("GenerateFlitering", new object[] { fields });

            Assert.AreEqual("[FIRST COL] >= 'FIRSTVAL' OR [SEC COL] < 'SECVAL' OR [THIRD COL] <= 'THIRDVAL'", retVal);
        }

        [TestMethod()]
        public void GenerateFlitering_OneRecordUsingStartsWiths()
        {
            FormController target = new FormController();
            PrivateObject obj = new PrivateObject(target);

            List<CriteriaFieldViewModel> fields = new List<CriteriaFieldViewModel>();
            CriteriaFieldViewModel record = new CriteriaFieldViewModel();
            record.CriteriaValue = new Dictionary<string, string>();

            record.FieldLabel = "TWO COL";
            record.CriteriaLogic = "startswith";
            record.CriteriaValue[record.FieldLabel] = "TEST";
            fields.Add(record);

            var retVal = obj.Invoke("GenerateFlitering", new object[] { fields });

            Assert.AreEqual("[TWO COL] LIKE 'TEST*'", retVal);
        }

        [TestMethod()]
        public void GenerateFlitering_OneRecordUsingEndWiths()
        {
            FormController target = new FormController();
            PrivateObject obj = new PrivateObject(target);

            List<CriteriaFieldViewModel> fields = new List<CriteriaFieldViewModel>();
            CriteriaFieldViewModel record = new CriteriaFieldViewModel();
            record.CriteriaValue = new Dictionary<string, string>();

            record.FieldLabel = "TWO COL";
            record.CriteriaLogic = "endswith";
            record.CriteriaValue[record.FieldLabel] = "TEST";
            fields.Add(record);

            var retVal = obj.Invoke("GenerateFlitering", new object[] { fields });

            Assert.AreEqual("[TWO COL] LIKE '*TEST'", retVal);
        }

        [TestMethod()]
        public void GenerateFlitering_OneRecordUsingContains()
        {
            FormController target = new FormController();
            PrivateObject obj = new PrivateObject(target);

            List<CriteriaFieldViewModel> fields = new List<CriteriaFieldViewModel>();
            CriteriaFieldViewModel record = new CriteriaFieldViewModel();
            record.CriteriaValue = new Dictionary<string, string>();

            record.FieldLabel = "TWO COL";
            record.CriteriaLogic = "contains";
            record.CriteriaValue[record.FieldLabel] = "TEST";
            fields.Add(record);

            var retVal = obj.Invoke("GenerateFlitering", new object[] { fields });

            Assert.AreEqual("[TWO COL] LIKE '*TEST*'", retVal);
        }

        [TestMethod()]
        public void GenerateFlitering_OneRecordUsingDoesNotContains()
        {
            FormController target = new FormController();
            PrivateObject obj = new PrivateObject(target);

            List<CriteriaFieldViewModel> fields = new List<CriteriaFieldViewModel>();
            CriteriaFieldViewModel record = new CriteriaFieldViewModel();
            record.CriteriaValue = new Dictionary<string, string>();

            record.FieldLabel = "TWO COL";
            record.CriteriaLogic = "doesnotcontain";
            record.CriteriaValue[record.FieldLabel] = "TEST";
            fields.Add(record);

            var retVal = obj.Invoke("GenerateFlitering", new object[] { fields });

            Assert.AreEqual("[TWO COL] NOT LIKE '*TEST*'", retVal);
        }

        [TestMethod()]
        public void GenerateFlitering_OneRecordUsingIn()
        {
            FormController target = new FormController();
            PrivateObject obj = new PrivateObject(target);

            List<CriteriaFieldViewModel> fields = new List<CriteriaFieldViewModel>();
            CriteriaFieldViewModel record = new CriteriaFieldViewModel();
            record.CriteriaValue = new Dictionary<string, string>();

            record.FieldLabel = "TWO COL";
            record.CriteriaLogic = "in";
            record.CriteriaValue[record.FieldLabel] = "TEST";
            fields.Add(record);

            var retVal = obj.Invoke("GenerateFlitering", new object[] { fields });

            Assert.AreEqual("[TWO COL] IN (TEST)", retVal);
        }
    }
}