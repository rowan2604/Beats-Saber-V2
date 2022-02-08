using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{

    [SerializeField]
    private float _offset;
    [SerializeField]
    private AudioClip _audioClip;
    [SerializeField]
    private Transform _playerTransform;

    private float _musicSpeed;
    private AudioSource _audioSource;
    private Transform _startPosition;
    private bool _isPlaying;
    private bool _inPause;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _startPosition = _playerTransform;
        _audioSource.clip = _audioClip;
    }

    private void ManageMusic()
    {
        if (_isPlaying)
            StopMusic();
        else if (_inPause)
            _audioSource.UnPause();
        else
            LauchMusic();
    }

    private void LauchMusic()
    {
        float time;
        _isPlaying = true;
        time = _musicSpeed / (_playerTransform.position.z - _startPosition.position.z);
        _audioSource.time = time - _offset;
        _audioSource.Play();
    }

    private void StopMusic()
    {
        _audioSource.Stop();
        _isPlaying = false;
    }

    private void PauseMusic()
    {
        if (_isPlaying && !_inPause)
        {
            _audioSource.Pause();
            _inPause = true;
            _isPlaying = false;
        }           
        else
        {
            _audioSource.UnPause();
            _inPause = false;
        }
            
    }
}
