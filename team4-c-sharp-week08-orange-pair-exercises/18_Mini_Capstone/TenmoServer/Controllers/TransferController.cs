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
    public class TransferController : ControllerBase
    {
        private ITransferDAO iTransferDAO;
       
        public TransferController(ITransferDAO iTransferDAO)
        {
            this.iTransferDAO = iTransferDAO; 
        }

        [HttpGet]
        public ActionResult<List<Transfer>> GetTransferTo()
        {
            int userId = int.Parse(this.User.FindFirst("sub").Value);
            List<Transfer> transfers = iTransferDAO.GetTransferTo(userId);

            if (transfers.Count > 0)
            {
                return Ok(transfers);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("transfername")]
        public ActionResult<List<Transfer>> GetTransferFrom()
        {
            int userId = int.Parse(this.User.FindFirst("sub").Value);
            List<Transfer> transfers = iTransferDAO.GetTransferFrom(userId);

            if (transfers.Count > 0)
            {
                return Ok(transfers);
            }
            else
            {
                return NotFound();
            }
        }


    }
}
