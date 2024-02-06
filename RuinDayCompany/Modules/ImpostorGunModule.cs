using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RuinDayCompany.Modules
{
    public class ImpostorGunModule : BaseGunModule
    {
        public override void OnShoot()
        {
            _delay = Plugin.Instance.RuinDayConfig.ImpostorKillingDelay.Value;
        }
    }
}
