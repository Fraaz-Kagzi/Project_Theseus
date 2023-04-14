using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart2 : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (GameVariables.Health <= 1)
        {
            Destroy(gameObject);
        }
    }
}
