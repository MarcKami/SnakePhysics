﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementWASD : MonoBehaviour {

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var z = Input.GetAxis("Vertical") * Time.deltaTime;
        var x = Input.GetAxis("Horizontal") * Time.deltaTime;

        transform.Translate(-x, 0, -z);

    }
}
