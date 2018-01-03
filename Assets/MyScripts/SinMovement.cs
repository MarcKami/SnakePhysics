using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinMovement : MonoBehaviour {

    private float idleDelay = 0.5f;
    public Transform[] joints;
    private MyVector3 axis;

    private float PositionFX;
    private float PositionFY;
    // Use this for initialization
    void Start () {
        axis.x = transform.right.x;
        axis.x = transform.right.x;
        axis.x = transform.right.x;
       
    }
	
	// Update is called once per frame
	void Update () {

        float module = Mathf.Sin(Time.time % 2 * Mathf.PI);
        float modifier = 0.66f;
        
        for (int i = 0; i < joints.Length; i++)
        {
            PositionFX= joints[i].position.x;
            PositionFY= joints[i].position.y;
            Vector3 idleTilt = new Vector3(joints[i].position.x + module * modifier, joints[i].position.y, joints[i].position.z - 1.0f);

            //Vector3 idleTilt = new Vector3(joints[i].position.x + module * modifier, joints[i].position.y, joints[i].position.z - 1.0f);
            //Vector3 idleTilt = new Vector3(transform.position.x + module * modifier, transform.position.y, transform.position.z - 1.0f);

            //joints[i].position = Vector3.Lerp(joints[i].position, idleTilt, idleDelay);
            //transform.position = Vector3.Lerp(transform.position, idleTilt, idleDelay);
            
            //PositionFX =  0.5f*Mathf.Sin(5f * (joints[i].position.x) - 2* Time.time);
            joints[i].position= new Vector3(joints[i].position.x+(Mathf.Sin(Time.time)*0.0005f), joints[i].position.y, joints[i].position.z);



        }
    }
}
