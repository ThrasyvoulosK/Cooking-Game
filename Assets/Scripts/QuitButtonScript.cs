using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButtonScript : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("quitting the game");
        Application.Quit();
    }
}
