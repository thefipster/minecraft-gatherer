namespace TheFipster.Minecraft.Speedrun.Domain.Analytics
{
    public enum StartTimeStrategy
    {
        SetTimeStrategy,
        PlayerJoinStrategy,
        AchievementsStrategy,
        WorldCreationStrategy
    }

    public enum FinishTimeStrategy
    {
        DragonKilledStrategy,
        EnteredEndStrategy,
        WildGuess,
        NotFinished
    }
}
