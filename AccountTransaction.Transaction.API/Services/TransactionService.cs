using AccountTransaction.Transaction.API.DTO.Request;
using AccountTransaction.Transaction.API.Models;
using AccountTransaction.Transaction.API.Services.Interface;

namespace AccountTransaction.Transaction.API.Services
{
    public class TransactionService : Service, ITransactionService
    {
        public Task<Transacao> Create(TransactionAddRequestDTO accountAddRequestDTO)
        {
            throw new NotImplementedException();
        }

        public Task<List<Transacao>> FindAll()
        {
            throw new NotImplementedException();
        }

        public Task<Transacao> FindByContaAndAgencia(TransactionBaseRequestDTO accountBaseRequestDTO)
        {
            throw new NotImplementedException();
        }

        public Task<Transacao> Update(TransactionUpdateRequestDTO accountUpdateRequestDTO)
        {
            throw new NotImplementedException();
        }
    }
}
