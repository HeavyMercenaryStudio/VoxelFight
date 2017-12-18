using CameraUI;

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

    }

}
