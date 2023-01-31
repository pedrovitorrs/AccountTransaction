using AccountTransaction.Account.API.DTO.Request;
using AccountTransaction.Account.API.Models;

namespace AccountTransaction.Account.API.Services.Interface
{
    public interface IAccountService : IService
    {
        Task<Conta> Create(AccountAddRequestDTO accountAddRequestDTO);
        Task<Conta> FindByContaAndAgencia(AccountBaseRequestDTO accountBaseRequestDTO);
        Task<List<Conta>> FindAll();
        Task<List<Conta>> Search(AccountSearchRequestDTO accountSearchRequestDTO);
        Task<Conta> Update(AccountUpdateRequestDTO accountUpdateRequestDTO);
    }
}
