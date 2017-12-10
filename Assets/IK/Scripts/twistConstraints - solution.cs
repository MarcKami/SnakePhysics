using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class twistConstraints : MonoBehaviour
{
    public bool cancelTwist;



    [SerializeField]
    Transform parent;

    public enum ForwardDir { X, Y, Z };
    public ForwardDir localForward;


    Quaternion getTwist()
    {

        Quaternion localRotation = Quaternion.Inverse(parent.rotation) * transform.rotation;
        // this is the same than 
        //Quaternion localRotation = transform.localRotation;

        Quaternion twist = localRotation;

        switch (localForward)
        {
            case ForwardDir.X:
                twist.y = 0;
                twist.z = 0;
                break;

            case ForwardDir.Y:
                twist.x = 0;
                twist.z = 0;
                break;

            case ForwardDir.Z:
                twist.x = 0;
                twist.y = 0;
                break;


        }

        twist = norm(twist);
        return twist;



    }

    Quaternion getSwing()
    {



        Quaternion localRotation = Quaternion.Inverse(parent.rotation) * transform.rotation;
        Quaternion twist = getTwist();

        Quaternion swing = localRotation * Quaternion.Inverse(twist);
        return swing;

    }

    void LateUpdate()
    {
        if(cancelTwist)
            transform.rotation = getSwing() * parent.rotation;
    

	}

    public Quaternion norm(Quaternion q)
    {
        float module = Mathf.Sqrt(q.x * q.x + q.y * q.y + q.z * q.z + q.w * q.w);
        Quaternion q2 = new Quaternion(q.x/module,q.y/module,q.z/module,q.w/module) ; 
        return q2;


    }

}
