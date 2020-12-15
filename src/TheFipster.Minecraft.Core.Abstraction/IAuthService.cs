using System.Security.Claims;
using System.Threading.Tasks;
using TheFipster.Minecraft.Core.Domain;

namespace TheFipster.Minecraft.Core.Abstractions
{
    public interface IAuthService
    {
        Task<ClaimsPrincipal> Validate(LoginRequest user);
    }
}
