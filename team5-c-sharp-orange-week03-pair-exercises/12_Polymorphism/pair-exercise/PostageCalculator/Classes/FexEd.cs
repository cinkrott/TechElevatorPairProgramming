using System;
using System.Collections.Generic;
using System.Text;

namespace PostageCalculator.Classes
{
    class FexEd : IDeliveryDriver
    {
        public int Distance { get; set; }
        public double Weight { get; set; }

        public double CalculateRate(int distance, double weight)
        {
            Distance = distance;
            Weight = weight;
            double rate = 20.00;

            if (Distance > 500)
            {
               rate = rate + 5.00;
            }
            if (Weight > 48)
            {
                rate = rate + 3.00;
            }
            return rate;
        }

        public string GetName()
        {
            return "FexEd                     ";
        }
    }
}