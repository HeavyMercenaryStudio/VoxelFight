using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable {

    [HideInInspector] public Animator animatorControler;

    [SerializeField] float maxHealthPoints = 100f;
    [SerializeField] GameObject blood;

    public float healthAsPercentage { get { return currentHealthPoints / (float)maxHealthPoints; } }
    public bool isDestroyed;

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
        if (isDestroyed) return;

		currentHealthPoints = Mathf.Clamp (currentHealthPoints - damage, 0f, maxHealthPoints);

        GameObject blood1 = Instantiate (blood, transform.position + new Vector3(0,5f), Quaternion.LookRotation(-transform.forward)) as GameObject;
        Destroy (blood1, 0.5f);

        if (currentHealthPoints == 0)
        {
            isDestroyed = true;
            animatorControler.SetTrigger ("Death");
            GetComponent<Collider> ().enabled = false;

            onEnemyDeath ();
			Destroy (gameObject, stiffDestroyTime);
        }
    }

    public bool IsDestroyed()
    {
        return isDestroyed;
    }
}
