using CameraUI;
using System.Collections.Generic;

namespace Data { 

    /// <summary>
    /// In Game Static Data
    /// </summary>
    public class WorldData {

        static Mission nextMission;
        public static Mission NextMission
        {
            get { return nextMission;}
            set { nextMission = value;}
        }

        static int numberOfPlayers = 2;
        public static int NumberOfPlayers
        {
            get{return numberOfPlayers; }
            set{ numberOfPlayers = value;}
        }

        static int inifinityModeSize = 11;
        public static int InifinityModeSize
        {
            get
            {
                return inifinityModeSize;
            }

            set
            {
                inifinityModeSize = value;
            }
        }

        static Dictionary<int,float> playerHealth = new Dictionary<int, float>();
        public static Dictionary<int, float> PlayerHealth
        {
            get
            {
                return playerHealth;
            }

            set
            {
                playerHealth = value;
            }
        }

        static int sceneIndex;
        public static int SceneIndex
        {
            get
            {
                return sceneIndex;
            }

            set
            {
                sceneIndex = value;
            }
        }

    }

}
