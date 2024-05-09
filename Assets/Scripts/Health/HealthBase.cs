using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour, IDamageable
{
    public float startLife = 10f;
    public bool destroyOnKill = false;
    public float currentLife;
    public FlashColor flashColor;
    public ParticleSystem particleSystem;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;


    private void Awake()
    {
        init();
    }
    public void init()
    {
        ResetLife();
    }

    protected void ResetLife()
    {
        currentLife = startLife;
    }

    protected virtual void Kill()
    {
        if(destroyOnKill)
            Destroy(gameObject, 2f);

        OnKill?.Invoke(this);
    }

    [NaughtyAttributes.Button]
    public void Damage()
    {
        Damage(5);
    }

    public void Damage(float f)
    {
        if (flashColor != null) flashColor.Flash();
        if (particleSystem != null) particleSystem.Emit(15);
        currentLife -= f;
        if (currentLife <= 0)
        {
            Kill();
            if (particleSystem != null) particleSystem.Emit(40);
        }

        OnDamage?.Invoke(this);
    }

    public void Damage(float damage, Vector3 dir)
    {
        Damage(damage);
    }
}
