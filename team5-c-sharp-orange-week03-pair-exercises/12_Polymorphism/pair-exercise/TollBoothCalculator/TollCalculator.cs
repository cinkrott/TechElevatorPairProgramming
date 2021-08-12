using System;
using System.Collections.Generic;
using System.Text;

namespace TollBoothCalculator.Classes

{
    class TollCalculator
    {
        static void Main(string[] args)
        {
            //List of interface vehicles
            List<IVehicle> vehicles = new List<IVehicle>();

            vehicles.Add(new Car(false));
            vehicles.Add(new Car(true));
            vehicles.Add(new Tank());
            vehicles.Add(new Truck(4));
            vehicles.Add(new Truck(6));
            vehicles.Add(new Truck(8));

            int totalDistance = 0;
            decimal totalToll = 0;

            Console.WriteLine("Vehicle                        Distance Traveled       Toll $");
            Console.WriteLine("-------------------------------------------------------------");

            foreach (IVehicle vehicle in vehicles)
            {
                Console.WriteLine(vehicle.GetName()+"             "+vehicle.GetRandomMiles()+"                    $"+(decimal)vehicle.CalculateToll(vehicle.GetRandomMiles()));
                totalDistance += vehicle.GetRandomMiles();
                totalToll += (decimal)vehicle.CalculateToll(vehicle.GetRandomMiles());
            }
            Console.WriteLine("");
            Console.WriteLine("Total Miles Traveled:          " + totalDistance);
            Console.WriteLine("Total Tollbooth Revenue:       $" + (decimal)totalToll);
        }
    }
}
