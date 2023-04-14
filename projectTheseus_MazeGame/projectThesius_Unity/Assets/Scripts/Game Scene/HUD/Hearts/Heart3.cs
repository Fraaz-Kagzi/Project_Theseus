using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart3 : MonoBehaviour
{
    
    

    // Update is called once per frame
    void Update()
    {
        if(GameVariables.Health <= 2)
        {
            Destroy(gameObject);
        }
    }
}
