using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRender : MonoBehaviour {

    [SerializeField] private SinMovement Snake;
    [SerializeField] private LineRenderer speedLineRenderer;
    [SerializeField] private LineRenderer[] forceLineRenderer;
    private Transform[] forcePositions;
    private Transform[] forceDirections;
    public float lineWidth = 0.005f;
    public Vector3 dir;
    //public Vector3 dirMagnuss;
    public Material forces;
    public Material velocities;
    public Material positions;
    public int type;

    void Start()
    {
        Vector3[] initLaserPositions = new Vector3[2] { Vector3.zero, Vector3.zero };
        //initLaserPositions[0] = new Vector3();
        speedLineRenderer.SetPositions(initLaserPositions);
        speedLineRenderer.SetWidth(lineWidth, lineWidth);
        for (int i = 0; i < forceLineRenderer.Length; i++) {
            forceLineRenderer[i].SetPositions(initLaserPositions);
            forceLineRenderer[i].SetWidth(lineWidth, lineWidth);
        }
    }

    void Update()
    {
        speedLineRenderer.enabled = true;
        speedLineRenderer.material = velocities;
        //dir = new Vector3(move.magnuss.x, move.magnuss.y, move.magnuss.z);
        ShootLaserFromTargetPosition(speedLineRenderer.transform.position, -transform.forward, Snake.speed * 0.1f);
        for (int i = 0; i < forceLineRenderer.Length; i++) {
            forceLineRenderer[i].enabled = true;
            forceLineRenderer[i].material = forces;
        }
        ShootLaserFromTargetPositionForces(Snake.joints, Snake.joints, Snake.forcesFinal);
    }

    void ShootLaserFromTargetPosition(Vector3 targetPosition, Vector3 direction, float length) {
        Ray ray = new Ray(targetPosition, direction);
        RaycastHit raycastHit;
        Vector3 endPosition = targetPosition + (length * direction);

        if (Physics.Raycast(ray, out raycastHit, length)) {
            endPosition = raycastHit.point;
        }

        speedLineRenderer.SetPosition(0, targetPosition);
        speedLineRenderer.SetPosition(1, endPosition);
    }
    void ShootLaserFromTargetPositionForces(Transform[] targetPosition, Transform[] direction, float[] length) {
        for (int i = 0; i < targetPosition.Length; i++) {
            Ray ray = new Ray(targetPosition[i].position, -direction[i].right);
            RaycastHit raycastHit;
            Vector3 endPosition = targetPosition[i].position + ((length[i] * 0.01f) * -direction[i].right);

            if (Physics.Raycast(ray, out raycastHit, (length[i] * 0.01f))) {
                endPosition = raycastHit.point;
            }

            forceLineRenderer[i].SetPosition(0, targetPosition[i].position);
            forceLineRenderer[i].SetPosition(1, endPosition);
        }
    }
}
