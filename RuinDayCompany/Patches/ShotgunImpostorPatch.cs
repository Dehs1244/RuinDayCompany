using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using RuinDayCompany.Modules;

namespace RuinDayCompany.Patches
{
    [HarmonyPatch(typeof(ShotgunItem))]
    public class ShotgunImpostorPatch
    {
        public static ImpostorGunModule GetImpostorModule(ShotgunItem instance) =>
            instance.gameObject.GetComponent<ImpostorGunModule>();

        [HarmonyPatch(nameof(ShotgunItem.ShootGun))]
        [HarmonyPrefix]
        public static bool DelayImposterShoot(ShotgunItem __instance)
        {
            var imposterGun = GetImpostorModule(__instance);
            if (imposterGun == null) return true;

            if (!imposterGun.IsCanUse) return false;
            imposterGun.OnShoot();

            return true;
        }

        [HarmonyPatch(nameof(ShotgunItem.ShootGun))]
        [HarmonyPostfix]
        public static void InfinityShellsShoot(ShotgunItem __instance)
        {
            var imposterGun = GetImpostorModule(__instance);
            if (imposterGun == null) return;

            __instance.shellsLoaded = 100;
        }
    }
}
