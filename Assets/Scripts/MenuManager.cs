using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("LaSeinedeBase");
    }

    public void Edit()
    {
        SceneManager.LoadScene("EditScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
