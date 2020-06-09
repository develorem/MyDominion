using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev.Dominion.Scraper.Tests
{
    [TestClass]
    public class Class1
    {
        [TestMethod]
        public void Foo()
        {
            var testString = "Abc<br/>def<br />ghi<br>jkl<BR>mno";

            var arr = testString.Split(new[] {"<br>", "<br />", "<br/>", "<BR>"}, StringSplitOptions.RemoveEmptyEntries);

            Assert.AreEqual(5, arr.Length);
        }
    }
}