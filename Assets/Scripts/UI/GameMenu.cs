using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void LeaveGame()
    {
        Application.Quit();
    }
}
