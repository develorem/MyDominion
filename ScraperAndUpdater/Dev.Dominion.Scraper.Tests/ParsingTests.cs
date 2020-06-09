using System;
using Dev.Dominion.Scraper.Models;
using Dev.Dominion.Scraper.Parsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev.Dominion.Scraper.Tests
{
    [TestClass]
    public class ParsingTests
    {
        [TestMethod]
        public void TestGettingCardRow()
        {
            var set = SetTestData.ExampleSetOneCard("Alchemy", "Herbalist");
            int next;
            var cardRow = SetParser.GetCardRow(0, set, out next);
            var expectedRow = SetTestData.ExampleOneCardInner("Alchemy", "Herbalist");

            Assert.AreEqual(expectedRow, cardRow);
        }

        [TestMethod]
        public void TestGettingCardName()
        {
            var cardRow = SetTestData.ExampleOneCardInner("Alchemy", "Herbalist");
            var name = SetParser.GetCardName(cardRow);

            Assert.AreEqual("Herbalist", name);
        }

        [TestMethod]
        public void TestGettingCardUrl()
        {
            var cardRow = SetTestData.ExampleOneCardInner("Alchemy", "Herbalist");
            var name = SetParser.GetCardUrl(cardRow);

            Assert.AreEqual("./?card=!herbalist", name);
        }

        [TestMethod]
        public void TestParsingSet()
        {
            var html = SetTestData.ExampleSetMultipleCards("Alchemy");
            var set = new Set { Name = "Alchemy"};
            SetParser.ParseSetHtml(set, html);

            Assert.AreEqual(4, set.Cards.Count);
        }

    }
}
