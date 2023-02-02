using AccountTransaction.Account.API.DTO.Request;
using AccountTransaction.Account.API.Models;
using AccountTransaction.Account.API.Services;
using AccountTransaction.Account.API.Services.Interface;
using AccountTransaction.Commom.Core.PagedList;
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

        [HttpGet("cards/{numero_cartao?}")]
        public async Task<ActionResult<Cartao>> Find([FromRoute] CardBaseRequestDTO cardBaseRequestDTO)
        {
            try
            {
                var card = await _cardService.FindByNumeroCartao(long.Parse(cardBaseRequestDTO.Numero_Cartao));
                if (card == null) return NotFound();
                return Ok(card);
            }
            catch (Exception ex)
            {
                return TratarException(ex);
            }
        }

        [HttpGet("cards")]
        public async Task<ActionResult<PagedResult<Cartao>>> FindAll([FromQuery] CardFindAllRequestDTO accountSearchRequestDTO, [FromQuery] int pagesize = 10, [FromQuery] int page = 1)
        {
            try
            {
                var cards = await _cardService.FindAll(accountSearchRequestDTO, pagesize, page);
                if (cards == null) return NotFound();
                return Ok(cards);
            }
            catch (Exception ex)
            {
                return TratarException(ex);
            }
        }

        [HttpPost("cards")]
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

        [HttpPut("cards")]
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
