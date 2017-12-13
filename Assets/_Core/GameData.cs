using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData {

    static int applicationRunTimes;
    public static int ApplicationRunTimes
    {
        get { return applicationRunTimes; }
        set { applicationRunTimes = value; }
    }
}
