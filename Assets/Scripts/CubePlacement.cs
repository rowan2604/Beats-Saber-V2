using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePlacement : MonoBehaviour
{
    [SerializeField] private Transform grid;
    [SerializeField] private Transform rightHandTransform;

    private Plane gridPlane;
    private float enter;
    private Ray ray;
    // Start is called before the first frame update
    void Start()
    {
        gridPlane = new Plane(Vector3.back, grid.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        ray = new Ray(rightHandTransform.position, rightHandTransform.forward);
        if (gridPlane.Raycast(ray, out enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            GetNearestPoint(hitPoint);
        }
    }

    private Transform GetNearestPoint(Vector3 origin)
    {
        Transform nearestPoint = grid.GetChild(0);
        foreach (Transform t in grid) {
            if ((t.position - origin).magnitude <= (nearestPoint.position - origin).magnitude)
            {
                nearestPoint.GetComponent<Renderer>().material.color = Color.white;
                nearestPoint = t;
                t.GetComponent<Renderer>().material.color = Color.red;
            }
            else t.GetComponent<Renderer>().material.color = Color.white;
        }
        return nearestPoint;
    }
}
