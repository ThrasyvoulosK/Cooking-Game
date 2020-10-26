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
    public List<Image> images = new List<Image>();
    public Image recipeimage;// = new Image();
    public Image speechbubble;

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

        if(theFoodLayer!=null)
        {
            gameObject.GetComponent<RecipeCheckScript>().recipeimage.enabled = false;

            gameObject.GetComponent<RecipeCheckScript>().images[0].enabled = false;
            gameObject.GetComponent<RecipeCheckScript>().images[1].enabled = false;
            gameObject.GetComponent<RecipeCheckScript>().images[2].enabled = false;
            gameObject.GetComponent<RecipeCheckScript>().images[3].enabled = false;

        }

        recipenote.text = null;
        
        //show the recipe only if we haven't paused/disabled the game
        if (theNextRecipe.gamepause==false)
        {
            //text version of below
            /*recipenote.text = "Make me:\n";
            if (furnscript.recipe.nameofrecipe != null)
                recipenote.text = "Make me " + furnscript.recipe.nameofrecipe + "\n\n";*/

            //the recipe we want to make, depicted
            gameObject.GetComponent<RecipeCheckScript>().recipeimage.enabled = true;
            gameObject.GetComponent<RecipeCheckScript>().recipeimage.sprite = furnscript.recipe.recipeSprite;

            for (int i = 0; i < furnscript.recipe.neededIngr.Count; i++)
            {
                //if we already have all of this ingredient we don't need to show it
                ///if (furnscript.usable_number_of_ingredients[i] > 0)
                ///{
                    //text version
                    //recipenote.text += " - " + furnscript.usable_number_of_ingredients[i].ToString() + " " + furnscript.recipe.neededIngr[i] + "\n";

                    //image version
                    gameObject.GetComponent<RecipeCheckScript>().images[i].enabled = true;
                    //gameObject.GetComponent<RecipeCheckScript>().images[i].sprite = theFoodLayer.SpriteChooseIngredient(furnscript.recipe.name, furnscript.recipe.neededIngr[i]);
                    gameObject.GetComponent<RecipeCheckScript>().images[i].sprite = RecipeIngredientCheckListSprite(furnscript.recipe.neededIngr[i],furnscript.usable_number_of_ingredients[i]);
                    //gameObject.GetComponent<RecipeCheckScript>().images[i].sprite = RecipeIngredientCheckListSprite(furnscript.recipe.neededIngr[i],furnscript.current_number_of_ingredients[i]);
                ///}
                //Debug.Log(furnscript.recipe.neededIngr.Count);
                //Debug.Log(furnscript.usable_number_of_ingredients.Count);
            }
        }
        

    }
    //decide on which sprite to show, based on our needs
    //also take into account that some objects get checked out
    public Sprite RecipeIngredientCheckListSprite(string ingredientname,int numberofingredientsleft)
    {
        if (ingredientname == "Cheese")
        {
            if(numberofingredientsleft>0)
                return Resources.LoadAll<Sprite>("canteen_υλικα πινακα copy")[1];
            else
                return Resources.LoadAll<Sprite>("canteen_υλικα πινακα copy")[3];
        }
        else if (ingredientname == "Ham")
        {
            if (numberofingredientsleft > 0)
                return Resources.LoadAll<Sprite>("canteen_υλικα πινακα copy")[4];
            else
                return Resources.LoadAll<Sprite>("canteen_υλικα πινακα copy")[6];
        }
        else if (ingredientname == "Lettuce")
        {
            if (numberofingredientsleft > 0)
                return Resources.LoadAll<Sprite>("canteen_υλικα πινακα copy")[5];
            else
                return Resources.LoadAll<Sprite>("canteen_υλικα πινακα copy")[7];
        }
        else if (ingredientname == "Tomato")
        {
            if (numberofingredientsleft > 0)
                return Resources.LoadAll<Sprite>("canteen_υλικα πινακα copy")[0];
            else
                return Resources.LoadAll<Sprite>("canteen_υλικα πινακα copy")[2];
        }
        return null;
    }
}
