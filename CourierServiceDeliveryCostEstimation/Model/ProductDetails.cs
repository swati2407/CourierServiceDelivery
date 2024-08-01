
namespace CourierServiceDeliveryCostEstimation.Model
{
    public class ProductDetails
    {
        public string Package_Id { get; set; }
        public double Package_Weight { get; set; }
        public double DistanceToTravel { get; set; }
        public string? OfferCode { get; set; }
    }
}
