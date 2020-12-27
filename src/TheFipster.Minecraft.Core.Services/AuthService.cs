using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Core.Domain;

namespace TheFipster.Minecraft.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMojangService _mojang;
        private readonly IWhitelistReader _whitelistReader;

        public AuthService(
            IMojangService mojang,
            IWhitelistReader whitelistReader)
        {
            _mojang = mojang;
            _whitelistReader = whitelistReader;
        }

        public async Task<ClaimsPrincipal> Validate(LoginRequest user)
        {
            var mojangAccount = await _mojang.VerifyAccountAsync(user.Email, user.Password);

            if (!_whitelistReader.Exists(mojangAccount.SelectedProfile.Id))
                throw new Exception("You are not whitelisted.");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, mojangAccount.SelectedProfile.Id),
                new Claim(ClaimTypes.Name, mojangAccount.SelectedProfile.Name),
                new Claim(ClaimTypes.Email, mojangAccount.User.Username)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            return new ClaimsPrincipal(identity);
        }
    }
}
