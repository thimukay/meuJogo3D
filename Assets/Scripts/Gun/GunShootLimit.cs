using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootLimit : GunBase
{

    private float _currentShoots;
    private bool _recharging = false;

    private void Awake()
    {
        GetAllUIs();
    }
    private void OnEnable()
    {
        UpdateUI(_currentShoots);
    }

    protected override IEnumerator ShootCoroutine()
    {
        /*while (true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShoot);
        }*/

        if (_recharging) yield break;
        while (true)
        {
            if(_currentShoots < maxShoot)
            {
                Shoot();
                _currentShoots++;
                CheckRecharge();
                UpdateUI(_currentShoots);
                yield return new WaitForSeconds(timeBetweenShoot);
            }

        }
    }

    private void CheckRecharge()
    {
        if (_currentShoots >= maxShoot)
        {
            StopShoot();
            StartRecharge();
        }
    }

    private void StartRecharge()
    {
        _recharging = true;
        StartCoroutine(RechargeCoroutine());
    }

    IEnumerator RechargeCoroutine()
    {
        float time = 0;
        while(time < timeToRecharge)
        {
            time += Time.deltaTime;
            uIGunUpdaters.ForEach(i => i.UpdateValue(time/timeToRecharge));
            yield return new WaitForEndOfFrame();
        }
        _currentShoots = 0;
        _recharging = false;
    }
}
