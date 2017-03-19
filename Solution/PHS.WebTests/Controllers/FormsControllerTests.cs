using Microsoft.VisualStudio.TestTools.UnitTesting;
using PHS.FormBuilder.ViewModels;
using System.Collections.Generic;

namespace PHS.Web.Controllers.Tests
{
    [TestClass()]
    public class FormsControllerTests
    {
        [TestMethod()]
        public void GenerateSortingTest()
        {
            FormsController target = new FormsController();
            PrivateObject obj = new PrivateObject(target);

            List<SortFieldViewModel> sortFields = new List<SortFieldViewModel>();

            var retVal = obj.Invoke("GenerateSorting", new object[] { sortFields });
            
            Assert.AreEqual(retVal, "");
        }
    }
}