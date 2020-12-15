namespace TheFipster.Minecraft.Speedrun.Web
{
    public class AccountIndexViewModel
    {
        public AccountIndexViewModel() { }


        public AccountIndexViewModel(string playerId)
        {
            PlayerId = playerId;
        }

        public int Attemps { get; internal set; }
        public string PlayerId { get; internal set; }
    }
}