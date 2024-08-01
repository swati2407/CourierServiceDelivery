using CourierServiceDeliveryCostEstimation.Helper;
using CourierServiceDeliveryCostEstimation.Model;
using Moq;

namespace CourierServiceDeliveryCostEstimation.Test
{
    [TestFixture]
    public class CalculateDeliveryCostTests
    {
        private Mock<IRateCalculator> _mockRateCalculator;

        [SetUp]
        public void Setup()
        {
            _mockRateCalculator = new Mock<IRateCalculator>();
        }

        [Test]
        public void CanConstruct()
        {
            var instance = new CalculateDeliveryCost(_mockRateCalculator.Object);
            Assert.IsNotNull(instance);
        }

        [Test]
        public void CalculateProductPriceDetailsWhenNoOfferApplied()
        {
            _mockRateCalculator.Setup(x => x.CalculateItemWiseDiscountPrice(It.IsAny<ProductDetails>(), It.IsAny<double>())).Returns(0);
            ProductDetails productDetails = new ProductDetails()
            {
                Package_Id = "PKG1",
                DistanceToTravel = 5,
                Package_Weight = 5,
                OfferCode = "OFR001"
            };
            List<ProductDetails> productDetailsList = [productDetails];
            Products products = new Products()
            {
                BasePrice = 100,
                NoOfPackages = 1,
                ProductDetails = productDetailsList
            };

            var instance = new CalculateDeliveryCost(_mockRateCalculator.Object);

            var response = instance.CalculateProductPriceDetails(products);
            var discountedPrice = response.FirstOrDefault();

            Assert.IsNotNull(response);
            Assert.That(discountedPrice.TotalPrice, Is.EqualTo(175));
        }

        [Test]
        public void CalculateProductPriceDetailsWhenOfferApplied()
        {
            _mockRateCalculator.Setup(x => x.CalculateItemWiseDiscountPrice(It.IsAny<ProductDetails>(), It.IsAny<double>())).Returns(35);
            ProductDetails productDetails = new ProductDetails()
            {
                Package_Id = "PKG3",
                DistanceToTravel = 100,
                Package_Weight = 10,
                OfferCode = "OFR003"
            };
            List<ProductDetails> productDetailsList = new List<ProductDetails>();
            productDetailsList.Add(productDetails);
            Products products = new Products()
            {
                BasePrice = 100,
                NoOfPackages = 1,
                ProductDetails = productDetailsList
            };

            var instance = new CalculateDeliveryCost(_mockRateCalculator.Object);

            var response = instance.CalculateProductPriceDetails(products);
            var discountedPrice = response.FirstOrDefault();

            Assert.IsNotNull(response);
            Assert.That(discountedPrice.TotalPrice, Is.EqualTo(665));
        }

    }
}