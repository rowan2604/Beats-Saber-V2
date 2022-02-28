using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CubeData
{
    public float x;
    public float y;
    public float z;

    public float rotation;
    public bool color;

    public CubeData(float xposition, float yposition, float zposition, float inputrotation, bool colorbool)
    {
        x = xposition;
        y = yposition;
        z = zposition;
        rotation = inputrotation;
        color = colorbool;
    }
}
