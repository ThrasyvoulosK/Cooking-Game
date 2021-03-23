using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
using UnityEngine.SceneManagement;

/*ChangeSceneScript changes our current scene to the next one, based on the build settings*/
/*it is called only when a level is won, or when a button  that explicitly says so is clicked*/
public class ChangeSceneScript : MonoBehaviour
{    
    public int level_id;//
    void Start()
    {
        /*foreach (Scene scen in scenes)
            scenes.Add(scen);
        Debug.Log("amount of scenes" + scenes.Count);*/
    }

    //change scene immediately
    public void change_scene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +level_id*2+1);
    }

    //save the selected scene to load it later from the main menu
    public void change_scene_option()
    {
        GameObject.Find("GameMaster").GetComponent<GameMasterScript>().levelid = (SceneManager.GetActiveScene().buildIndex + level_id*2 +1);
        Debug.Log("scene changed to:" + GameObject.Find("GameMaster").GetComponent<GameMasterScript>().levelid);
        GameObject.Find("GameMaster").GetComponent<GameMasterScript>().levelchanged = true;
    }

    //go back to the menu
    public void changeToMenu()
    {
        Debug.Log("Going to menu");

        GameObject.Find("GameMaster").GetComponent<GameMasterScript>().levelid = (0);
        GameObject.Find("GameMaster").GetComponent<GameMasterScript>().levelchanged = true;

        //save
        GameObject.Find("GameMaster").GetComponent<GameMasterScript>().SaveGame();

        //resets game
        Destroy(GameObject.Find("GameMaster"));

        SceneManager.LoadScene(0);
    }

}
