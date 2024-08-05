
using CourierServiceDeliveryCostEstimation.Model;

namespace CourierServiceDeliveryCostEstimation.Helper
{
    public class CalculateDeliveryTime : ICalculateDeliveryTime
    {
        public List<ProductPriceDetails> CalculateProductEstimatedDeliveryTime(List<ProductDetails> products,
            VehicleDetails vehicleDetails, List<ProductPriceDetails> productPriceDetails)
        {
            //order the vehicle first based on weight (descending), then by distance (ascending)
            products = products.OrderByDescending(x => x.Package_Weight).ThenBy(x => x.DistanceToTravel).ToList();

            //Create a dictionary which will save estimated time for each packages
            Dictionary<string, double> deliveryTimes = new Dictionary<string, double>();

            //Define the dictionary to maintain the vehicle return time 
            Dictionary<int, double> vehicleAvailability = new Dictionary<int, double>();

            double currentTime = 0.0;
            int currentVehicle = 1;

            while(products.Count > 0)
            {
                List<ProductDetails> currentBatch = new List<ProductDetails>();
                double currentLoad = 0;

                List<ProductDetails> productToBeRemoved = new List<ProductDetails>();

                //Get the avaialble set based on vehicle package weight
                currentBatch = FindNextAvailableSet(products, vehicleDetails);
                productToBeRemoved = currentBatch;

                foreach (var product in productToBeRemoved)
                { 
                    products.Remove(product);
                }

                //if no of packages could be added to the batch, break to avoid infinite loop
                if(currentBatch.Count == 0)
                {
                    break;
                }

                //calculate the time for current batch
                double furthestDistance = currentBatch.Max(x => x.DistanceToTravel);
                double timeforCurrentBatch = ((2 * furthestDistance) / vehicleDetails.MaxSpeed) + currentTime;

                vehicleAvailability[currentVehicle] = Math.Round(timeforCurrentBatch, 2);

                //assign delivery time to each package in batch
                foreach (var current in currentBatch) 
                {
                    var deliveryTime = (current.DistanceToTravel / vehicleDetails.MaxSpeed) + currentTime;
                    deliveryTimes[current.Package_Id] = Math.Round(deliveryTime, 2);
                }

                //switch to the next vehicle
                currentVehicle = (currentVehicle % vehicleDetails.NoOfVehicles) + 1;
                if (vehicleAvailability.Count == vehicleDetails.NoOfVehicles)
                {
                    var vehicleAvavilableInfo = GetNextAvailableVehicle(vehicleAvailability, currentTime);
                    currentTime += vehicleAvavilableInfo.Item2;
                    currentVehicle = vehicleAvavilableInfo.Item1;
                }
            }

            //update the estimated delivery time for each package 
            foreach (var productPrice in productPriceDetails)
            {
                if(deliveryTimes.TryGetValue(productPrice.Package_Id, out double estimatedTime))
                {
                    productPrice.EstimatedDeliveryTime = estimatedTime;
                }
            }

            return productPriceDetails;
        }

        private Tuple<int, double> GetNextAvailableVehicle(Dictionary<int, double> vehicleAvailability, double currentTime)
        {
            var firstAvailableVehicle = vehicleAvailability.OrderBy(x => x.Value).FirstOrDefault();

            int vehicleNumber = firstAvailableVehicle.Key;
            double latest_CurrentTime = firstAvailableVehicle.Value - currentTime;

            return new Tuple<int, double>(vehicleNumber, latest_CurrentTime);
        }

        private static List<ProductDetails> FindNextAvailableSet(List<ProductDetails> products, VehicleDetails vehicleDetails)
        {
            ProductDetails? bestPackage1 = null;
            ProductDetails? bestPackage2 = null;
            double maxWeight = double.MinValue;
                        
            //check the individual product
            foreach (var product in products)
            {
                if(product.Package_Weight > maxWeight 
                    && product.Package_Weight <= vehicleDetails.MaxWeight)
                {
                    maxWeight = product.Package_Weight;
                    bestPackage1 = product;
                    bestPackage2 = null ;
                }
            }
            //check the combination of two product
            for (int i = 0; i < products.Count; i++)
            {
                for (int j = i+1; j < products.Count; j++)
                {
                    double currentWeightSum = products[i].Package_Weight + products[j].Package_Weight;
                    if(currentWeightSum > maxWeight 
                        && currentWeightSum <= vehicleDetails.MaxWeight)
                    {
                        maxWeight = currentWeightSum;
                        bestPackage1 = products[i];
                        bestPackage2 = products[j];
                    }
                }
            }
            List<ProductDetails> bestProducts = new List<ProductDetails>();
            if(bestPackage1 != null)
            {
                bestProducts.Add(bestPackage1);
            }
            if(bestPackage2 != null)
            {
                bestProducts.Add(bestPackage2);
            }
            return bestProducts;

        }

    }
}
