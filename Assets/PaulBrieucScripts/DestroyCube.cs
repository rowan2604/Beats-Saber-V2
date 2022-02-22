using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCube : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("CorrectSurface"))
            Goodhit();
        else if(collider.CompareTag("WrongSurface"))
            Badhit();

        if (collider.CompareTag("Cube"))
            Destroy(collider.gameObject);
    }

    private void Goodhit()
    {
        Debug.Log("GoodHit");
    }

    private void Badhit()
    {
        Debug.Log("BadHit");
    }
}
