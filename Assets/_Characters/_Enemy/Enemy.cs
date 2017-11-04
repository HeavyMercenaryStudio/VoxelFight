using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float maxHealthPoints = 100f;

    GameObject player;

    //AICharacterControl aiController;

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

		if (currentHealthPoints == 0)
			Destroy (gameObject);
	}


}
