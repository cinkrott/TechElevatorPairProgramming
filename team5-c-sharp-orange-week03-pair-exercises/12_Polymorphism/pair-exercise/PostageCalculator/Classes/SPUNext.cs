using System;
using System.Collections.Generic;
using System.Text;

namespace PostageCalculator.Classes
{
    class SPUNext : IDeliveryDriver
    {
        public int Distance { get; set; }
        public double Weight { get; set; }

        public double CalculateRate(int distance, double weight)
        {
            Distance = distance;
            Weight = weight;
            double rate = (Weight * .075) * Distance;
            return rate;
        }
        public string GetName()
        {
            return "SPU (Next Day)            ";
        }
    }
}