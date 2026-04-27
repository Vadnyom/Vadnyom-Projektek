using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FairOrder;

namespace UnitTest.Test
{
    [TestFixture]
    public class FilterServiceTest
    {
        private FilterService _filterService;
        private List<Product> _termekek;

        [SetUp]
        public void Setup()
        {
            _filterService = new FilterService();
            _termekek = new List<Product>
            {
                new Product { Sku = "ABC001" },
                new Product { Sku = "ABC002" },
                new Product { Sku = "XYZ999" },
                new Product { Sku = "xyz100" },
            };
        }

        [Test]
        [TestCase("ABC", 2)]
        [TestCase("XYZ", 2)]
        [TestCase("999", 1)]
        [TestCase("ZZZ", 0)]
        public void FilterBySku_ReturnsCorrectCount(string szuro, int expectedCount)
        {
            var result = _filterService.FilterBySku(_termekek, szuro);
            Assert.That(result.Count, Is.EqualTo(expectedCount));
        }

        [Test]
        [TestCase("")]
        [TestCase(" ")]
        public void FilterBySku_EmptyOrNullFilter_ReturnsAll(string szuro)
        {
            var result = _filterService.FilterBySku(_termekek, szuro);
            Assert.That(result.Count, Is.EqualTo(_termekek.Count));
        }

        [Test]
        public void FilterBySku_IsCaseInsensitive()
        {
            var upperResult = _filterService.FilterBySku(_termekek, "abc");
            var lowerResult = _filterService.FilterBySku(_termekek, "ABC");
            Assert.That(upperResult.Count, Is.EqualTo(lowerResult.Count));
        }

        [Test]
        public void FilterBySku_EmptyProductList_ReturnsEmpty()
        {
            var emptyList = new List<Product>();
            var result = _filterService.FilterBySku(emptyList, "ABC");
            Assert.That(result.Count, Is.EqualTo(0));
        }
    }
}