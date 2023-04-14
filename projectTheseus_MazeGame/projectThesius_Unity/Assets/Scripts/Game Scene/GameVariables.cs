using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class GameVariables
{
    public static int KeyCount;
    public static int KeySpawn = 1;
    public static int Health = 3;
    public static bool gameFinish = false;
    public static int NoOfEnemies = 1;
    public static bool InEndlift = false;
    public static bool resetKeys = true;
    public static int Level=1;

    public static GameObject player = GameObject.FindGameObjectWithTag("Player");
}



