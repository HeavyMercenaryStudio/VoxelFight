using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldData : MonoBehaviour {

    static Mission nextMission;
    public static Mission NextMission
    {
        get { return nextMission;}
        set { nextMission = value;}
    }
}
