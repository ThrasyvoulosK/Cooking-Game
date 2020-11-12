using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
//using System.Diagnostics;
using UnityEngine;

/*VERY IMPORTANT SCRIPT*/
/*IngredientScript handles everything about the ingredients after their spawning, including their movement and their fitting within recipes */
/*Among other things, it interacts with most other scripts, including the FoodLayerScipt by choosing the correct layered sprite, the FurnaceScript by deciding what to do when a recipe is finished and the MoneyScript by changing our current amount of money*/
/*Also includes an 'invisible' sprite function*/
public class IngredientScript : MonoBehaviour
{
   
    BoxCollider2D boxCollider2D;

    //current mouse position coordinates
    Vector2 mouseposition;

    /*float initialmousepositionX;
    float initialmousepositionY;*/

    public FurnaceScript theFurnace;
    public SpawningScript theSpawning;
    public ChangeSceneScript theChangeScene;
    public FoodLayersScript theFoodLayer;
    public SpawningFoodLayerScript theSPF;
    public GameObject foodlayergameobject=null;
    public MoneyScript theMoney;
    public NextRecipeScript theNextRecipe;

    public Sprite spr;

    GameObject target;
    Vector2 directiontotarget;

    public GameObject foodlayerclone;

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

        theMoney = GameObject.Find("Money").GetComponent<MoneyScript>();

        theSPF = GameObject.Find("SpawningFoodLayer").GetComponent<SpawningFoodLayerScript>();
        theSPF.spawnfoodlayer();//

        foodlayerclone = GameObject.Find("FoodLayer(Clone)");

        theNextRecipe = GameObject.Find("NextRecipeButton").GetComponent<NextRecipeScript>();
    }
    // Update is called once per frame
    void Update()
    {
        
        
        //keep moving until the edge if no interaction happens
        if (gameObject.name.Contains("Clone"))
        {
            transform.Translate(Vector2.right * Time.deltaTime*theSpawning.gamespeed);
            if (gameObject.transform.position.x >11)
            {
                Destroy(gameObject);
            }
        }

        MoveIngredient();

    }

    bool notnullrecipeexists;

    public int inum = 0;
    public void OnMouseDown()
    {
        //Debug.Log("onmousedown");

        //do not allow clicking if the game is paused/disabled
        if (theNextRecipe.gamepause)
            return;

        //create a layered-food if we don't already have one
        if ((foodlayerclone = GameObject.Find("FoodLayer(Clone)"))==false)
        {
            Debug.Log("foodlayerclone is false");
            theSPF.spawnfoodlayer();
            foodlayerclone = GameObject.Find("FoodLayer(Clone)");

            Debug.Log("foodlayerclone: "+foodlayerclone);
        }
        theFoodLayer = GameObject.Find("FoodLayer(Clone)").GetComponent<FoodLayersScript>();

        //get the correct base-sprite
        //foodlayerclone.GetComponent<FoodLayersScript>().renderers[3].sprite = theFoodLayer.SpriteLayerBase(theFurnace.recipe.name);

        //theNextRecipe.gamepause = true;
        theNextRecipe.gamepause = false;

        //rename our object so that it is usable within recipes
        gameObject.name = gameObject.name.Substring(0, gameObject.name.Length - 7);//remove (clone) from string

        //add to current recipe
        theFurnace.current_recipe.Add(gameObject.name);

        bool ingredientisonthelist = false;
        //remove from the correct igredient from the numbers list
        int i = 0;

        //Debug.Log("before loops");

        foreach (string checkingr in theFurnace.recipe.neededIngr)
        {
            if (checkingr == gameObject.name)
            {
                //if we have all of what we need from this ingredient, we shouldn't bother dealing with it
                if(theFurnace.usable_number_of_ingredients[i]==0)
                {
                    //Debug.Log("filled ingreadient");
                    ingredientisonthelist = false;
                    break;
                }

                // otherwise, we are going to use it in our recipe:

                //assign to the correct number
                theFurnace.usable_number_of_ingredients[i]--;

                inum = i;
                ingredientisonthelist = true;
                target = GameObject.Find("Furnace");

                //also, clone it as a transparent-ticked object
                string formrname = gameObject.name;
                invisivise2(formrname,gameObject);// Instantiate(gameObject).name="gamobject(Clone)";//

                //since we have our target, we should change our sprite to something more convenient
                gameObject.GetComponent<SpriteRenderer>().sprite = theFoodLayer.SpriteChooseIngredient(theFurnace.recipe.name, gameObject.name);
                //disable text labels as well
                //gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
                gameObject.GetComponentInChildren<TextMeshPro>().enabled = false;


                break;
            }
            i++;
        }
        
        //destroy only if it is on the list
        if (ingredientisonthelist == false)
        {
            //remove ingredient from the current list if not needed
            theFurnace.current_recipe.Remove(gameObject.name);
            Debug.Log("object not on list");
            //rename it back to its original name
            //(this is a workaround for allowing only "(clones)" to move!
            gameObject.name += "(Clone)";            
        }

        //when we destroy an object, we can instantiate the next one
        theSpawning.spawnallowed = true;

    }

    //move towards the target (when clicked correctly)
    void MoveIngredient()
    {
        if(target!=null)
        {
            directiontotarget = (target.transform.position - transform.position).normalized;
            GetComponent<Rigidbody2D>().velocity = new Vector2(directiontotarget.x *  8*theSpawning.gamespeed, directiontotarget.y *  8*theSpawning.gamespeed);
        }
    }

    //handle the collision with the Furnace object
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name=="Furnace")
        {
            foodlayerclone.GetComponent<FoodLayersScript>().renderers[inum].sprite = theFoodLayer.SpriteChooseIngredient(theFurnace.recipe.name, gameObject.name);
            inum = 0;

            int i = 0;

            //Debug.Log("before second loop");
            foreach (int correctingr in theFurnace.usable_number_of_ingredients)
            {

                int it = 0;

                //Debug.Log("within second loop");
                //continue if we have at least one correct ingredient in our recipe
                if (correctingr <= 0)
                {
                    //Debug.Log("fully completed ingredient"+theFurnace.recipe.neededIngr[correctingr]);
                    //ingredientcompleted[correctingr] = true;
                    i++;
                }
                /*else
                {
                    ingredientcompleted[correctingr] = false;
                }*/
                //if we have all the correct ingredients in our recipe, decide what to do next
                if (i == theFurnace.recipe.neededIngr.Count)
                {
                    Debug.Log("recipe ready!");


                    //allow for a pause between recipes//
                    //wait for the button to be pressed
                    theNextRecipe.gamepause = true;
                    //Time.timeScale = 0;

                    //add money
                    theMoney.money = theMoney.money + 2f;

                    //add the upper bun graphically
                    theFoodLayer.SpriteLayerTop(theFurnace.recipe.name, foodlayerclone.GetComponent<FoodLayersScript>().renderers);


                    //reset currentrecipe list
                    theFurnace.current_recipe.Clear();

                    //prepare for the next recipe or end the level
                    if (theFurnace.next_recipe != null)
                    {
                        //don't search if there aren't any recipes left
                        // and change the level, since this is the Winning Condition!
                        if (theFurnace.next_recipe.Count == 0)
                        {
                            //Debug.Log("no more recipes");
                            theChangeScene.change_scene();
                        }

                        //otherwise,
                        //search for a non-null recipe in the nextrecipes lists
                        // and replace our current recipe with its fields

                        it = Random.Range(0, theFurnace.next_recipe.Count);

                        //replace all our current-recipe's values with the ones from next-recipe
                        if (theFurnace.next_recipe.Count > 0)
                        {
                            theFurnace.recipe = theFurnace.next_recipe[it];
                            theFurnace.recipe.numbOfIng = theFurnace.next_recipe[it].numbOfIng;//
                            theFurnace.recipe.neededIngr = theFurnace.next_recipe[it].neededIngr;//
                            theFurnace.recipe = theFurnace.next_recipe[it];

                            //remove "it" member of list-of-next-recipes
                            theFurnace.next_recipe.RemoveAt(it);

                        }

                        //Debug.Log("curent recipes first ingredient" + theFurnace.recipe.neededIngr[0]);

                        //???change the next recipe???
                        //Debug.Log("next recipe length " + theFurnace.next_recipe.Count);
                    }

                    //now that wehave chosen our new recipe, we should gather the ingredients
                    theFurnace.usable_number_of_ingredients.Clear();

                    //allow for a new order to be placed as well
                    foodlayerclone.GetComponent<FoodLayersScript>().change = true;
                    foodlayerclone.transform.position = new Vector2(0, 1);

                    theSPF.spawnfoodlayerallowed = true;//

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

            Destroy(gameObject);
        }
        //Debug.Log("collision of " + gameObject.name+" with "+collision.name);
    }

    //duplicate an ingredient object
    // and choose an appropriate sprite for it
    void invisivise2(string formername,GameObject ingredient)
    {
        //ingredient.GetComponent<SpriteRenderer>().sprite = null;
        //Debug.Log("resourcesize: " + Resources.LoadAll<Sprite>("προϊόντα/canteen_checked").Length);

        GameObject fingredient = Instantiate(ingredient);
        fingredient.name = "ingredient(Clone)";

        //disable text labels
        //fingredient.GetComponentInChildren<MeshRenderer>().enabled = false;
        //gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
        //fingredient.GetComponentInChildren<TextMeshPro>().enabled = false;
        fingredient.GetComponent<SpriteRenderer>().sortingOrder = 7;

        if (formername.StartsWith("Cheese"))
        {
            fingredient.GetComponent<SpriteRenderer>().sprite= Resources.LoadAll<Sprite>("προϊόντα/canteen_checked")[1];
            //ingredient.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("canteen_checked _1")[0];
        }
        else if (formername.StartsWith("Tomato"))
        {
            fingredient.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("προϊόντα/canteen_checked")[0];
        }
        else if (formername.StartsWith("Ham"))
        {
            fingredient.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("προϊόντα/canteen_checked")[2];
        }
        else if (formername.StartsWith("Lettuce"))
        {
            fingredient.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("προϊόντα/canteen_checked")[3];
        }
        else if (formername.StartsWith("Milk"))
        {
            fingredient.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("προϊόντα/canteen_checked")[4];
        }
        else if (formername.StartsWith("Coffee"))
        {
            fingredient.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("προϊόντα/canteen_checked")[5];
        }
        else if (formername.StartsWith("Ice"))
        {
            fingredient.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("προϊόντα/canteen_checked")[6];
        }
        else
        {
            Debug.Log("falsename");
        }
        //Instantiate(ingredient).name = "none(Clone)";
        

    }
}
