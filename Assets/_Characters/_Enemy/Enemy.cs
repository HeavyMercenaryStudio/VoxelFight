using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable {

    [HideInInspector] public Animator animatorControler;

    [SerializeField] float maxHealthPoints = 100f;
    [SerializeField] GameObject blood; 
    [SerializeField] GameObject explosion;
    [SerializeField] SoundMenager audioClips;

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

	public void TakeDamage(float damage, GameObject bullet)
	{
        if (isDestroyed) return;

        Destroy (bullet);

       

        currentHealthPoints = Mathf.Clamp (currentHealthPoints - damage, 0f, maxHealthPoints);

        var offset = this.GetComponent<Collider> ().bounds.extents.y;
        GameObject blood1 = Instantiate (blood, transform.position + new Vector3(0,offset), blood.transform.rotation);
        Destroy (blood1, 5f);

        if (currentHealthPoints == 0)
        {
            GameObject ex = Instantiate (explosion, transform.position + new Vector3 (0, offset), explosion.transform.rotation);
            Destroy (ex, 1f);

            isDestroyed = true;
            GetComponent<Collider> ().enabled = false;

            if(AudioMenager.Instance != null) AudioMenager.Instance.PlayClip (audioClips.GetHitClip ());

            onEnemyDeath ();
			Destroy (gameObject);
        }
    }

    public bool IsDestroyed()
    {
        return isDestroyed;
    }
}
