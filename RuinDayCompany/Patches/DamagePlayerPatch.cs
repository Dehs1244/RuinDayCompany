using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameNetcodeStuff;
using HarmonyLib;

namespace RuinDayCompany.Patches
{
    [HarmonyPatch(typeof(PlayerControllerB))]
    public class DamagePlayerPatch
    {
        [HarmonyPatch("Hit")]
        [HarmonyPrefix]
        public static bool Damage(PlayerControllerB playerWhoHit, PlayerControllerB __instance)
        {
            if(Plugin.Instance.RuinDayConfig.IsImpostorInstantKill.Value) __instance.DamagePlayer(100);
            return false;
        }
    }
}
