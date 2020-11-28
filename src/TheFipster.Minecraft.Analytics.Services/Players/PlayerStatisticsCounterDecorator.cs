using System.Collections.Generic;
using TheFipster.Minecraft.Analytics.Abstractions;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Analytics.Services.Players
{
    public class PlayerStatisticsCounterDecorator : IPlayerAnalyzer
    {
        private readonly IPlayerAnalyzer _component;

        public PlayerStatisticsCounterDecorator(IPlayerAnalyzer component)
        {
            _component = component;
        }

        public ICollection<PlayerAnalytics> Analyze(RunImport import)
        {
            var players = _component.Analyze(import);

            foreach (var player in players)
            {
                if (!import.Stats.ContainsKey(player.Id))
                    continue;

                var stats = import.Stats[player.Id];
                player.Statistics = analyzeStatistics(stats);
            }

            return players;
        }

        private PlayerStatistics analyzeStatistics(Stats stats)
        {
            var player = new PlayerStatistics();

            player.Broken = convertSnakeDictionary(stats.Broken);
            player.Crafted = convertSnakeDictionary(stats.Crafted);
            player.Dropped = convertSnakeDictionary(stats.Dropped);
            player.Killed = convertSnakeDictionary(stats.Killed);
            player.KilledBy = convertSnakeDictionary(stats.KilledBy);
            player.Mined = convertSnakeDictionary(stats.Mined);
            player.PickedUp = convertSnakeDictionary(stats.PickedUp);
            player.Used = convertSnakeDictionary(stats.Used);

            return player;
        }

        private Dictionary<string, int> convertSnakeDictionary(Dictionary<string, int> snakeCaseItems)
        {
            var items = new Dictionary<string, int>();

            if (snakeCaseItems == null || snakeCaseItems.Count == 0)
                return items;

            foreach (var snakeItem in snakeCaseItems)
            {
                var item = convertFromMinecraftNamespace(snakeItem.Key);
                items.Add(item, snakeItem.Value);
            }

            return items;
        }

        private string convertFromMinecraftNamespace(string minecraftEntity)
        {
            var sanitized = minecraftEntity.Replace("minecraft:", string.Empty);
            return convertSnakeCase(sanitized);
        }

        private static string convertSnakeCase(string snakeCase)
        {
            var words = snakeCase.Split("_");
            var capitalizedWords = new List<string>();

            foreach (var word in words)
            {
                var firstLetter = word[0];
                var capitalizedWord = char.ToUpper(firstLetter) + word.Substring(1);
                capitalizedWords.Add(capitalizedWord);
            }

            return string.Join(" ", capitalizedWords);
        }
    }
}
