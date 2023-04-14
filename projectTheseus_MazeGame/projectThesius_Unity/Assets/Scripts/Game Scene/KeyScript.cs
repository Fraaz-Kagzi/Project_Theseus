using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{

    
    
    private void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.name == "PlayerCube1st3rd")
        {
           
            
            Debug.Log("player touching key");
            GameVariables.KeyCount += 1;
            Destroy(gameObject);
        }
        
    }
    
}
