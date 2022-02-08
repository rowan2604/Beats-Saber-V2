using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnCube : MonoBehaviour
{
    [SerializeField]
    public GameObject PrefabCube;
    [SerializeField]
    public Transform TransformCube;

    public InputActionReference SpawnAction;
    private void Start()
    {
        SpawnAction.action.Enable();
        SpawnAction.action.performed += SpawnACube;
    }
    void SpawnACube(InputAction.CallbackContext ctx)
    {
        Instantiate(PrefabCube, TransformCube.position, Quaternion.identity);
    }

}
