using AccountTransaction.WebUI.Configuration.Settings;
using AccountTransaction.WebUI.Services.Interface;
using AccountTransaction.WebUI.ViewModel.Base;
using AccountTransaction.WebUI.ViewModel.Cliente;
using AccountTransaction.WebUI.ViewModel.Transaction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AccountTransaction.WebUI.Services.Implementation
{
    public class TransactionService : Service, ITransactionService
    {
        private readonly HttpClient _httpClient;

        public TransactionService(HttpClient httpClient,
            IOptions<ApplicationSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.TransactionUrl);
            _httpClient = httpClient;
        }

        public async Task<List<TransactionViewModel>> ListAll()
        {
            var Response = await _httpClient.GetAsync($"/transactions");

            ManageResponseErrors(Response);

            return await DeserializeResponse<List<TransactionViewModel>>(Response);
        }
    }
}
