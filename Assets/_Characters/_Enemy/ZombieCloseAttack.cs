using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ZombieCloseAttack : MonoBehaviour
{
    [SerializeField]
    float damagePerSecond;
    [SerializeField]
    GameObject target;
    private HouseHealth targetHealth;

    public void Start()
    {
        targetHealth = target.GetComponent<HouseHealth>();
    }

    public void TakeDamage()
    {
        targetHealth.TakeDamage(damagePerSecond);
    }

}
