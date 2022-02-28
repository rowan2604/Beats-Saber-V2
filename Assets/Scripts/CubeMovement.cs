using UnityEngine;
using UnityEngine.InputSystem;

public class CubeMovement : MonoBehaviour
{

    private static CubeMovement _instance;
    
    public float deplacementSpeed;

    private MoveCubes _moveCubesInputAction;
    private float _sensMovement = 1;
    private bool _isplayLevel;
    private Vector3 _startpos;
    private void Awake()
    {
        _instance = this;
        _moveCubesInputAction = new MoveCubes();
        _startpos = transform.position;
    }

    private void OnEnable()
    {
        if(Main.CurrentLevelType == Main.LevelType.Creation)
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
        if(Main.CurrentLevelType == Main.LevelType.Playing)
            transform.position = transform.position - new Vector3(0, 0, deplacementSpeed * Time.deltaTime);

        if (Main.CurrentLevelType == Main.LevelType.Creation && _isplayLevel)
            transform.position = transform.position - new Vector3(0, 0, deplacementSpeed * Time.deltaTime);
    }


    private void ForwardBackward(InputAction.CallbackContext context)
    {
        float movement = context.ReadValue<Vector2>().y;
        if (movement > 0)
            _sensMovement = 1;
        else
            _sensMovement = -1;
        if(transform.position.z - (_sensMovement * deplacementSpeed * 5 * Time.deltaTime) < _startpos.z)
            transform.position = transform.position - new Vector3(0, 0, _sensMovement * deplacementSpeed * 5 * Time.deltaTime);
    }


    public static void PlayingLevel(bool state)
    {
        _instance._isplayLevel = state;
    }
}
