﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using PHS.DB.ViewModels.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.DB.ViewModels.Form.Tests
{
    [TestClass()]
    public class AddressViewModelTests
    {
        [TestMethod()]
        public void ConvertToOneLineAddressTest()
        {
            AddressViewModel address = new AddressViewModel()
            {
                Blk = "123",
                Unit = "11-11",
                StreetAddress = "Jalan Jalan",
                ZipCode = "789456"
            };
            Assert.AreEqual("Blk 123, Street Jalan Jalan, Unit 11-11", address.ConvertToOneLineAddress());
        }

        [TestMethod()]
        public void InitializeTest()
        {
            AddressViewModel address = AddressViewModel.Initialize("Blk 123, Street Jalan Jalan, Unit 11-11");
            Assert.AreEqual("123", address.Blk);
            Assert.AreEqual("Jalan Jalan", address.StreetAddress);
            Assert.AreEqual("11-11", address.Unit);
            Assert.IsNull(address.ZipCode);
        }
    }
}