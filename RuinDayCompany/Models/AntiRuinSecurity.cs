using RuinDayCompany.Interfaces;
using RuinDayCompany.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinDayCompany.Models
{
    public class AntiRuinSecurity : RuinEmployee
    {
        public AntiRuinSecurity()
        {

        }

        public AntiRuinSecurity(IRuinCrewmate crewmate) : base(crewmate)
        {
        }

        public AntiRuinSecurity(int playerId) : base(playerId)
        {
        }

        internal void GiveSecurityGun(IImpostorGameMode game)
        {
            string[] weapons = new string[] { "Shotgun", "Shovel" };
            string impostorWeapon = weapons[1];

            if (Plugin.Instance.RuinDayConfig.IsSecurityHaveShotgun.Value) impostorWeapon = weapons[0];

            var gun = game.SpawnObject<GrabbableObject>(impostorWeapon);
            var instance = gun.gameObject.AddComponent<AntiRuinGunModule>();
            instance.Gun = gun;

            if (gun is ShotgunItem shotgun) shotgun.shellsLoaded = 100;

            GiveItem(gun);
            SwitchToEmptySlot();
        }
    }
}
