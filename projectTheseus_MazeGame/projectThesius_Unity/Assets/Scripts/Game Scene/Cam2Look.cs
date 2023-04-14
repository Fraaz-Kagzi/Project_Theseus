using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam2Look : MonoBehaviour
{
    private float rspeedvert = 2.0f;

    int limit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        float rvert = rspeedvert * Input.GetAxis("Mouse Y");

        limit = 1;
        if(transform.rotation.eulerAngles.x > 60 && transform.rotation.eulerAngles.x <= 89 && rvert<0)
        {
            limit = 0;
        }
        if (transform.rotation.eulerAngles.x < 300 && transform.rotation.eulerAngles.x >= 271 && rvert > 0)
        {
            limit = 0;
        }
        //Debug.Log(transform.rotation.eulerAngles.x * limit);
        
        transform.Rotate(-rvert * limit, 0, 0);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
