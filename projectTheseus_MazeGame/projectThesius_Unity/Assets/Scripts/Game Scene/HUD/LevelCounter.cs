using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCounter : MonoBehaviour
{
    
    public GameObject levelCounter;

    // Update is called once per frame
    void Update()
    {
        levelCounter.GetComponent<Text>().text = "LEVEL " + GameVariables.Level;
    }
}
