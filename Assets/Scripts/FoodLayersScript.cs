using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using System.Security.Cryptography;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

/*FoodLayersScript deals with the appearance of the layered food that is under construction in the middle of the screen.
  It also deals with its movement, scaling and disappearance routines*/
/*sprite-handling functions for the base-of-the-recipe-graphic and its middle and top ingredients are included*/
public class FoodLayersScript : MonoBehaviour
{
    public SpriteRenderer renderer;
    public Sprite oldsprite,newsprite,othersprite;
    public List<SpriteRenderer> renderers = new List<SpriteRenderer>();

    float distances_between_ingredients = 0.175f;

    //we need access to the furnace in order to obtain variables like number-of-ingredients
    public FurnaceScript theFurnace;

    public SpawningFoodLayerScript theSPF;

    public NextRecipeScript theNextRecipe;

    public ChangeSceneScript theChangeScene;

    public GameMasterScript theGameMaster;

    public MoneyScript theMoney;

    GameObject target;
    GameObject movetoward;

    bool potatoeson=false;
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

        theMoney = GameObject.Find("Money").GetComponent<MoneyScript>();

        gameObject.name = "FoodLayer(Clone)";
        
        /*make changes to the food layer for the following specific recipes*/

        //change ice cream's position to within the board's surface
        if (theFurnace.recipe.name.StartsWith("IceCream")|| theFurnace.recipe.name.StartsWith("Salad"))
            gameObject.transform.position = new Vector3(-0.67f, 0.4f, 0);
        else if(theFurnace.recipe.name.StartsWith("Club"))//initialise club sandwich, if needed
        {
            int clchldrn;
            clchldrn = gameObject.transform.childCount;
            
            for(int i=1;i<clchldrn;i++)
            {
                //Debug.Log("forloop");

                if (theFurnace.recipe.neededIngr.Count < i)
                {
                    gameObject.transform.GetChild(i - 1).GetComponent<Transform>().localPosition += new Vector3(0f, -0.175f * i + 0.05f * (i)+0.1f, 0);
                    break;
                }
                // Debug.Log("i "+i+" neededingr "+theFurnace.recipe.neededIngr.Count);
                //Debug.Log(theFurnace.recipe.neededIngr[i-1]);

                //gameObject.transform.GetChild(i - 1).GetComponent<Transform>().localPosition = new Vector3(0f, 0.05f * (i-1), 0);
                gameObject.transform.GetChild(i - 1).GetComponent<Transform>().localPosition += new Vector3(0f, -0.175f*i+0.05f*(i) , 0);
                Debug.Log("0.05*i-1=" + 0.05 * (i-1));

                if (theFurnace.recipe.neededIngr[i-1] == "Potato")
                {
                    potatoeson = true;

                    //assign the base's position on potatoes
                    gameObject.transform.GetChild(i - 1).GetComponent<Transform>().localPosition = gameObject.transform.GetChild(clchldrn-2).GetComponent<Transform>().localPosition;

                    gameObject.transform.GetChild(i-1).GetComponent<SpriteRenderer>().sortingOrder = 4;
                }
                                 
            }
            for (int i = 1; i < clchldrn; i++)
            {
                gameObject.transform.GetChild(i - 1).GetComponent<Transform>().localPosition += new Vector3(0f, 0.175f, 0);
                if (potatoeson)
                {
                    gameObject.transform.GetChild(i - 1).GetComponent<SpriteRenderer>().sortingOrder++;
                    if(i==clchldrn-1)
                        //gameObject.transform.GetChild(i).GetComponent<Transform>().localPosition += new Vector3(0f, -0.700f, 0);
                        gameObject.transform.GetChild(i).GetComponent<Transform>().localPosition = new Vector3(0f, theFurnace.recipe.neededIngr.Count*0.05f, 0);

                }
                /*if ((i != (clchldrn - 2)))
                { 
                    gameObject.transform.GetChild(i).GetComponent<Transform>().localPosition = new Vector3(0f, 0.05f * (i), 0);
                    if (theFurnace.recipe.neededIngr.Count>=i )
                    {
                        if (theFurnace.recipe.neededIngr[i] == "Potato")
                        {
                            Debug.Log("1");
                            if (i < clchldrn - 2)
                            {
                                Debug.Log("2");
                                gameObject.transform.GetChild(i).GetComponent<Transform>().localPosition = new Vector3(0f, 0f, 0);
                            }
                        }
                    }
                }*/
                /*else
                {
                    gameObject.transform.GetChild(i).GetComponent<Transform>().localPosition = new Vector3(0f, 0, 0);
                }*/

                //gameObject.transform.GetChild(i - 1).GetComponent<Transform>().localPosition += new Vector3(0f, 0.175f, 0);
                

            }

            gameObject.GetComponent<SpriteRenderer>().sprite= theGameMaster.spriteslayers["Plate"];
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 4;

        }

        set_ingredient_distance();
        
        //Debug.Log("k");
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
                renderers[renderers.Count - 2].sprite = null;
        }
        else
        {
            //Debug.Log("spriterenderedfully");
            if (gameObject.name.EndsWith("(Clone)"))
                renderers[renderers.Count-2].sprite = SpriteLayerBase(target.GetComponent<FurnaceScript>().recipe.name);
        }

    }

    //choose and load a sprite directly by its name
    // this functions includes sprites for all parts of the foodlayer,
    // including base ingredients, middle ingredients and ending ingredients
    public Sprite SpriteHandler(string ingredientname)
    {
        Sprite ingredientsprite=null;

        if(ingredientname=="Cheese")
            ingredientsprite = Resources.LoadAll<Sprite>("canteen_toast-01")[15];
        else if (ingredientname == "Ham")
            ingredientsprite = Resources.LoadAll<Sprite>("canteen_toast-01")[16];
        else if (ingredientname == "Lettuce")
            ingredientsprite = Resources.LoadAll<Sprite>("canteen_toast-01")[17];
        else if (ingredientname == "Tomato")
            ingredientsprite = Resources.LoadAll<Sprite>("canteen_toast-01")[14];
        else if (ingredientname == "Toast_Bread")
            ingredientsprite = Resources.LoadAll<Sprite>("προϊόντα/canteen_toast-01")[13];
        else if (ingredientname == "Sandwich_Bread_Down")
            ingredientsprite = Resources.LoadAll<Sprite>("προϊόντα/canteen_sandwich-02")[16];
        else if (ingredientname == "Sandwich_Bread_Top")
            ingredientsprite = Resources.LoadAll<Sprite>("προϊόντα/canteen_sandwich-02")[13];
        else if (ingredientname == "Coffee_Down")
            ingredientsprite = Resources.LoadAll<Sprite>("προϊόντα/canteen_coffee")[8];
        else if (ingredientname == "Coffee_Up")
            return null;

        return ingredientsprite;
    }

    //choose a sprite for the bottom part of the recipe
    public Sprite SpriteLayerBase(string recipename)
    {
        if (recipename.StartsWith("Sandwich"))
            return SpriteHandler("Sandwich_Bread_Down");
        else if (recipename.StartsWith("Toast"))
            return SpriteHandler("Toast_Bread");
        else if (recipename.StartsWith("Coffee"))
            return SpriteHandler("Coffee_Down");
        else if (recipename.StartsWith("IceCream"))
            return theGameMaster.spriteslayers["IceCream_Down"];
        else if (recipename.StartsWith("Salad"))
            return theGameMaster.spriteslayers["Salad_Bowl"];
        else if(recipename.StartsWith("Club"))
            return theGameMaster.spriteslayers["Club_Down"];

        //Debug.Log("null base: "+recipename);
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
                    if (potatoeson)
                        rendr.GetComponent<Transform>().position -= new Vector3(0, 0.175f, 0);
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
                return GameMasterScript.Instance.spriteslayers["Sandwich_Cheese"];
            else if (ingredientname == "Ham")
                return GameMasterScript.Instance.spriteslayers["Sandwich_Ham"];
            else if (ingredientname == "Lettuce")
                return GameMasterScript.Instance.spriteslayers["Sandwich_Lettuce"];
            else if (ingredientname == "Tomato")
                return GameMasterScript.Instance.spriteslayers["Sandwich_Tomato"];

            return GameMasterScript.Instance.spriteslayers[ingredientname];

        }
        else if (recipename.StartsWith("Toast"))
        {
            if (ingredientname == "Cheese")
                return GameMasterScript.Instance.spriteslayers["Toast_Cheese"];
            else if (ingredientname == "Ham")
                return GameMasterScript.Instance.spriteslayers["Toast_Ham"];
            else if (ingredientname == "Lettuce")
                return GameMasterScript.Instance.spriteslayers["Toast_Lettuce"];
            else if (ingredientname == "Tomato")
                return GameMasterScript.Instance.spriteslayers["Toast_Tomato"];
        }
        else if (recipename.StartsWith("Coffee"))
        {
            if (ingredientname == "Coffee")
                return GameMasterScript.Instance.spriteslayers["Coffee"];
            else if (ingredientname == "Ice")
                return GameMasterScript.Instance.spriteslayers["Ice"];
            else if (ingredientname == "Milk")
                return GameMasterScript.Instance.spriteslayers["Milk"];

            return GameMasterScript.Instance.spriteslayers[ingredientname];

        }
        else if (recipename.StartsWith("IceCream"))
            return GameMasterScript.Instance.spriteslayers[ingredientname];
        else if (recipename.StartsWith("Salad"))
        {
            if (ingredientname == "Tomato")
                return GameMasterScript.Instance.spriteslayers["Salad_Tomato"];
            else if (ingredientname == "Lettuce")
                return GameMasterScript.Instance.spriteslayers["Salad_Lettuce"];

            return GameMasterScript.Instance.spriteslayers[ingredientname];
        }
        else if(recipename.StartsWith("Club"))
            return GameMasterScript.Instance.spriteslayers["Club_" + ingredientname];

        return null;
    }

    Vector2 directiontotarget;

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
        if (SceneManager.GetActiveScene().buildIndex != (SceneManager.sceneCountInBuildSettings - 2))
        {
            if ((theFurnace.numberofcompletedrecipes == theFurnace.numberofrecipesinlevel))
                theChangeScene.change_scene();
        }
        else
        {
            //Debug.Log("Level 10 with money: " + theMoney.money);
            if (theMoney.money >= 10000)
                theChangeScene.change_scene();
        }

        Destroy(gameObject);
        //theNextRecipe.gamepause = false;//
    }

    void set_ingredient_distance()
    {
        int clchldrn=gameObject.transform.childCount;
        float newdist;
        newdist = 0.1f;
        if ((theFurnace.recipe.name.StartsWith("Sandwich") || theFurnace.recipe.name.StartsWith("Toast") || theFurnace.recipe.name.StartsWith("Salad")) == false)
            return;
        gameObject.transform.GetChild(clchldrn - 2).GetComponent<Transform>().localPosition = new Vector3(0, -newdist, 0);
        for(int i=0;i<clchldrn;i++)
        {
            if(i!=clchldrn-2)
                gameObject.transform.GetChild(i).GetComponent<Transform>().localPosition = new Vector3(0, i*newdist, 0);
        }
    }
}
