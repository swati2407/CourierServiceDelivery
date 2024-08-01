
namespace CourierServiceDeliveryCostEstimation.Model
{
    public class OfferDetails
    {
        public string OfferCode { get; set; }
        public Range DistanceRange { get; set; }
        public Range WeightRange { get; set; }
        public double DiscountPercentage { get; set; }
    }

    public class Range
    {
        public int Min { get; set; }
        public int Max { get; set; }
    }

}
