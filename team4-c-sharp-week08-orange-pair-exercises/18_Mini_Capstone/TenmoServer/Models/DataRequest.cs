using System;
using System.Collections.Generic;
using System.Text;

namespace TenmoServer.Models
{
    public class DataRequest
    {
        public int UserId { get; set; }
        public decimal AmountToTransfer { get; set; }

    }
}
