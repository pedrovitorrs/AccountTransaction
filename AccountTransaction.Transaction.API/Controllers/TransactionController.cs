using AccountTransaction.Commom.Core.PagedList;
using AccountTransaction.MessageBus;
using AccountTransaction.MessageBus.RabbitMQSender;
using AccountTransaction.Transaction.API.DTO.Request;
using AccountTransaction.Transaction.API.Models;
using AccountTransaction.Transaction.API.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AccountTransaction.Transaction.API.Controllers
{
    [ApiController]
    public class TransactionController : BaseController
    {
        private readonly ITransactionService _transactionService;
        private readonly IRabbitMQMessageSender _rabbitMessageSender;
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(ITransactionService transactionService, IRabbitMQMessageSender rabbitMessageSender, ILogger<TransactionController> logger)
        {
            _logger = logger;
            _transactionService = transactionService;
            _rabbitMessageSender = rabbitMessageSender;
        }

        [HttpGet("transactions/{id?}")]
        public async Task<ActionResult<Transacao>> Find([FromRoute] TransactionBaseRequestDTO accountFindByIdRequestDTO)
        {
            try
            {
                var account = await _transactionService.FindById(accountFindByIdRequestDTO.Id);
                if (account == null) return NotFound();
                return Ok(account);
            }
            catch (Exception ex)
            {
                return TratarException(ex);
            }
        }

        [HttpGet("transactions")]
        public async Task<ActionResult<PagedResult<Transacao>>> FindAll([FromQuery] TransactionFindAllRequestDTO transactionFindAllRequestDTO, [FromQuery] int pagesize = 10, [FromQuery] int page = 1)
        {
            try
            {
                var accounts = await _transactionService.FindAll(transactionFindAllRequestDTO, pagesize, page);
                if (accounts == null) return NotFound();
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return TratarException(ex);
            }
        }

        [HttpPost("transactions")]
        public async Task<ActionResult> Add([FromBody] TransactionAddRequestDTO accountAddRequestDTO)
        {
            try
            {
                _rabbitMessageSender.SendMessage<TransactionAddRequestDTO>(accountAddRequestDTO, Routing_Keys.TRANSACTION_CREATED);
                return Ok();
            }
            catch (Exception ex)
            {
                return TratarException(ex);
            }
        }

        [HttpPut("transactions")]
        public async Task<ActionResult> Update([FromBody] TransactionAddRequestDTO accountUpdateRequestDTO)
        {
            try
            {
                _rabbitMessageSender.SendMessage<TransactionAddRequestDTO>(accountUpdateRequestDTO, Routing_Keys.TRANSACTION_CREATED);
                return Ok();
            }
            catch (Exception ex)
            {
                return TratarException(ex);
            }
        }
    }
}
