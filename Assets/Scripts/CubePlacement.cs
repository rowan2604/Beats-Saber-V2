using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubePlacement : MonoBehaviour
{
    [SerializeField] private Transform grid;
    [SerializeField] private Transform rightHandTransform;
    [SerializeField] private GameObject PrefabCube;
    [SerializeField] private Transform TransformCube;

    private Plane gridPlane;
    private float enter;
    private Ray ray;

    private InputActionReference SpawnAction;

    void Start()
    {
        SpawnAction.action.Enable();
        SpawnAction.action.performed += SpawnACube;
        gridPlane = new Plane(Vector3.back, grid.transform.position);
    }

    void Update()
    {
        ray = new Ray(rightHandTransform.position, rightHandTransform.forward);
        if (gridPlane.Raycast(ray, out enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            GetNearestPoint(hitPoint);
        }
    }
    void SpawnACube(InputAction.CallbackContext ctx)
    {
        Instantiate(PrefabCube, TransformCube.position, Quaternion.identity);
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
