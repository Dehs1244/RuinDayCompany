using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RuinDayCompany.Modules
{
    public abstract class BaseGunModule : MonoBehaviour
    {
        [field: SerializeField]
        public GrabbableObject Gun { get; set; }

        [field: SerializeField]
        public int ItemSlotId { get; set; }

        [SerializeField]
        protected float _delay;

        public virtual bool IsCanUse => _delay <= 0;

        public void FixedUpdate()
        {
            if (!IsCanUse)
            {
                _delay -= 1 * Time.deltaTime;
            }
        }

        public abstract void OnShoot();
    }
}
