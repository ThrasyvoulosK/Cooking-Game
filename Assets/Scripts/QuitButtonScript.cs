using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*QuitButtonScript quits the game*/
/* either through its button on the top-right, or by pressing the 'escape' button anytime*/
public class QuitButtonScript : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("quitting the game");
        //save the game before quitting
        GameObject.Find("GameMaster").GetComponent<GameMasterScript>().SaveGame();
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetKey("escape"))
        {
            Debug.Log("escape pressed");
            QuitGame();
        }
    }
}
