using Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shields { 
    public class BoostShield : Shield {

        [SerializeField] float boostSpeedValue = 10f;
        Movement controller;

        public override void SetFireButtonDown(bool value)
        {
            base.SetFireButtonDown(value);
            if (value && CurrentEnergy > 0)
                controller.BoostMe(boostSpeedValue);
            else
                controller.BoostMe(0);
        }
        public new void Start()
        {
            base.Start();
            controller = GetComponent<Movement>();
        }
    }
}
