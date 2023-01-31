using AccountTransaction.Account.API.DTO.Request;
using AccountTransaction.Account.API.Models;
using AccountTransaction.Account.API.Services.Interface;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace AccountTransaction.Account.API.Controllers
{
    [ApiController]
    public class CardController : BaseController
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpPost("card/add")]
        public async Task<ActionResult<Cartao>> Add([FromBody] CardAddRequestDTO cardAddRequestDTO)
        {
            try
            {
                var accountsCreated = await _cardService.Create(cardAddRequestDTO);
                if (accountsCreated == null) return NotFound();
                return Ok(accountsCreated);
            }
            catch (Exception ex)
            {
                return TratarException(ex);
            }
        }

        [HttpPut("card/update")]
        public async Task<ActionResult<Cartao>> Update([FromBody] CardUpdateRequestDTO accountUpdateRequestDTO)
        {
            try
            {
                var accountUpdated = await _cardService.Update(accountUpdateRequestDTO);
                if (accountUpdated == null) return NotFound();
                return Ok(accountUpdated);
            }catch(Exception ex)
            {
                return TratarException(ex);
            }
        }
    }
}
