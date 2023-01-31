using AccountTransaction.Account.API.Configuration.DateParse;
using AccountTransaction.Account.API.Data.Repository;
using AccountTransaction.Account.API.DTO.Request;
using AccountTransaction.Account.API.Models;
using AccountTransaction.Account.API.Services.Interface;
using AccountTransaction.Account.API.Tipos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountTransaction.Account.API.Services
{
    public class CardService : Service, ICardService
    {
        private readonly IRepository<Cartao> _repository;
        private readonly IAccountService _accountService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public CardService(IRepository<Cartao> repository, IAccountService accountService)
        {
            _repository = repository;
            _accountService = accountService;
        }

        public async Task<Cartao> Create(CardAddRequestDTO cardAddRequestDTO)
        {
            if (await FindByNumeroCartao(cardAddRequestDTO) != null)
            {
                LogicalException("Cartão já cadastrado.");
            }

            if (await _accountService.FindByContaAndAgencia(new AccountBaseRequestDTO() { Numero_Agencia = cardAddRequestDTO.Numero_Agencia, Numero_Conta = cardAddRequestDTO .Numero_Conta }) == null )
            {
                LogicalException("Não foi encontrado cadastro para a agencia e conta informada.");
            }

            var account = new Cartao()
            {
                Numero_Cartao = long.Parse(cardAddRequestDTO.Numero_Cartao),
                Data_Vencimento = new DateParse(cardAddRequestDTO.Data_Vencimento).DataParseada,
                CVC = int.Parse(cardAddRequestDTO.CVC),
                Numero_Conta = cardAddRequestDTO.Numero_Conta.Value,
                Numero_Agencia = cardAddRequestDTO.Numero_Agencia.Value,
                Limite_Saldo = cardAddRequestDTO.Limite_Saldo,
                Limite_Saldo_Disponivel = cardAddRequestDTO.Limite_Saldo,
                Ativo = (int?)TipoSituacaoAtividade.ATIVA
            };

            var accountCreated = await _repository.Insert(account);
            await _repository.CommitAsync();
            return accountCreated;
        }

        public Task<Cartao> FindAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Cartao> FindByNumeroCartao(CardBaseRequestDTO cardBaseRequestDTO)
        {
            var card = await _repository.Table.Where(conta => conta.Numero_Cartao == long.Parse(cardBaseRequestDTO.Numero_Cartao)).FirstOrDefaultAsync();
            return card;
        }

        public Task<List<Cartao>> Search(CardSearchRequestDTO conta)
        {
            throw new NotImplementedException();
        }

        public Task<Cartao> Update(CardUpdateRequestDTO accountUpdateRequestDTO)
        {
            throw new NotImplementedException();
        }
    }
}
