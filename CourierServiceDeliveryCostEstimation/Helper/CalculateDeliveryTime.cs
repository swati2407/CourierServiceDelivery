
using CourierServiceDeliveryCostEstimation.Model;

namespace CourierServiceDeliveryCostEstimation.Helper
{
    public class CalculateDeliveryTime : ICalculateDeliveryTime
    {
        public List<ProductPriceDetails> CalculateProductEstimatedDeliveryTime(List<ProductDetails> products,
            VehicleDetails vehicleDetails, List<ProductPriceDetails> productPriceDetails)
        {
            products = products.OrderByDescending(x => x.Package_Weight).ThenBy(x => x.DistanceToTravel).ToList();


            throw new NotImplementedException();
        }
    }
}
