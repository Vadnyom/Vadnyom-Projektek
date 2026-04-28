using FairOrder;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Test
{
    [TestFixture]
    public class KosarTetelTest
    {
        [Test]
        [TestCase(1000, 3, 3000)]   // normál eset
        [TestCase(1000, 0, 0)]      // zéró mennyiség
        [TestCase(0, 5, 0)]         // zéró ár
        [TestCase(500, 2, 1000)]    // normál eset
        public void Osszesen_ReturnsCorrectTotal(decimal sitePrice, int mennyiseg, decimal expected)
        {
            // Arrange
            var tetel = new KosarTetel { SitePrice = sitePrice, Mennyiseg = mennyiseg };

            // Act
            var result = tetel.Osszesen;

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase("SKU001", 2, 500, "SKU001")]      // SKU látható a stringben
        [TestCase("ABC999", 1, 1000, "ABC999")]     // másik SKU látható a stringben
        [TestCase("XYZ123", 3, 750, "x3")]          // mennyiség látható a stringben
        public void ToString_ContainsCorrectInfo(string sku, int mennyiseg, decimal sitePrice, string expectedContent)
        {
            // Arrange
            var tetel = new KosarTetel { Sku = sku, Mennyiseg = mennyiseg, SitePrice = sitePrice };

            // Act
            var result = tetel.ToString();

            // Assert
            StringAssert.Contains(expectedContent, result); // Ellenőrizzük, hogy a várt tartalom benne van-e a stringben (isequalto-ra nincs szükség)
        }
    }
}