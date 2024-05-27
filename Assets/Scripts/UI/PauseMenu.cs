using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void LeaveGame()
    {
        Application.Quit();
        print("Application shuld be closed");
    }
}
