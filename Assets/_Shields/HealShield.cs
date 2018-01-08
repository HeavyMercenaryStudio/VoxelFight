using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters;

namespace Shields { 
    public class HealShield : Shield {

        [SerializeField] float healthPercentage = 0.1f;
        PlayerController controller;
        public new void Start()
        {
            base.Start();
            controller = GetComponent<PlayerController>();    
        }
        public override void DefenseUp()
        {
            base.DefenseUp();
            controller.HealMe(healthPercentage);
        }


    }
}
