using UnityEngine;
using UnityEngine.InputSystem;

public class Audio : MonoBehaviour
{
    private static Audio Instance;

    [SerializeField]
    private float _offset;
    [SerializeField]
    private AudioClip _audioClip;
    [SerializeField]
    private GameObject _cubesParent;
    [SerializeField]
    private float _deplacementSpeed;
    [SerializeField] 
    private LevelType _levelType;


    private AudioInputAction _audioInputAction;
    
    private AudioSource _audioSource;
    private Vector3 _startPosition;
    private bool _isPlaying;
    private bool _inPause;

    private void Awake()
    {
        Instance = this;

        _audioSource = GetComponent<AudioSource>();
        _audioInputAction = new AudioInputAction();
    }

    private void Start()
    {
        _startPosition = _cubesParent.transform.position;
        _audioSource.clip = _audioClip;
        if (_levelType == LevelType.Playing)
        {
            CubeMovement.PlayingLevel(true);
            _audioSource.Play();
        }
    }

    private void OnEnable()
    {
        if(_levelType == LevelType.Creation)
        {
            _audioInputAction.Audio.Pause.performed += Pause;
            _audioInputAction.Audio.Pause.Enable();

            _audioInputAction.Audio.Play.performed += Play;
            _audioInputAction.Audio.Play.Enable();
        }

    }

    private void OnDisable()
    {
        _audioInputAction.Audio.Play.Disable();
        _audioInputAction.Audio.Pause.Disable();
    }
    public void Play(InputAction.CallbackContext context)
    {
        if (_isPlaying)
        {

            _audioSource.Stop();
            _isPlaying = false;
            CubeMovement.PlayingLevel(false);

        }
        else
        {

            float time;
            _isPlaying = true;
            CubeMovement.PlayingLevel(true);
            time = (_startPosition.z - _cubesParent.transform.position.z) / _deplacementSpeed;
            Debug.Log(time);
            _audioSource.time = (time > _offset)? time - _offset : 0;
            _audioSource.Play();

        }
    }


    public static void Pause(InputAction.CallbackContext context)
    {
        if (Instance._isPlaying && !Instance._inPause)
        {

            Instance._audioSource.Pause();
            Instance._inPause = true;
            Instance._isPlaying = false;
            CubeMovement.PlayingLevel(false);

        }
        else if (Instance._inPause)
        {

            Instance._audioSource.UnPause();
            Instance._inPause = false;
            Instance._isPlaying = true;
            CubeMovement.PlayingLevel(true);

        }
    }

}
