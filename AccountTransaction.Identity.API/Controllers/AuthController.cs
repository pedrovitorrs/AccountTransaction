using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using NetDevPack.Identity.Interfaces;
using NetDevPack.Security.Jwt.Core.Interfaces;
using AccountTransaction.Identity.API.Models;

namespace AccountTransaction.Identity.API.Controllers
{
    [Route("api/identity")]
    public class AuthController : MainController
    {
        private readonly IJwtBuilder _jwtBuilder;
        public readonly SignInManager<IdentityUser> SignInManager;
        public readonly UserManager<IdentityUser> UserManager;
        public AuthController(
            IJwtBuilder jwtBuilder,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            _jwtBuilder = jwtBuilder;
            SignInManager = signInManager;
            UserManager = userManager;
        }

        [HttpPost("new-account")]
        public async Task<ActionResult> Register(NewUser newUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = newUser.Email,
                Email = newUser.Email,
                EmailConfirmed = true
            };

            var result = await UserManager.CreateAsync(user, newUser.Password);

            if (result.Succeeded)
            {
                var jwt = await _jwtBuilder
                                            .WithEmail(newUser.Email)
                                            .WithJwtClaims()
                                            .WithUserClaims()
                                            .WithUserRoles()
                                            .WithRefreshToken()
                                            .BuildUserResponse();

                return CustomResponse(jwt);
            }

            foreach (var error in result.Errors)
            {
                AddErrorToStack(error.Description);
            }

            return CustomResponse();
        }

        [HttpPost("auth")]
        public async Task<ActionResult> Login(UserLogin userLogin)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await SignInManager.PasswordSignInAsync(userLogin.Email, userLogin.Password,
                false, true);

            if (result.Succeeded)
            {

                var jwt = await _jwtBuilder
                                            .WithEmail(userLogin.Email)
                                            .WithJwtClaims()
                                            .WithUserClaims()
                                            .WithUserRoles()
                                            .WithRefreshToken()
                                            .BuildUserResponse();
                return CustomResponse(jwt);
            }

            if (result.IsLockedOut)
            {
                AddErrorToStack("User temporary blocked. Too many tries.");
                return CustomResponse();
            }

            AddErrorToStack("User or Password incorrect");
            return CustomResponse();
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult> RefreshToken([FromBody] string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
            {
                AddErrorToStack("Invalid Refresh Token");
                return CustomResponse();
            }

            var token = await _jwtBuilder.ValidateRefreshToken(refreshToken);

            if (!token.IsValid)
            {
                AddErrorToStack("Expired Refresh Token");
                return CustomResponse();
            }

            var jwt = await _jwtBuilder
                        .WithUserId(token.UserId)
                        .WithJwtClaims()
                        .WithUserClaims()
                        .WithUserRoles()
                        .WithRefreshToken()
                        .BuildUserResponse();

            return CustomResponse(jwt);
        }

        [HttpPost("validate-jwt")]
        public async Task<ActionResult> ValidateJwt([FromServices] IJwtService jwtService, [FromForm] string jwt)
        {
            var handler = new JsonWebTokenHandler();

            var result = await handler.ValidateTokenAsync(jwt, new TokenValidationParameters()
            {
                ValidIssuer = "https://localhost",
                ValidAudience = "AccountTransaction",
                ValidateAudience = true,
                ValidateIssuer = true,
                RequireSignedTokens = false,
                IssuerSigningKey = await jwtService.GetCurrentSecurityKey(),
            });

            if (!result.IsValid)
                return BadRequest();

            return Ok(result.Claims.Select(s => new { s.Key, s.Value }));
        }
    }
}