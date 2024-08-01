
using CourierServiceDeliveryCostEstimation.Model;

namespace CourierServiceDeliveryCostEstimation.Helper
{
    public interface ICalculateDeliveryCost
    {
        List<ProductPriceDetails> CalculateProductPriceDetails(Products products);
    }
}
