using BepInEx;
using BepInEx.Configuration;
using RuinDayCompany;
using RuinDayCompany.Enums;
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
        public ConfigEntry<int> MinPlayers { get; private set; }
        private ConfigEntry<string> _allowedGameRules;
        public GameRuleType AllowedGameRule => _ParseGameRulesFromEntry();
        public ConfigEntry<bool> IsRandomGameRule { get; private set; }

        public ConfigEntry<bool> IsImpostorHaveWeapon { get; private set; }
        public ConfigEntry<bool> IsImpostorHaveShotgun { get; private set; }
        public ConfigEntry<bool> IsImpostorInstantKill { get; private set; }
        public ConfigEntry<bool> IsImpostorCanLeaveShip { get; private set; }
        public ConfigEntry<bool> IsImpostorAlone { get; private set; }
        public ConfigEntry<bool> IsMonstersReactImpostor { get; private set; }
        public ConfigEntry<bool> IsRandomImpostorWeapon { get; private set; }
        public ConfigEntry<int> ImpostorKillingDelay { get; private set; }
        public ConfigEntry<bool> IsImpostorCanBeInvisible { get; private set; }
        public ConfigEntry<int> ImpostorInvisibleDelay { get; private set; }
        public ConfigEntry<int> ImpostorInvisibleAttempts { get; private set; }
        public ConfigEntry<int> ImpostorInvisibleTime { get; private set; }

        public ConfigEntry<bool> IsImpostorTraceDeadBody { get; private set; }

        public ConfigEntry<bool> IsSecurityInCrew { get; private set; }
        public ConfigEntry<bool> IsSecurityHaveShotgun { get; private set; }
        public ConfigEntry<int> SecurityShotAttempts { get; private set; }

        public RuinDayConfig()
        {
            Plugin.Log("Loading RuinDay config...");
            var filePath = Path.Combine(Paths.ConfigPath, $"{CONFIG_NAME}.{CONFIG_EXTENSION}");
            _file = new ConfigFile(filePath, true);

            MinPlayers = _file.Bind("Ruin Game", "MinPlayers", 3, "Minimum number of players to start Ruin Day");
            _allowedGameRules = _file.Bind("Ruin Game", "AllowedGameRules", "All", "Specify the game modes separated by commas. \nPossible values: \n* All - All game rules allowed \n* KillEveryone \n* AbortQuota");
            IsRandomGameRule = _file.Bind("Ruin Game", "RandomRules", true, "The rules of the game will be selected randomly from the list of allowed ones");

            IsImpostorHaveWeapon = _file.Bind("Impostor", "IsHaveWeapon", true, "Does Impostor get a weapon at the beginning of the game");
            IsImpostorCanLeaveShip = _file.Bind("Impostor", "IsImpostorCanLaunchShip", false, "Can an impostor launch a ship from a location");
            IsImpostorAlone = _file.Bind("Impostor", "IsImpostorAlone", false, "One impostor is selected from company");
            IsMonstersReactImpostor = _file.Bind("Impostor", "IsMonstersReactImpostor", true, "Do monsters react to impostor");
            IsRandomImpostorWeapon = _file.Bind("Impostor", "IsImpostorGetRandomWeapon", true, "Does the impostor get a random weapon from the game");
            IsImpostorTraceDeadBody = _file.Bind("Impostor", "IsImpostorTraceDeadBody", true, "Does the impostor leave traces on the bodies");

            IsImpostorInstantKill = _file.Bind("Impostor Capabilities", "IsInstantKill", false, "Does Impostor killing instant");
            ImpostorKillingDelay = _file.Bind("Impostor Capabilities", "ImpostorKillingDelay", 5, "The delay between the murders of the impostor in seconds");
            IsImpostorHaveShotgun = _file.Bind("Impostor Capabilities", "IsImpostorHaveShotgun", false, "Should give the impostor a shotgun, otherwise the reference point will be on a shovel or random gun");

            IsImpostorCanBeInvisible = _file.Bind("Impostor Invisible", "Enabled", true, "Can an impostor become invisible for a while");
            ImpostorInvisibleDelay = _file.Bind("Impostor Invisible", "Delay", 15, "The delay between the disappearing of the impostor in seconds");
            ImpostorInvisibleAttempts = _file.Bind("Impostor Invisible", "Attempts", 2, "How many times can an impostor become invisible");
            ImpostorInvisibleTime = _file.Bind("Impostor Invisible", "Time", 5, "A time in which an impostor can be invisible in seconds");

            IsSecurityInCrew = _file.Bind("Crew Security", "Enabled", true, "Is there a guard (sheriff or police like in mafia) on the ship");
            IsSecurityHaveShotgun = _file.Bind("Crew Security", "HaveShotgun", true, "Does the guard have a shotgun otherwise he has a shovel");
            SecurityShotAttempts = _file.Bind("Crew Security", "Attempts", 2, "How many attempts have a guard to detect impostor (just try killing him)");

            Plugin.Log("Ruin Day config initialized!");
        }

        private GameRuleType _ParseGameRulesFromEntry() => _allowedGameRules.Value.Split(',')
            .Select(x => (GameRuleType)Enum.Parse(typeof(GameRuleType), x))
            .Aggregate((x, y) => x | y);
    }
}
