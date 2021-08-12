using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class CateringItem
    {
        // This class should contain the definition for one catering item
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public int Quantity { get; set; }
        public int AmountBought { get; set; }
       
        public override string ToString()
        {
            if(Quantity == 0)
            {
                return ($"{Code} {Name} {Price} SOLD OUT");
            }

            return ($"{Code} {Name} {Price} {Quantity}");
        }
    }

}
