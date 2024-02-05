using UnityEngine;
using BepInEx;
using HarmonyLib;
using RuinDayCompany.Patches;
using RuinDayCompany.Infrastructure;
using System.Text;
using TMPro;
using RuinDayCompany.Utils;
using RuinDayCompany.Network;
using RuinDayCompany.Core;
using GameNetcodeStuff;
using System.Linq;

namespace RuinDayCompany
{
    [BepInPlugin(GUID, "RuinDayCompany", VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "blowoutteam.dehs.ruinday";
        public const string VERSION = "1.0.0";

        public static Plugin Instance { get; private set; }
        private Harmony _harmony = new Harmony(GUID);

        public RuinTranslator Translator { get; private set; }

        internal RuinDayConfig RuinDayConfig { get; private set; }

        internal GameNetworkSynchronization GameSynchronization { get; private set; }

        public RuinDayGame CurrentGame { get; private set; }

        public static bool IsGameStarted => Instance.CurrentGame != null;

        private void Awake()
        {
            if(Instance == null) Instance = this;

            RuinDayConfig = new RuinDayConfig();

            Logger.LogMessage("Loading custom signal translator...");
            Translator = new RuinTranslator();
            Logger.LogMessage("Loaded custom signal stranslator");

            Logger.LogMessage("Loading synchronization managers...");
            GameSynchronization = new GameNetworkSynchronization("game");
            Logger.LogMessage("Game Synchronization Loaded!");

            _harmony.PatchAll(typeof(Plugin));
            _harmony.PatchAll(typeof(LeverPatcher));
            _harmony.PatchAll(typeof(ShovelImpostorPatch));
            _harmony.PatchAll(typeof(ShotgunImpostorPatch));
            _harmony.PatchAll(typeof(GrabbableObjectPatch));
            _harmony.PatchAll(typeof(MonstersEnemyPatch));
            _harmony.PatchAll(typeof(DamagePlayerPatch));
            _harmony.PatchAll(typeof(RoundGamePatcher));

            Logger.Log(BepInEx.Logging.LogLevel.Message, $"Plugin {GUID} {VERSION} is loaded successfully, enjoy!");
        }

        public static void Log(string message) => Instance.Logger.LogMessage(message);

        public static void SendLocalMessage(string message)
        {
            if (HUDManager.Instance.lastChatMessage == message)
            {
                return;
            }
            HUDManager.Instance.lastChatMessage = message;
            HUDManager.Instance.PingHUDElement(HUDManager.Instance.Chat, 4f, 1f, 0.2f);
            if (HUDManager.Instance.ChatMessageHistory.Count >= 4)
            {
                HUDManager.Instance.chatText.text.Remove(0, HUDManager.Instance.ChatMessageHistory[0].Length);
                HUDManager.Instance.ChatMessageHistory.Remove(HUDManager.Instance.ChatMessageHistory[0]);
            }
            StringBuilder stringBuilder = new StringBuilder(message);
            stringBuilder.Replace("[playerNum0]", StartOfRound.Instance.allPlayerScripts[0].playerUsername);
            stringBuilder.Replace("[playerNum1]", StartOfRound.Instance.allPlayerScripts[1].playerUsername);
            stringBuilder.Replace("[playerNum2]", StartOfRound.Instance.allPlayerScripts[2].playerUsername);
            stringBuilder.Replace("[playerNum3]", StartOfRound.Instance.allPlayerScripts[3].playerUsername);
            message = stringBuilder.ToString();
            string item = "<color=#B7410E>" + message + "</color>";

            HUDManager.Instance.ChatMessageHistory.Add(item);
            HUDManager.Instance.chatText.text = "";
            for (int i = 0; i < HUDManager.Instance.ChatMessageHistory.Count; i++)
            {
                TextMeshProUGUI textMeshProUGUI = HUDManager.Instance.chatText;
                textMeshProUGUI.text = textMeshProUGUI.text + "\n" + HUDManager.Instance.ChatMessageHistory[i];
            }
        }

        public static void StartGame()
        {
            if (Instance.CurrentGame != null)
                return;

            var game = new RuinDayGame(FindObjectsOfType<PlayerControllerB>().Where(x => x.isPlayerControlled));
            Instance.CurrentGame = new RuinDayGame();
            Instance.GameSynchronization.Synchronize(game);
        }

        public static void EndGame()
        {
            if (Instance.CurrentGame == null)
                return;

            Instance.CurrentGame = null;
            Instance.GameSynchronization.Synchronize(null);
        }
    }
}
