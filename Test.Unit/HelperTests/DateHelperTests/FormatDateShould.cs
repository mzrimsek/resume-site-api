using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Core.Helpers;

namespace Test.Unit.HelperTests.DateHelperTests
{
    [TestClass]
    public class FormatDateShould
    {
        [TestMethod]
        public void ReturnCorrectlyFormattedDateString()
        {
            var date = new DateTime(2017, 7, 22);
            var formattedDate = DateHelper.FormatDate(date);
            Assert.AreEqual("7/22/2017", formattedDate);
        }
    }
}