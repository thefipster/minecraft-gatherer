using System.Threading.Tasks;

namespace TheFipster.Minecraft.Core.Abstractions
{
    public interface IRconService
    {
        Task<string> SendAsync(string command);
    }
}
