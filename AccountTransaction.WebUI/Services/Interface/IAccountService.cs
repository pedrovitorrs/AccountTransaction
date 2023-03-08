using AccountTransaction.WebUI.ViewModel.Base;
using AccountTransaction.WebUI.ViewModel.Cliente;

namespace AccountTransaction.WebUI.Services.Interface
{
    public interface IAccountService
    {
        Task<PagedViewModel<ClienteIndexViewModel>> ListAll(int pageSize, int pageIndex, string query = null);
        Task<ClienteIndexViewModel> GetById(Guid id);
    }
}
