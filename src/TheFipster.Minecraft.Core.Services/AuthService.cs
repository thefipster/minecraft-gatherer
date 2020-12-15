using Microsoft.AspNetCore.Authentication.Cookies;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Core.Domain;

namespace TheFipster.Minecraft.Core.Services
{
    public class AuthService : IAuthService
    {
        private IMojangService _mojang;

        public AuthService(IMojangService mojang)
        {
            _mojang = mojang;
        }

        public async Task<ClaimsPrincipal> Validate(LoginRequest user)
        {
            var mojangAccunt = await _mojang.VerifyAccountAsync(user.Email, user.Password);

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, mojangAccunt.SelectedProfile.Id));
            claims.Add(new Claim(ClaimTypes.Name, mojangAccunt.SelectedProfile.Name));
            claims.Add(new Claim(ClaimTypes.Email, mojangAccunt.User.Username));

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            return new ClaimsPrincipal(identity);
        }
    }
}
