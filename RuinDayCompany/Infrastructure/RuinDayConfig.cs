using BepInEx;
using BepInEx.Configuration;
using RuinDayCompany;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinDayCompany.Infrastructure
{
    internal class RuinDayConfig
    {
        public const string CONFIG_NAME = "ruinday_company";
        public const string CONFIG_EXTENSION = "cfg";

        private readonly ConfigFile _file;
        public ConfigEntry<string> Message { get; private set; }
        public ConfigEntry<int> MinPlayers { get; private set; }
        public ConfigEntry<string> AllowedGameRule { get; private set; }
        public ConfigEntry<bool> IsRandomGameRule { get; private set; }

        public ConfigEntry<bool> IsImposterHaveWeapon { get; private set; }
        public ConfigEntry<bool> IsImposterInstantKill { get; private set; }
        public ConfigEntry<bool> IsImposterCanLeaveShip { get; private set; }
        public ConfigEntry<bool> IsImposterAlone { get; private set; }
        public ConfigEntry<bool> IsMonstersReactImposter { get; private set; }
        public ConfigEntry<bool> IsRandomImposterWeapon { get; private set; }
        public ConfigEntry<int> ImposterKillingDelay { get; private set; }
        public ConfigEntry<bool> IsImposterCanBeInvisible { get; private set; }
        public ConfigEntry<int> ImposterInvisibleDelay { get; private set; }
        public ConfigEntry<bool> IsImposterTraceDeadBody { get; private set; }

        public ConfigEntry<bool> IsSecurityInCrew { get; private set; }
        public ConfigEntry<bool> IsSecurityHaveShotgun { get; private set; }
        public ConfigEntry<int> SecurityShotAttempts { get; private set; }

        public RuinDayConfig()
        {
            Plugin.Log("Loading RuinDay config...");
            var filePath = Path.Combine(Paths.ConfigPath, $"{CONFIG_NAME}.{CONFIG_EXTENSION}");
            _file = new ConfigFile(filePath, true);
            Message = _file.Bind("Config", "Message", "You noob", "Apply message to signal translator");

            MinPlayers = _file.Bind("Ruin Game", "MinPlayers", 3, "Minimum number of players to start Ruin Day");

            Plugin.Log("Ruin Day config initialized!");
        }
    }
}
