using System.Threading.Tasks;
using TheFipster.Minecraft.Core.Abstractions;

namespace TheFipster.Minecraft.Core.Services.Rcon
{
    public class ListQuery : IListQuery
    {
        private const string ListCommand = "list";

        private readonly IRconService _rconService;

        public ListQuery(IRconService rconService)
            => _rconService = rconService;

        public async Task<string> ExecuteAsync()
            => await _rconService.SendAsync(ListCommand);
    }
}
