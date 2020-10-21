using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
using UnityEngine.UI;

//this script handles the text display
public class RecipeCheckScript : MonoBehaviour
{
    public Text recipenote;

    public FurnaceScript furnscript;
    // Start is called before the first frame update
    void Start()
    {
        recipenote = GetComponent<Text>();

        furnscript = GameObject.Find("Furnace").GetComponent<FurnaceScript>();

    }

    // Update is called once per frame
    void Update()
    {
        recipenote.text = null;
        /*if (Time.timeScale != 0)
        {*/
            recipenote.text = "Make me:\n";
            if (furnscript.recipe.nameofrecipe != null)
                recipenote.text = "Make me " + furnscript.recipe.nameofrecipe + "\n\n";
            for (int i = 0; i < furnscript.recipe.neededIngr.Count; i++)
            {
                //if we already have all of this ingredient we don't need to show it
                if (furnscript.usable_number_of_ingredients[i] > 0)
                    recipenote.text += " - " + furnscript.usable_number_of_ingredients[i].ToString() + " " + furnscript.recipe.neededIngr[i] + "\n";
                //Debug.Log(furnscript.recipe.neededIngr.Count);
                //Debug.Log(furnscript.usable_number_of_ingredients.Count);
            }
        /*}*/

    }
}
