using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cloth;

public class HealthBase : MonoBehaviour, IDamageable
{
    public float startLife = 10f;
    public bool destroyOnKill = false;
    [SerializeField] private float _currentLife;
    public FlashColor flashColor;
    public ParticleSystem particleSystem;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;


    public List<UIHealthUpdater> uiFillUpdater;

    public float damageMultiply = 1f;

    private void Awake()
    {
        init();
    }
    public void init()
    {
        ResetLife();
    }

    public float getCurrentLife()
    {
        return _currentLife;
    }

    public void ResetLife()
    {
        _currentLife = startLife;
        UpdateUI();
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
        _currentLife -= f * damageMultiply;
        if (_currentLife <= 0)
        {
            Kill();
            if (particleSystem != null) particleSystem.Emit(40);
        }
        UpdateUI();
        OnDamage?.Invoke(this);
    }

    public void Damage(float damage, Vector3 dir)
    {
        Damage(damage);
    }

    private void UpdateUI()
    {
        if (uiFillUpdater != null)
        {
            uiFillUpdater.ForEach(i => i.UpdateValue((float)_currentLife/startLife));

        }
    }

    public void ChangeDamageMultiply(float damage, float duration)
    {
        this.damageMultiply = damage;
        //StartCoroutine(ChangeDamageMultiplyCoroutine(damage, duration));
    }
    IEnumerator ChangeDamageMultiplyCoroutine(float damageMultiply, float duration)
    {
        this.damageMultiply = damageMultiply;
        yield return new WaitForSeconds(duration);
        this.damageMultiply = 1f;
    }
}
