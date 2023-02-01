using AccountTransaction.Transaction.API.DTO.Request;
using AccountTransaction.Transaction.API.Models;

namespace AccountTransaction.Transaction.API.Services.Interface
{
    public interface ITransactionService
    {
        Task<Transacao> Create(TransactionAddRequestDTO accountAddRequestDTO);
        Task<Transacao> FindByContaAndAgencia(TransactionBaseRequestDTO accountBaseRequestDTO);
        Task<List<Transacao>> FindAll();
        Task<Transacao> Update(TransactionUpdateRequestDTO accountUpdateRequestDTO);
    }
}
