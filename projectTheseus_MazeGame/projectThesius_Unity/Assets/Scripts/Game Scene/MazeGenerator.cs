using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MazeGenerator : MonoBehaviour
{

    public static int n = 10;

    public int connectedness; //0-100
    public int difficulty; //0-100

    //Game Variables
    public GameObject player;
    public static int keyCount;
    public GameObject PauseScreen;
    private bool pauseMenu;



    public GraphClass graphMx;

    public int[] enemyList;
    

    public string[] enemyDict = new string[5];

    // Use this for initialization

    //Visuals
    private GameObject[,] mazeGrid;
    private GameObject[] mazeWall;
    private GameObject[] enemyArray;
    private GameObject[] mazeElevators = new GameObject[3];


    void Start()
    {
        NewLevel();

        ElevatorMaker();
        GateSpawner();

        player.transform.position = new Vector3((n - 1) / 2 * 10, 3f, -n * 10);

    }

    public void NewLevel()
    {
        Debug.Log("LEVEL" + GameVariables.Level);
       
        GameVariables.InEndlift = false;
        GameVariables.KeyCount = 0;
        GameVariables.Health = 3;
        int[,] mazeMx = new int[n, n];

        //int[] enemyList = new int[1];

        //enemyList[0] = 0;

        GenerateMatrix(mazeMx);

        //FixGraph(mazeMx);

        ZeroFilter(mazeMx);

        FixGraph(mazeMx);

        ThreeFilter(mazeMx);

        MazeVisualizer(mazeMx);

        graphMx = new GraphClass(n);

        MatrixToGraph(mazeMx);

        LevelSpawner(enemyList);

        //Enemies
        KeySpawner();



    }

    // Update is called once per frame
    void Update()
    {
        //END GAME
        if (GameVariables.Health <= 0)
        {
            //Time.timeScale = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            GameVariables.gameFinish = true;
            

        }
        /* n = 9 + GameVariables.Level;
         if (n >= 21)
             n = 21;*/
      
        // resets maze and ai
        /*if (Input.GetKey(KeyCode.T))
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    //Resources.UnloadAsset(mazeGrid[i, j]);
                    Destroy(mazeGrid[i, j]);
                }
            }
            Debug.Log("MazeDestroyed");
            for (int i = 0; i < mazeWall.Length; i++)
            {
                Destroy(mazeWall[i]);
            }
            for (int i = 0; i < enemyArray.Length; i++)
            {
                Destroy(enemyArray[i]);
            }
            NewLevel();
           

        }*/
        if (Input.GetKey(KeyCode.L))
        {
            Player.stamina = 100;
        }
            //Next level
            if (GameVariables.InEndlift == true)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    //Resources.UnloadAsset(mazeGrid[i, j]);
                    Destroy(mazeGrid[i, j]);
                }
            }
            Debug.Log("MazeDestroyed");
            for (int i = 0; i < mazeWall.Length; i++)
            {
                Destroy(mazeWall[i]);
            }
            for (int i = 0; i < enemyArray.Length; i++)
            {
                Destroy(enemyArray[i]);
            }
            Destroy(mazeElevators[0]);
            Destroy(mazeElevators[1]);
            GameVariables.Level++;
            n++;
            if (n > 21)
                n = 21;
            if((GameVariables.Level) % 3 == 0)
            {
                GameVariables.KeySpawn++;
                if (GameVariables.KeySpawn >= 4)
                    GameVariables.KeySpawn = 4;
                GameVariables.NoOfEnemies += 2;
                if (GameVariables.NoOfEnemies >= 7)
                    GameVariables.NoOfEnemies = 7;

            }
            NewLevel();
            ElevatorMaker();
            GateSpawner();
            Player.stamina = 100;
            player.transform.position = new Vector3((n - 1) / 2 * 10, 3f, -n * 10);
        }
        if (Input.GetKey(KeyCode.Escape))
        {            
            PauseScreen.SetActive(true);
            pauseMenu = true;
        }
        if(pauseMenu == true)
        {
            if (Input.GetKey(KeyCode.E))
            {
                PauseScreen.SetActive(false);
            }
        }
     }


    public int Ninja(Vector3 targetVec)
    {
        int i = (int)Mathf.Round(targetVec.x / 10f);
        int j = (int)Mathf.Round(targetVec.z / 10f);

        int a = i + n * j;
        return a;
    } //takes in Vector3 position and rounds to node index


    void GenerateMatrix(int[,] mazeMx)
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                mazeMx[i, j] = Random.Range(0, 4); 
                //Debug.Log("Matrix" + "(" + i.ToString() + ","+ j.ToString() + ")" + "=="+ mazeMx[i,j]);
            }
        }
        mazeMx[n - 2, n - 1] = 1;
        mazeMx[(n - 1) / 2, n - 1] = 3;
    }


    void FixGraph(int[,] mazeMx)
    {
        Vector2[] linkSet = new Vector2[n * n];

        int count = 0;
        while ((CountSet(mazeMx, linkSet) < n * n) && (count < count + 1))
        //while (count < 10)
        {
            //CountSet(mazeMx, linkSet);

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - 1; j++)
                {
                    int flip = Random.Range(0, 2);

                    if (flip == 0)
                    {
                        if (AddToSet(new Vector2(i + 1, j), linkSet))
                        {
                            mazeMx[i, j] = mazeMx[i, j] + 1;
                            CountSet(mazeMx, linkSet);
                        }
                        else if (AddToSet(new Vector2(i, j + 1), linkSet))
                        {
                            mazeMx[i, j] = mazeMx[i, j] + 2;
                            CountSet(mazeMx, linkSet);
                        }
                    }

                    if (flip == 1)
                    {
                        if (AddToSet(new Vector2(i, j + 1), linkSet))
                        {
                            mazeMx[i, j] = mazeMx[i, j] + 2;
                            CountSet(mazeMx, linkSet);
                        }
                        else if (AddToSet(new Vector2(i + 1, j), linkSet))
                        {
                            mazeMx[i, j] = mazeMx[i, j] + 1;
                            CountSet(mazeMx, linkSet);
                        }
                    }
                }
            }

            count++;

        }

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (AddToSet(new Vector2(i, j), linkSet))
                {
                    Debug.Log("added to set (" + i.ToString() + "," + j.ToString() + ")" + "==" + mazeMx[i, j]);
                }

                //Debug.Log("MatrixAm" + "(" + i.ToString() + "," + j.ToString() + ")" + "==" + mazeMx[i, j]);
            }
        }

    }

    int CountSet(int[,] mazeMx, Vector2[] linkSet)
    {
        ThreeFilter(mazeMx);

        for (int i = 0; i < n * n; i++)
        {
            linkSet[i] = new Vector2(-1, -1);
        }

        SetCount(0, 0, mazeMx, linkSet);
        int setSum = 0;

        for (int i = 0; i < n * n; i++)
        {
            if (linkSet[i] != new Vector2(-1, -1))
            {
                setSum = setSum + 1;
                //Debug.Log("Link Set " + linkSet[i]);
            }
        }
        //Debug.Log("Set Sum " + setSum);        

        return setSum;
    }

    void SetCount(int x, int y, int[,] mazeMx, Vector2[] linkSet)
    {
        AddToSet(new Vector2(x, y), linkSet); //ADD BEFORE 1ST ITERATION

        if (x < n - 1)
        {
            if (mazeMx[x, y] == 1 || mazeMx[x, y] == 3)
            {
                if (AddToSet(new Vector2(x + 1, y), linkSet))
                {
                    SetCount(x + 1, y, mazeMx, linkSet);
                }
            }
        }
        if (y < n - 1)
        {
            if (mazeMx[x, y] == 2 || mazeMx[x, y] == 3)
            {
                if (AddToSet(new Vector2(x, y + 1), linkSet))
                {
                    SetCount(x, y + 1, mazeMx, linkSet);
                }
            }
        }

        if (x > 0)
        {
            if (mazeMx[x - 1, y] == 1 || mazeMx[x - 1, y] == 3)
            {
                if (AddToSet(new Vector2(x - 1, y), linkSet))
                {
                    SetCount(x - 1, y, mazeMx, linkSet);
                }
            }
        }

        if (y > 0)
        {
            if (mazeMx[x, y - 1] == 2 || mazeMx[x, y - 1] == 3)
            {
                if (AddToSet(new Vector2(x, y - 1), linkSet))
                {
                    SetCount(x, y - 1, mazeMx, linkSet);
                }
            }
        }

    }

    bool AddToSet(Vector2 vec, Vector2[] linkSet)
    {
        for (int i = 0; i < n * n; i++)
        {
            if (linkSet[i] != new Vector2(-1, -1) && linkSet[i] == vec)
            {
                linkSet[i] = vec;
                return false;
            }
        }
        for (int i = 0; i < n * n; i++)
        {
            if (linkSet[i] == new Vector2(-1, -1))
            {
                linkSet[i] = vec;
                return true;
            }
        }
        return false;
    }


    void ZeroFilter(int[,] mazeMx)
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (mazeMx[i, j] == 0)
                {
                    if (Random.Range(0, 100) < connectedness)
                    {
                        mazeMx[i, j] = mazeMx[i, j] + Random.Range(1, 4);
                    }
                }
            }
        }
    }

    void ThreeFilter(int[,] mazeMx)
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (mazeMx[i, j] > 3)
                {
                    mazeMx[i, j] = 3;
                }
            }
        }
    }


    void MazeVisualizer(int[,] mazeMx)
    {
        mazeGrid = new GameObject[n, n];
        mazeWall = new GameObject[4 * n - 2];
        int wallCounter = 0;

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                //mazeGrid[i, j] = Instantiate(Resources.Load("wall3", typeof(GameObject))) as GameObject;

                if (mazeMx[i, j] == 0)
                {
                    mazeGrid[i, j] = Instantiate(Resources.Load("wall0", typeof(GameObject))) as GameObject;
                    //Debug.Log("Wall 0");
                }
                if (mazeMx[i, j] == 1)
                {
                    mazeGrid[i, j] = Instantiate(Resources.Load("wall1", typeof(GameObject))) as GameObject;
                    //Debug.Log("Wall 1");
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
                if (i == n - 1 && mazeMx[i, j] != 0 && mazeMx[i, j] != 2)
                {
                    mazeWall[wallCounter] = Instantiate(Resources.Load("RightWall", typeof(GameObject))) as GameObject;
                    mazeWall[wallCounter].GetComponent<Transform>().transform.position = new Vector3(10 * n - 5, 7.5f, -10 * j);
                    wallCounter++;
                }
                if (j == 0 && i != (n - 1) / 2)
                {
                    mazeWall[wallCounter] = Instantiate(Resources.Load("DownWall", typeof(GameObject))) as GameObject;
                    mazeWall[wallCounter].GetComponent<Transform>().transform.position = new Vector3(i * 10, 7.5f, 5);
                    wallCounter++;
                }
                if (j == n - 1 && i != (n - 1) / 2 && mazeMx[i, j] != 0 && mazeMx[i, j] != 1)
                {
                    mazeWall[wallCounter] = Instantiate(Resources.Load("DownWall", typeof(GameObject))) as GameObject;
                    mazeWall[wallCounter].GetComponent<Transform>().transform.position = new Vector3(i * 10, 7.5f, -10 * n + 5);
                    wallCounter++;
                }

            }
        }

    }

    void ElevatorMaker()
    {
        

        mazeElevators[0] = Instantiate(Resources.Load("EndElevator", typeof(GameObject))) as GameObject;
        mazeElevators[0].GetComponent<Transform>().transform.position = new Vector3((n - 1) / 2 * 10, 0f, 10);
        mazeElevators[0].GetComponent<Transform>().transform.Rotate(new Vector3(0, 1, 0), 180f);

        mazeElevators[1] = Instantiate(Resources.Load("Elevator", typeof(GameObject))) as GameObject;
        mazeElevators[1].GetComponent<Transform>().transform.position = new Vector3((n - 1) / 2 * 10, 0f, -n * 10);



    }
    void GateSpawner()
    {
        GameObject[] Gate = new GameObject[2];
        Gate[0] = Instantiate(Resources.Load("Gate", typeof(GameObject))) as GameObject;
        Gate[0].GetComponent<Transform>().transform.position = new Vector3((n - 1) / 2 * 10, 2f, 10 - 5);
    }



    void MatrixToGraph(int[,] mazeMx)
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (i != n - 1)
                {
                    if ((mazeMx[i, j]) % 2 == 1)
                    {
                        graphMx.AddEdge(i + n * j, (i + 1) + n * j);
                    }
                }
                if (j != n - 1)
                {
                    if ((mazeMx[i, j]) / 2 >= 1)
                    {
                        graphMx.AddEdge(i + n * j, i + n * (j + 1));
                    }
                }
            }
        }



    }

    void LevelSpawner(int[] enemyList)
    {
        enemyArray = new GameObject[enemyList.Length];
        if(GameVariables.NoOfEnemies >= 1)
        {
            for (int i = 0; i < GameVariables.NoOfEnemies; i++)
            {
                string enemyName = enemyDict[enemyList[i]];
                enemyArray[i] = Instantiate(Resources.Load(enemyName, typeof(GameObject))) as GameObject;
                enemyArray[i].transform.position = new Vector3(10 * Random.Range(0, n), 0, -10 * Random.Range(0, n));

                enemyArray[i].GetComponent<Enemy>().Values(graphMx, n, enemyName);
            }
        }
        

    }
    void KeySpawner()
    {
        GameObject[] KeyCount = new GameObject[3];
        for (int i = 0; i < GameVariables.KeySpawn; i++)
        {
            KeyCount[i] = Instantiate(Resources.Load("Key", typeof(GameObject))) as GameObject;
            KeyCount[i].transform.position = new Vector3(10 * Random.Range(0, n), 1, -10 * Random.Range(0, n));

        }

    }
    
}

