namespace TheFipster.Minecraft.Meta.Abstractions
{
    public interface ITimingBlacklister
    {
        void Add(string worldname);

        void Remove(string worldname);

        bool IsBlacklisted(string worldname);
    }
}
