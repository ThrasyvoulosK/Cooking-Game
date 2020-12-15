﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//
using UnityEngine.UI;

/*RecipeCheckScipt handles the appearance of the objects on the recipe-board, on the top-left corner of the screen*/
public class RecipeCheckScript : MonoBehaviour
{
    public Text recipenote;

    public FurnaceScript furnscript;
    public NextRecipeScript theNextRecipe;
    public FoodLayersScript theFoodLayer;

    public List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();
    public List<Image> images = new List<Image>();
    public Image recipeimage;// = new Image();
    public SpriteRenderer speechbubble;

    public GameMasterScript theGameMaster;

    public GameObject firework;
    public ParticleSystem fireworkplay;

    // Start is called before the first frame update
    void Start()
    {
        recipenote = GetComponent<Text>();

        furnscript = GameObject.Find("Furnace").GetComponent<FurnaceScript>();
        theGameMaster = GameObject.Find("GameMaster").GetComponent<GameMasterScript>();

        theNextRecipe = GameObject.Find("NextRecipeButton").GetComponent<NextRecipeScript>();

        speechbubble = GameObject.Find("SpeechBubble").GetComponent<SpriteRenderer>();

        firework=Instantiate(firework);
        fireworkplay = firework.GetComponent<ParticleSystem>();

        if (GameObject.Find("IngredientsText") != null)
            GameObject.Find("IngredientsText").GetComponent<Text>().text = theGameMaster.languagehandler["Ingredients"];

        /*foreach (Image imagei in gameObject.GetComponent<RecipeCheckScript>().images)
        {
            //resize image, if recipe has five or more ingredients
            if (images.Count >= 5)
            {
                imagei.GetComponent<RectTransform>().sizeDelta = new Vector3(60f, 60f, 0);
                //imagei.GetComponentInChildren<RectTransform>().position -= new Vector3(0, 990f, 0);
                imagei.GetComponent<RectTransform>().position += new Vector3(0, 75f, 0);
                Debug.Log("imagei children: "+imagei.GetComponentInChildren<RectTransform>().position);
                Debug.Log("imagei children: "+imagei.GetComponentInChildren<Text>().text);
                //imagei.GetComponentInChildren<RectTransform>().localPosition += new Vector3(0, 90f, 0);


            }
        }*/
    }

    //bool ingredientnumber
    // Update is called once per frame
    void Update()
    {
        theFoodLayer = null;
        //theFoodLayer = GameObject.Find("FoodLayer(Clone)").GetComponent<FoodLayersScript>();
        if ( GameObject.Find("FoodLayer(Clone)") == null)
            return;
        else //if(theFoodLayer!=null)
        {
            theFoodLayer = GameObject.Find("FoodLayer(Clone)").GetComponent<FoodLayersScript>();

            gameObject.GetComponent<RecipeCheckScript>().recipeimage.enabled = false;

            foreach(Image imagei in gameObject.GetComponent<RecipeCheckScript>().images)
            {
                imagei.enabled = false;
                imagei.preserveAspect = true;

                //imagei.GetComponentInChildren<RectTransform>().anchoredPosition = new Vector3(0, 100f, 0);
                imagei.GetComponentInChildren<Text>().enabled = false;

                ////resize image, if recipe has five or more ingredients
                //if (images.Count >= 5)
                //{
                //    imagei.GetComponent<RectTransform>().sizeDelta = new Vector3(60f, 60f, 0); 
                    //imagei.GetComponentInChildren<RectTransform>().localPosition += new Vector3(0, 1f, 0); 
                //    imagei.GetComponent<RectTransform>().position -= new Vector3(0, 150f, 0); 
                //Debug.Log("imagei children: " + imagei.GetComponentInChildren<Text>().text);
                //}
            }

            speechbubble = GameObject.Find("SpeechBubble").GetComponent<SpriteRenderer>();
            speechbubble.enabled = true;

        }


        recipenote.text = null;
        
        //show the recipe only if we haven't paused/disabled the game
        if (theNextRecipe.gamepause==false)
        {
            //the recipe we want to make, depicted
            gameObject.GetComponent<RecipeCheckScript>().recipeimage.enabled = true;
            gameObject.GetComponent<RecipeCheckScript>().recipeimage.sprite = furnscript.recipe.recipeSprite;

            for (int i = 0; i < furnscript.recipe.neededIngr.Count; i++)
            {
                //if we already have all of this ingredient we don't need to show it anymore
                //image version
                gameObject.GetComponent<RecipeCheckScript>().images[i].enabled = true;

                speechbubble.enabled = true;
                speechbubble.sprite= Resources.LoadAll<Sprite>("canteen_things")[1];
                speechbubble.GetComponentInChildren<TextMeshPro>().enabled = false;

                Debug.Log(furnscript.recipe.neededIngr[i]);
                gameObject.GetComponent<RecipeCheckScript>().images[i].sprite = RecipeIngredientCheckListSprite(furnscript.recipe.neededIngr[i],furnscript.usable_number_of_ingredients[i]);

                //text description of images
                RecipeIngredientDesc(furnscript.recipe.neededIngr[i], gameObject.GetComponent<RecipeCheckScript>().images[i]);
                gameObject.GetComponent<RecipeCheckScript>().images[i].GetComponentInChildren<Text>().enabled = true;
                
                //Debug.Log(furnscript.recipe.neededIngr.Count);
                //Debug.Log(furnscript.usable_number_of_ingredients.Count);
            }

            fireworkplay.Stop();
        }
        else
        {
            //speechbubble = null;
            //speechbubble.enabled = false;
            speechbubble.sprite = Resources.LoadAll<Sprite>("canteen _ευχαριστω bubble copy")[0];
            speechbubble.GetComponentInChildren<TextMeshPro>().enabled = true;

            //Instantiate(firework);
            //fireworkplay.Stop();
            fireworkplay.Play();
        }
        

    }
    //decide on which sprite to show, based on our needs
    //also take into account that some objects get checked out
    public Sprite RecipeIngredientCheckListSprite(string ingredientname,int numberofingredientsleft)
    {
        if (ingredientname == "Cheese")
        {
            if (numberofingredientsleft > 0)
            {
                return Resources.LoadAll<Sprite>("προϊόντα/canteen_υλικα πινακα copy")[1]; 
            }
            else
                return Resources.LoadAll<Sprite>("προϊόντα/canteen_υλικα πινακα copy")[3];
        }
        else if (ingredientname == "Ham")
        {
            if (numberofingredientsleft > 0)
                return Resources.LoadAll<Sprite>("προϊόντα/canteen_υλικα πινακα copy")[4];
            else
                return Resources.LoadAll<Sprite>("προϊόντα/canteen_υλικα πινακα copy")[6];
        }
        else if (ingredientname == "Lettuce")
        {
            if (numberofingredientsleft > 0)
                return Resources.LoadAll<Sprite>("προϊόντα/canteen_υλικα πινακα copy")[5];
            else
                return Resources.LoadAll<Sprite>("προϊόντα/canteen_υλικα πινακα copy")[7];
        }
        else if (ingredientname == "Tomato")
        {
            if (numberofingredientsleft > 0)
                return Resources.LoadAll<Sprite>("προϊόντα/canteen_υλικα πινακα copy")[0];
            else
                return Resources.LoadAll<Sprite>("προϊόντα/canteen_υλικα πινακα copy")[2];
        }
        else if (ingredientname == "Coffee")
        {
            if (numberofingredientsleft > 0)
                return Resources.LoadAll<Sprite>("προϊόντα/canteen_υλικα πινακα copy")[8];
            else
                return Resources.LoadAll<Sprite>("προϊόντα/canteen_υλικα πινακα copy")[10];
        }
        else if (ingredientname == "Ice")
        {
            if (numberofingredientsleft > 0)
                return Resources.LoadAll<Sprite>("προϊόντα/canteen_υλικα πινακα copy")[9];
            else
                return Resources.LoadAll<Sprite>("προϊόντα/canteen_υλικα πινακα copy")[11];
        }
        else if (ingredientname == "Milk")
        {
            if (numberofingredientsleft > 0)
                return Resources.LoadAll<Sprite>("προϊόντα/canteen_υλικα πινακα copy")[12];
            else
                return Resources.LoadAll<Sprite>("προϊόντα/canteen_υλικα πινακα copy")[13];
        }
        else
        {
            if (numberofingredientsleft <= 0)
                return theGameMaster.spriteslayers[ingredientname + "_ing_done"];
            return theGameMaster.spriteslayers[ingredientname + "_ing"];
        }
        return null;
    }

    public void RecipeIngredientDesc(string ingredientname, Image gmo)
    {
        //Debug.Log("recipeingredientdesc");
        if (gmo.GetComponentInChildren<Text>() != null)
        {
            //Debug.Log("text not null");
            //gmo.GetComponentInChildren<Text>().text = theGameMaster.languagehandler[ingredientname + "_lc"];
            gmo.GetComponentInChildren<Text>().text = theGameMaster.languagehandler[ingredientname];
        }
        else
        {
            Debug.Log("textisnull");
            Debug.Log(gmo.name);
        }
    }
}
