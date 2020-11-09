using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
using UnityEngine.SceneManagement;

/*ChangeSceneScript changes our current scene to the next one, based on the build settings*/
/*it is called only when a level is won, or when a button  that explicitly says so is clicked*/
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
