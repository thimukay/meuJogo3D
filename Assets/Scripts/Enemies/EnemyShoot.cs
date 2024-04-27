using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boss;


namespace Enemy
{
    public class EnemyShoot : EnemyBase
    {
        public GunBase gunBase;
        public BossBase bossBase;

        protected override void Init()
        {
            base.Init();
            gunBase.StartShoot();
        }

        protected override void OnKill()
        {
            base.OnKill();
            bossBase.gameObject.SetActive(true);
        }
    }
}

