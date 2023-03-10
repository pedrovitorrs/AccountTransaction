using AccountTransaction.WebUI.DTO;
using AccountTransaction.WebUI.ViewModel.User;

namespace AccountTransaction.WebUI.Services.Interface
{
    public interface IAuthService
    {
        Task<UserLoginResponse> Login(UserLoginViewModel userLogin);

        Task<UserLoginResponse> Register(UserRegisterViewModel userRegister);

        Task DoLogin(UserLoginResponse resposta);
        Task Logout();

        bool ExpiredToken();

        Task<bool> ValidRefreshToken();
    }
}
