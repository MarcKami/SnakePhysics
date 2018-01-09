using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SinMovement : MonoBehaviour {
    
    [SerializeField] private Slider ForceSlider;
    [SerializeField] private Slider FrictionSlider;
    [SerializeField] private Slider MassSlider;
    [SerializeField] private Text ForceText;
    [SerializeField] private Text FrictionText;
    [SerializeField] private Text MassText;
    [SerializeField] private Text SpeedText;
    [SerializeField] private Text FinalForceText;

    private float idleDelay = 0.5f;
    public Transform[] joints;
    public float[] forcesAction;
    public float[] forcesReaction;
    public float[] forcesFinal;
    public float[] forcesFinalZ;
    public float totalForceZ;
    public float accel;
    public float speed;
    public float friction1, friction2;
    public float force;
    public float normal;
    public float mass;
    private float gravity;

    private float bias = 0;
    private float treshold;

    private float posX;
    private float posY;
    private float posZ;

    private float rotX;
    private float rotY;
    private float rotZ;

    private float scaleX;
    private float scaleY;
    private float scaleZ;

    public float amplitude;
    public float frequency;

    // Use this for initialization
    void Start() {
        forcesAction = new float[17];
        forcesReaction = new float[17];
        forcesFinal = new float[17];
        forcesFinalZ = new float[17];

        totalForceZ = 0.0f;
        accel = 0.0f;
        speed = 0.0f;

        friction1 = 0.9f;
        friction2 = 0.1f;
        force = 100.0f;

        mass = 5.0f;
        gravity = 9.81f;

        bias = Time.time;
        treshold = 0.70f;
    }

    // Update is called once per frame
    void Update () {

        float module = Mathf.Sin(Time.time % 2 * Mathf.PI);
        float timer = Time.fixedTime;

        totalForceZ = 0.0f;
        accel = 0.0f;
        speed = 0.0f;

        for (int i = 0; i < joints.Length; i++) {

            //float n = Vector3.Distance(joints[i + 1].position, joints[i].position);
            //Vector3 idleTilt = new Vector3(joints[i].position.x + module * modifier, joints[i].position.y, joints[i].position.z - 1.0f);
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

            //speed = sinSpeed((Time.time - bias), amplitude, frequency, joints[i].position.z);

            //Vector3 idleTilt = new Vector3(joints[i].position.x + module * modifier, joints[i].position.y, joints[i].position.z - 1.0f);
            //Vector3 idleTilt = new Vector3(transform.position.x + module * modifier, transform.position.y, transform.position.z - 1.0f);

            //joints[i].position = Vector3.Lerp(joints[i].position, idleTilt, idleDelay);
            //transform.position = Vector3.Lerp(transform.position, idleTilt, idleDelay);

            //PositionFX =  0.5f*Mathf.Sin(5f * (joints[i].position.x) - 2* Time.time);

            //joints[i].position = new Vector3(joints[i].position.x + (Mathf.Sin(timer + i) * 0.0008f), joints[i].position.y, joints[i].position.z);


            // joints[i].transform.position = new Vector3(speed, joints[i].position.y, joints[i].position.z);

            normal = (mass * Mathf.Abs(Mathf.Sin(timer - joints[i].position.z / 2))) * gravity;

            if (Mathf.Cos(timer - joints[i].position.z / 2) < treshold && Mathf.Sin(timer - joints[i].position.z / 2) < 0.0f) {
                forcesAction[i] = force - friction1 * normal; 
                forcesReaction[i] = force - friction2 * normal;
                forcesFinal[i] = forcesReaction[i] - forcesAction[i];
            }
            else if (Mathf.Cos(timer - joints[i].position.z / 2) > -treshold && Mathf.Sin(timer - joints[i].position.z / 2) > 0.0f) {
                forcesAction[i] = force - friction1 * normal;
                forcesReaction[i] = force - friction2 * normal;
                forcesFinal[i] = forcesReaction[i] - forcesAction[i];
            }
            else {
                forcesAction[i] = 0.0f;
                forcesReaction[i] = 0.0f;
                forcesFinal[i] = 0.0f;
            }

            forcesFinalZ[i] = forcesFinal[i] * Mathf.Abs((Mathf.Cos(timer - joints[i].position.z / 2) / 2));
            totalForceZ += forcesFinalZ[i];


            joints[i].position = new Vector3(joints[i].position.x + (Mathf.Sin(timer - joints[i].position.z * 2) * 0.005f + (Mathf.Sin(timer - joints[i].position.z / 2) * 0.005f)), joints[i].position.y, joints[i].position.z);
            
                       
            
            //joints[i].position = new Vector3((1 / n * Mathf.Sin(a * Mathf.Cos((1 / n)*2) + (1 / n)) * (joints[i].position.x)) *0.05f, joints[i].position.y, joints[i].position.z); ;
        }

        accel = totalForceZ * mass;
        speed = accel * Time.deltaTime;

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - speed * 0.001f);

        RenderText();
    }

    public float sinSpeed(float t, float amplitude, float frequency, float x) {
        float omega = ((Mathf.PI * 2) / frequency);
        float current = amplitude * omega * Mathf.Sin(omega * (t + x));
        return current;
    }


    public void Force() {
        force = ForceSlider.value;
    }

    public void Friction() {
        friction1 = FrictionSlider.value;
    }

    public void Mass() {
        mass = MassSlider.value;
    }

    public void RenderText(){
        SpeedText.text = "SPEED: " + speed.ToString("n2"); ;
        FinalForceText.text = "FINAL FORCE: " + totalForceZ.ToString("n2"); ;
        ForceText.text = "FORCE: " + force.ToString("n2"); ;
        MassText.text = "MASS: " + mass.ToString("n2"); ;
        FrictionText.text = "FRICTION: " + friction1.ToString("n2");;
    }
}


