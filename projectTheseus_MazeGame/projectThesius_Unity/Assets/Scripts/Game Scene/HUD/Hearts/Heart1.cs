using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart1 : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (GameVariables.Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
