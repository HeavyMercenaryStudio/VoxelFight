using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable {

    [SerializeField] Weapon weapon;
    [SerializeField] GameObject weaponFireSlot;
    [SerializeField] GameObject shootLine;

    
    int currentWeaponAmmo;
    AudioSource weaponAudio;

    public delegate void OnShoot(float ammo);
    public OnShoot notifyOnShoot;

    float lastShoot;
    private void Start()
    {
        currentWeaponAmmo = weapon.GetWeaponMaxAmmo ();
        weaponAudio = GetComponentInChildren<AudioSource> ();
        weaponAudio.clip = weapon.GetWeaponSound ();
    }
    private void Update()
    {
        if (Input.GetKey (KeyCode.Mouse0))
            TryShoot ();
    }

    void TryShoot()
    {
        //Handle attack speed
        if(Time.time > lastShoot + weapon.GetWeaponSpeed ())
        {
            if (currentWeaponAmmo != 0)
            {
                UpdateAmmo ();
                Shoot ();

                lastShoot = Time.time;
            }
        }
    }

    private void UpdateAmmo()
    {
        //Default weapon has unlimited ammo
        if (weapon.name.Contains ("Default")){
            notifyOnShoot (999);
        }
        else{
            currentWeaponAmmo--;
            notifyOnShoot (currentWeaponAmmo);
        }

        
    }

    private void Shoot()
    {
        Ray ray = new Ray (weaponFireSlot.transform.position, weaponFireSlot.transform.forward);
        RaycastHit hit;

        Vector3 hitPointPosition = transform.forward * weapon.GetWeaponRange () + transform.position;

        if (Physics.Raycast (ray, out hit, weapon.GetWeaponRange ()))
        {
            var enemy = hit.collider.GetComponent<Enemy> ();
            if (enemy)
            {
                enemy.TakeDamage (weapon.GetWeaponDamage ());
            }

            hitPointPosition = hit.point;
        }

        Effects (hitPointPosition);

    }

    private void Effects(Vector3 hitPointPosition)
    {
        weaponAudio.Play ();

        GameObject line = Instantiate (shootLine) as GameObject;
        LineRenderer lineRender = line.GetComponent<LineRenderer> ();
        lineRender.SetPosition (0, weaponFireSlot.transform.position);
        lineRender.SetPosition (1, hitPointPosition);

        Destroy (line, 0.1f);
    }

    public void TakeDamage(float damage)
    {
        Debug.Log ("Trafili mnie");
    }



    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawRay (weaponFireSlot.transform.position, weaponFireSlot.transform.forward * weapon.GetWeaponRange());
    //}
}
