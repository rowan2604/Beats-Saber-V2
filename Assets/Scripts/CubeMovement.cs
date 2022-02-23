using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum LevelType
{
    Creation,
    Playing
}


public class CubeMovement : MonoBehaviour
{


    public float DeplacementSpeed;
    [SerializeField] private LevelType _levelType;
    [SerializeField] private float _interval;

    private MoveCubes _moveCubesInputAction;
    private bool _isForward = false;
    private bool _isBackward = false;
    private void Start()
    {
        _moveCubesInputAction = new MoveCubes();
    }
    // Update is called once per frame
    void Update()
    {
        if(_levelType == LevelType.Playing || _isForward)
        {
            transform.position = transform.position - new Vector3(0, 0, DeplacementSpeed * Time.deltaTime);
            _isForward = false;
        }
            
        if(_isBackward)
        {
            transform.position = transform.position + new Vector3(0, 0, DeplacementSpeed * Time.deltaTime);
            _isBackward = false;
        }
    }

    private void OnEnable()
    {
        if(_levelType == LevelType.Creation)
        {

            _moveCubesInputAction.MovesCube.ForwardBackward.performed += ForwardBackward;
            _moveCubesInputAction.MovesCube.ForwardBackward.Enable();
        }
        
    }

    private void OnDisable()
    {
        _moveCubesInputAction.MovesCube.ForwardBackward.Disable();
    }

    private void ForwardBackward(InputAction.CallbackContext context)
    {
        Debug.Log("Hey");
    }

    private void Backward(InputAction.CallbackContext context)
    {
        _isBackward = true;
    }
}
