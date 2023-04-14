using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key2 : MonoBehaviour
{
    public GameObject Key;
    // Start is called before the first frame update
    void Start()
    {
        Key.SetActive(false);
        if (GameVariables.KeySpawn == 2)
            Key.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameVariables.KeyCount >= GameVariables.KeySpawn - 1)
            Key.SetActive(false);
        else
            Key.SetActive(true);
    }
}