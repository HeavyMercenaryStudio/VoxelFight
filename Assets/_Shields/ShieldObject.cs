using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldObject : MonoBehaviour, IDamageable {

    public delegate void OnShieldHit(GameObject projectile);
    public OnShieldHit notifyShieldHit;

    public bool IsDestroyed()
    {
        return false;
    }
    public void TakeDamage(float damage, GameObject bullet)
    {
        notifyShieldHit(bullet);
    }   
}
