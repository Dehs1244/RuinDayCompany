using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using RuinDayCompany.Modules;

namespace RuinDayCompany.Patches
{
    [HarmonyPatch(typeof(GrabbableObject))]
    public class ShovelImpostorPatch
    {
        [HarmonyPatch("ActivateItemClientRpc")]
        [HarmonyPrefix]
        public static bool ShovelImpostorGun(GrabbableObject __instance)
        {
            if (__instance is Shovel shovel)
            {
                var imposterGun = __instance.gameObject.GetComponent<ImpostorGunModule>();
                if (imposterGun == null) return true;

                if (!imposterGun.IsCanUse) return false;
                imposterGun.OnShoot();
            }

            return true;
        }
    }
}
