
using CourierServiceDeliveryCostEstimation.Model;

namespace CourierServiceDeliveryCostEstimation.Helper
{
    public class CalculateDeliveryTime : ICalculateDeliveryTime
    {
        public List<ProductPriceDetails> CalculateProductEstimatedDeliveryTime(List<ProductDetails> products,
            VehicleDetails vehicleDetails, List<ProductPriceDetails> productPriceDetails)
        {
            List<List<ProductDetails>> vehicleDetailsList = new List<List<ProductDetails>>();

            for (int i = 0; i < vehicleDetails.NoOfVehicles; i++)
            {
                vehicleDetailsList.Add(new List<ProductDetails>());
            }

            products = products.OrderByDescending(x => x.Package_Weight).ThenBy(x => x.DistanceToTravel).ToList();

            //Assign packages to the vehicles
            foreach (var product in products)
            {
                foreach (var vehicle in vehicleDetailsList)
                {
                    double currrentWeight = vehicle.Sum(p => p.Package_Weight);
                    if(currrentWeight + product.Package_Weight <= vehicleDetails.MaxWeight)
                    {
                        vehicle.Add(product);
                        break;
                    }
                }
            }

            for (int i = 0;i < vehicleDetailsList.Count;i++)
            {
                var vehicle = vehicleDetailsList[i];
                if(vehicle.Count > 0)
                {
                    double totalDistance = vehicle.Sum(x => x.DistanceToTravel);
                    double maxDistance = vehicle.Max(x => x.DistanceToTravel);
                    double estimatedDeliveryTime = (double)maxDistance / totalDistance;
                    string packageId = vehicle.Select(x => x.Package_Id).ToString();
                }
            }
            return productPriceDetails;
        }
    }
}
