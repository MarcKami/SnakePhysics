using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinMovement : MonoBehaviour {

    private float idleDelay = 0.5f;
    public Transform[] joints;

    private float PositionFX;
    private float PositionFY;

    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {

        float module = Mathf.Sin(Time.time % 2 * Mathf.PI);
        float modifier = 0.66f;
        float timer = Time.fixedTime;
        float a = 30 * Mathf.Rad2Deg;
        
        for (int i = 0; i < joints.Length; i++) {
            PositionFX= joints[i].position.x;
            PositionFY= joints[i].position.y;
            float n = Vector3.Distance(joints[i + 1].position, joints[i].position);
            Vector3 idleTilt = new Vector3(joints[i].position.x + module * modifier, joints[i].position.y, joints[i].position.z - 1.0f);

            //Vector3 idleTilt = new Vector3(joints[i].position.x + module * modifier, joints[i].position.y, joints[i].position.z - 1.0f);
            //Vector3 idleTilt = new Vector3(transform.position.x + module * modifier, transform.position.y, transform.position.z - 1.0f);

            //joints[i].position = Vector3.Lerp(joints[i].position, idleTilt, idleDelay);
            //transform.position = Vector3.Lerp(transform.position, idleTilt, idleDelay);

            //PositionFX =  0.5f*Mathf.Sin(5f * (joints[i].position.x) - 2* Time.time);

            //joints[i].position = new Vector3(joints[i].position.x + (Mathf.Sin(timer + i) * 0.0008f), joints[i].position.y, joints[i].position.z);

            joints[i].position = new Vector3((1 / n * Mathf.Sin(a * Mathf.Cos(1 / n) + (1 / n)))*0.05f, joints[i].position.y, joints[i].position.z); ;
        }

    }
}
