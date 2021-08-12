using System;
using System.Collections.Generic;
using System.Text;

namespace PostageCalculator.Classes
{
    class PostageCalculator
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter the weight of the package: ");
            double weight = double.Parse(Console.ReadLine());
            Console.WriteLine(("(P)ounds of (O)unces? "));
            string weightType = (Console.ReadLine()).ToUpper();
            Console.WriteLine("What distance will it be traveling? ");
            int distance = int.Parse(Console.ReadLine());

            List<IDeliveryDriver> drivers = new List<IDeliveryDriver>();

            drivers.Add(new PostalServiceFirst());
            drivers.Add(new PostalServiceSecond());
            drivers.Add(new PostalServiceThird());
            drivers.Add(new FexEd());
            drivers.Add(new SPU4());
            drivers.Add(new SPU2());
            drivers.Add(new SPUNext());

            if(weightType == "P")
            {
                weight = weight * 16;
            }

            Console.WriteLine("Delivery Method              $ cost");
            Console.WriteLine("-----------------------------------");

            foreach(IDeliveryDriver driver in drivers)
            {
                
                Console.WriteLine(driver.GetName() + "   " + "$" + (decimal)driver.CalculateRate(distance, weight));
            }

        }
    }
}
