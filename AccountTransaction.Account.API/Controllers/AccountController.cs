using AccountTransaction.Account.API.Data.Repository;
using AccountTransaction.Account.API.DTO.Request;
using AccountTransaction.Account.API.Models;
using AccountTransaction.Account.API.Services.Interface;
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

        [HttpGet("account/findbycontaandagencia")]
        public async Task<ActionResult<Conta>> Find([FromQuery] AccountFindByIdRequestDTO accountFindByIdRequestDTO)
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

        [HttpGet("account/findall")]
        public async Task<ActionResult<List<Conta>>> FindAll()
        {
            try
            {
                var accounts = await _accountService.FindAll();
                if (accounts == null) return NotFound();
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return TratarException(ex);
            }
        }

        [HttpPost("account/add")]
        public async Task<ActionResult<Conta>> Add([FromBody] AccountAddRequestDTO accountAddRequestDTO)
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

        [HttpPut("account/update")]
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
