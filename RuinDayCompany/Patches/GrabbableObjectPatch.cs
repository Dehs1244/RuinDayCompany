using GameNetcodeStuff;
using HarmonyLib;
using RuinDayCompany.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinDayCompany.Patches
{
    [HarmonyPatch(typeof(PlayerControllerB))]
    public class GrabbableObjectPatch
    {
        [HarmonyPatch(nameof(PlayerControllerB.DiscardHeldObject))]
        [HarmonyPrefix]
        public static bool DropImpostorObject(PlayerControllerB __instance)
        {
            if (__instance.currentlyHeldObjectServer == null) return true;

            var imposterGun = __instance.currentlyHeldObjectServer.gameObject.GetComponent<ImpostorGunModule>();
            if (imposterGun == null) return true;

            return false;
        }
    }
}
