using CourierServiceDeliveryCostEstimation.Model;

namespace CourierServiceDeliveryCostEstimation.Helper
{
    public interface ICalculateDeliveryTime
    {
        List<ProductPriceDetails> CalculateProductEstimatedDeliveryTime(List<ProductDetails> products,
            VehicleDetails vehicleDetails, List<ProductPriceDetails> productPriceDetails);
    }
}
