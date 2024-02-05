using GameNetcodeStuff;
using HarmonyLib;
using RuinDayCompany;
using RuinDayCompany.Core;
using RuinDayCompany.Models;
using RuinDayCompany.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;

namespace RuinDayCompany.Patches
{
    [HarmonyPatch(typeof(StartMatchLever))]
    public class LeverPatcher
    {
        private static bool _isStarted;

        [HarmonyPatch(nameof(StartMatchLever.PullLever))]
        [HarmonyPatch(nameof(StartMatchLever.LeverAnimation))]
        [HarmonyPrefix]
        public static bool PushLever(StartMatchLever __instance)
        {
            if (Plugin.Instance.CurrentGame == null)
                return true;

            if (Plugin.Instance.CurrentGame.Crew.IsLocalPlayerImpostor() && 
                !Plugin.Instance.RuinDayConfig.IsImpostorCanLeaveShip.Value)
                return false;

            if (__instance.leverHasBeenPulled)
            {
                Plugin.EndGame();
            }

            return true;
        }
    }
}
