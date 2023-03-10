using AccountTransaction.Commom.Core.PagedList;
using AccountTransaction.Transaction.API.DTO.Request;
using AccountTransaction.Transaction.API.Models;

namespace AccountTransaction.Transaction.API.Services.Interface
{
    public interface ITransactionService
    {
        Task<Transacao> Create(TransactionAddRequestDTO accountAddRequestDTO);
        Task<Transacao> FindById(Guid Id);
        Task<List<Transacao>> FindAll(TransactionFindAllRequestDTO transacao);
        Task<Transacao> Update(TransactionUpdateRequestDTO accountUpdateRequestDTO);
    }
}
