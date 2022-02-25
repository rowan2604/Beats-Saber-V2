using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    private Pause _setPause;

    [SerializeField] private GameObject _PauseUI;
    [SerializeField] private GameObject _Blocks;

    private bool _PreviousScriptState;

    private void Awake()
    {
        _setPause = new Pause();
    }

    private void OnEnable()
    {
        _setPause.PauseMap.LaunchPause.performed += PauseGame;
        _setPause.PauseMap.LaunchPause.Enable();
    }

    private void OnDisable()
    {
        _setPause.PauseMap.LaunchPause.Disable();
    }

    private void PauseGame(InputAction.CallbackContext context)
    {
        _PreviousScriptState = GetComponent<CubePlacement>().isActiveAndEnabled;
        GetComponent<CubePlacement>().enabled = false;
        _PauseUI.SetActive(true);
        _Blocks.SetActive(false);
    }

    public void ResumeGame()
    {
        GetComponent<CubePlacement>().enabled = _PreviousScriptState;
        _PauseUI.SetActive(false);
        _Blocks.SetActive(true);
    }
}
