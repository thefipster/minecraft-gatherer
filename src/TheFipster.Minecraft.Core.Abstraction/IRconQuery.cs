using System.Threading.Tasks;

namespace TheFipster.Minecraft.Core.Abstractions
{
    public interface IRconQuery<T>
    {
        Task<T> ExecuteAsync();
    }
}
