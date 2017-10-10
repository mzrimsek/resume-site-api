using System;
using Core.Helpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Unit.HelperTests.DateHelperTests
{
    [TestClass]
    public class FormatShould
    {
        [TestMethod]
        public void ReturnCorrectlyFormattedDateString()
        {
            var date = new DateTime(2017, 7, 22);
            var formattedDate = date.Format();
            formattedDate.Should().Be("7/22/2017");
        }
    }
}