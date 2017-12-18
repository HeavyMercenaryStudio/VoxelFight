using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Data { 

    /// <summary>
    /// Data passed between scenes
    /// </summary>
    public class GameData {

        static int applicationRunTimes;
        public static int ApplicationRunTimes
        {
            get { return applicationRunTimes; }
            set { applicationRunTimes = value; }
        }
    }
}
