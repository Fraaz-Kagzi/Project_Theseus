using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphClass : MonoBehaviour
{
    private int[,] graphCon;
    private int mazeSize;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GraphClass(int mazeSize)
    {
        graphCon = new int[mazeSize * mazeSize, 4];

        for (int i = 0; i < mazeSize * mazeSize; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                graphCon[i,j] = -1;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddEdge(int a, int b)
    {
        bool condition = true;
        for (int i = 0; i < 4; i++)
        {
            if((graphCon[a,i] == b) || (graphCon[b, i] == a))
            {
                condition = false;
            }
        }
        if (condition)
        {
            bool added = false;
            for (int i = 0; i < 4; i++)
            {
                if ((graphCon[a, i] == -1) && !(added))
                {
                    added = true;
                    graphCon[a, i] = b;
                }
            }
            added = false;
            for (int i = 0; i < 4; i++)
            {
                if ((graphCon[b, i] == -1) && !(added))
                {
                    added = true;
                    graphCon[b, i] = a;
                }
            }
        }
    }

    public int[,] GetGraphCon()
    {
        return graphCon;
    }


    public int BreathFirst(int target, int seeker)
    {
        Queue queue=new Queue(mazeSize);
        int temp = -1;
        Queue history = new Queue(mazeSize);

        queue.add(target);
        history.add(target);
        int count = 1;
        while (queue.getSize()!=0)
        {
            //Debug.Log(count + " : new while");
            count++;
            temp = queue.remove();
            //Debug.Log(count + " : Removed " + queue.getSize());
            count++;
            for (int i = 0; i<4; i++)
            {
                //Debug.Log(temp + " is temp");
                if(graphCon[temp,i]!=-1 && !(history.contains(graphCon[temp, i])))
                {
                    queue.add(graphCon[temp, i]);
                    //Debug.Log(count + " : Added " + queue.getSize());
                    count++;
                    history.add(graphCon[temp, i]);
                    if(graphCon[temp, i]==seeker)
                    {
                        return temp;
                    }
                }
            }
            
            //Debug.Log(count + " : The q is" + queue.getSize());
            count++;
        }

        return -1;
        
    }
    
}
