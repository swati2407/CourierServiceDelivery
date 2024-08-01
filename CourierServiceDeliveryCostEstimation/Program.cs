using CourierServiceDeliveryCostEstimation.Helper;
using CourierServiceDeliveryCostEstimation.Model;
using Microsoft.Extensions.DependencyInjection;

namespace CourierServiceDeliveryCostEstimation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Please enter the package Details");

            Console.WriteLine("Enter the base delivery cost");
            double baseCost = double.Parse(Console.ReadLine());

            Console.WriteLine("Enter the numbers of packages");
            int no_Of_Packages = int.Parse(Console.ReadLine());

            List<ProductDetails> products = new List<ProductDetails>();
            Console.WriteLine("Enter the package details by using space separator(i.e. PKGID PKHWeight Distance OfferCode) ");

            for (int i = 0; i < no_Of_Packages; i++)
            {
                string packageDetails = Console.ReadLine();
                string[] packageInfo = packageDetails.Split(' ');
                ProductDetails product = new ProductDetails()
                {
                    Package_Id = packageInfo[0],
                    Package_Weight = double.Parse(packageInfo[1]),
                    DistanceToTravel = double.Parse(packageInfo[2]),
                    OfferCode = packageInfo[3]
                };
                products.Add(product);
            }

            Console.WriteLine("Enter Vehicle Details by using space separator(i.e. NoOfVehicles MaxSpeed MaxWeight)");
            string vehicleDetails = Console.ReadLine();
            string[] vehicleInfo = vehicleDetails.Split(' ');
            VehicleDetails vehicles = new VehicleDetails()
            {
                NoOfVehicles = int.Parse(vehicleInfo[0]),
                MaxSpeed = double.Parse(vehicleInfo[1]),
                MaxWeight = double.Parse(vehicleInfo[2])
            };
            try
            {
                Products given_Products = new Products()
                {
                    BasePrice = baseCost,
                    NoOfPackages = no_Of_Packages,
                    ProductDetails = products
                };

                //set up dependency Injection
                var serviceProvider = new ServiceCollection()
                    .AddSingleton<ICalculateDeliveryCost, CalculateDeliveryCost>()
                    .AddSingleton<IRateCalculator, RateCalculater>()
                    .AddSingleton<ICalculateDeliveryTime, CalculateDeliveryTime>()
                    .BuildServiceProvider();


                var calculatorService = serviceProvider.GetService<ICalculateDeliveryCost>();
                var estimatedTimeService = serviceProvider.GetService<ICalculateDeliveryTime>();

                List<ProductPriceDetails> rateList = calculatorService.CalculateProductPriceDetails(given_Products);

                var rateList1 = estimatedTimeService.CalculateProductEstimatedDeliveryTime(products, vehicles, rateList);

                Console.WriteLine("PkgId    discount    totalCost");
                foreach (var item in rateList1)
                {
                    Console.WriteLine($"{item.Package_Id}   {item.DiscountAmount}   {item.TotalPrice} {item.EstimatedDeliveryTime}");
                }
                Console.WriteLine("Press exit to come out");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
