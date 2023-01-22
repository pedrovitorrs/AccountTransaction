using AccountTransaction.Core.DTO;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AccountTransaction.WebUI.Configuration.Settings;
using AccountTransaction.WebAPI.Core.User;
using AccountTransaction.WebUI.Models;
using AccountTransaction.WebUI.DTO;
using AccountTransaction.WebUI.Services.Interface;

namespace AccountTransaction.WebUI.Services.Implementation
{
    public class AuthService : Service, IAuthService
    {
        private readonly HttpClient _httpClient;

        private readonly IAspNetUser _user;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(HttpClient httpClient,
                                   IOptions<ApplicationSettings> settings,
                                   IAspNetUser user,
                                   IHttpContextAccessor httpContextAccessor)
        {
            httpClient.BaseAddress = new Uri(settings.Value.AuthUrl);

            _httpClient = httpClient;
            _user = user;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UserLoginResponse> Login(UserLoginViewModel userLogin)
        {
            var loginContent = GetContent(userLogin);

            var response = await _httpClient.PostAsync("/api/identity/auth", loginContent);

            if (!ManageResponseErrors(response))
            {
                return new UserLoginResponse
                {
                    ResponseResult = await DeserializeResponse<ResponseResult>(response)
                };
            }

            return await DeserializeResponse<UserLoginResponse>(response);
        }

        public async Task<UserLoginResponse> Register(UserRegisterViewModel userRegister)
        {
            var registroContent = GetContent(userRegister);

            var Response = await _httpClient.PostAsync("/api/identity/new-account", registroContent);

            if (!ManageResponseErrors(Response))
            {
                return new UserLoginResponse
                {
                    ResponseResult = await DeserializeResponse<ResponseResult>(Response)
                };
            }

            return await DeserializeResponse<UserLoginResponse>(Response);
        }

        public async Task<UserLoginResponse> UseRefreshToken(string refreshToken)
        {
            var refreshTokenContent = GetContent(refreshToken);

            var Response = await _httpClient.PostAsync("/api/identity/refresh-token", refreshTokenContent);

            if (!ManageResponseErrors(Response))
            {
                return new UserLoginResponse
                {
                    ResponseResult = await DeserializeResponse<ResponseResult>(Response)
                };
            }

            return await DeserializeResponse<UserLoginResponse>(Response);
        }

        public async Task DoLogin(UserLoginResponse resposta)
        {
            var token = FormatToken(resposta.AccessToken);

            var claims = new List<Claim>();
            claims.Add(new Claim("JWT", resposta.AccessToken));
            claims.Add(new Claim("RefreshToken", resposta.RefreshToken));
            claims.AddRange(token.Claims);

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8),
                IsPersistent = true
            };

            await _httpContextAccessor.HttpContext!.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        public async Task Logout()
        {
            await _httpContextAccessor.HttpContext!.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                null);
        }

        public static JwtSecurityToken FormatToken(string jwtToken)
        {
            return new JwtSecurityTokenHandler().ReadToken(jwtToken) as JwtSecurityToken;
        }

        public bool ExpiredToken()
        {
            var jwt = _user.GetUserToken();
            if (jwt is null) return false;

            var token = FormatToken(jwt);
            return token.ValidTo.ToLocalTime() < DateTime.Now;
        }

        public async Task<bool> ValidRefreshToken()
        {
            var resposta = await UseRefreshToken(_user.GetUserRefreshToken());

            if (resposta.AccessToken != null && resposta.ResponseResult == null)
            {
                await DoLogin(resposta);
                return true;
            }

            return false;
        }
    }
}
