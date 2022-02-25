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
    private bool _isplayLevel;
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

    void Update()
    {
        if(_isplayLevel)
        {
            transform.position = transform.position - new Vector3(0, 0,DeplacementSpeed * Time.deltaTime);
            Debug.Log(transform.position.z);
        }

    }


    private void ForwardBackward(InputAction.CallbackContext context)
    {
        float movement = context.ReadValue<Vector2>().y;
        if (movement > 0)
            _sensMovement = 1;
        else
            _sensMovement = -1;

        transform.position = transform.position - new Vector3(0, 0, _sensMovement * DeplacementSpeed * 5 * Time.deltaTime);
    }


    public void PlayingLevel(bool state)
    {
        _isplayLevel = state;
    }
}
