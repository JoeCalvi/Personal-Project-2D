using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// allows use of "Serializable" attribute
using System;
// allows the use of lists
using System.Collections.Generic;
// tells Random to use the Unity Engine random number generator
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    // NOTE Using Serializable allows us to embed a class with sub properties in the inspector.
    [Serializable]
    public class Count
    {
        public int minimum; //Minimum value for our Count class.
        public int maximum; //Maximum value for our Count class.

        //Assignment constructor.
        public Count (int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    public int columns = 8; //Number of columns in our game board.
    public int rows = 8; //Number of rows in our game board.

    //Lower and upper limit for our random number of walls per level.
    public Count wallCount = new Count(5, 9);

    //Lower and upper limit for our random number of food items per level.
    public Count foodCount = new Count(1, 5);
    public GameObject exit; //Prefab to spawn for exit.
    public GameObject[] floorTiles; //Array of floor prefabs.
    public GameObject[] wallTiles; //Array of wall prefabs.
    public GameObject[] foodTiles; //Array of food prefabs.
    public GameObject[] enemyTiles; //Array of enemy prefabs.
    public GameObject[] outerWallTiles; //Array of outer tile prefabs.

    //A variable to store a reference to the transform of our Board object.
    private Transform boardHolder;

    //A list of possible locations to place tiles.
    private List <Vector3> gridPositions = new List<Vector3>();

    //Clears our list gridPositions and prepares it to generate a new board.
    void InitialiseList()
    {
        gridPositions.Clear(); //Clear our list gridPositions.

        for(int x = 1; x < columns - 1; x++) //Loop through x axis (columns).
        {
            for(int y = 1; y < rows - 1; y++) //Within each column, loop through y axis (rows).
            {
                //At each index add a new Vector3 to our list with the x and y coordinates of that position.
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    //Sets up the outer walls and floor (background) of the game board.
    void BoardSetup ()
    {
        //Instantiate Board and set boardHolder to its transform.
        boardHolder = new GameObject ("Board").transform;

        //Loop along x axis, starting from -1 (to fill corner) with floor or outerwall edge tiles.
        for(int x = -1; x < columns + 1; x++)
        {
            //Loop along y axis, starting from -1 to place floor or outerwall tiles.
            for(int y = -1; y < rows + 1; y++)
            {
                //Choose a random tile from our array of floor tile prefabs and prepare to instantiate it.
                GameObject toInstantiate = floorTiles[Random.Range (0, floorTiles.Length)];

                //Check if we current position is at board edge, if so choose a random outer wall prefab from our array of outer wall tiles.
                if(x == -1 || x == columns || y == -1 || y == rows)
                    toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];

                    //Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
                    GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                    //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
                    instance.transform.SetParent(boardHolder);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
