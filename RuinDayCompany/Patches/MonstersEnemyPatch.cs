using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameNetcodeStuff;
using HarmonyLib;

namespace RuinDayCompany.Patches
{
    [HarmonyPatch(typeof(EnemyAI))]
    public class MonstersEnemyPatch
    {
        [HarmonyPatch(nameof(EnemyAI.PlayerIsTargetable))]
        [HarmonyPrefix]
        public static bool UntargetImpostor(PlayerControllerB __instance, ref bool __result)
        {
            //TODO: Check for impostors.
            if (Plugin.Instance.RuinDayConfig.IsMonstersReactImpostor.Value) return true;

            __result = true;
            return false;
        }

        [HarmonyPatch(nameof(EnemyAI.Update))]
        [HarmonyPrefix]
        public static bool MovePlayer(ref PlayerControllerB ___targetPlayer, ref bool ___movingTowardsTargetPlayer)
        {
            if (Plugin.Instance.RuinDayConfig.IsMonstersReactImpostor.Value) return true;
            if(!___targetPlayer) return true;

            ___targetPlayer = null;
            ___movingTowardsTargetPlayer = false;

            return false;
        }
    }

    [HarmonyPatch(typeof(NutcrackerEnemyAI), nameof(NutcrackerEnemyAI.CheckLineOfSightForLocalPlayer))]
    public class MonstersNutcrackerEnemyAiPathc
    {
        [HarmonyPrefix]
        public static bool Untarget() => Plugin.Instance.RuinDayConfig.IsMonstersReactImpostor.Value;
    }
}
