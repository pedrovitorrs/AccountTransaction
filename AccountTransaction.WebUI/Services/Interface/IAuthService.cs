using AccountTransaction.WebUI.DTO;
using AccountTransaction.WebUI.Models;

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
