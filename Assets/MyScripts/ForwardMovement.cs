using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardMovement : MonoBehaviour {

    private float idleDelay = 0.5f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 idleTilt = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1.0f);


        transform.position = Vector3.Lerp(transform.position, idleTilt, idleDelay);

    }
}
