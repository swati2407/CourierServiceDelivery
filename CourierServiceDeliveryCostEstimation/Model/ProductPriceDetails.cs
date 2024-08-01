
namespace CourierServiceDeliveryCostEstimation.Model
{
    public class ProductPriceDetails
    {
        public string Package_Id { get; set; }
        public double DiscountAmount { get; set; }
        public double TotalPrice { get; set; }
        public string EstimatedDeliveryTime { get; set; }
    }
}
