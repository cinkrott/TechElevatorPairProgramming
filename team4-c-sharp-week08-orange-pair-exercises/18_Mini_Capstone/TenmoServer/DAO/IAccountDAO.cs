using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface IAccountDAO
    {
        public decimal GetBalance(int userId);
        public bool AddBalance(int auserId, decimal amountToAdd);

        public bool Subtract(int userId, decimal amountToSubtract);
        public int GetAccountFromId(int userId);
        public bool LogTransfer(int senderAccount, int receiverAccount, decimal amount);
        


    }
}
