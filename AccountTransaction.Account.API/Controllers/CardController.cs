using AccountTransaction.Account.API.DTO.Request;
using AccountTransaction.Account.API.Models;
using AccountTransaction.Account.API.Services;
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

        [HttpGet("card/findbycontaandagencia")]
        public async Task<ActionResult<Cartao>> Find([FromQuery] CardBaseRequestDTO cardBaseRequestDTO)
        {
            try
            {
                var card = await _cardService.FindByNumeroCartao(cardBaseRequestDTO);
                if (card == null) return NotFound();
                return Ok(card);
            }
            catch (Exception ex)
            {
                return TratarException(ex);
            }
        }

        [HttpGet("card/findall")]
        public async Task<ActionResult<List<Cartao>>> FindAll()
        {
            try
            {
                var cards = await _cardService.FindAll();
                if (cards == null) return NotFound();
                return Ok(cards);
            }
            catch (Exception ex)
            {
                return TratarException(ex);
            }
        }

        [HttpPost("card/search")]
        public async Task<ActionResult<Cartao>> Search([FromBody] CardSearchRequestDTO accountSearchRequestDTO)
        {
            try
            {
                var account = await _cardService.Search(accountSearchRequestDTO);
                if (account == null) return NotFound();
                return Ok(account);
            }
            catch (Exception ex)
            {
                return TratarException(ex);
            }
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
