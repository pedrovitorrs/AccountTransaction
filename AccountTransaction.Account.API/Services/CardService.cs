using AccountTransaction.Account.API.DTO.Request;
using AccountTransaction.Account.API.Models;
using AccountTransaction.Account.API.Services.Interface;

namespace AccountTransaction.Account.API.Services
{
    public class CardService : Service, ICardService
    {
        public Task<Cartao> Create(CardAddRequestDTO cardAddRequestDTO)
        {
            throw new NotImplementedException();
        }

        public Task<Cartao> FindAll()
        {
            throw new NotImplementedException();
        }

        public Task<Cartao> FindByNumeroCartao(CardBaseRequestDTO cardBaseRequestDTO)
        {
            throw new NotImplementedException();
        }

        public Task<List<Cartao>> Search(Cartao conta)
        {
            throw new NotImplementedException();
        }

        public Task<Cartao> Update(CardUpdateRequestDTO accountUpdateRequestDTO)
        {
            throw new NotImplementedException();
        }
    }
}
