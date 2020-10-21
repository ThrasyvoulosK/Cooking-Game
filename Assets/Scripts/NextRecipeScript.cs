using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextRecipeScript : MonoBehaviour
{
   public bool gamepause=true;

    public void allow_next_recipe()
    {
        Debug.Log("nextrecipescript");

        if (gamepause)
        {
            Time.timeScale = 1;
            gamepause = false;

            //enabled = true;
        }
        else
        {
            Time.timeScale = 0;
            gamepause = true;

            //enabled = false;
        }

        //isclickable = false;

    }

    void Start()
    {
        gamepause = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
