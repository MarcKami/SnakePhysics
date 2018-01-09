﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinMovement : MonoBehaviour {

    private float idleDelay = 0.5f;
    public Transform[] joints;

    private float bias = 0;

    private float posX;
    private float posY;
    private float posZ;

    private float rotX;
    private float rotY;
    private float rotZ;

    private float scaleX;
    private float scaleY;
    private float scaleZ;

    private LineRenderer Force1;

    public float amplitude;
    public float frequency;

    // Use this for initialization
    public Color c1 = Color.yellow;
    public Color c2 = Color.red;
    public int lengthOfLineRenderer = 20;
    void Start()
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.SetColors(c1, c2);
        lineRenderer.SetWidth(0.2F, 0.2F);
        lineRenderer.SetVertexCount(lengthOfLineRenderer);

       

        bias = Time.time;

    }
    // Update is called once per frame
    void Update () {

        float module = Mathf.Sin(Time.time % 2 * Mathf.PI);
        float modifier = 0.66f;
        float timer = Time.fixedTime;
        float a = 30 * Mathf.Rad2Deg;
        float speed;
        float j = 0;


        for (int i = 0; i < joints.Length; i++) {
            
            float n = Vector3.Distance(joints[i + 1].position, joints[i].position);
            Vector3 idleTilt = new Vector3(joints[i].position.x + module * modifier, joints[i].position.y, joints[i].position.z - 1.0f);
            /*
            posX = joints[i].position.x;
            posY = joints[i].position.y;
            posZ = joints[i].position.z;

            rotX = joints[i].rotation.eulerAngles.x;
            rotY = joints[i].rotation.eulerAngles.y;
            rotZ = joints[i].rotation.eulerAngles.z;

            scaleX = joints[i].localScale.x;
            scaleY = joints[i].localScale.y;
            scaleZ = joints[i].localScale.z;*/

            speed = sinSpeed((Time.time - bias), amplitude, frequency, joints[i].position.z);

            //Vector3 idleTilt = new Vector3(joints[i].position.x + module * modifier, joints[i].position.y, joints[i].position.z - 1.0f);
            //Vector3 idleTilt = new Vector3(transform.position.x + module * modifier, transform.position.y, transform.position.z - 1.0f);

            //joints[i].position = Vector3.Lerp(joints[i].position, idleTilt, idleDelay);
            //transform.position = Vector3.Lerp(transform.position, idleTilt, idleDelay);

            //PositionFX =  0.5f*Mathf.Sin(5f * (joints[i].position.x) - 2* Time.time);

            //joints[i].position = new Vector3(joints[i].position.x + (Mathf.Sin(timer + i) * 0.0008f), joints[i].position.y, joints[i].position.z);


            // joints[i].transform.position = new Vector3(speed, joints[i].position.y, joints[i].position.z);
            
                joints[i].position = new Vector3(joints[i].position.x + (Mathf.Sin(timer - joints[i].position.z * 2) * 0.005f + (Mathf.Sin(timer - joints[i].position.z / 2) * 0.005f)), joints[i].position.y, joints[i].position.z - 0.001f);
            /*
            if (joints[i].position.x < 0)
            {

                Force1.SetPosition(0, joints[i].position);
            }*/
            
            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            int y = 0;
            while (y < lengthOfLineRenderer)
            {
                //Vector3 pos = new Vector3(joints[i].position.x + y * 0.5F, joints[i].position.y, joints[i].position.z);
                Vector3 pos = new Vector3(joints[i].position.x  , joints[i].position.y, joints[i].position.z + y);

                lineRenderer.SetPosition(y, pos);
                y++;
            }


            
            //joints[i].position = new Vector3((1 / n * Mathf.Sin(a * Mathf.Cos((1 / n)*2) + (1 / n)) * (joints[i].position.x)) *0.05f, joints[i].position.y, joints[i].position.z); ;
        }
        
    }

    public float sinSpeed(float t, float amplitude, float frequency, float x)
    {
        float omega = ((Mathf.PI * 2) / frequency);
        float current = amplitude * omega * Mathf.Sin(omega * (t + x));
        return current;
    }
}
