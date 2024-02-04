using Newtonsoft.Json;
using RuinDayCompany.Core;
using RuinDayCompany.Interfaces;
using RuinDayCompany.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinDayCompany.Models
{
    public class RuinImpostor : RuinEmployee
    {
        public RuinImpostor(IRuinCrewmate employee) : base(employee)
        {
        }

        [JsonConstructor]
        public RuinImpostor(int playerId) : base(playerId)
        {

        }

        internal void GiveImpostorGun(IImpostorGameMode game)
        {
            if (!Plugin.Instance.RuinDayConfig.IsImpostorHaveWeapon.Value) return;

            string[] weapons = new string[] { "Shotgun", "Shovel" };
            string impostorWeapon = string.Empty;

            if (Plugin.Instance.RuinDayConfig.IsRandomImpostorWeapon.Value) impostorWeapon = weapons[RoundManager.Instance.LevelRandom.Next(weapons.Length)];
            else if (Plugin.Instance.RuinDayConfig.IsImpostorHaveShotgun.Value) impostorWeapon = weapons[0];
            else impostorWeapon = weapons[1];

            var gun = game.SpawnObject<GrabbableObject>(impostorWeapon);
            var instance = gun.gameObject.AddComponent<ImpostorGunModule>();
            instance.GrabbableObject = gun;

            if (gun is ShotgunItem shotgun) shotgun.shellsLoaded = 100; 

            GiveItem(gun);
            SwitchToEmptySlot();
        }
    }
}
