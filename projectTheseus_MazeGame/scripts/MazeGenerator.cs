using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MazeGenerator : MonoBehaviour
{
    public int mazeSize;

    public GraphClass graphMx;

    public int[] enemyList;

    public string[] enemyDict = new string[5];

    // Use this for initialization

    //Visuals
    private GameObject[,] mazeGrid;
    private GameObject[] mazeWall;
    private GameObject[] enemyArray;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        NewLevel();
        ElevatorMaker();
        player.transform.position = new Vector3(70, 0, 8);
        player.transform.Rotate(0, 180, 0);

    }

    // Update is called once per frame
    void Update()
    {

    }
    public int Ninja(Vector3 targetVec)
    {
        int i = (int)Mathf.Round(targetVec.x / 10f);
        int j = (int)Mathf.Round(targetVec.z / 10f);

        int a = i + mazeSize * j;
        return a;
    } //takes in Vector3 position and rounds to node index
    void NewLevel()
    {

        int[,] mazeMx = new int[mazeSize, mazeSize];
        GenerateMatrix(mazeMx);
        FixGraph(mazeMx);
        MazeVisualizer(mazeMx);
        graphMx = new GraphClass(mazeSize);
        MatrixToGraph(mazeMx);
        LevelSpawner(enemyList);
    }
    void GenerateMatrix(int[,] mazeMx)
    {
        for (int i = 0; i < mazeSize; i++)
        {
            for (int j = 0; j < mazeSize; j++)
            {
                mazeMx[i, j] = Random.Range(0, 4); //0,1,2,3 each combination of bottom and right walls
                //Debug.Log(i.ToString() + j.ToString() + "Matrix value " + mazeMx[i, j]);
            }
        }
        mazeMx[mazeSize - 2, mazeSize - 1] = 1;//set as 1 to avoid errors
        mazeMx[(mazeSize - 1) / 2, mazeSize - 1] = 3;// forced to 3 in order to allow a clear exit from elevator
    }

    void FixGraph(int[,] mazeMx)
    {
       
        Vector2[] linkSet = new Vector2[mazeSize * mazeSize];  // an array of coordinates that are linked to another accessable coordinate
        while ((CountSet(mazeMx, linkSet) < mazeSize * mazeSize)) 
        {
            for (int i = 0; i < mazeSize - 1; i++)
            {
                for (int j = 0; j < mazeSize - 1; j++)
                {
                    if ((AddToSet(new Vector2(i + 1, j), linkSet))&& ((mazeMx[i,j] == 0)|| (mazeMx[i, j] == 2)))
                    {
                        mazeMx[i, j] = mazeMx[i, j] + 1; // breaks right wall
                        CountSet(mazeMx, linkSet); // resets and does depth first again
                    }
                    else if (AddToSet(new Vector2(i, j + 1), linkSet) && ((mazeMx[i, j] == 0) || (mazeMx[i, j] == 1)))
                    {
                         mazeMx[i, j] = mazeMx[i, j] + 2; // breaks down wall
                         CountSet(mazeMx, linkSet);
                     }
                }
            }
            
        }
    }
    int CountSet(int[,] mazeMx, Vector2[] linkSet)
    {
        for (int i = 0; i < mazeSize * mazeSize; i++)
        {
            linkSet[i] = new Vector2(-1, -1);
        }

        DepthFirst(0, 0, mazeMx, linkSet); // starts depth first at matrix origin 0,0
        int setSum = 0;

        for (int i = 0; i < mazeSize * mazeSize; i++)
        {
            if (linkSet[i] != new Vector2(-1, -1))
            {
                setSum = setSum + 1; // needs to be equal to n*n to be a maze where every square is accessable
                
            }
        }
         
        return setSum;
    }
    void DepthFirst(int x, int y, int[,] mazeMx, Vector2[] linkSet)
    {
        AddToSet(new Vector2(x, y), linkSet); // adds starting square to link set
        // order priority: right, down ,left, up

        if (x < mazeSize - 1) // has to have right border wall if x< mazeSize -1
        {
            if (mazeMx[x, y] == 1 || mazeMx[x, y] == 3) // if the coordinates have no right wall
            {
                if (AddToSet(new Vector2(x + 1, y), linkSet)) //checks  right
                {
                    DepthFirst(x + 1, y, mazeMx, linkSet); // recursive function
                }
            }
        }

        if (y < mazeSize - 1) // has to have down border wall if y< mazeSize -1
        {
            if (mazeMx[x, y] == 2 || mazeMx[x, y] == 3) // if the coordinates have no down wall
            {
                if (AddToSet(new Vector2(x, y + 1), linkSet)) //checks down
                {
                    DepthFirst(x, y + 1, mazeMx, linkSet);
                }
            }
        }

        if (x > 0)
        {
            if (mazeMx[x - 1, y] == 1 || mazeMx[x - 1, y] == 3)
            {
                if (AddToSet(new Vector2(x - 1, y), linkSet)) // checks left
                {
                    DepthFirst(x - 1, y, mazeMx, linkSet);
                }
            }
        }

        if (y > 0)
        {
            if (mazeMx[x, y - 1] == 2 || mazeMx[x, y - 1] == 3)
            {
                if (AddToSet(new Vector2(x, y - 1), linkSet)) // checks up
                {
                    DepthFirst(x, y - 1, mazeMx, linkSet);
                }
            }
        }


    }
    bool AddToSet(Vector2 vec, Vector2[] linkSet)
    {
        for (int i = 0; i < mazeSize * mazeSize; i++)
        {
            if (linkSet[i] != new Vector2(-1, -1) && linkSet[i] == vec)// checks if (x,y) is already in linkset
            {
                
                return false; // returns false if already in linkset
            }
        }
        for (int i = 0; i < mazeSize * mazeSize; i++)
        {
            if (linkSet[i] == new Vector2(-1, -1)) // already checked if (x,y) is in linkset
            {
                linkSet[i] = vec;
                return true; // returns true if not in linkset, adds vector to link set
            }
        }
        return false;
    }
    void MazeVisualizer(int[,] mazeMx)
    {
        mazeGrid = new GameObject[mazeSize, mazeSize];
        mazeWall = new GameObject[4 * mazeSize - 2];
        int wallCounter = 0;

        for (int i = 0; i < mazeSize; i++)
        {
            for (int j = 0; j < mazeSize; j++)
            {
                

                if (mazeMx[i, j] == 0)
                {
                    mazeGrid[i, j] = Instantiate(Resources.Load("wall0", typeof(GameObject))) as GameObject;
                    
                }
                if (mazeMx[i, j] == 1)
                {
                    mazeGrid[i, j] = Instantiate(Resources.Load("wall1", typeof(GameObject))) as GameObject;
                    
                }
                if (mazeMx[i, j] == 2)
                {
                    mazeGrid[i, j] = Instantiate(Resources.Load("wall2", typeof(GameObject))) as GameObject;
                    //Debug.Log("Wall 2");
                }
                if (mazeMx[i, j] == 3)
                {
                    mazeGrid[i, j] = Instantiate(Resources.Load("wall3", typeof(GameObject))) as GameObject;
                    //Debug.Log("Wall 3");
                }

                mazeGrid[i, j].GetComponent<Transform>().transform.position = new Vector3(i * 10, 0, -10 * j);



                if (i == 0)
                {
                    mazeWall[wallCounter] = Instantiate(Resources.Load("RightWall", typeof(GameObject))) as GameObject;
                    mazeWall[wallCounter].GetComponent<Transform>().transform.position = new Vector3(-5, 7.5f, -10 * j);
                    wallCounter++;
                }
                if (i == mazeSize - 1 && mazeMx[i, j] != 0 && mazeMx[i, j] != 2)
                {
                    mazeWall[wallCounter] = Instantiate(Resources.Load("RightWall", typeof(GameObject))) as GameObject;
                    mazeWall[wallCounter].GetComponent<Transform>().transform.position = new Vector3(10 * mazeSize - 5, 7.5f, -10 * j);
                    wallCounter++;
                }
                if (j == 0 && i != (mazeSize - 1) / 2)
                {
                    mazeWall[wallCounter] = Instantiate(Resources.Load("DownWall", typeof(GameObject))) as GameObject;
                    mazeWall[wallCounter].GetComponent<Transform>().transform.position = new Vector3(i * 10, 7.5f, 5);
                    wallCounter++;
                }
                if (j == mazeSize - 1 && i != (mazeSize - 1) / 2 && mazeMx[i, j] != 0 && mazeMx[i, j] != 1)
                {
                    mazeWall[wallCounter] = Instantiate(Resources.Load("DownWall", typeof(GameObject))) as GameObject;
                    mazeWall[wallCounter].GetComponent<Transform>().transform.position = new Vector3(i * 10, 7.5f, -10 * mazeSize + 5);
                    wallCounter++;
                }

            }
        }

    }
    void ElevatorMaker()
    {
        GameObject[] mazeElevators = new GameObject[2];

        mazeElevators[0] = Instantiate(Resources.Load("Elevator", typeof(GameObject))) as GameObject;
        mazeElevators[0].GetComponent<Transform>().transform.position = new Vector3((mazeSize - 1) / 2 * 10, 0f, 10);
        mazeElevators[0].GetComponent<Transform>().transform.Rotate(new Vector3(0, 1, 0), 180f);

        mazeElevators[1] = Instantiate(Resources.Load("Elevator", typeof(GameObject))) as GameObject;
        mazeElevators[1].GetComponent<Transform>().transform.position = new Vector3((mazeSize - 1) / 2 * 10, 0f, -mazeSize * 10);

    }
    void MatrixToGraph(int[,] mazeMx)
    {
        for (int i = 0; i < mazeSize; i++)
        {
            for (int j = 0; j < mazeSize; j++)
            {
                if (i != mazeSize - 1)
                {
                    if ((mazeMx[i, j]) % 2 == 1)
                    {
                        graphMx.AddEdge(i + mazeSize * j, (i + 1) + mazeSize * j);
                    }
                }
                if (j != mazeSize - 1)
                {
                    if ((mazeMx[i, j]) / 2 >= 1)
                    {
                        graphMx.AddEdge(i + mazeSize * j, i + mazeSize * (j + 1));
                    }
                }
            }
        }



    }

    void LevelSpawner(int[] enemyList)
    {
        enemyArray = new GameObject[enemyList.Length];

        for (int i = 0; i < enemyList.Length; i++)
        {
            string enemyName = enemyDict[enemyList[i]];
            enemyArray[i] = Instantiate(Resources.Load(enemyName, typeof(GameObject))) as GameObject;
            enemyArray[i].transform.position = new Vector3(10 * Random.Range(0, mazeSize), 0, -10 * Random.Range(0, mazeSize));

            enemyArray[i].GetComponent<Enemy>().Values(graphMx, mazeSize, enemyName);
        }

    }

}


