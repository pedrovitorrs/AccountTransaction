using AccountTransaction.Account.API.DTO.Request;
using AccountTransaction.Account.API.Models;

namespace AccountTransaction.Account.API.Services.Interface
{
    public interface ICardService : IService
    {
        Task<Cartao> Create(CardAddRequestDTO cardAddRequestDTO);
        Task<Cartao> FindByNumeroCartao(CardBaseRequestDTO cardBaseRequestDTO);
        Task<Cartao> FindAll();
        Task<List<Cartao>> Search(CardSearchRequestDTO conta);    
        Task<Cartao> Update(CardUpdateRequestDTO accountUpdateRequestDTO);
    }
}
