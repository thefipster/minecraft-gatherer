namespace TheFipster.Minecraft.Core.Domain
{
    public class RconConfig
    {
        public RconConfig() { }

        public RconConfig(string hostname, ushort port, string password)
        {
            Hostname = hostname;
            Port = port;
            Password = password;
        }

        public string Hostname { get; set; }
        public ushort Port { get; set; }
        public string Password { get; set; }
    }
}
