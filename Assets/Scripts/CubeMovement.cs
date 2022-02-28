using UnityEngine;
using UnityEngine.InputSystem;

public enum LevelType
{
    Creation,
    Playing
}


public class CubeMovement : MonoBehaviour
{

    private static CubeMovement instance;   
    public float DeplacementSpeed;
    [SerializeField] private LevelType _levelType;

    private MoveCubes _moveCubesInputAction;
    private float _sensMovement = 1;
    private bool _isplayLevel;
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
        if(_levelType == LevelType.Playing)//_isplayLevel)
        {
            transform.position = transform.position - new Vector3(0, 0,DeplacementSpeed * Time.deltaTime);
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


    public static void PlayingLevel(bool state)
    {
        instance._isplayLevel = state;
    }
}
