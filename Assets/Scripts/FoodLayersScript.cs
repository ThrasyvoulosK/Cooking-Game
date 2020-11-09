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

    GameObject target;
    GameObject movetoward;

    



    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        //oldsprite = null;
        //oldsprite = Resources.Load<Sprite>("Triangle");
        //newsprite = Resources.Load<Sprite>("Square");//Resourses.

        //renderer.sprite = oldsprite;

        //renderers[0].sprite

        theSPF = GameObject.Find("SpawningFoodLayer").GetComponent<SpawningFoodLayerScript>();

        target = GameObject.Find("Furnace");
        movetoward = GameObject.Find("SpeechBubble");

        //load a base graphic
        //renderers[3].sprite = SpriteLayerBase(target.GetComponent<FurnaceScript>().recipe.name);

        theNextRecipe = GameObject.Find("NextRecipeButton").GetComponent<NextRecipeScript>();

        //gameObject.transform.localScale *=2;
        gameObject.transform.localScale = new Vector3(1.5f,1.5f,1.5f);

        theChangeScene = GameObject.Find("Canvas").GetComponent<ChangeSceneScript>();
        theFurnace = GameObject.Find("Furnace").GetComponent<FurnaceScript>();

        gameObject.name = "FoodLayer(Clone)";

    }

    public bool change = false;
    // Update is called once per frame
    void Update()
    {
        // if (theNextRecipe.gamepause)
        //    renderers[3].sprite = null;//renderer.enabled = false;
        //else
        //renderers[3].sprite = SpriteLayerBase(target.GetComponent<FurnaceScript>().recipe.name);//renderer.enabled = true;

        //MoveFoodLayer();
        if (change == true)
        {
            //rename our object so that it doesn't interfere with other similar objects' functionality
            if (gameObject.name.EndsWith("(Clone)"))
                gameObject.name = gameObject.name.Substring(0, gameObject.name.Length - 7);//remove (clone) from string


            transform.position = Vector2.MoveTowards(transform.position, movetoward.transform.position, Time.deltaTime * 03);

            /*foreach (SpriteRenderer renderer in gameObject.GetComponent<FoodLayersScript>().renderers)
            {
                renderer.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, renderer.material.color.a * 0.1f);
            }
            */
            //StartCoroutine("Fade");

            theSPF.spawnfoodlayer();

            //if (gameObject.transform.position ==movetoward.transform.position)&&(gameObject.transform.position == movetoward.transform.position)
            if ((gameObject.transform.position.x == movetoward.transform.position.x) && (gameObject.transform.position.y == movetoward.transform.position.y))
            {
                //       transform.localScale = new Vector3(3f, 3f, 3f);
                //StartCoroutine("Fade");
                /*foreach (SpriteRenderer renderer in gameObject.GetComponent<FoodLayersScript>().renderers)
                    StartCoroutine("Scale");
                StartCoroutine("Scale");*/
                StartCoroutine("ScaleO");
                //Destroy(gameObject);
                change = false;
            }
        }
        else if (theNextRecipe.gamepause)
         //if (theNextRecipe.gamepause)
        {
            //Debug.Log("rendred null");
            //renderers[3].sprite = null;
            if (gameObject.name.EndsWith("(Clone)"))
                renderers[4].sprite = null;
        }
        else
        {
            //Debug.Log("spriterenderedfully");
            //renderers[3].sprite = SpriteLayerBase(target.GetComponent<FurnaceScript>().recipe.name);
            if (gameObject.name.EndsWith("(Clone)"))
                renderers[4].sprite = SpriteLayerBase(target.GetComponent<FurnaceScript>().recipe.name);
        }

    }

    public void changesprite()
    {
        change = true;
        Debug.Log("changesprite");
        renderer.sprite = newsprite;

        //renderer.transform.position.Set(10, 10, 0);
        transform.position = new Vector2(transform.position.x+0,transform.position.y+2);

        //Sprite nextsprite = Instantiate(newsprite,Vector2(0,  1),transform.position(0,0));
    }


    public void AddIngredient(Sprite sprite) {
        change = true;
        renderers[0].sprite = sprite;

        renderers[1].sprite = sprite;

        renderers[2].sprite = sprite;

       // newsprite = sprite;
        Debug.Log("addingredient");

        othersprite = sprite;

    }

    public void ResetIngredient(Sprite sprite)
    {
        change = false;
        renderers[0].sprite = sprite;

        renderers[1].sprite = sprite;

        renderers[2].sprite = sprite;

        // newsprite = sprite;
        Debug.Log("resetingredient");

    }

    //choose and load a sprite directly by its name
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
        return null;
    }

    Vector2 directiontotarget;


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

    IEnumerator ScaleO()
    {
        Debug.Log("coroutinescale");
        theFurnace.numberofcompletedrecipes += 1;
        theNextRecipe.gamepause = true;//
        for (float ft = 1f; ft <= 2f; ft += 0.1f)
        {
            gameObject.transform.localScale = new Vector3(+ft, +ft, +ft);
            //gameObject.transform.localScale += new Vector3(+ft, +ft, +ft);
            //gameObject.transform.localScale += new Vector3(3f, 3f, 3f);
            yield return new WaitForSeconds(.025f);
        }

        for (float ft = 2f; ft >= 0; ft -= 0.1f)
        {
            gameObject.transform.localScale = new Vector3(+ft, +ft, +ft);
            //gameObject.transform.localScale += new Vector3(+ft, +ft, +ft);
            //gameObject.transform.localScale += new Vector3(3f, 3f, 3f);
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
