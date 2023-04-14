using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queue 
{
    private int[] queueArray;
    private int pointer = 0; // number of values in the array

    public Queue(int mazeSize)
    {
        queueArray = new int[mazeSize * mazeSize];
        for(int i=0;i < mazeSize * mazeSize; i++)
        {
            queueArray[i] = -1;//inistialise to -1 because 0 is a valid value
        }
    }


    public void add(int value)
    {
        queueArray[pointer] = value;
        pointer++; // add 1 to pointer so queue knows which value to add/remove
        //Debug.Log("Queue sizeA: " + pointer);
    }

    public int remove()
    {
        int temp = queueArray[0];
        for(int i = 0; i < pointer; i++)
        {
            queueArray[i] = queueArray[i + 1];//shifts every value in the queue down by 1
        }
        pointer--;// pointer minus 1
        //Debug.Log("Queue sizeR: " + pointer);

        return temp;

    }

    public bool contains(int value)
    {
        for (int i = 0; i < pointer; i++)// checks every value in queue
        {
            if (queueArray[i] == value)// if the int in queue position "i" is == value
            {
                return true; // //double slash to start comment
            }  
        }
        return false;
    }

    public int getSize()
    {
        return pointer;
    }

    public int[] getQueue()
    {
        return queueArray;
    }
}
