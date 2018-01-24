using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// End point.
/// </summary>
public class Point{

    public Point(int x, int y) { X = x; Y = y; }
    public Point() { }

    public int X { get; set; }
	public int Y { get; set; }

    public int PathLenght { get; set; }
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
    public GameObject[] enemySpawnerPrefabs;
    public GameObject exitPrefab;
    public GameObject playerPrefab;
	
    //Maze size
	public int mazeHeight = 11;
	public int mazeWidht = 11;

	//Maze Array 
	public int[,] globalMaze;
    public int[,] globalMazeOrginalCopy;

    List<Point> deadRooms = new List<Point>();

    //Random
    static System.Random random = new System.Random();

    public Transform mazeParent;

    void Start () {

        //Generate The Maze
        globalMazeOrginalCopy = new int[mazeHeight, mazeWidht];
        globalMaze = generateMaze (mazeHeight, mazeWidht);
        Array.Copy(globalMaze, globalMazeOrginalCopy, globalMaze.Length);

        CreateRoomAtCenter();

        solveMaze(globalMaze, new Point(1,1), new Point(mazeHeight-1, mazeWidht-1)); // do not solve maze just get all deadrooms
        globalMaze[mazeHeight - 1, mazeWidht - 1] = MazeElements.wall; // mark abstract finish point as wall

        CreateFinishPoint();

        spawnMaze ();
    }

    private void CreateFinishPoint()
    {
        Point start = new Point(mazeHeight / 2, mazeWidht / 2);

        int[,] mazeRooms = new int[mazeHeight, mazeWidht];
        Array.Copy(globalMazeOrginalCopy, mazeRooms, globalMazeOrginalCopy.Length);

        var deadRoomsCopy = deadRooms.ToList(); ;
        for (int i = 0; i < deadRoomsCopy.Count; i++)
        {
            deadRooms.Clear();
            solveMaze(mazeRooms, start, deadRoomsCopy[i]);
            CountPath(mazeRooms, deadRoomsCopy[i]);

            Array.Copy(globalMazeOrginalCopy, mazeRooms, globalMazeOrginalCopy.Length);
        }

        deadRooms = deadRoomsCopy.ToList();
    }
    private void CountPath(int[,] maze, Point deadRoom)
    {
        for (int i = 0; i < mazeHeight; i++)
            for (int j = 0; j < mazeWidht; j++)
                if (maze[i, j] == MazeElements.truePath)
                    deadRoom.PathLenght++;
    }

    private void CreateRoomAtCenter()
    {
        int roomSize = 4;
        int start = (mazeHeight + 1 - roomSize) / 2;

        for (int i = 0; i < roomSize; i++)
        {
            for (int j = 0; j < roomSize; j++)
            {
                globalMaze[start + i, start + j] = MazeElements.freeField; 
            }
        }

    }
    /*
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
    */

    public void spawnMaze(){

        var sorted = deadRooms.OrderBy(d => d.PathLenght).ToList();
        var finishPoint = sorted[sorted.Count - 1];

        var wallSize = wallPrefab.transform.localScale;

        for (int i = 0; i < deadRooms.Count; i++)
        {
             if(deadRooms[i] != finishPoint)
            {
                int n = UnityEngine.Random.Range(0, enemySpawnerPrefabs.Length - 1);
                SpawnSpawner(enemySpawnerPrefabs[n], wallSize, deadRooms[i].X, deadRooms[i].Y);
            }
            else
                SpawnPointAs(exitPrefab, wallSize, deadRooms[i].X, deadRooms[i].Y);
        }

        //Create Maze based on maze Array on Map
        for (int i = 0; i < mazeHeight; i++)
			for (int j = 0; j < mazeWidht; j++)
			{
				//spawn walls
				if (globalMaze [i, j] == MazeElements.wall)
                {
                    SpawnPointAs(wallPrefab, wallSize, i, j);
                }
            }
    }
    private void SpawnPointAs(GameObject prefab, Vector3 wallSize, int i, int j)
    {
        GameObject _wall = Instantiate(prefab) as GameObject;
        _wall.transform.SetParent(mazeParent);

        Vector3 pos = new Vector3((i * (mazeParent.transform.position.x + wallSize.x)), 0,
                                   (j * (mazeParent.transform.position.z + wallSize.z)));

        pos -= new Vector3(mazeWidht * wallSize.x / 2, 0, mazeHeight * wallSize.z / 2);
        _wall.transform.position = pos;
        _wall.layer = 8;
        _wall.AddComponent<NavMeshSourceTag>();
    }
    private void SpawnSpawner(GameObject prefab, Vector3 wallSize, int i, int j)
    {
        GameObject _wall = Instantiate(prefab) as GameObject;
        _wall.transform.SetParent(mazeParent);

        Vector3 pos = new Vector3((i * (mazeParent.transform.position.x + wallSize.x)), 0,
                                   (j * (mazeParent.transform.position.z + wallSize.z)));

        pos -= new Vector3(mazeWidht * wallSize.x / 2, 0, mazeHeight * wallSize.z / 2);
        _wall.transform.position = pos;
      
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
	public void solveMaze(int[,] maze, Point startPos, Point endPosition){

        maze [endPosition.X, endPosition.Y] = MazeElements.finishPoint;
		maze [startPos.X, startPos.Y] = MazeElements.startPoint;

		//DFS alghoritm Solver
		trySolve (maze, startPos.X, startPos.Y);
    }
	public int trySolve(int[,] maze, int x, int y){

        // if success
        if (maze[x, y] == MazeElements.finishPoint)
            return 1;

		//colisions with wall 
		if (maze [x, y] != MazeElements.freeField && maze [x, y] != MazeElements.startPoint)
            return 0;

        // mark as visited
        maze [x, y] = MazeElements.truePath;

        //check north
        if (trySolve(maze, x, y - 1) == MazeElements.wall) {  return 1; }

		//check east
		if( trySolve(maze, x+1, y) == MazeElements.wall) {  return 1; }

        //check south
        if ( trySolve(maze, x, y+1) == MazeElements.wall) {return 1; }

        //check west
        if ( trySolve(maze, x-1, y) == MazeElements.wall) {  return 1; }

        //mark as wrong path
	    maze [x, y] = MazeElements.wrongWay;

      if((maze[x,y-1] == MazeElements.wall && maze[x+1, y] == MazeElements.wall && maze[x-1, y] == MazeElements.wall) || //NEW
         (maze[x, y+1] == MazeElements.wall && maze[x+1, y] == MazeElements.wall && maze[x-1, y] == MazeElements.wall) || //SEW
         (maze[x, y-1] == MazeElements.wall && maze[x, y+1] == MazeElements.wall && maze[x-1, y] == MazeElements.wall) || // NSW
         (maze[x, y-1] == MazeElements.wall && maze[x, y+1] == MazeElements.wall && maze[x+1, y] == MazeElements.wall)) //NSE 
            deadRooms.Add(new Point(x, y));

        return 0;
    }
}
