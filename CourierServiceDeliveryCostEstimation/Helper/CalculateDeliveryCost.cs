using CourierServiceDeliveryCostEstimation.Model;

namespace CourierServiceDeliveryCostEstimation.Helper
{
    public class CalculateDeliveryCost : ICalculateDeliveryCost
    {
        private readonly IRateCalculator _rateCalculator;

        public CalculateDeliveryCost(IRateCalculator rateCalculator)
        {
            _rateCalculator = rateCalculator;
        }
        public List<ProductPriceDetails> CalculateProductPriceDetails(Products products)
        {
            try
            {
                List<ProductPriceDetails> productPrices = new List<ProductPriceDetails>();

                foreach (var product in products.ProductDetails)
                {
                    var estimatedPrice = products.BasePrice +
                        (product.Package_Weight * 10) + (product.DistanceToTravel * 5);
                    var discountAmount = _rateCalculator.CalculateItemWiseDiscountPrice(product, estimatedPrice);
                    ProductPriceDetails priceDetails = new ProductPriceDetails()
                    {
                        Package_Id = product.Package_Id,
                        DiscountAmount = discountAmount,
                        TotalPrice = estimatedPrice - discountAmount
                    };
                    productPrices.Add(priceDetails);
                }

                return productPrices ?? new List<ProductPriceDetails>();
            }
            catch (Exception ex)
            {
                throw ex;
                //we can implement logger method here which can log the error in detail
            }
        }
    }
}
