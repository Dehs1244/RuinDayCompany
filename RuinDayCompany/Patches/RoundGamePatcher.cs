using GameNetcodeStuff;
using HarmonyLib;
using RuinDayCompany.Core;
using RuinDayCompany.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinDayCompany.Patches
{
    [HarmonyPatch(typeof(RoundManager))]
    public class RoundGamePatcher
    {
        [HarmonyPatch("FinishGeneratingNewLevelClientRpc")]
        [HarmonyPostfix]
        public static void FinishLoad(RoundManager __instance)
        {
            if (Plugin.IsGameStarted)
                return;

            Plugin.StartGame();
        }
    }
}
