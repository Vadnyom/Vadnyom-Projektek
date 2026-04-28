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
    public class OrderRequestTest
    {

        //test: order request default értékei helyesek-e
        [Test]
        [TestCase("IsPlaced", true)]
        [TestCase("StoreId", 1)]
        [TestCase("ApplyVATRules", true)]
        public void OrderRequest_DefaultValues_AreCorrect(string property, object expected)
        {
            // Arrange & Act
            var order = new OrderRequest();

            // Assert
            var actual = order.GetType().GetProperty(property).GetValue(order);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]

        //test: manuális order requestnél minden érték helyesen van-e beállítva
        public void OrderRequest_WhenBuilt_HasCorrectProperties()
        {
            // Arrange & Act
            var order = new OrderRequest
            {
                UserEmail = "teszt@gmail.com",
                UserID = "1",
                TotalGrand = 1270m,
                TotalOrderBeforeDiscounts = 1000m,
                ItemsTax = 270m,
                TotalTax = 270m,
                BillingAddress = new BillingAddress
                {
                    FirstName = "Teszt",
                    LastName = "Felhasználó",
                    CountryName = "Hungary"
                },
                Items = new System.Collections.Generic.List<OrderItem>
        {
            new OrderItem
            {
                ProductSku = "SKU001",
                Quantity = 2,
                BasePricePerItem = 500m,
                LineTotal = 1000m,
                TaxRate = 0.27m,
                TaxPortion = 270m
            }
        }
            };

            // Assert
            Assert.That(order.UserEmail, Is.EqualTo("teszt@gmail.com"));
            Assert.That(order.TotalGrand, Is.EqualTo(1270m));
            Assert.That(order.BillingAddress.CountryName, Is.EqualTo("Hungary"));
            Assert.That(order.Items.Count, Is.EqualTo(1));
            Assert.That(order.Items[0].ProductSku, Is.EqualTo("SKU001"));
            Assert.That(order.Items[0].TaxRate, Is.EqualTo(0.27m));
        }
    }
}