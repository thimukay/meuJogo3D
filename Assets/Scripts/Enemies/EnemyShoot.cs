using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemy
{
    public class EnemyShoot : EnemyBase
    {
        public GunBase gunBase;

        protected override void Init()
        {
            base.Init();
            //yield return new WaitForSeconds(2);
            gunBase.StartShoot();
        }
    }
}

