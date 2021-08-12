using System;
using System.Collections.Generic;
using System.Text;

namespace PostageCalculator.Classes
{
    class SPU2 : IDeliveryDriver
    {
        public int Distance { get; set; }
        public double Weight { get; set; }

        public double CalculateRate(int distance, double weight)
        {
            Distance = distance;
            Weight = weight;
            double rate = (Weight * .050) * Distance;
            return rate;

        }
        public string GetName()
        {
            return "SPU (2-Day Business)      ";
        }

    }
}
