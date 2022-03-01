using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saber : MonoBehaviour
{
    public enum Color
    {
        Red = 0,
        Blue = 1
    }

    public Color color;
    [SerializeField] private float _demiCube;

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider.gameObject.name);
        if (color != collider.GetComponent<Cube>().color)
        {
            Debug.Log("Mauvaise couleur");
            return;
        }
        Vector3 cubeCenter = collider.transform.position;
        Vector3 faceCenter = cubeCenter + Vector3.back * _demiCube;
        Plane plane = new Plane(Vector3.forward, faceCenter);
        Ray ray = new Ray(transform.position, transform.forward);
        if (plane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            if (ValidHit(hitPoint, faceCenter, (int)collider.transform.rotation.z))
            {
                Debug.Log($"Good Hit with saber {color}!");
            }
            else
                Debug.Log("raté");
            Destroy(collider.gameObject);
        }
        else
            Debug.Log("Erreur : le sabre n'intersecte pas le plan du cube");
    }

    private bool ValidHit(Vector3 hitPoint, Vector3 faceCenter, int rotation)
    {
        bool horizontal = Mathf.Abs(hitPoint.x - faceCenter.x) > Mathf.Abs(hitPoint.y - faceCenter.y);
        if (horizontal)
        {
            if (!(rotation / 90 % 2 == 1))
                return false;
            bool left = hitPoint.x < faceCenter.x;
            return left ? rotation == 90 : rotation == 270;
        }
        else
        {
            if (!(rotation / 90 % 2 == 0))
                return false;
            bool bottom = hitPoint.y < faceCenter.y;
            return bottom ? rotation == 180 : rotation == 0;
        }
    }
}
