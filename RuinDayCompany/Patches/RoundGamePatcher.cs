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
            var ruinDayGame = __instance.gameObject.GetComponent<RuinGameModule>();
            if (ruinDayGame != null)
                return;

            var game = new RuinDayGame(UnityEngine.Object.FindObjectsOfType<PlayerControllerB>().Where(x => x.isPlayerControlled));
            var gameModule = __instance.gameObject.AddComponent<RuinGameModule>();
            gameModule.StartGame(game);
        }
    }
}
