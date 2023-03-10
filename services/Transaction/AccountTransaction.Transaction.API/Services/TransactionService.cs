using AccountTransaction.Commom.Core.PagedList;
using AccountTransaction.Transaction.API.Data.Repository;
using AccountTransaction.Transaction.API.DTO.Request;
using AccountTransaction.Transaction.API.Models;
using AccountTransaction.Transaction.API.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace AccountTransaction.Transaction.API.Services
{
    public class TransactionService : Service, ITransactionService
    {
        private readonly IRepository<Transacao> _repository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public TransactionService(IRepository<Transacao> repository)
        {
            _repository = repository;
        }

        public async Task<Transacao> Create(TransactionAddRequestDTO accountAddRequestDTO)
        {
            var transacao = new Transacao()
            {
                Numero_Cartao = long.Parse(accountAddRequestDTO.Numero_Cartao),
                Id_Aprovacao = Guid.NewGuid(),
                Valor_Transacao = decimal.Parse(accountAddRequestDTO.Valor_Transacao),
                Data_Transacao = DateTime.Now,
            };

            var transacaoCreated = await _repository.Insert(transacao);
            await _repository.CommitAsync();
            return transacaoCreated;
        }

        public async Task<List<Transacao>> FindAll(TransactionFindAllRequestDTO transacao)
        {
            var transactionQuery = _repository.Table.AsQueryable();

            return await transactionQuery
                .OrderBy(x => x.Id)
                .ToListAsync();
        }

        public async Task<Transacao> FindById(Guid Id)
        {
            var trasaction = await _repository.Table.Where(conta => conta.Id == Id).FirstOrDefaultAsync();
            return trasaction;
        }

        public Task<Transacao> Update(TransactionUpdateRequestDTO accountUpdateRequestDTO)
        {
            throw new NotImplementedException();
        }
    }
}
