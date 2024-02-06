using UnityEngine;

namespace RuinDayCompany.Modules
{
    public class AntiRuinGunModule : BaseGunModule
    {
        [field: SerializeField]
        private int _attempts = 0;

        public override bool IsCanUse => _attempts > 0;

        public void Awake()
        {
            _attempts = Plugin.Instance.RuinDayConfig.SecurityShotAttempts.Value;
        }

        public override void OnShoot()
        {
            _attempts--;
        }
    }
}
