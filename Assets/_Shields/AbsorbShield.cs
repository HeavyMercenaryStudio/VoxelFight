using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shields { 
    public class AbsorbShield : Shield {

        public new void Start()
        {
            base.Start();
            Name = "ABSORB SHIELD";
        }
    }
}
