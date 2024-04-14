using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{

    public GunBase primaryGun;
    public GunBase secondaryGun;

    public Transform gunPosition;

    private GunBase _currentGun;
    private GunBase _primaryGun;
    private GunBase _secondaryGun;

    protected override void Init()
    {
        base.Init();

        CreateGuns();

        inputs.Gameplay.Shoot.performed += ctx => StartShoot();
        inputs.Gameplay.Shoot.canceled += ctx => CancelShoot();

        inputs.Gameplay.PrimaryGun.performed += ctx => ChangetoPrimaryGun();
        inputs.Gameplay.SecondaryGun.performed += ctx => ChangetoSecondaryGun();
    }


    private void ChangetoPrimaryGun()
    {
        _primaryGun.gameObject.SetActive(true);
        _currentGun = _primaryGun;
        _secondaryGun.gameObject.SetActive(false);
    }

    private void ChangetoSecondaryGun()
    {
        _secondaryGun.gameObject.SetActive(true);
        _currentGun = _secondaryGun;
        _primaryGun.gameObject.SetActive(false);
    }

    private void CreateGuns()
    {
        _currentGun = _primaryGun = Instantiate(primaryGun, gunPosition);
        _secondaryGun = Instantiate(secondaryGun, gunPosition);

        _currentGun.transform.localPosition = _currentGun.transform.localEulerAngles = Vector3.zero;
        _secondaryGun.transform.localPosition = _currentGun.transform.localEulerAngles = Vector3.zero;
        _secondaryGun.gameObject.SetActive(false);
    }
    private void StartShoot()
    {
        _currentGun.StartShoot();
        Debug.Log("Start Shooting");
    }

    private void CancelShoot()
    {
        _currentGun.StopShoot();
        Debug.Log("Cancel Shooting");
    }
}
