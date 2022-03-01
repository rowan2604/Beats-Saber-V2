using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private Pause _setPause;

    [SerializeField] private GameObject _pauseUI;
    [SerializeField] private GameObject _blocks;

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
        _pauseUI.SetActive(true);
        _blocks.SetActive(false);
        Audio.Pause(context);
    }

    public void ResumeGame()
    {
        _pauseUI.SetActive(false);
        _blocks.SetActive(true);
        Audio.Pause(new InputAction.CallbackContext());
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
