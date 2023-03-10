using AccountTransaction.Account.API.Configuration.DateParse;
using AccountTransaction.Account.API.Data.Repository;
using AccountTransaction.Account.API.DTO.Request;
using AccountTransaction.Account.API.Models;
using AccountTransaction.Account.API.Services.Interface;
using AccountTransaction.Account.API.Tipos;
using AccountTransaction.Commom.Core.PagedList;
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
            if (await FindByNumeroCartao(long.Parse(cardAddRequestDTO.Numero_Cartao)) != null)
            {
                LogicalException("Cartão já cadastrado.");
            }

            if (await _accountService.FindByContaAndAgencia(new AccountBaseRequestDTO() { Numero_Agencia = cardAddRequestDTO.Numero_Agencia, Numero_Conta = cardAddRequestDTO.Numero_Conta }) == null)
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

        public async Task<PagedResult<Cartao>> FindAll(CardFindAllRequestDTO model, int pagesize, int pageindex)
        {
            var cardQuery = _repository.Table.AsQueryable();

            var cards = await cardQuery
                .Where(a =>
                    (model.Numero_Cartao == null || model.Numero_Cartao == a.Numero_Cartao) &&
                    (model.CVC == null || model.CVC == a.CVC) &&
                    (model.Numero_Conta == null || model.Numero_Conta == a.Numero_Conta) &&
                    (model.Numero_Agencia == null || model.Numero_Agencia == a.Numero_Agencia) &&
                    (model.Ativo == null || model.Ativo == a.Ativo)
                )
                .OrderBy(x => x.Numero_Conta)
                .Skip(pagesize * (pageindex - 1))
                .Take(pagesize)
                .ToListAsync();

            return new PagedResult<Cartao>()
            {
                List = cards,
                TotalResults = cards.Count,
                PageIndex = pageindex,
                PageSize = pagesize
            };
        }

        public async Task<Cartao> FindByNumeroCartao(long numeroCartao)
        {
            var card = await _repository.Table.Include(card => card.Conta).Where(conta => conta.Numero_Cartao == numeroCartao).FirstOrDefaultAsync();
            return card;
        }

        public async Task<Cartao> Update(CardUpdateRequestDTO accountUpdateRequestDTO)
        {
            var card = await FindByNumeroCartao(long.Parse(accountUpdateRequestDTO.Numero_Cartao));
            if (card == null)
            {
                LogicalException("Conta e agência não encontrados.");
            }

            card.Data_Vencimento = string.IsNullOrEmpty(accountUpdateRequestDTO.Data_Vencimento) ? card.Data_Vencimento : new DateParse(accountUpdateRequestDTO.Data_Vencimento).DataParseada;
            card.Limite_Saldo_Disponivel = accountUpdateRequestDTO.Limite_Saldo_Disponivel ?? card.Limite_Saldo_Disponivel;
            card.Ativo = accountUpdateRequestDTO.Ativo ?? card.Ativo;
            card.Bloqueado = accountUpdateRequestDTO.Ativo == (int)TipoSituacaoAtividade.ATIVA ? (int)TipoSituacaoAtividade.INATIVA : (int)TipoSituacaoAtividade.ATIVA;

            var contaUpdated = await _repository.Update(card);
            await _repository.CommitAsync();
            return contaUpdated;
        }
    }
}
