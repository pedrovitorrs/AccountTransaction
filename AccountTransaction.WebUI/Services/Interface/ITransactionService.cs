using AccountTransaction.WebUI.ViewModel.Base;
using AccountTransaction.WebUI.ViewModel.Cliente;
using AccountTransaction.WebUI.ViewModel.Transaction;

namespace AccountTransaction.WebUI.Services.Interface
{
    public interface ITransactionService
    {
        Task<List<TransactionViewModel>> ListAll();
    }
}
