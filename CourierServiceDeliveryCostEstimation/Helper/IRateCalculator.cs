using CourierServiceDeliveryCostEstimation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierServiceDeliveryCostEstimation.Helper
{
    public interface IRateCalculator
    {
        double CalculateItemWiseDiscountPrice(ProductDetails product, double basePrice);
    }
}
