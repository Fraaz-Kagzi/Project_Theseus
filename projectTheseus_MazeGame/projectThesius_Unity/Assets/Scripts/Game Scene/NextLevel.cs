using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    
        private void OnTriggerEnter(Collider collider)
    {
        

        if (collider.gameObject.name == "PlayerCube1st3rd")
        {

            GameVariables.InEndlift = true;
            GameVariables.resetKeys = true;
            if (GameVariables.resetKeys == true)
                Debug.Log("reset keys!");

            Debug.Log("NEW LEVEL");

        }
            

    }
}
