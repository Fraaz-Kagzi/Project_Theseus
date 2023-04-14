using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AqcuireChanRotate : MonoBehaviour
{

    private float rspeed = 0.1f;
    private float rspeedvert = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float r = rspeed * Input.GetAxis("Mouse X");
        float rvert = rspeedvert * Input.GetAxis("Mouse Y");
        transform.Rotate(-rvert * 0, r, 0);
    }
}
