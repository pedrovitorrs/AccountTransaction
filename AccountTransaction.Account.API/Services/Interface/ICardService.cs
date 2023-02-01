using AccountTransaction.Account.API.DTO.Request;
using AccountTransaction.Account.API.Models;
using AccountTransaction.Commom.Core.PagedList;

namespace AccountTransaction.Account.API.Services.Interface
{
    public interface ICardService : IService
    {
        Task<Cartao> Create(CardAddRequestDTO cardAddRequestDTO);
        Task<Cartao> FindByNumeroCartao(CardBaseRequestDTO cardBaseRequestDTO);
        Task<PagedResult<Cartao>> FindAll(CardFindAllRequestDTO conta, int pagesize, int pageindex);
        Task<Cartao> Update(CardUpdateRequestDTO accountUpdateRequestDTO);
    }
}
