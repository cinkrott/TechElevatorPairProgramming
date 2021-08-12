using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.DAO;

using TenmoServer.Models;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]


    public class AccountController : ControllerBase
    {
        private IAccountDAO iaccountDAO;
       

        public AccountController(IAccountDAO accountDAO)
        {
            this.iaccountDAO = accountDAO;
        }

        [HttpGet]
        public ActionResult<decimal> GetBalance()
        {

            int userId = int.Parse(this.User.FindFirst("sub").Value);
            decimal balance = iaccountDAO.GetBalance(userId);
            return Ok(balance);

        }

        [HttpPost]
        public ActionResult<bool> Transfer(DataRequest dataRequest)
        {
            
            int senderId = int.Parse(this.User.FindFirst("sub").Value);
            int receiverId = dataRequest.UserId;
            decimal amount = dataRequest.AmountToTransfer;
            int senderAccount = iaccountDAO.GetAccountFromId(senderId);
            int receiverAccount = iaccountDAO.GetAccountFromId(receiverId);
            bool subtractedBalance = iaccountDAO.Subtract(senderId, amount);
            bool addedBalance = iaccountDAO.AddBalance(receiverId, amount);
            bool logged = iaccountDAO.LogTransfer(senderAccount, receiverAccount, amount);

          

            if (subtractedBalance && addedBalance && logged)
            {
                return Ok(logged);
            }
            else
            {
                return NoContent();
            }

        }      

      
    }
}
