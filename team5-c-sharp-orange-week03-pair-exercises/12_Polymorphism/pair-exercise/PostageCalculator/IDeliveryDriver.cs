using System;
using System.Collections.Generic;
using System.Text;

namespace PostageCalculator
{
    interface IDeliveryDriver
    {
        double CalculateRate(int distance, double weight);

        string GetName();
    }

    
}
