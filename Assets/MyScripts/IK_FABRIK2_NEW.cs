using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IK_FABRIK2_NEW : MonoBehaviour
{
    public Transform[] joints;
    public Transform target;

    private MyVector3[] copy;
    private float[] distances;
    private bool done;

    float treshold_condition = 0.1f;

    void Start() {
        distances = new float[joints.Length - 1];
        copy = new MyVector3[joints.Length];
    }

    void Update() {
        // Copy the joints positions to work with
        for (int i = 0; i < joints.Length; i++){
            copy[i] = new MyVector3(joints[i].position.x, joints[i].position.y, joints[i].position.z); //Copy the joints
            if (i < joints.Length - 1){
                distances[i] = MyVector3.Distance(joints[i+1].position, joints[i].position); //Calculate the distances
            }
        }

        done = (copy[copy.Length - 1] - new MyVector3(target.position.x, target.position.y, target.position.z)).magnitude < treshold_condition;

        if (!done) {
            float targetRootDist = MyVector3.Distance(copy[0], new MyVector3(target.position.x, target.position.y, target.position.z));

            // Update joint positions
            if (targetRootDist > distances.Sum()) {
                // The target is unreachable
                for(int i = 0; i < copy.Length - 1; i++) {
                    float r = (new MyVector3(target.position.x, target.position.y, target.position.z) - copy[i]).magnitude;
                    float lambda = distances[i] / r;
                    copy[i + 1] = copy[i] * (1 - lambda) + new MyVector3(target.position.x, target.position.y, target.position.z) * lambda;
                }
            }
            else {
                MyVector3 b = copy[0];
                float difA = (copy[copy.Length - 1] - new MyVector3(target.position.x, target.position.y, target.position.z)).magnitude;

                // The target is reachable
                while ( difA > treshold_condition) {
                    // STAGE 1: FORWARD REACHING
                    copy[copy.Length - 1] = new MyVector3(target.position.x, target.position.y, target.position.z);
                    for (int i = copy.Length - 2; i > 0; i--) {
                        float r = (copy[i + 1] - copy[i]).magnitude;
                        float lambda = distances[i] / r;
                        copy[i] = copy[i + 1] * (1 - lambda) + copy[i] * lambda;
                    }

                    // STAGE 2: BACKWARD REACHING
                    copy[0] = b;
                    for (int i = 0; i < copy.Length - 1; i++) {
                        float r = (copy[i + 1] - copy[i]).magnitude;
                        float lambda = distances[i] / r;
                        copy[i + 1] = copy[i] * (1 - lambda) + copy[i + 1] * lambda;
                    }

                    difA = (copy[copy.Length - 1] - new MyVector3(target.position.x, target.position.y, target.position.z)).magnitude;
                }
            }
            
            // Update original joint rotations
            for (int i = 0; i <= joints.Length - 1; i++) {
                MyVector3 A = new MyVector3(joints[i + 1].position - joints[i].position);
                MyVector3 B = copy[i + 1] - copy[i];

                float cosa = MyVector3.Dot(MyVector3.Normalize(A), MyVector3.Normalize(B));
                float sina = MyVector3.Cross(MyVector3.Normalize(A), MyVector3.Normalize(B)).magnitude;

                float alpha = Mathf.Atan2(sina, cosa) * Mathf.Rad2Deg;

                MyVector3 myAxis = MyVector3.Normalize(MyVector3.Cross(A, B));
                //Vector3 axis = new Vector3(myAxis.x, myAxis.y, myAxis.z);

                MyQuaternion myQuat = MyQuaternion.AngleAxis(alpha, ref myAxis);
                
                Quaternion quat = new Quaternion(myQuat.x, myQuat.y, myQuat.z, myQuat.w);

                joints[i].rotation = quat * joints[i].rotation;
                //joints[i].rotation = Quaternion.AngleAxis(alpha, axis) * joints[i].rotation;
                joints[i + 1].position = new Vector3(copy[i + 1].x, copy[i + 1].y, copy[i + 1].z);
            }         
        }
    }
}
