using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key1 : MonoBehaviour
{
    public GameObject Key;
    public GameObject Key2;
    public GameObject Key3;
    public GameObject Key4;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        //respawn keys 
        /*
        if(GameVariables.resetKeys == true && (GameVariables.KeySpawn >= 1))
        {
            Key.SetActive(true);
                
        }
        if (GameVariables.resetKeys == true && (GameVariables.KeySpawn >= 2))
        {
            Key2.SetActive(true);
            
        }
        if (GameVariables.resetKeys == true && (GameVariables.KeySpawn >= 3))
        {
            Key3.SetActive(true);
            
        }
        if (GameVariables.resetKeys == true && (GameVariables.KeySpawn >= 4))
        {
            Key4.SetActive(true);
            
        }
        GameVariables.resetKeys = false; */

        if (GameVariables.KeyCount >= GameVariables.KeySpawn)
            Key.SetActive(false);
        else
            Key.SetActive(true);

        if (GameVariables.KeyCount >= GameVariables.KeySpawn - 1)
            Key2.SetActive(false);
        else
            Key2.SetActive(true);

        if (GameVariables.KeyCount >= GameVariables.KeySpawn - 2)
            Key3.SetActive(false);
        else
            Key3.SetActive(true);

        if (GameVariables.KeyCount >= GameVariables.KeySpawn - 3)
            Key4.SetActive(false);
        else
            Key4.SetActive(true);

        /*else
            Key.SetActive(true);*/




    }
}
