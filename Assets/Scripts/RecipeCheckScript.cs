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
    public NextRecipeScript theNextRecipe;
    public FoodLayersScript theFoodLayer;

    public List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();
    // Start is called before the first frame update
    void Start()
    {
        recipenote = GetComponent<Text>();

        furnscript = GameObject.Find("Furnace").GetComponent<FurnaceScript>();

        theNextRecipe = GameObject.Find("NextRecipeButton").GetComponent<NextRecipeScript>();

        //theFoodLayer = GameObject.Find("FoodLayer(Clone)").GetComponent<FoodLayersScript>();

    }

    // Update is called once per frame
    void Update()
    {
        theFoodLayer = GameObject.Find("FoodLayer(Clone)").GetComponent<FoodLayersScript>();
        //spriteRenderers[0].sprite= theFoodLayer.SpriteChooseIngredient("Toast", "Cheese");
        //spriteRenderers[0].sprite= theFoodLayer.SpriteChooseIngredient("Toast", "Cheese");
        if(theFoodLayer!=null)
        {
            //spriteRenderers[0].sprite = theFoodLayer.SpriteChooseIngredient("Toast", "Cheese");
            gameObject.GetComponent<RecipeCheckScript>().spriteRenderers[0].sprite = theFoodLayer.SpriteChooseIngredient("Toast", "Cheese");
            gameObject.GetComponent<RecipeCheckScript>().spriteRenderers[1].sprite = theFoodLayer.SpriteChooseIngredient("Toast", "Cheese");
            gameObject.GetComponent<RecipeCheckScript>().spriteRenderers[2].sprite = theFoodLayer.SpriteChooseIngredient("Toast", "Cheese");
            gameObject.GetComponent<RecipeCheckScript>().spriteRenderers[3].sprite = theFoodLayer.SpriteChooseIngredient("Toast", "Cheese");
        }

        recipenote.text = null;
        
        //show the recipe only if we haven't paused/disabled the game
        if (theNextRecipe.gamepause==false)
        {
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
        }
        

    }
}
