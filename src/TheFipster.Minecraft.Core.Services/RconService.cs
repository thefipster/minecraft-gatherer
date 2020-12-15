using CoreRCON;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Core.Domain;

namespace TheFipster.Minecraft.Core.Services
{
    public class RconService : IRconService, IDisposable
    {
        public const string RconConfigSection = "Rcon";

        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        private RconConfig _config;
        private RCON _rcon;

        private readonly ILogger<RconService> _logger;

        public RconService(IConfiguration config, ILogger<RconService> logger)
        {
            _logger = logger;

            bindConfig(config);
            setupRcon();
        }


        public bool IsConnected { get; private set; }

        public async Task<string> SendAsync(string command)
        {
            await _semaphore.WaitAsync();
            string result = null;

            if (await ensureConnection())
                result = await _rcon.SendCommandAsync(command);
            else
                _logger.LogWarning("RCON connection failed.");

            _semaphore.Release();
            return result;
        }

        public void Dispose()
        {
            _semaphore.Dispose();
            _rcon.Dispose();
        }

        private void bindConfig(IConfiguration config)
        {
            _config = new RconConfig();
            config.GetSection(RconConfigSection).Bind(_config);

            if (string.IsNullOrWhiteSpace(_config.Hostname))
                throw new Exception("Rcon config is invalid. Missing hostname.");

            if (_config.Port == 0)
                throw new Exception("Rcon config is invalid. Missing port.");

            if (string.IsNullOrWhiteSpace(_config.Password))
                throw new Exception("Rcon config is invalid. Missing password.");
        }

        private void setupRcon()
        {
            var ips = Dns.GetHostAddresses(_config.Hostname);
            var ip = ips.FirstOrDefault(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);

            if (ip == null)
                throw new Exception("Couldn't resolve rcon hostname to ip.");

            _rcon = new RCON(ip, _config.Port, _config.Password);
            _rcon.OnDisconnected += onDisconnect;
        }

        private async Task connectAsync()
        {
            if (IsConnected)
                return;

            await _rcon.ConnectAsync();
            IsConnected = true;
        }

        private void onDisconnect()
        {
            IsConnected = false;
            _rcon.Dispose();
            setupRcon();
        }

        private async Task<bool> ensureConnection()
        {
            try
            {
                if (!IsConnected)
                    await connectAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "RCON connection failed.");
                return false;
            }
        }
    }
}
