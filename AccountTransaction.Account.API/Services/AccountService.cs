using AccountTransaction.Account.API.Data.Repository;
using AccountTransaction.Account.API.DTO.Request;
using AccountTransaction.Account.API.Models;
using AccountTransaction.Account.API.Services.Interface;
using AccountTransaction.Account.API.Tipos;
using AccountTransaction.Commom.Core.PagedList;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Polly;
using Polly.Caching;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AccountTransaction.Account.API.Services
{
    public class AccountService : Service, IAccountService
    {
        private readonly IRepository<Conta> _repository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public AccountService(IRepository<Conta> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountAddRequestDTO"></param>
        /// <returns></returns>
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
                Ativa = (int?)TipoSituacaoAtividade.ATIVA
            };

            var accountCreated = await _repository.Insert(account);
            await _repository.CommitAsync();
            return accountCreated;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountBaseRequestDTO"></param>
        /// <returns></returns>
        public async Task<Conta> FindByContaAndAgencia(AccountBaseRequestDTO accountBaseRequestDTO)
        {
            var account = await _repository.Table.Include(c => c.Cartoes).Where(conta => conta.Numero_Conta == accountBaseRequestDTO.Numero_Conta.Value && conta.Numero_Agencia == accountBaseRequestDTO.Numero_Agencia.Value).FirstOrDefaultAsync();
            return account;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<PagedResult<Conta>> FindAll(AccountFindAllRequestDTO model, int pagesize, int pageindex)
        {
            var accountsQuery = _repository.Table.AsQueryable();

            var accounts = await accountsQuery
                .Include(c => c.Cartoes)
                .Where(a =>
                    (string.IsNullOrEmpty(model.Tipo_Conta) || a.Tipo_Conta == model.Tipo_Conta) &&
                    (model.Numero_Conta == null || model.Numero_Conta == a.Numero_Conta) &&
                    (model.Numero_Agencia == null || model.Numero_Agencia == a.Numero_Agencia) &&
                    (model.Ativa == null || model.Ativa == a.Ativa) &&
                    (string.IsNullOrEmpty(model.Identificador_Titular) || a.Identificador_Titular.ToLower().Contains(model.Identificador_Titular.ToLower())) &&
                    (string.IsNullOrEmpty(model.Nome_Titular) || a.Nome_Titular.ToLower().Contains(model.Nome_Titular.ToLower()))
                )
                .OrderBy(x => x.Numero_Conta)
                .Skip(pagesize * (pageindex - 1))
                .Take(pagesize)
                .ToListAsync();

            return new PagedResult<Conta>()
            {
                List = accounts,
                TotalResults = accounts.Count,
                PageIndex = pageindex,
                PageSize = pagesize
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountUpdateRequestDTO"></param>
        /// <returns></returns>
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
    }
}
