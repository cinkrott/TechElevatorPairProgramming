using System;
using System.Collections.Generic;
using System.Text;

namespace PostageCalculator.Classes
{
    class PostalServiceFirst : IDeliveryDriver
    {
        public int Distance { get; set; }
        public double Weight { get; set; }

        public double CalculateRate(int distance, double weight)
        {
            Distance = distance;
            Weight = weight;
            double rate = 0;
            
            if(Weight <= 2)
            {
                rate = .035 * distance;
            }
            else if (Weight <= 8)
            {
                rate = .040 * distance;
            }
            else if (Weight <= 15)
            {
                rate = .047 * distance;
            }
            else if (Weight <= 48)
            {
                rate = .195 * distance;
            }
            else if (Weight <= 128)
            {
                rate = .450 * distance;
            }
            else
            {
            return rate = .500 * distance;

            }
            return rate;
        }

        public string GetName()
        {
            return "Postal Service (1st Class)";
        }
    }
}
