using AccountTransaction.Account.API.Data.Repository;
using AccountTransaction.Account.API.DTO.Request;
using AccountTransaction.Account.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountTransaction.Account.API.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRepository<Conta> _repository;

        public AccountController(IRepository<Conta> repository)
        {
            _repository = repository;
        }

        [HttpGet("find")]
        public async Task<ActionResult<Conta>> Find([FromQuery] AccountFindByIdRequestDTO accountFindByIdRequestDTO)
        {
            var account = await _repository.Table.Where(conta => conta.Numero_Conta == accountFindByIdRequestDTO.Numero_Conta && conta.Numero_Agencia == accountFindByIdRequestDTO.Numero_Agencia).FirstOrDefaultAsync();
            if (account == null) return NotFound();
            return Ok(account);
        }

        [HttpGet("findAll")]
        public async Task<ActionResult<List<Conta>>> FindAll()
        {
            var contas = await _repository.FindAll();
            if (contas == null) return NotFound();
            return Ok(contas);
        }

        [HttpPost("add")]
        public async Task<ActionResult<AccountAddRequestDTO>> Add([FromBody] AccountAddRequestDTO accountAddRequestDTO)
        {
            var contaCreated = new Conta()
            {
                Numero_Conta = accountAddRequestDTO.Numero_Conta,
                Numero_Agencia = accountAddRequestDTO.Numero_Agencia,
                Nome_Titular = accountAddRequestDTO.Nome_Titular,
                Tipo_Conta = accountAddRequestDTO.Identificador_Titular.Length > 14 ? "J" : "F",
                Identificador_Titular = accountAddRequestDTO.Identificador_Titular,
                Ativa = 1
            };
            await _repository.Insert(contaCreated);
            await _repository.CommitAsync();

            if (contaCreated == null) return NotFound();
            return Ok(accountAddRequestDTO);
        }

        [HttpPut("update")]
        public async Task<ActionResult<AccountUpdateRequestDTO>> Update([FromBody] AccountUpdateRequestDTO accountUpdateRequestDTO)
        {
            var account = await _repository.Table.Where(conta => conta.Numero_Conta == accountUpdateRequestDTO.Numero_Conta && conta.Numero_Agencia == accountUpdateRequestDTO.Numero_Agencia).FirstOrDefaultAsync();
            if (account == null) return NotFound();

            account.Nome_Titular = accountUpdateRequestDTO.Nome_Titular;
            account.Identificador_Titular = accountUpdateRequestDTO.Identificador_Titular;
            account.Ativa = accountUpdateRequestDTO.Ativa;

            var contaUpdated = await _repository.Update(account);
            await _repository.CommitAsync();

            if (contaUpdated == null) return NotFound();
            return Ok(contaUpdated);
        }
    }
}
