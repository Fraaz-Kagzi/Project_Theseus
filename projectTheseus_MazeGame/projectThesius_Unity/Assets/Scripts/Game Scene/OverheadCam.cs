﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverheadCam : MonoBehaviour
{

    public GameObject followTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(followTarget.transform.position.x, this.transform.position.y, followTarget.transform.position.z);
    }
}
