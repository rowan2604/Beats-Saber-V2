using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fdp : MonoBehaviour
{
    AudioSource _temp;
    // Start is called before the first frame update
    private void Awake()
    {
        _temp = GetComponent<AudioSource>();
    }
    void Start()
    {
        _temp.time = 50f;
        Debug.Log(_temp.time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
