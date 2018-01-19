using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// End point.
/// </summary>
public class EndPoint{

    public EndPoint(int x, int y) { posX = x; posY = y; }
    public EndPoint() { }

    public int posX { get; set; }
	public int posY { get; set; }

	public int pathLenght{ get; set; }
}


public class MazeElements{

	public static int wall = 1;
	public static int freeField = 0;
	public static int truePath = 2;
	public static int wrongWay = 4;
	public static int startPoint = 3;
	public static int finishPoint = 5;
	public static int player = 8;
}

public class MazeGen : MonoBehaviour {

	//Object to spawn as tile 
	public GameObject wallPrefab;
	public GameObject playerPrefab;
	public GameObject finishPrefab;

	//Maze size
	public static int mazeHeight = 81;
	public static int mazeWidht = 81;

	//Maze Array 
	public static int[,] maze;

	//Random
	static System.Random random = new System.Random();

	// Position where Player start 
	public static Vector2 startMazePosition;

	//Finish Point
	public static Vector2 finishMazePosition;

	// Use this for initialization

	void Awake(){
        mazeHeight = 21;
		mazeWidht = mazeHeight;
	}


	void Start () {

		//Generate The Maze
		maze = generateMaze (mazeHeight, mazeWidht);

        //CreateRooms();

		//Solve maze using DFS alghoritm to find longhest Path 
		List<EndPoint> endPoints = new List<EndPoint>();

		//Solve maze depends on diffrend end points
		for (int i = 0; i < mazeHeight * mazeWidht; i++)
			endPoints.Add(solveMaze ());

        //Sort maze path list
        IEnumerable<EndPoint> sorderList = from point in endPoints
		                                   orderby point.pathLenght descending
		                                   select point;

		//mark the finish position where Player end his JOURNEY :D 
		//Getting lonest path in maze path array
		finishMazePosition = new Vector2 (sorderList.First ().posX, sorderList.First ().posY); 

		//mark the end into maze array
		//maze [(int)finishMazePosition.x, (int)finishMazePosition.y] = MazeElements.finishPoint;

		//mark player position
		//maze [(int)startMazePosition.x, (int)startMazePosition.y] = MazeElements.player;

		spawnMaze ();

	}

    private void CreateRooms()
    {
        int roomSize = 10;
        float roomFreq = 0.1f;

        for (int i = roomSize; i < mazeHeight - roomSize; i++)
        {
            for (int j = roomSize; j < mazeWidht - roomSize; j++)
            {
                float rand = UnityEngine.Random.Range(0.0f, 1.0f);
                if (rand > roomFreq)
                {
                    CreateRoom(i, j, roomSize);
                }
            }
        }
    }
    void CreateRoom(int x, int y, int roomSize)
    {
        for (int i = 0; i < roomSize; i++)
            for (int j = 0; j < roomSize; j++)
                maze[x + i,y + j] = MazeElements.freeField;
    }

    public void spawnMaze(){


		Transform mazePanel = GameObject.Find ("MazePanel").transform;

        var wallSize = wallPrefab.transform.localScale;

        for (int i = 0; i < deadRooms.Count; i++)
        {
            GameObject _wall = Instantiate(wallPrefab) as GameObject;
            _wall.transform.SetParent(mazePanel);

            Vector3 pos = new Vector3((deadRooms[i].posX * (mazePanel.transform.position.x + wallSize.x)), 0,
                                       (deadRooms[i].posY * (mazePanel.transform.position.z + wallSize.z)));


            pos -= new Vector3(mazeWidht * wallSize.x / 2, 0, mazeHeight * wallSize.z / 2);
            _wall.transform.position = pos;
            _wall.GetComponent<Renderer>().material.color = Color.red;
            _wall.layer = 8;
        }

            //Create Maze based on maze Array on Map
            for (int i = 0; i < mazeHeight; i++)
			for (int j = 0; j < mazeWidht; j++)
			{
				//spawn walls
				if (maze [i, j] == MazeElements.wall)
				{
					GameObject _wall = Instantiate (wallPrefab) as GameObject;
					_wall.transform.SetParent (mazePanel);
               
					Vector3 pos = new Vector3 ((i * (mazePanel.transform.position.x + wallSize.x)), 0,  
						                       (j * (mazePanel.transform.position.z + wallSize.z)));


                    pos -= new Vector3(mazeWidht*wallSize.x/2, 0, mazeHeight*wallSize.z/2);
                    _wall.transform.position = pos;
                    _wall.layer = 8;
                }

                if (maze[i, j] == MazeElements.wrongWay)
                {
                    GameObject _wall = Instantiate(wallPrefab) as GameObject;
                    _wall.transform.SetParent(mazePanel);

                    Vector3 pos = new Vector3((i * (mazePanel.transform.position.x + wallSize.x)), 0,
                                               (j * (mazePanel.transform.position.z + wallSize.z)));


                    pos -= new Vector3(mazeWidht * wallSize.x / 2, 0, mazeHeight * wallSize.z / 2);
                    _wall.transform.position = pos;
                    _wall.GetComponent<Renderer>().material.color = Color.red;
                    _wall.layer = 8;
                }

                //spawn player at start pos
                //if (new Vector2(i, j) == startMazePosition)
                //{

                //	GameObject player = Instantiate (playerPrefab) as GameObject;

                //	player.transform.SetParent (mazePanel);

                //	//ustaw pozycje
                //	Vector2 pos = new Vector2 ((i + mazePanel.transform.position.x)/2, 
                //		(j + mazePanel.transform.position.y)/2);

                //	//Wysrodkuj
                //	pos -= new Vector2((mazeWidht)/4.0f,(mazeHeight)/4.0f);

                //	player.transform.position = pos;

                //}

                ////spawn finish pos
                //if (new Vector2(i, j) == finishMazePosition)
                //{
                //	GameObject finish = Instantiate (finishPrefab) as GameObject;

                //	finish.transform.SetParent (mazePanel);

                //	Vector2 pos = new Vector2 ((i + mazePanel.transform.position.x)/2, 
                //		(j + mazePanel.transform.position.y)/2);

                //	pos -= new Vector2((mazeWidht)/4.0f,(mazeHeight)/4.0f);	

                //	finish.transform.position = pos;

                //}

            }

    }

	/// <summary>
	/// Generates the maze.
	/// </summary>
	/// <returns>Array of maze.</returns>
	/// <param name="height">Height of Maze.</param>
	/// <param name="widht">Widht of Maze.</param>
	private int[,] generateMaze(int height, int widht){

		//create local array to used to return finished maze
		int[,] _maze = new int[height,widht];

		//fill array with "1"
		for (int i = 0; i < height; i++)
			for (int j = 0; j < widht; j++)
				_maze [i,j] = MazeElements.wall;

		// Check if the row or columns are not boreder of the maze
		System.Random rand = new System.Random();

		int r = rand.Next (height);
		while (r % 2 == 0)
			r = rand.Next (height);

		int c = rand.Next (widht);
		while (c % 2 == 0)
			c = rand.Next (widht);

		//set first free field
		_maze [r,c] = MazeElements.freeField;

		//DFS algorithm implementation
		mazeDigger (_maze, r, c);

		//set start pos
		startMazePosition = new Vector2 (r, c);

		return _maze;
	}

	private void mazeDigger(int [,] _maze, int r, int c){
	
		//used for determinate actual direction when algoritm go
		int[] directions = { 1, 2, 3, 4 };

		//mix the array eleents
		shuffle (directions);

		for (int i = 0; i < directions.Length; i++)
		{
			switch (directions [i])
			{
				case 1://North
					//If actual row is not  out of maze
					if (r - 2 <= 0)
						continue;

					//check if its not alredy null field
					// if is not, set up field 2 step and 1 step of actual postion as null, making "transition"
					// do this for all of directions in array
					// every time mixing direction in array
					if (_maze [r-2,c] != MazeElements.freeField)
					{
						_maze [r - 2, c] = MazeElements.freeField;
						_maze [r - 1, c] = MazeElements.freeField;
						mazeDigger(_maze, r - 2, c);
					}
					break;

				case 2://South
					if (r + 2 >= mazeHeight - 1)
						continue;
					if (_maze [r + 2, c] != MazeElements.freeField)
					{
						_maze [r + 2, c] = MazeElements.freeField;
						_maze [r + 1, c] = MazeElements.freeField;
						mazeDigger (_maze, r + 2, c);
					}
					break;

				case 3://East
					if (c + 2 >= mazeWidht - 1)
						continue;
					if (_maze [r, c + 2] != MazeElements.freeField)
					{
						_maze [r, c + 2] = MazeElements.freeField;
						_maze [r, c + 1] = MazeElements.freeField;
						mazeDigger (_maze, r, c + 2);
					}
					break;

				case 4://West
					if (c - 2 <= 0)
						continue;
					if (_maze [r, c -2] != MazeElements.freeField)
					{
						_maze [r, c - 2] = MazeElements.freeField;
						_maze [r, c - 1] = MazeElements.freeField;
						mazeDigger (_maze, r, c - 2);
					}
					break;
			}
					
		}
	}

	public static void shuffle<T> (T[] array){
	
		int n = array.Length;

		// mix th array from argument
		for (int i = 0; i < n; i++)
		{
			int r = random.Next(0,n);

			T t = array [r];
			array [r] = array [i];
			array [i] = t;
		}
	}
		
	/// <summary>
	/// Solves the maze.
	/// </summary>
	public EndPoint solveMaze(){

		//copy maze to maze temp
		int[,] mazeTemp = new int[mazeHeight, mazeWidht];

		for (int i = 0; i < mazeHeight; i++)
			for (int j = 0; j < mazeWidht; j++)
				mazeTemp [i, j] = maze [i, j];

		//Generate random end point to set up alghoritm 
		finishMazePosition = generateRandomEnd ();

		//mark generate end point to maze array
		maze [(int)finishMazePosition.x, (int)finishMazePosition.y] = MazeElements.finishPoint;

		//mark point where alghoritm start
        startMazePosition = generateRandomEnd();
        int startX = (int)startMazePosition.x;
		int startY = (int)startMazePosition.y;
		maze [startX, startY] = MazeElements.startPoint;

		//DFS alghoritm Solver
		trySolve (startX, startY);

		//Count path lenght
		int pathLenght = 0;
		for (int i = 0; i < mazeHeight; i++)
			for (int j = 0; j < mazeWidht; j++)
				if (maze [i, j] == MazeElements.truePath)
					pathLenght++;

        //back to orignal maze array
        for (int i = 0; i < mazeHeight; i++)
			for (int j = 0; j < mazeWidht; j++)
				maze [i, j] = mazeTemp [i, j];


		//return current pathLenght
		EndPoint end = new EndPoint();
		end.posX = (int)finishMazePosition.x;
		end.posY = (int)finishMazePosition.y;
		end.pathLenght = pathLenght;

		return end;

	}


	public Vector2 generateRandomEnd(){

		List<Vector2> ableFields = new List<Vector2> ();

		//Get All able to move positions
		for (int i = 0; i < mazeHeight; i++)
			for (int j = 0; j < mazeWidht; j++)
				if (maze [i, j] == MazeElements.freeField)
					ableFields.Add(new Vector2 (i, j));

		//generate a random number
		int randN = random.Next (0, mazeHeight + mazeWidht);

		//little shuffle
		ableFields.Reverse ();

		//return generated point
		return ableFields [randN];
	}

    bool deadRoom;
    List<EndPoint> deadRooms = new List<EndPoint>();
	public int trySolve(int x, int y){

		// if success
		if (maze [x, y] == MazeElements.finishPoint)
			return 1;

		//colisions with wall 
		if (maze [x, y] != MazeElements.freeField && maze [x, y] != MazeElements.startPoint)
			return 0;

		// mark as visited
		maze [x, y] = MazeElements.truePath;

        //check north
        if (trySolve(x, y - 1) == MazeElements.wall) { deadRoom = false;  return 1; }

		//check east
		if( trySolve(x+1, y) == MazeElements.wall) { deadRoom = false; return 1; }

        //check south
        if ( trySolve(x, y+1) == MazeElements.wall) { deadRoom = false; return 1; }

        //check west
        if ( trySolve(x-1, y) == MazeElements.wall) { deadRoom = false; return 1; }

        //mark as wrong path
        if (!deadRoom) { 
		    maze [x, y] = MazeElements.wrongWay;

            if (deadRooms.Count > 0)
            {
                foreach (EndPoint item in deadRooms)
                {
                    if (item.posX != x || item.posY != y) { 
                        deadRooms.Add(new EndPoint(x, y));
                        break;
                    }
                }
            }
            else
                deadRooms.Add(new EndPoint(x, y));

            deadRoom = true;
        }
        return 0;
	}
		
}
