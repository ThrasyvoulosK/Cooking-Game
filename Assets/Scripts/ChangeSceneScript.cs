using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
using UnityEngine.SceneManagement;

public class ChangeSceneScript : MonoBehaviour
{

    /*public void change_scene(string nameofscene)
    {
        Debug.Log(nameofscene);
        SceneManager.LoadScene(nameofscene);

    }*/
    public void change_scene()
    {
        Debug.Log("change scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);

    }

    public void change_sceneplus2()
    {
        Debug.Log("change scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);

    }

}
