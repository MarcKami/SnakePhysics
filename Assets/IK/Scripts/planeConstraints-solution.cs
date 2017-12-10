using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planeConstraints : MonoBehaviour
{
    public bool active;
    public bool debugLines;

    public Transform parent;
    public Transform child;

    public Transform plane;
 
    // To define how "strict" we want to be
    private float threshold = 0.00001f;
 
    void LateUpdate()
    {
           
            Vector3 normal1 = Vector3.Cross((parent.position - transform.position), (child.position - transform.position));
            bool inplane = (  Mathf.Abs (Vector3.Dot(plane.up.normalized, normal1)) < threshold);

            float dprod = 0; //if inplane is false, the cosinus between the child vector and the normal is 90, the dotproduct is 0

            if (!inplane)
            {
                
                dprod = Vector3.Dot(plane.up.normalized, (child.position - transform.position));
                Vector3 vertComponent = plane.up.normalized * dprod ;

                if (debugLines) { 
                    Debug.DrawLine(transform.position,  plane.up.normalized + transform.position, Color.red);
                    Debug.DrawLine(child.position,   child.position - vertComponent, Color.green);
                }

                //we want this:
                //child.position = child.position - vertComponent;
                //but we want it with the right rotations i nthe parent
                Vector3 targetPos = child.position - vertComponent;

                if (active)
                {
                
                    Vector3 axis = Vector3.Cross( (child.position - transform.position).normalized, (targetPos - transform.position).normalized).normalized;
                   
	
					//float angle =	Mathf.Acos(Vector3.Dot(ToParent, ToChild)) * 57.29578f;//1 radiant in degrees
	
                    //Quaternion correctionRot = Quaternion.AngleAxis(angle, axis);
                    //transform.rotation = correctionRot * transform.rotation;

            }
                if (debugLines)
                {
                    Debug.DrawLine(child.position, targetPos, Color.blue);

                }
        }

    }

    

    


}
