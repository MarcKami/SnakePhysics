using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinMovement : MonoBehaviour {

    private float idleDelay = 0.5f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        float module = Mathf.Sin(Time.time % 2 * Mathf.PI);
        float modifier = 0.66f;
        
        Vector3 idleTilt = new Vector3(transform.position.x + module * modifier, transform.position.y, transform.position.z - 1.0f);


        transform.position = Vector3.Lerp(transform.position, idleTilt, idleDelay);

    }
}
