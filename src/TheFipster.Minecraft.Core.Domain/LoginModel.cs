﻿namespace TheFipster.Minecraft.Core.Domain
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
