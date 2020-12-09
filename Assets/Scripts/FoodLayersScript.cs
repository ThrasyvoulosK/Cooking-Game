using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using System.Security.Cryptography;
//using System.Diagnostics;
using UnityEngine;

/*FoodLayersScript deals with the appearance of the layered food that is under construction in the middle of the screen.
  It also deals with its movement, scaling and disappearance routines*/
/*sprite-handling functions for the base-of-the-recipe-graphic and its middle and top ingredients are included*/
public class FoodLayersScript : MonoBehaviour
{
    public SpriteRenderer renderer;
    public Sprite oldsprite,newsprite,othersprite;
    public List<SpriteRenderer> renderers = new List<SpriteRenderer>();

    //we need access to the furnace in order to obtain variables like number-of-ingredients
    public FurnaceScript theFurnace;

    public SpawningFoodLayerScript theSPF;

    public NextRecipeScript theNextRecipe;

    public ChangeSceneScript theChangeScene;

    public GameMasterScript theGameMaster;

    GameObject target;
    GameObject movetoward;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();

        theSPF = GameObject.Find("SpawningFoodLayer").GetComponent<SpawningFoodLayerScript>();

        target = GameObject.Find("Furnace");
        movetoward = GameObject.Find("SpeechBubble");

        //load a base graphic
        //renderers[3].sprite = SpriteLayerBase(target.GetComponent<FurnaceScript>().recipe.name);

        theNextRecipe = GameObject.Find("NextRecipeButton").GetComponent<NextRecipeScript>();

        gameObject.transform.localScale = new Vector3(1.5f,1.5f,1.5f);

        theChangeScene = GameObject.Find("Canvas").GetComponent<ChangeSceneScript>();
        theFurnace = GameObject.Find("Furnace").GetComponent<FurnaceScript>();

        theGameMaster = GameObject.Find("GameMaster").GetComponent<GameMasterScript>();

        gameObject.name = "FoodLayer(Clone)";
        
        //change ice cream's position to within the board's surface
        if (theFurnace.recipe.name.StartsWith("IceCream")|| theFurnace.recipe.name.StartsWith("Salad"))
            gameObject.transform.position = new Vector3(-0.67f, 0.4f, 0);
        else if(theFurnace.recipe.name.StartsWith("Club"))//initialise club sandwich, if needed
        {
            //gameObject.GetComponentInChildren<Transform>().localPosition = new Vector3(0f, 0f, 0);
            int clchldrn;
            clchldrn = gameObject.transform.childCount;
            
            for(int i=1;i<clchldrn;i++)
            {
                Debug.Log("forloop");
                
                if (theFurnace.recipe.neededIngr.Count < i)
                    break;
               // Debug.Log("i "+i+" neededingr "+theFurnace.recipe.neededIngr.Count);
                Debug.Log(theFurnace.recipe.neededIngr[i-1]);
                
                if (theFurnace.recipe.neededIngr[i-1] == "Potato")
                { 
                    gameObject.transform.GetChild(i-1).GetComponent<Transform>().localPosition = new Vector3(0f, -0.10f, 0);
                    gameObject.transform.GetChild(i-1).GetComponent<SpriteRenderer>().sortingOrder = 5;
                    //continue;
                }
                
                 
            }
            for (int i = 1; i < clchldrn; i++)
                gameObject.transform.GetChild(i - 1).GetComponent<Transform>().localPosition += new Vector3(0f, 0.250f, 0);

            gameObject.GetComponent<SpriteRenderer>().sprite= theGameMaster.spriteslayers["Plate"];
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 4;

        }
        
        Debug.Log("k");
    }

    public bool change = false;
    // Update is called once per frame
    void Update()
    {

        //MoveFoodLayer();

        //the "change " boolean variable is called when a recipe is finished,
        // in order to allow our current foodlayer to disappear
        // and a new one to take its place
        if (change == true)
        {
            //rename our object so that it doesn't interfere with other similar objects' functionality
            if (gameObject.name.EndsWith("(Clone)"))
                gameObject.name = gameObject.name.Substring(0, gameObject.name.Length - 7);//remove (clone) from string


            transform.position = Vector2.MoveTowards(transform.position, movetoward.transform.position, Time.deltaTime * 03);

            
            //StartCoroutine("Fade");

            theSPF.spawnfoodlayer();

            
            if ((gameObject.transform.position.x == movetoward.transform.position.x) && (gameObject.transform.position.y == movetoward.transform.position.y))
            {
                StartCoroutine("ScaleO");
                //Destroy(gameObject);
                change = false;
            }
        }
        else if (theNextRecipe.gamepause)// do not show anything if we aren't prepared to handle the next customer!
        {
            //Debug.Log("rendred null");
            if (gameObject.name.EndsWith("(Clone)"))
                renderers[4].sprite = null;
        }
        else
        {
            //Debug.Log("spriterenderedfully");
            if (gameObject.name.EndsWith("(Clone)"))
                renderers[4].sprite = SpriteLayerBase(target.GetComponent<FurnaceScript>().recipe.name);
        }

    }

    //choose and load a sprite directly by its name
    // this functions includes sprites for all parts of the foodlayer,
    // including base ingredients, middle ingredients and ending ingredients
    public Sprite SpriteHandler(string ingredientname)
    {
        Sprite ingredientsprite;
        if(ingredientname=="Cheese")
        {
            //ingredientsprite= Resources.LoadAll<Sprite>("canteen_toast-01").Single(s => s.name == "toast_cheese");
            ingredientsprite = Resources.LoadAll<Sprite>("canteen_toast-01")[15];
            return ingredientsprite;
        }
        else if (ingredientname == "Ham")
        {
            ingredientsprite = Resources.LoadAll<Sprite>("canteen_toast-01")[16];
            return ingredientsprite;
        }
        else if (ingredientname == "Lettuce")
        {
            ingredientsprite = Resources.LoadAll<Sprite>("canteen_toast-01")[17];
            return ingredientsprite;
        }
        else if (ingredientname == "Tomato")
        {
            ingredientsprite = Resources.LoadAll<Sprite>("canteen_toast-01")[14];
            return ingredientsprite;
        }
        else if (ingredientname == "Toast_Bread")
        {
            ingredientsprite = Resources.LoadAll<Sprite>("προϊόντα/canteen_toast-01")[13];
            return ingredientsprite;
        }
        else if (ingredientname == "Sandwich_Bread_Down")
        {
            ingredientsprite = Resources.LoadAll<Sprite>("προϊόντα/canteen_sandwich-02")[16];
            return ingredientsprite;
        }
        else if (ingredientname == "Sandwich_Bread_Top")
        {
            ingredientsprite = Resources.LoadAll<Sprite>("προϊόντα/canteen_sandwich-02")[13];
            return ingredientsprite;
        }
        else if (ingredientname == "Coffee_Down")
        {
            ingredientsprite = Resources.LoadAll<Sprite>("προϊόντα/canteen_coffee")[8];
            //ingredientsprite = Resources.LoadAll<Sprite>("προϊόντα/canteen_coffee")[12];
            return ingredientsprite;
        }
        else if (ingredientname == "Coffee_Up")
        {
            /*ingredientsprite = Resources.LoadAll<Sprite>("προϊόντα/canteen_coffee")[12];
            //ingredientsprite = Resources.LoadAll<Sprite>("προϊόντα/canteen_coffee")[8];
            return ingredientsprite;*/
            return null;
        }


        return null;
    }

    //choose a sprite for the bottom part of the recipe
    public Sprite SpriteLayerBase(string recipename)
    {
        if (recipename.StartsWith("Sandwich"))
        {
            return SpriteHandler("Sandwich_Bread_Down");
        }
        else if (recipename.StartsWith("Toast"))
        {
            return SpriteHandler("Toast_Bread");
        }
        else if (recipename.StartsWith("Coffee"))
        {
            return SpriteHandler("Coffee_Down");
        }
        else if (recipename.StartsWith("IceCream"))
        {
            //Debug.Log("icecream base");
            return theGameMaster.spriteslayers["IceCream_Down"];
        }
        else if (recipename.StartsWith("Salad"))
            return theGameMaster.spriteslayers["Salad_Bowl"];
        else if(recipename.StartsWith("Club"))
        {
            //add plate
            return theGameMaster.spriteslayers["Club_Down"];
        }


        Debug.Log("null base: "+recipename);
        return null;
    }

    //choose a sprite for the top layer of the recipe
    //useful for recipes with few ingredients,
    // that would otherwise leave lots of space empty between them
    public List<SpriteRenderer> SpriteLayerTop(string recipename, List<SpriteRenderer> renderers)
    {
        foreach (SpriteRenderer rendr in renderers)
        {
            if (rendr.sprite == null)
            {
                if (recipename.StartsWith("Sandwich"))
                {
                    rendr.sprite = SpriteHandler("Sandwich_Bread_Top");
                    return renderers;
                }
                else if (recipename.StartsWith("Toast"))
                {
                    rendr.sprite = SpriteHandler("Toast_Bread");
                    return renderers;
                }
                else if (recipename.StartsWith("Coffee"))
                {
                    rendr.sprite = SpriteHandler("Coffee_Up");
                    return renderers;
                }
                else if(recipename.StartsWith("Club"))
                {
                    rendr.sprite = theGameMaster.spriteslayers["Club_Up"];
                    return renderers;
                }

            }
        }
        return null;
    }

    //choose ingredient sprites based on recipes
    public Sprite SpriteChooseIngredient(string recipename,string ingredientname)
    {
        if (recipename.StartsWith("Sandwich"))
        {
            if (ingredientname == "Cheese")
            {
                //return Resources.LoadAll<Sprite>("προϊόντα/canteen_sandwich-02")[17];
                return GameMasterScript.Instance.spriteslayers["Sandwich_Cheese"];
            }
            else if (ingredientname == "Ham")
            {
                //return Resources.LoadAll<Sprite>("προϊόντα/canteen_sandwich-02")[18];
                return GameMasterScript.Instance.spriteslayers["Sandwich_Ham"];
            }
            else if (ingredientname == "Lettuce")
            {
                //return Resources.LoadAll<Sprite>("προϊόντα/canteen_sandwich-02")[15];
                return GameMasterScript.Instance.spriteslayers["Sandwich_Lettuce"];
            }
            else if (ingredientname == "Tomato")
            {
                return GameMasterScript.Instance.spriteslayers["Sandwich_Tomato"];
                //return Resources.LoadAll<Sprite>("προϊόντα/canteen_sandwich-02")[14];
            }

        }
        else if (recipename.StartsWith("Toast"))
        {
            if (ingredientname == "Cheese")
            {
                //return  Resources.LoadAll<Sprite>("προϊόντα/canteen_toast-01")[15];
                return GameMasterScript.Instance.spriteslayers["Toast_Cheese"];
            }
            else if (ingredientname == "Ham")
            {
                //return Resources.LoadAll<Sprite>("προϊόντα/canteen_toast-01")[16];
                return GameMasterScript.Instance.spriteslayers["Toast_Ham"];
            }
            else if (ingredientname == "Lettuce")
            {
                //return Resources.LoadAll<Sprite>("προϊόντα/canteen_toast-01")[17];
                return GameMasterScript.Instance.spriteslayers["Toast_Lettuce"];
            }
            else if (ingredientname == "Tomato")
            {
                //return Resources.LoadAll<Sprite>("προϊόντα/canteen_toast-01")[14];
                return GameMasterScript.Instance.spriteslayers["Toast_Tomato"];
            }
        }
        else if (recipename.StartsWith("Coffee"))
        {
            if (ingredientname == "Coffee")
            {
                //return  Resources.LoadAll<Sprite>("προϊόντα/canteen_coffee")[10];
                return GameMasterScript.Instance.spriteslayers["Coffee"];
            }
            else if (ingredientname == "Ice")
            {
                //return Resources.LoadAll<Sprite>("προϊόντα/canteen_coffee")[11];
                return GameMasterScript.Instance.spriteslayers["Ice"];
            }
            else if (ingredientname == "Milk")
            {
                //return Resources.LoadAll<Sprite>("προϊόντα/canteen_coffee")[9];
                return GameMasterScript.Instance.spriteslayers["Milk"];
            }

        }
        else if (recipename.StartsWith("IceCream"))
        {
            return GameMasterScript.Instance.spriteslayers[ingredientname];
        }
        else if (recipename.StartsWith("Salad"))
        {
            if (ingredientname == "Tomato")
                return GameMasterScript.Instance.spriteslayers["Salad_Tomato"];
            else if (ingredientname == "Lettuce")
                return GameMasterScript.Instance.spriteslayers["Salad_Lettuce"];
            return GameMasterScript.Instance.spriteslayers[ingredientname];
        }
        else if(recipename.StartsWith("Club"))
        {
            return GameMasterScript.Instance.spriteslayers["Club_" + ingredientname];
        }
        return null;
    }

    Vector2 directiontotarget;

    /*
    IEnumerator Fade()
    {
        for (float ft = 1f; ft >= 0; ft -= 0.1f)
        {
            Color c = renderer.material.color;
            c.a = ft;
            renderer.material.color = c;
            yield return new WaitForSeconds(.1f);
        }
    }
    */

    //a scaling and "de-scaling" coroutine that also calls the transparency coroutine
    // also checks if we have won the level
    IEnumerator ScaleO()
    {
        //Debug.Log("coroutinescale");
        theFurnace.numberofcompletedrecipes += 1;
        theNextRecipe.gamepause = true;//
        for (float ft = 1f; ft <= 2f; ft += 0.1f)
        {
            gameObject.transform.localScale = new Vector3(+ft, +ft, +ft);
            yield return new WaitForSeconds(.025f);
        }

        for (float ft = 2f; ft >= 0; ft -= 0.1f)
        {
            gameObject.transform.localScale = new Vector3(+ft, +ft, +ft);
            yield return new WaitForSeconds(.025f);
        }

        //theNextRecipe.gamepause = true;//
        CustomerScript.Instance.tesrFunction();

        //winning condition
        if (theFurnace.numberofcompletedrecipes == theFurnace.numberofrecipesinlevel)
            theChangeScene.change_scene();


        Destroy(gameObject);
        //theNextRecipe.gamepause = false;//
    }

}
