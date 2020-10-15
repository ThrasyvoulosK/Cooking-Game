using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using System.Diagnostics;
using UnityEngine;
public class IngredientScript : MonoBehaviour
{
   
    BoxCollider2D boxCollider2D;

    //current mouse position coordinates
    Vector2 mouseposition;

    float initialmousepositionX;
    float initialmousepositionY;

    public FurnaceScript theFurnace;
    public SpawningScript theSpawning;
    public ChangeSceneScript theChangeScene;
    public FoodLayersScript theFoodLayer;
    public MoneyScript theMoney;

    public Sprite spr;

    GameObject target;
    Vector2 directiontotarget;

    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();

        //furnace needs this as a reference
        theFurnace = GameObject.Find("Furnace").GetComponent<FurnaceScript>();
        //also spawning
        theSpawning = GameObject.Find("SpawningPoint").GetComponent<SpawningScript>();

        //the ingredients should fly towards this, if correct
        target = null;

        theChangeScene = GameObject.Find("Canvas").GetComponent<ChangeSceneScript>();

        theFoodLayer = GameObject.Find("Food").GetComponent<FoodLayersScript>();
        //theFoodLayer = GameObject.Find("SpawningFoodLayer").GetComponent<FoodLayersScript>();

        theMoney = GameObject.Find("Money").GetComponent<MoneyScript>();

        //spr = GameObject.Find("IngredientScript").GetComponent<spr>();
        //spr = GameObject.Find("IngredientScript").GetComponent<SpriteRenderer>().sprite;//
        //spr=Resources.Load
        
    }
    // Update is called once per frame
    void Update()
    {
        
        
        //keep moving
        if (gameObject.name.Contains("Clone"))
        {
            transform.Translate(Vector2.right * Time.deltaTime*2);
            if (gameObject.transform.position.x >11)
            {
                Destroy(gameObject);
            }
        }

        MoveIngredient();

    }

    bool notnullrecipeexists;

    bool mousealreadyclicked = false;

    //this list will be used in order to convert
    List<Recipe_SO> sorec = null;
    public GameObject foodlayerclone;
    public void OnMouseDown()
    {
        //theFoodLayer.changesprite();
        //spr = GameObject.Sprite;
        //theFoodLayer.AddIngredient(spr);
        /*theFoodLayer.theSPF.spawnfoodlayer();
        foodlayerclone = GameObject.Find("FoodLayer(Clone)");*/
        //foodlayerclone.GetComponent<FoodLayersScript>().theSPF.destroyfoodlayer();
        if((foodlayerclone = GameObject.Find("FoodLayer(Clone)"))==false)
        {
            Debug.Log("foodlayerclone is false");
            theFoodLayer.theSPF.spawnfoodlayer();
            foodlayerclone = GameObject.Find("FoodLayer(Clone)");
        }



        //Debug.Log("onmousedown");

        //replacing previous functionality
        //WORK IN PROGRESS!

        //rename our object so that it is usable within recipes
        gameObject.name = gameObject.name.Substring(0, gameObject.name.Length - 7);//remove (clone) from string

        //add to current recipe
        theFurnace.current_recipe.Add(gameObject.name);

        bool ingredientisonthelist = false;
        //remove from the correct igredient from the numbers list
        int i = 0;

        //Debug.Log("begore loops");

        foreach (string checkingr in theFurnace.recipe.neededIngr)
        {
            if (checkingr == gameObject.name)
            {
                //if we have all of what we need from this ingredient, we shouldn't bother dealing with it
                if(theFurnace.usable_number_of_ingredients[i]==0)
                {
                    //Debug.Log("filled ingreadient");
                    //theFoodLayer.AddIngredient(spr);//

                    //theFoodLayer.ResetIngredient(spr);

                    ingredientisonthelist = false;
                    break;
                }

                // otherwise, we are going to use it in our recipe:

                //assign to the correct number
                theFurnace.usable_number_of_ingredients[i]--;

                //theFoodLayer.AddIngredient(spr);

                //theFoodLayer.newsprite = gameObject.GetComponent<SpriteRenderer>().sprite;
                //theFoodLayer.newsprite = Resources.Load<Sprite>("Circle");
                //theFoodLayer.newsprite = Resources.Load<Sprite>("toast_cheese");
                //theFoodLayer.newsprite = Resources.Load<Sprite>("canteen_toast-01");

                //theFoodLayer.newsprite = Resources.LoadAll<Sprite>("canteen_toast-01").Single(s=>s.name=="toast_cheese");
                //theFoodLayer.newsprite=Resources.LoadAll<Sprite>("canteen_toast-01")[9];

                //theFoodLayer.newsprite = theFoodLayer.SpriteHandler(gameObject.name);

                theFoodLayer.renderers[i].sprite = theFoodLayer.SpriteHandler(gameObject.name);

                //foodlayerclone.renderers[i].sprite = theFoodLayer.SpriteHandler(gameObject.name);
                //foodlayerclone.GetComponent < SpriteRenderer >().sprite = theFoodLayer.SpriteHandler(gameObject.name);
                /*foodlayerclone.GetComponent<FoodLayersScript>().renderers[i].sprite = theFoodLayer.SpriteHandler(gameObject.name);*/
                //foodlayerclone.GetComponent<FoodLayersScript>().renderers[i].sprite = null;


                ingredientisonthelist = true;
                target = GameObject.Find("Furnace");
                break;
            }
            i++;
        }

        i = 0;

        //Debug.Log("before second loop");

        foreach (int correctingr in theFurnace.usable_number_of_ingredients)
        {

            int it = 0;

            //Debug.Log("within second loop");
            if (correctingr <= 0)
            {
                //Debug.Log("fully completed ingredient"+theFurnace.recipe.neededIngr[correctingr]);
                i++;
                //return;//
            }
            if (i == theFurnace.recipe.neededIngr.Count)
            {
                //WINNING CONDITION
                Debug.Log("recipe ready!");

                //add money
                theMoney.money = theMoney.money+2f;

                //reset currentrecipe list
                theFurnace.current_recipe.Clear();
                //Debug.Log("iterator " + iterator);
                if (theFurnace.next_recipe!=null)
                {
                    //don't search if there aren't any recipes
                    if(theFurnace.next_recipe.Count==0)
                    {
                        //Debug.Log("no more recipes");
                        theChangeScene.change_scene();
                    }

                    //search for a non-null recipe in the nextrecipes lists
                    // and replace our current recipe with its fields
                    /*foreach(Recipe_SO nrec in theFurnace.next_recipe)
                    {*/

                        it = Random.Range(0, theFurnace.next_recipe.Count);

                    /*if(nrec!=null)
                    {*/
                    if (theFurnace.next_recipe.Count > 0)
                    {
                        theFurnace.recipe = theFurnace.next_recipe[it];
                        theFurnace.recipe.numbOfIng = theFurnace.next_recipe[it].numbOfIng;//
                        theFurnace.recipe.neededIngr = theFurnace.next_recipe[it].neededIngr;//
                        theFurnace.recipe = theFurnace.next_recipe[it];

                        //we don't need it anymore, so we nullify it in order to be avoided by the next search
                        //theFurnace.next_recipe[it] = null;
                        theFurnace.next_recipe.RemoveAt(it);

                        /*break;*/

                        /* } */
                    
                        }

                    //it++;
                    //winning condition
                    /*if (it == theFurnace.next_recipe.Count)
                    {
                        Debug.Log("all recipes done");
                        //copy pasted from down
                        Debug.Log("win");
                        theChangeScene.change_scene();
                    }*/

                    /*}*/
                    //Debug.Log("curent recipes first ingredient" + theFurnace.recipe.neededIngr[0]);

                    //change the next recipe
                    //Debug.Log("next recipe length " + theFurnace.next_recipe.Count);

                    Destroy(foodlayerclone);//
                }

                //now that wehave chosen our recipe, we should gather the ingredients
                theFurnace.usable_number_of_ingredients.Clear();

                //theFoodLayer.ResetIngredient(spr);
                //theFoodLayer.theSPF.destroyfoodlayer();
                //foodlayerclone.GetComponent<FoodLayersScript>().theSPF.destroyfoodlayer();
                //Destroy(foodlayerclone);
                //theFoodLayer.
                /*foodlayerclone.GetComponent<FoodLayersScript>().renderers[0].sprite = null;
                foodlayerclone.GetComponent<FoodLayersScript>().renderers[1].sprite = null;
                foodlayerclone.GetComponent<FoodLayersScript>().renderers[2].sprite = null;
                foodlayerclone.GetComponent<FoodLayersScript>().renderers[3].sprite = null;
                foodlayerclone.GetComponent<FoodLayersScript>().renderers[4].sprite = null;*/
                /*foodlayerclone.GetComponent<FoodLayersScript>().theSPF.destroyfoodlayer();*/
                //foodlayerclone.GetComponent<FoodLayersScript>().theSPF.timeron = true;

                //Debug.Log("it: " + it);
                //Debug.Log("next recipe");
                foreach (int j in theFurnace.recipe.numbOfIng)
                {
                    theFurnace.usable_number_of_ingredients.Add(j);
                    //break;
                }
                //
                break;

            }
        }
        //destroy only if it is on the list
        if (ingredientisonthelist == false)
        {
            //remove from the current list if not needed
            theFurnace.current_recipe.Remove(gameObject.name);
            Debug.Log("object not on list");
            //rename it back to its original name
            //(this is a workaround for allowing only "(clones)" to move!
            gameObject.name += "(Clone)";            
        }

        //when we destroy an object, we can instantiate the next one
        theSpawning.spawnallowed = true;

    }

    void MoveIngredient()
    {
        if(target!=null)
        {
            directiontotarget = (target.transform.position - transform.position).normalized;
            GetComponent<Rigidbody2D>().velocity = new Vector2(directiontotarget.x *  8, directiontotarget.y *  8);
        }
    }



    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name=="Furnace")
        {
            Destroy(gameObject);
        }
        //Debug.Log("collision of " + gameObject.name+" with "+collision.name);
    }
}
