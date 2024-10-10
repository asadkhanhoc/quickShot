using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("Level2");
    }

    public void FinalLevel()
    {
        SceneManager.LoadScene("Level3");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
