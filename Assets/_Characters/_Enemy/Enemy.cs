using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float maxHealthPoints = 100f;

    [SerializeField] GameObject blood;

    GameObject player;

    float currentHealthPoints;
	public float healthAsPercentage { get { return currentHealthPoints / (float)maxHealthPoints; } }

	void Start()
	{
		player = GameObject.FindGameObjectWithTag ("Player");

		currentHealthPoints = maxHealthPoints;
	}


	public void TakeDamage(float damage)
	{
		currentHealthPoints = Mathf.Clamp (currentHealthPoints - damage, 0f, maxHealthPoints);

        GameObject blood1 = Instantiate (blood, transform.position + new Vector3(0,5f), Quaternion.identity) as GameObject;
        Destroy (blood1, 0.5f);

        if (currentHealthPoints == 0)
			Destroy (gameObject);
	}


}
