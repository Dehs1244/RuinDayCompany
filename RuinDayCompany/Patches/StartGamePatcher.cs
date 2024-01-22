using GameNetcodeStuff;
using HarmonyLib;
using RuinDayCompany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;

namespace RuinDayCompany.Patches
{
    [HarmonyPatch(typeof(RoundManager))]
    public class StartGamePatcher
    {
        [HarmonyPatch("FinishGeneratingNewLevelClientRpc")]
        [HarmonyPostfix]
        public static void FinishLoad(RoundManager __instance)
        {
            foreach(var player in UnityEngine.Object.FindObjectsOfType<PlayerControllerB>())
            {
                Plugin.SendLocalMessage(Plugin.Instance.RuinDayConfig.Message.Value + " " + player.playerUsername);
            }

            foreach(var a in StartOfRound.Instance.unlockablesList.unlockables)
            {
                Plugin.Log(a.prefabObject.name);
            }

            HUDManager.Instance.DisplayTip("It is Ruin Day!!!", "Beware the imposters.", isWarning: true, useSave: true, "LC_IntroTip1");
            Plugin.Instance.Translator.LocalTransmitMessage(Plugin.Instance.RuinDayConfig.Message.Value);
        }
    }
}
