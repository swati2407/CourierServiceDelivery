using CourierServiceDeliveryCostEstimation.Model;
using Newtonsoft.Json;

namespace CourierServiceDeliveryCostEstimation.Helper
{
    public class RateCalculater : IRateCalculator
    {
        public double CalculateItemWiseDiscountPrice(ProductDetails product, double basePrice)
        {
            var available_Offer = LoadOfferDetails();

            var rule = available_Offer.FirstOrDefault(x =>
            x.OfferCode == product.OfferCode &&
            product.DistanceToTravel >= x.DistanceRange.Min &&
            product.DistanceToTravel <= x.DistanceRange.Max &&
            product.Package_Weight >= x.WeightRange.Min &&
            product.Package_Weight <= x.WeightRange.Max);

            if (rule != null)
            {
                return (basePrice * rule.DiscountPercentage) / 100;
            }
            return 0;
        }

        private List<OfferDetails> LoadOfferDetails()
        {
            string jsonString = File.ReadAllText("OfferCode.json");
            return JsonConvert.DeserializeObject<List<OfferDetails>>(jsonString);
        }
    }
}
