using System;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType
{
    Health,
    Shield,
    Speed
}

public class PowerUp : MonoBehaviour
{
    public PowerUpType Type;
    public int Amount;
    public float EffectiveTime;
    public bool Active;
    public Sprite Icon;
    public bool Rechargeable;
    public float RechargeTime;
    public bool DestroyAfterUse;

    private float _activeTime;
    private float _elapsedRechargeTime;

    public bool CanActivate;

    public PowerUp(PowerUpType type, int amount = 0, float effectiveTime = 0.0f, bool destroyAfterUse = true, bool rechargeable = false, bool active = false)
    {
        Type = type;
        Amount = amount;
        EffectiveTime = effectiveTime;
        DestroyAfterUse = destroyAfterUse;
        Rechargeable = rechargeable;
        Active = active;
        CanActivate = !active;
    }

    public void Activate()
    {
        Active = true;
        CanActivate = false;
    }

    void Update()
    {
        if (Active)
        {
            _activeTime += Time.deltaTime;

            if (_activeTime >= EffectiveTime)
            { 
                Active = false;

                if (DestroyAfterUse)
                    Destroy(this);
            }
        }
        else if (Rechargeable && !CanActivate)
        {
            _elapsedRechargeTime += Time.deltaTime;

            if (_elapsedRechargeTime >= RechargeTime)
                CanActivate = true;
        }
    }
}
