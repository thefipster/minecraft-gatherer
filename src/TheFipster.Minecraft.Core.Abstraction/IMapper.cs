namespace TheFipster.Minecraft.Core.Abstractions
{
    public interface IMapper<TFrom, TTo>
    {
        TTo Map(TFrom item);

        TFrom Map(TTo item);
    }
}
