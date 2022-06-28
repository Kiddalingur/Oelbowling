using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleQuit : MonoBehaviour
{
   public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is Exiting");
    }
    public void ToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
