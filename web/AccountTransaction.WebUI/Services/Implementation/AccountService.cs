using AccountTransaction.WebUI.Configuration.Settings;
using AccountTransaction.WebUI.Services.Interface;
using AccountTransaction.WebUI.ViewModel.Base;
using AccountTransaction.WebUI.ViewModel.Cliente;
using Microsoft.Extensions.Options;

namespace AccountTransaction.WebUI.Services.Implementation
{
    public class AccountService : Service, IAccountService
    {
        private readonly HttpClient _httpClient;

        public AccountService(HttpClient httpClient,
            IOptions<ApplicationSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.AccountUrl);

            _httpClient = httpClient;
        }

        public async Task<ClienteIndexViewModel> GetById(Guid id)
        {
            var Response = await _httpClient.GetAsync($"/catalog/products/{id}");

            ManageResponseErrors(Response);

            return await DeserializeResponse<ClienteIndexViewModel>(Response);
        }

        public async Task<PagedViewModel<ClienteIndexViewModel>> ListAll(int pageSize, int pageIndex, string query = null)
        {
            var Response = await _httpClient.GetAsync($"/accounts?pageSize={pageSize}&page={pageIndex}&q={query}");

            ManageResponseErrors(Response);

            return await DeserializeResponse<PagedViewModel<ClienteIndexViewModel>>(Response);
        }
    }
}
