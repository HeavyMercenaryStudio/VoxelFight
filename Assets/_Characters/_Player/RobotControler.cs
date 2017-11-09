using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RobotControler : MonoBehaviour {

    public bool isDead;

    [SerializeField] int playerNumber;

    //Robot Atributes
    [SerializeField] float maxHealth;
    [SerializeField] float shootingSpeed;
    [SerializeField] int ammunition;

    [SerializeField] protected float gunMaxRange;
    [SerializeField] protected float gunDamage;

    [SerializeField] protected GameObject gunMuzzeFlash;

    [SerializeField] SoundMenager soundMenager;
    AudioSource weaponAudio;

    float lastShoot;
    float currentHealth;

    PlayerGUI playerGUI;

    public abstract void Shoot();

    public float GetHealthAsPercentage()
    {
        return currentHealth / maxHealth;
    }
    public void SetHealthAsPercentage(float percentage)
    {
        currentHealth = maxHealth * percentage / 100.0f;
        playerGUI.UpdateHealthInfo (GetHealthAsPercentage ());
    }
    public int GetPlayerNumber()
    {
        return playerNumber;
    }

    private void Start()
    {
        currentHealth = maxHealth;
        playerGUI = GetComponent<PlayerGUI> ();
        weaponAudio = GetComponentInChildren<AudioSource> ();
    }

    private void Update()
    {
        if (isDead) { return; }

        if (Input.GetButton ("Fire" + playerNumber))
            TryShoot ();

        if (Input.GetKey (KeyCode.X))
            TakeDamage (5);

    }

    private void TryShoot()
    {
        //Handle attack speed
        if (Time.time > lastShoot + shootingSpeed)
        {
            if (ammunition != 0)
            {
                ammunition--;
                Shoot ();

                lastShoot = Time.time;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        float damagedHealth = currentHealth - damage;
        currentHealth = Mathf.Clamp (damagedHealth, 0, maxHealth);

        playerGUI.UpdateHealthInfo (GetHealthAsPercentage ());

        if (currentHealth == 0)
            isDead = true;
    }

}
