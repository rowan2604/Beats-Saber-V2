using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Audio : MonoBehaviour
{

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
        _audioSource = GetComponent<AudioSource>();
        _audioInputAction = new AudioInputAction();
    }

    private void Start()
    {
        _startPosition = _cubesParent.transform.position;
        _audioSource.clip = _audioClip;
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

        }
        else
        {

            float time;
            _isPlaying = true;
            CubeMovement.instance.PlayingLevel(true);
            time = (Math.Abs(_cubesParent.transform.position.z - _startPosition.z))/ _deplacementSpeed;

            _audioSource.time = (time > _offset)? time - _offset : 0;
            _audioSource.Play();

        }
    }


    public void Pause(InputAction.CallbackContext context)
    {
        Debug.Log("FDP");
        if (_isPlaying && !_inPause)
        {
            
            _audioSource.Pause();
            _inPause = true;
            _isPlaying = false;
            CubeMovement.instance.PlayingLevel(false);

        }
        else if (_inPause)
        {

            _audioSource.UnPause();
            _inPause = false;
            _isPlaying = true;
            CubeMovement.instance.PlayingLevel(true);

        }
    }

}
