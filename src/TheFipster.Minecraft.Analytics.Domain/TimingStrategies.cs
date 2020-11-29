namespace TheFipster.Minecraft.Analytics.Domain
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
