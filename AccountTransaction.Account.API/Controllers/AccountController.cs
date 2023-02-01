using AccountTransaction.Account.API.Data.Repository;
using AccountTransaction.Account.API.DTO.Request;
using AccountTransaction.Account.API.Models;
using AccountTransaction.Account.API.Services.Interface;
using AccountTransaction.Commom.Core.PagedList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace AccountTransaction.Account.API.Controllers
{
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("accounts/{numero_agencia?}/{numero_conta?}")]
        public async Task<ActionResult<Conta>> FindByContaAndAgencia(AccountFindByContaAndAgenciaRequestDTO accountFindByIdRequestDTO)
        {
            try
            {
                var account = await _accountService.FindByContaAndAgencia(accountFindByIdRequestDTO);
                if (account == null) return NotFound();
                return Ok(account);
            }
            catch (Exception ex)
            {
                return TratarException(ex);
            }
        }

        [HttpGet("accounts")]
        public async Task<ActionResult<PagedResult<Conta>>> FindAll([FromQuery] AccountFindAllRequestDTO accountSearchRequestDTO, [FromQuery] int pagesize = 10, [FromQuery] int page = 1)
        {
            try
            {
                var accounts = await _accountService.FindAll(accountSearchRequestDTO, pagesize, page);
                if (accounts == null) return NotFound();
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return TratarException(ex);
            }
        }

        [HttpPost("accounts")]
        public async Task<ActionResult<Conta>> Create([FromBody] AccountAddRequestDTO accountAddRequestDTO)
        {
            try
            {
                var accountsCreated = await _accountService.Create(accountAddRequestDTO);
                if (accountsCreated == null) return NotFound();
                return Ok(accountsCreated);
            }
            catch (Exception ex)
            {
                return TratarException(ex);
            }
        }

        [HttpPut("accounts")]
        public async Task<ActionResult<Conta>> Update([FromBody] AccountUpdateRequestDTO accountUpdateRequestDTO)
        {
            try
            {
                var accountUpdated = await _accountService.Update(accountUpdateRequestDTO);
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
