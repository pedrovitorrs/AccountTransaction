using AccountTransaction.Commom.Core.PagedList;
using AccountTransaction.Transaction.API.DTO.Request;
using AccountTransaction.Transaction.API.Models;

namespace AccountTransaction.Transaction.API.Services.Interface
{
    public interface ITransactionService
    {
        Task<Transacao> Create(TransactionAddRequestDTO accountAddRequestDTO);
        Task<Transacao> FindById(Guid Id);
        Task<PagedResult<Transacao>> FindAll(TransactionFindAllRequestDTO transacao, int pagesize, int pageindex);
        Task<Transacao> Update(TransactionUpdateRequestDTO accountUpdateRequestDTO);
    }
}
