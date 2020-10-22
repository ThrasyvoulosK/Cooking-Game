using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using System.Security.Cryptography;
//using System.Diagnostics;
using UnityEngine;

public class FoodLayersScript : MonoBehaviour
{
    public SpriteRenderer renderer;
    public Sprite oldsprite,newsprite,othersprite;
    public List<SpriteRenderer> renderers = new List<SpriteRenderer>();

    //we need access to the furnace in order to obtain variables like number-of-ingredients
    public FurnaceScript theFurnace;

    public SpawningFoodLayerScript theSPF;

    public NextRecipeScript theNextRecipe;

    GameObject target;

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

        //load a base graphic
        //renderers[3].sprite = SpriteLayerBase(target.GetComponent<FurnaceScript>().recipe.name);

        theNextRecipe = GameObject.Find("NextRecipeButton").GetComponent<NextRecipeScript>();

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
            //very buggy in updayte! 
            if (gameObject.name.EndsWith("(Clone)"))
                gameObject.name = gameObject.name.Substring(0, gameObject.name.Length - 7);//remove (clone) from string
            //transform.position = new Vector2(0,1);
            transform.Translate(Vector2.right * Time.deltaTime * 4);
            theSPF.spawnfoodlayer();
            if (gameObject.transform.position.x > 4)
            {
                Destroy(gameObject);
                change = false;
                //theSPF.spawnfoodlayer();
            }
        }
        else if (theNextRecipe.gamepause)
            renderers[3].sprite = null;
        else
            renderers[3].sprite = SpriteLayerBase(target.GetComponent<FurnaceScript>().recipe.name);

    }

    public void changesprite()
    {
        change = true;
        Debug.Log("changesprite");
        renderer.sprite = newsprite;

        //renderer.transform.position.Set(10, 10, 0);
        transform.position = new Vector2(transform.position.x+0,transform.position.y+1);

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
            ingredientsprite = Resources.LoadAll<Sprite>("canteen_toast-01")[13];
            return ingredientsprite;
        }
        else if (ingredientname == "Sandwich_Bread_Down")
        {
            ingredientsprite = Resources.LoadAll<Sprite>("canteen_sandwich-02")[16];
            return ingredientsprite;
        }
        else if (ingredientname == "Sandwich_Bread_Top")
        {
            ingredientsprite = Resources.LoadAll<Sprite>("canteen_sandwich-02")[13];
            return ingredientsprite;
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
        else
        {
            return SpriteHandler("Toast_Bread");
        }
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
                    //break;
                    //return rendr.sprite;
                    return renderers;
                }
                else if (recipename.StartsWith("Toast"))
                {
                    rendr.sprite = SpriteHandler("Toast_Bread");
                    //break;
                    //return rendr.sprite;
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
                return Resources.LoadAll<Sprite>("canteen_sandwich-02")[17];
            }
            else if (ingredientname == "Ham")
            {
                return Resources.LoadAll<Sprite>("canteen_sandwich-02")[18];
            }
            else if (ingredientname == "Lettuce")
            {
                return Resources.LoadAll<Sprite>("canteen_sandwich-02")[15];
            }
            else if (ingredientname == "Tomato")
            {
                return Resources.LoadAll<Sprite>("canteen_sandwich-02")[14];
            }

        }
        else if (recipename.StartsWith("Toast"))
        {
            if (ingredientname == "Cheese")
            {
                return  Resources.LoadAll<Sprite>("canteen_toast-01")[15];
            }
            else if (ingredientname == "Ham")
            {
                return Resources.LoadAll<Sprite>("canteen_toast-01")[16];
            }
            else if (ingredientname == "Lettuce")
            {
                return Resources.LoadAll<Sprite>("canteen_toast-01")[17];
            }
            else if (ingredientname == "Tomato")
            {
                return Resources.LoadAll<Sprite>("canteen_toast-01")[14];
            }

        }
        return null;
    }

    Vector2 directiontotarget;
    /*
    void MoveFoodLayer()
    {
        if (target != null)
        {
            directiontotarget = (target.transform.position - transform.position).normalized;
            //GetComponent<Rigidbody2D>().velocity = new Vector2(directiontotarget.x * 8, directiontotarget.y * 8);
            //transform.position = new Vector2(directiontotarget.x * 8, directiontotarget.y * 8);
            //transform.position = new Vector2(transform.position.x+directiontotarget.x * 8, transform.position.y+directiontotarget.y * 8);

            float newpos = Mathf.Repeat(Time.time * 1f, 20);
            transform.position = Vector2.right * newpos;
            ///transform.position = new Vector2(directiontotarget.x * 1, directiontotarget.y * 1);
        }
    }
    */

}
