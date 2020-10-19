﻿using System.Collections;
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
    public SpawningFoodLayerScript theSPF;
    public GameObject foodlayergameobject=null;
    public MoneyScript theMoney;

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

        //theFoodLayer = GameObject.Find("Food").GetComponent<FoodLayersScript>();//

        theMoney = GameObject.Find("Money").GetComponent<MoneyScript>();

        theSPF = GameObject.Find("SpawningFoodLayer").GetComponent<SpawningFoodLayerScript>();
        theSPF.spawnfoodlayer();//

        foodlayerclone = GameObject.Find("FoodLayer(Clone)");
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

    //bool mousealreadyclicked = false;

    //this list will be used in order to convert
    List<Recipe_SO> sorec = null;
    //public GameObject foodlayerclone;
    public void OnMouseDown()
    {
        //Debug.Log("onmousedown");

        //create a layered-food if we don't already have one
        if ((foodlayerclone = GameObject.Find("FoodLayer(Clone)"))==false)
        {
            Debug.Log("foodlayerclone is false");
            theSPF.spawnfoodlayer();
            foodlayerclone = GameObject.Find("FoodLayer(Clone)");

            //theFoodLayer = GameObject.Find("Food").GetComponent<FoodLayersScript>();

            Debug.Log("foodlayerclone: "+foodlayerclone);
        }
        theFoodLayer = GameObject.Find("FoodLayer(Clone)").GetComponent<FoodLayersScript>();

        if (theFurnace.recipe.name.StartsWith("Sandwich"))
        {
            Debug.Log("sandwich");
            foodlayerclone.GetComponent<FoodLayersScript>().renderers[3].sprite = theFoodLayer.SpriteHandler("Sandwich_Bread_Down");
        }
        else
        {
            Debug.Log("toast");
            foodlayerclone.GetComponent<FoodLayersScript>().renderers[3].sprite = theFoodLayer.SpriteHandler("Toast_Bread");
        }

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


                //theFoodLayer.renderers[i].sprite = theFoodLayer.SpriteHandler(gameObject.name);
                if (theFurnace.recipe.name.StartsWith("Sandwich"))
                {
                    Debug.Log("sandwich");
                    //foodlayerclone.GetComponent<FoodLayersScript>().renderers[4].sprite = theFoodLayer.SpriteHandler("Sandwich_Bread_Down");
                }
                else
                {
                    //foodlayerclone.GetComponent<FoodLayersScript>().renderers[4].sprite = theFoodLayer.SpriteHandler("Toast_Bread");
                    Debug.Log("toast");
                }
                foodlayerclone.GetComponent<FoodLayersScript>().renderers[i].sprite = theFoodLayer.SpriteHandler(gameObject.name);
                //foodlayerclone.GetComponent<FoodLayersScript>().renderers[i].sprite = theFoodLayer.SpriteHandler(gameObject.name);


                ingredientisonthelist = true;
                target = GameObject.Find("Furnace");
                //since we have our target, we should change our sprite to something more convenient
                gameObject.GetComponent<SpriteRenderer>().sprite= theFoodLayer.SpriteHandler(gameObject.name);
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
            //continue if we have at least one correct ingredient in our recipe
            if (correctingr <= 0)
            {
                //Debug.Log("fully completed ingredient"+theFurnace.recipe.neededIngr[correctingr]);
                i++;
            }
            //if we have all the correct ingredients in our recipe, decide what to do next
            if (i == theFurnace.recipe.neededIngr.Count)
            {
                Debug.Log("recipe ready!");

                //add money
                theMoney.money = theMoney.money+2f;

                //add the upper bun
                //foodlayerclone.GetComponent<FoodLayersScript>().renderers[3].sprite= theFoodLayer.SpriteHandler("Toast_Bread");
                //foodlayerclone.GetComponent<FoodLayersScript>().renderers[4].sprite= theFoodLayer.SpriteHandler("Toast_Bread");
                if (theFurnace.recipe.name.StartsWith("Sandwich"))
                {
                    Debug.Log("sandwich");
                    foodlayerclone.GetComponent<FoodLayersScript>().renderers[4].sprite = theFoodLayer.SpriteHandler("Sandwich_Bread_Top");
                    //foodlayerclone.GetComponent<FoodLayersScript>().renderers[3].sprite = theFoodLayer.SpriteHandler("Sandwich_Bread_Down");
                }
                else
                {
                    foodlayerclone.GetComponent<FoodLayersScript>().renderers[4].sprite = theFoodLayer.SpriteHandler("Toast_Bread");
                    //foodlayerclone.GetComponent<FoodLayersScript>().renderers[3].sprite = theFoodLayer.SpriteHandler("Toast_Bread");
                    Debug.Log("toast");
                }


                //reset currentrecipe list
                theFurnace.current_recipe.Clear();

                //prepare for the next recipe or end the level
                if (theFurnace.next_recipe!=null)
                {
                    //don't search if there aren't any recipes left
                    // and change the level, since this is the Winning Condition!
                    if(theFurnace.next_recipe.Count==0)
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
                //foodlayerclone.GetComponent<FoodLayersScript>().theSPF.spawnfoodlayerallowed = true;//
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
