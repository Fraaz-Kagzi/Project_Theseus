using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour { 

    private GraphClass graph;
    private int mazeSize;

    public GameObject targetPlayer;
    
    public int moveSpeed;
    public int lostRange;
    public int huntRange;
    public int patrolRange;
    public Vector3 targMv;
       

    private bool isMoving;

    private Vector3 targetPos;

    private string monsterName;

    void Start()
    {
        
        targetPlayer = GameObject.FindGameObjectWithTag("Player");
        if (targetPlayer == null)
        {
            Debug.Log("player not found");
        }
        //InvokeRepeating("MoveEnemy", 5f, thinkTime);
        //Vector3 a = new Vector3(10, 1, -10);
        //int b = Navigate(a);
        //Debug.Log(b);
    }

    void Update()
    {
        

        if ((targMv-transform.position).magnitude < 0.5)
        {
            isMoving = false;
        }

        if (!(isMoving))
        {
            Vector3 targ = TargetingSys();

            //Vector3 targ = targetObject.transform.position; //need reassign targ intargeting script
            targMv = DeNinja(Navigate(targ));

            isMoving = true;

            //Vector3 dif = targMv - transform.position;
        }
        
        MoveEnemy(Vector3.MoveTowards(transform.position,targMv,0.005f * moveSpeed));
    }

    public void Values(GraphClass newGraph,int n, string enemyName)
    {
        graph = newGraph;
        mazeSize = n;
        monsterName = enemyName;
    }

    public int Navigate(Vector3 targetVec)
    {
        Debug.Log(targetVec);
        int target = Ninja(targetVec);
        Debug.Log("target" + target);
        int seeker = Ninja(transform.position);
        Debug.Log("seeker" + seeker);
        //Debug.Log("seeker " + seeker);
        int result = seeker;
        float s = Time.realtimeSinceStartup;
        if (target >= 0 && target < (mazeSize*mazeSize))
        {
             result = graph.BreathFirst(target, seeker);
        }
       
        Debug.Log("result" + result);
        Debug.Log("Calc Time = " + (Time.realtimeSinceStartup - s));
        return result;


    }  //takes position of target and outputs node index of optimal move
    
    public int Ninja(Vector3 targetVec)
    {
        int i = (int) Mathf.Round(targetVec.x / 10f);
        int j = (int) Mathf.Round(targetVec.z / 10f);

        Debug.Log("i " + i);
        Debug.Log("j " + j);

        int a = i - mazeSize * j;
        return a;
    } //takes in Vector3 position and rounds to node index

    public Vector3 DeNinja(int index)
    {
        int x = 0;
        int z = 0;
        Debug.Log("mazesize is " + mazeSize);
        x = index % mazeSize;
        z = (index - x) / mazeSize;
        Vector3 pos = new Vector3(x * 10, 0, -z * 10);  //MONSTER HEIGHT HERE

        return pos;
    } //takes node index and outputs Vector3 position

   
    public void MoveEnemy(Vector3 destination)
    {
        Vector3 path = destination - transform.position;
        Vector3 rotpath = new Vector3(path.x, 0, path.z);
        transform.rotation = Quaternion.LookRotation(rotpath.normalized, new Vector3(0, 1, 0));
        transform.Translate(path, 0);
    } //takes destination vector and translates object to it
    

    //Targeting System and Individual monster strategies

    public Vector3 TargetingSys()
    {
        if ((transform.position - targetPlayer.transform.position).magnitude > lostRange || (transform.position - targetPlayer.transform.position).magnitude < huntRange)
        {
            targetPos = targetPlayer.transform.position;
        }
        if ((transform.position - targetPlayer.transform.position).magnitude < lostRange && (transform.position - targetPlayer.transform.position).magnitude > huntRange)
        {
            targetPos = DeNinja(Random.Range(0, mazeSize));
        }

        switch (monsterName)
        {
            case "TrollGiant":
                TrollGiant();
                break;
            case "TrollGiant1":
                TrollGiant1();
                break;
            case "TrollGiant2":
                TrollGiant2();
                break;
            case "TrollGiant3":
                TrollGiant3();
                break;
            case "TrollGiant4":
                TrollGiant4();
                break;
        }
        
        return targetPos;
    }

    private void TrollGiant()
    {
        //Debug.Log("TrollGiant Script");
    }
    private void TrollGiant1()
    {

    }
    private void TrollGiant2()
    {

    }
    private void TrollGiant3()
    {

    }
    private void TrollGiant4()
    {

    }

}
