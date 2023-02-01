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

        public TransactionController(ITransactionService transactionService, IRabbitMQMessageSender rabbitMessageSender)
        {
            _transactionService = transactionService;
            _rabbitMessageSender = rabbitMessageSender;
        }

        [HttpGet("transaction/findbyid")]
        public async Task<ActionResult<Transacao>> Find([FromQuery] TransactionBaseRequestDTO accountFindByIdRequestDTO)
        {
            try
            {
                var account = await _transactionService.FindByContaAndAgencia(accountFindByIdRequestDTO);
                if (account == null) return NotFound();
                return Ok(account);
            }
            catch (Exception ex)
            {
                return TratarException(ex);
            }
        }

        [HttpGet("transaction/findall")]
        public async Task<ActionResult<List<Transacao>>> FindAll()
        {
            try
            {
                var accounts = await _transactionService.FindAll();
                if (accounts == null) return NotFound();
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return TratarException(ex);
            }
        }

        [HttpPost("transaction/add")]
        public async Task<ActionResult<Transacao>> Add([FromBody] TransactionAddRequestDTO accountAddRequestDTO)
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

        [HttpPut("transaction/update")]
        public async Task<ActionResult<Transacao>> Update([FromBody] TransactionUpdateRequestDTO accountUpdateRequestDTO)
        {
            try
            {
                var accountUpdated = await _transactionService.Update(accountUpdateRequestDTO);
                if (accountUpdated == null) return NotFound();
                return Ok(accountUpdated);
            }
            catch (Exception ex)
            {
                return TratarException(ex);
            }
        }
    }
}
