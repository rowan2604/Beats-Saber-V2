using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] public Saber.Color color;
    public bool Collide { get; private set; }

    private void OnCollisionEnter(Collision collision)
    {
        Collide = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        Collide = false;
    }
}
