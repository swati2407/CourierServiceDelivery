
namespace CourierServiceDeliveryCostEstimation.Model
{
    public class Products
    {
        public double BasePrice { get; set; }
        public int NoOfPackages { get; set; }
        public List<ProductDetails> ProductDetails { get; set; }
    }
}
