using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace Shields { 
    public class ReflectShield : Shield {

        Color reflectColor = Color.blue;

        public new void Start()
        {
            base.Start();
            Name = "REFLECTIVE SHIELD";
        }

        public override void ShieldHitted(GameObject projectile)
        {
            var renderes = projectile.GetComponentsInChildren<Renderer>();
            foreach(Renderer r in renderes){
                r.material.SetColor("_TintColor", reflectColor);
            }

            var projectileComponent = projectile.GetComponent<Projectile>();
            projectileComponent.SetShooter(this.gameObject);
            var projectileRigibody = projectileComponent.GetComponent<Rigidbody>();
            projectileRigibody.velocity = -projectileRigibody.velocity;
        }
    }
}
