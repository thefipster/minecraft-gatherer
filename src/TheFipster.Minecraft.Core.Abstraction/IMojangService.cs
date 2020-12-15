using System.Threading.Tasks;
using TheFipster.Minecraft.Core.Domain;

namespace TheFipster.Minecraft.Core.Abstractions
{
    public interface IMojangService
    {
        Task<MojangAccount> VerifyAccountAsync(string username, string password);
    }
}
