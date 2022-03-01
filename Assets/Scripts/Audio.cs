using UnityEngine;
using UnityEngine.InputSystem;

public class Audio : MonoBehaviour
{
    private static Audio _instance;

    [SerializeField] private float _offset;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private GameObject _cubesParent;
    [SerializeField] private float _deplacementSpeed;

    private AudioInputAction _audioInputAction;
    
    private AudioSource _audioSource;
    private Vector3 _startPosition;
    private bool _isPlaying;
    private bool _inPause;

    private void Awake()
    {
        _instance = this;

        _audioSource = GetComponent<AudioSource>();
        _audioInputAction = new AudioInputAction();
    }

    private void Start()
    {
        _startPosition = _cubesParent.transform.position;
        _audioSource.clip = _audioClip;
        if (Main.CurrentLevelType == Main.LevelType.Playing)
        {
            CubeMovement.PlayingLevel(true);
            _audioSource.Play();
        }
    }

    private void OnEnable()
    {
        if(Main.CurrentLevelType == Main.LevelType.Creation)
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
        if (_instance._isPlaying && !_instance._inPause)
        {
            _instance._audioSource.Pause();
            _instance._inPause = true;
            _instance._isPlaying = false;
            CubeMovement.PlayingLevel(false);
        }
        else if (_instance._isPlaying && _instance._inPause)
        {
            _instance._audioSource.UnPause();
            _instance._inPause = false;
            _instance._isPlaying = true;
            CubeMovement.PlayingLevel(true);
        }
    }
}
