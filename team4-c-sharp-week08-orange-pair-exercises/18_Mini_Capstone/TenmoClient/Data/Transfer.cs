using System;
using System.Collections.Generic;
using System.Text;

namespace TenmoClient.Data
{
    public class Transfer
    {
        public int TransferId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public int AccountFrom { get; set; }
        public int AccountTo { get; set; }
        public int UserId { get; set; }
        public string TransferStatus { get; set; }
        public string TransferType { get; set; }



    }
}
