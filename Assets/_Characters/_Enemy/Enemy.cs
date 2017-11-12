using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public Animator animatorControler;

    [SerializeField] float maxHealthPoints = 100f;
    [SerializeField] float zombieAttackSpeed;
    [SerializeField] float damagePerSecond;

    [SerializeField] GameObject blood;
    
    public float healthAsPercentage { get { return currentHealthPoints / (float)maxHealthPoints; } }
    public float GetZombieAttackSpeed()
    {
        return zombieAttackSpeed;
    }
    public float GetZombieDamage()
    {
        return damagePerSecond;
    }
    public bool isDeath;

    public delegate void OnEnemyDeath();
    public static event OnEnemyDeath onEnemyDeath;

    float currentHealthPoints;
    float stiffDestroyTime = 20f;

    void Start()
	{
		currentHealthPoints = maxHealthPoints;

        animatorControler = GetComponent<Animator> ();
    }

	public void TakeDamage(float damage)
	{
		currentHealthPoints = Mathf.Clamp (currentHealthPoints - damage, 0f, maxHealthPoints);

        GameObject blood1 = Instantiate (blood, transform.position + new Vector3(0,5f), Quaternion.identity) as GameObject;
        Destroy (blood1, 0.5f);

        if (currentHealthPoints == 0)
        {
            isDeath = true;
            animatorControler.SetTrigger ("Death");
            GetComponent<Collider> ().isTrigger = true;
            onEnemyDeath ();
			Destroy (gameObject, stiffDestroyTime);
        }
    }

    
}
