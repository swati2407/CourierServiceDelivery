
using CourierServiceDeliveryCostEstimation.Helper;
using CourierServiceDeliveryCostEstimation.Model;

namespace CourierService.Test
{
    [TestFixture]
    public class RateCalculaterTests
    {
        [Test]
        public void CanConstruct()
        {
            var instance = new RateCalculater();
            Assert.IsNotNull(instance);
        }

        [Test]
        public void CalculateItemWiseDiscountPriceWhenNoDiscountApplied()
        {
            ProductDetails productDetails = new ProductDetails()
            {
                Package_Id = "PKG001",
                DistanceToTravel = 5,
                Package_Weight = 5,
                OfferCode = "OFR001"
            };

            double DeliveryCost = 175;

            var instance = new RateCalculater();
            var discountAmount = instance.CalculateItemWiseDiscountPrice(productDetails, DeliveryCost);

            Assert.IsNotNull(discountAmount);
            Assert.That(discountAmount, Is.EqualTo(0));

        }

        [Test]
        public void CalculateItemWiseDiscountPriceWhenDiscountApplied()
        {
            ProductDetails productDetails = new ProductDetails()
            {
                Package_Id = "PKG003",
                DistanceToTravel = 100,
                Package_Weight = 10,
                OfferCode = "OFR003"
            };

            double DeliveryCost = 700;

            var instance = new RateCalculater();
            var discountAmount = instance.CalculateItemWiseDiscountPrice(productDetails, DeliveryCost);

            Assert.IsNotNull(discountAmount);
            Assert.That(discountAmount, Is.EqualTo(35));

        }
    }
}
