using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameNetcodeStuff;
using HarmonyLib;
using RuinDayCompany.Modules;

namespace RuinDayCompany.Patches
{
    [HarmonyPatch(typeof(PlayerControllerB))]
    public class DamagePlayerPatch
    {
        [HarmonyPatch(nameof(PlayerControllerB.DamagePlayerFromOtherClientClientRpc))]
        [HarmonyPrefix]
        public static bool Damage(ref int playerWhoHit, PlayerControllerB __instance)
        {
            var player = StartOfRound.Instance.allPlayerScripts[playerWhoHit];
            return false;
        }
    }
}
