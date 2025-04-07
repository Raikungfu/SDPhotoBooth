using BusinessLogic.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }


        [HttpGet]
        [Authorize("Manager")]
        public async Task<IActionResult> GetAllTransactions()
        {
            var transactions = await transactionService.GetAllTransactions();
            if (transactions == null)
            {
                return NotFound();
            }
            return Ok(transactions);
        }

        [HttpGet("mobile")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetTransaction()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var transaction = await transactionService.GetTransactionByCusId(userId);
            if (transaction == null)
            {
                return NotFound();
            }
            return Ok(transaction);
        }
    }
}
