using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RuinDayCompany.Modules
{
    public class ImpostorGunModule : MonoBehaviour
    {
        [field: SerializeField]
        public GrabbableObject GrabbableObject { get; set; }

        //DestroyItemInSlot
        [field: SerializeField]
        public int ItemSlotId { get; set; }

        [SerializeField]
        private float _delay;

        public bool IsCanUse => _delay <= 0;

        public void FixedUpdate()
        {
            if (!IsCanUse)
            {
                _delay -= 1 * Time.deltaTime;
            }
        }

        public void OnShoot()
        {
            _delay = Plugin.Instance.RuinDayConfig.ImpostorKillingDelay.Value;
        }
    }
}
