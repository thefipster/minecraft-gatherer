namespace TheFipster.Minecraft.Import.Abstractions
{
    public interface IWorldDeleter
    {
        void Delete(string worldname);

        void Rename(string worldname);
    }
}
