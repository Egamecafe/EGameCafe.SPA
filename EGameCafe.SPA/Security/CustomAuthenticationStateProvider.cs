using Blazored.LocalStorage;
using EGameCafe.SPA.Services;
using EGameCafe.SPA.Services.AccountService;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EGameCafe.SPA.Security
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ICurrentUserService _currentUserService;

        public CustomAuthenticationStateProvider(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var savedToken = await _currentUserService.GetAuthToken();

            if (string.IsNullOrWhiteSpace(savedToken))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            return new AuthenticationState(new ClaimsPrincipal( await GetPrincipleFromToken(savedToken)));
        }

        public async void MarkUserAsAuthenticated(string token)
        {
            var identity = await GetPrincipleFromToken(token);

            var user = new ClaimsPrincipal(identity);

            var authState = Task.FromResult(new AuthenticationState(user));

            NotifyAuthenticationStateChanged(authState);
        }

        public void MarkUserAsLoggedOut()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public async Task<ClaimsIdentity> GetPrincipleFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var jsonToken = tokenHandler.ReadJwtToken(token) as JwtSecurityToken;

            var claims = jsonToken.Claims.ToList();

            var userId = claims.Where(e => e.Type == "id").Select(type => type.Value).FirstOrDefault();

            await _currentUserService.SetUserId(userId);

            var role = claims.Where(e => e.Type == "role").Select((type,value) => new Claim(ClaimTypes.Role, type.Value)).ToList();

            claims.AddRange(role);
            
            return new ClaimsIdentity(claims, "apiauth");
        }

     
    }
}
