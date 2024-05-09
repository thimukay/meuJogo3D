using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{

    public List<UIFillUpdater> uIGunUpdaters;
    public float maxShoot = 5;
    public float timeToRecharge = 1f;

    public ProjectileBase prefabProjectile;

    public Transform positionToShoot;
    public float timeBetweenShoot = .2f;
    public float speed = 50f;

    private Coroutine _currentCoroutine;

    

    protected virtual IEnumerator ShootCoroutine()
    {
        while(true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShoot);
        }
    }

    public virtual void Shoot()
    {
        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = positionToShoot.position;
        projectile.transform.rotation = positionToShoot.rotation;
        projectile.speed = speed;
    }

    public void StartShoot()
    {
        StopShoot();
        _currentCoroutine = StartCoroutine(ShootCoroutine());
    }

    public void StopShoot()
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);
    }

    public void UpdateUI(float currentShoots)
    {
        uIGunUpdaters.ForEach(i => i.UpdateValue(maxShoot, currentShoots));
    }

    public void GetAllUIs()
    {
        uIGunUpdaters = GameObject.FindObjectsOfType<UIFillUpdater>().ToList();
    }
}
