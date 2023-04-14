using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    private GameObject[] KeyHUD = new GameObject[5];
    
    // Start is called before the first frame update
    void Start()
    {
        KeyHUDSpawner();
    }

    // Update is called once per frame
    void Update()
    {
        


    }
    void KeyHUDSpawner()
    {
        KeyHUD[0] = Instantiate(Resources.Load("KeyHUD", typeof(GameObject))) as GameObject;
        KeyHUD[0].GetComponent<Transform>().transform.position = new Vector2(4, -286f);
    }
}
