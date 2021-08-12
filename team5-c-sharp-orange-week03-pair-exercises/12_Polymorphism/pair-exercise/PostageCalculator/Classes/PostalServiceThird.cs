using System;
using System.Collections.Generic;
using System.Text;

namespace PostageCalculator.Classes
{
    class PostalServiceThird : IDeliveryDriver
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
                rate = .0020 * distance;
            }
            else if (Weight <= 8)
            {
                rate = .0022 * distance;
            }
            else if (Weight <= 15)
            {
                rate = .0024 * distance;
            }
            else if (Weight <= 48)
            {
                rate = .0150 * distance;
            }
            else if (Weight <= 128)
            {
                rate = .0160 * distance;
            }
            else
            {
                return rate = .0170 * distance;

            }
            return rate;
        }
        public string GetName()
        {
            return "Postal Service (3rd Class)";
        }
    }
}

