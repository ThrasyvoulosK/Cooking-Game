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
    public int level_id;//
    //public List<Scene> scenes = new List<Scene>();
    void Start()
    {
        /*foreach (Scene scen in scenes)
            scenes.Add(scen);
        Debug.Log("amount of scenes" + scenes.Count);*/
    }
    public void change_scene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +level_id*2+1);

    }
    public void change_scene_option()
    {
        GameObject.Find("GameMaster").GetComponent<GameMasterScript>().levelid = SceneManager.GetActiveScene().buildIndex + level_id * 2 + 1;
        GameObject.Find("GameMaster").GetComponent<GameMasterScript>().levelchanged = true;
    }

}
