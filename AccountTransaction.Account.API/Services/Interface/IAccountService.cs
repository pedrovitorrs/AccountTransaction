using AccountTransaction.Account.API.DTO.Request;
using AccountTransaction.Account.API.Models;
using AccountTransaction.Commom.Core.PagedList;

namespace AccountTransaction.Account.API.Services.Interface
{
    public interface IAccountService : IService
    {
        Task<Conta> Create(AccountAddRequestDTO accountAddRequestDTO);
        Task<Conta> FindByContaAndAgencia(AccountBaseRequestDTO accountBaseRequestDTO);
        Task<PagedResult<Conta>> FindAll(AccountFindAllRequestDTO accountSearchRequestDTO, int pagesize, int page);
        Task<Conta> Update(AccountUpdateRequestDTO accountUpdateRequestDTO);
    }
}
