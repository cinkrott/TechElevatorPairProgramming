using System;
using System.Collections.Generic;
using System.Text;

namespace PostageCalculator.Classes
{
    class PostalServiceSecond : IDeliveryDriver
    {
        public int Distance { get; set; }
        public double Weight { get; set; }

        public double CalculateRate(int distance, double weight)
        {
            Distance = distance;
            Weight = weight;
            double rate = 0;

            if (Weight <= 2)
            {
                rate = .0035 * distance;
            }
            else if (Weight <= 8)
            {
                rate = .0040 * distance;
            }
            else if (Weight <= 15)
            {
                rate = .0047 * distance;
            }
            else if (Weight <= 48)
            {
                rate = .0195 * distance;
            }
            else if (Weight <= 128)
            {
                rate = .0450 * distance;
            }
            else
            {
                return rate = .0500 * distance;

            }
            return rate;
        }
        public string GetName()
        {
            return "Postal Service (2nd Class)";
        }
    }
}

