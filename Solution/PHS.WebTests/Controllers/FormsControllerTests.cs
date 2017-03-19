using Microsoft.VisualStudio.TestTools.UnitTesting;
using PHS.FormBuilder.ViewModels;
using System.Collections.Generic;

namespace PHS.Web.Controllers.Tests
{
    [TestClass()]
    public class FormsControllerTests
    {
        [TestMethod()]
        public void GenerateSortingTest_EmptyRecords()
        {
            FormsController target = new FormsController();
            PrivateObject obj = new PrivateObject(target);

            List<SortFieldViewModel> sortFields = new List<SortFieldViewModel>();

            var retVal = obj.Invoke("GenerateSorting", new object[] { sortFields });
            
            Assert.AreEqual(retVal, "");
        }

        [TestMethod()]
        public void GenerateSortingTest_OneRecord()
        {
            FormsController target = new FormsController();
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
        public void GenerateSortingTest_MultipleRecords()
        {
            FormsController target = new FormsController();
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
    }
}