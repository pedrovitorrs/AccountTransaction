using AccountTransaction.Account.API.Data.Repository;
using AccountTransaction.Account.API.DTO.Request;
using AccountTransaction.Account.API.Models;
using AccountTransaction.Account.API.Services.Interface;
using AccountTransaction.Account.API.Tipos;
using Microsoft.EntityFrameworkCore;

namespace AccountTransaction.Account.API.Services
{
    public class AccountService : Service, IAccountService
    {
        private readonly IRepository<Conta> _repository;

        public AccountService(IRepository<Conta> repository)
        {
            _repository = repository;
        }

        public async Task<Conta> Create(AccountAddRequestDTO accountAddRequestDTO)
        {
            if (await FindByContaAndAgencia(accountAddRequestDTO) != null)
            {
                LogicalException("Conta e agência já cadastrados.");
            }

            var account = new Conta()
            {
                Numero_Conta = accountAddRequestDTO.Numero_Conta.Value,
                Numero_Agencia = accountAddRequestDTO.Numero_Agencia.Value,
                Nome_Titular = accountAddRequestDTO.Nome_Titular,
                Tipo_Conta = TipoPessoa.GetTipo(accountAddRequestDTO?.Identificador_Titular),
                Identificador_Titular = accountAddRequestDTO?.Identificador_Titular,
                Ativa = (int?)TipoSituacaoConta.ATIVA
            };

            var accountCreated = await _repository.Insert(account);
            await _repository.CommitAsync();
            return accountCreated;
        }

        public async Task<Conta> FindByContaAndAgencia(AccountBaseRequestDTO accountBaseRequestDTO)
        {
            var account = await _repository.Table.Where(conta => conta.Numero_Conta == accountBaseRequestDTO.Numero_Conta.Value && conta.Numero_Agencia == accountBaseRequestDTO.Numero_Agencia.Value).FirstOrDefaultAsync();
            return account;
        }

        public async Task<List<Conta>> FindAll()
        {
            return await _repository.Table.ToListAsync();
        }

        public async Task<Conta> Update(AccountUpdateRequestDTO accountUpdateRequestDTO)
        {
            var account = await FindByContaAndAgencia(accountUpdateRequestDTO);
            if (account == null)
            {
                LogicalException("Conta e agência não encontrados.");
            }

            account.Nome_Titular = accountUpdateRequestDTO.Nome_Titular;
            account.Identificador_Titular = accountUpdateRequestDTO.Identificador_Titular;
            account.Ativa = accountUpdateRequestDTO.Ativa;

            var contaUpdated = await _repository.Update(account);
            await _repository.CommitAsync();
            return contaUpdated;
        }

        public Task<List<Conta>> Search(Conta accountBaseRequestDTO)
        {
            throw new NotImplementedException();
        }
    }
}
