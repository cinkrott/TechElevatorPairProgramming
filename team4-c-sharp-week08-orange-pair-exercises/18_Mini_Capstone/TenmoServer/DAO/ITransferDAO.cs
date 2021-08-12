using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface ITransferDAO
    {
        public List<Transfer> GetTransferTo(int userId);

        public List<Transfer> GetTransferFrom(int userId);



    }
}
