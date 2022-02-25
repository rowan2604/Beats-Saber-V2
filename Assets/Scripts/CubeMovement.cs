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

    public static CubeMovement instance;
    private bool isplayLevel;
    public float DeplacementSpeed;
    [SerializeField] private LevelType _levelType;
    [SerializeField] private float _interval;

    private Vector3 _currentPosition;
    private MoveCubes _moveCubesInputAction;
    private bool _isForward = false;
    private bool _isBackward = false;
    private bool _isMoving = false;
    private float _sensMovement = 1;

    private void Awake()
    {
        instance = this;
        _moveCubesInputAction = new MoveCubes();
    }

    private void OnEnable()
    {
        _moveCubesInputAction.MovesCube.ForwardBackward.performed += ForwardBackward;
        _moveCubesInputAction.MovesCube.ForwardBackward.Enable();
        _moveCubesInputAction.MovesCube.StopMoving.performed += StopMoving;
        _moveCubesInputAction.MovesCube.StopMoving.Enable();
    }

    private void OnDisable()
    {
        _moveCubesInputAction.MovesCube.ForwardBackward.Disable();
    }

    void Update()
    {
        if( isplayLevel)
        {
            transform.position = transform.position - new Vector3(0, 0,_sensMovement * DeplacementSpeed * Time.deltaTime);
            Debug.Log(transform.position.z);
        }
        if(_levelType == LevelType.Creation || _isForward)
        {
            transform.position = transform.position - new Vector3(0, 0, DeplacementSpeed * 5 * Time.deltaTime);
            _isForward = false;
        }
            
        if(_isBackward)
        {
            transform.position = transform.position + new Vector3(0, 0, DeplacementSpeed * 5 * Time.deltaTime);
            _isBackward = false;
        }

        if(!_isMoving)
        {
            if ((transform.position - _currentPosition).sqrMagnitude < _interval * _interval)
                transform.position = _currentPosition + new Vector3(0, 0, _interval);
        }
    }


    private void ForwardBackward(InputAction.CallbackContext context)
    {
        _currentPosition = transform.position;
        _isMoving = true;
        float movement = context.ReadValue<Vector2>().y;
        if (movement > 0)
            _sensMovement = 1;
        else
            _sensMovement = -1;
    }

    private void StopMoving(InputAction.CallbackContext context)
    {
        _isMoving = false;
        Debug.Log("Stop");
    }


    public void PlayingLevel(bool state)
    {
        isplayLevel = state;
    }
}
