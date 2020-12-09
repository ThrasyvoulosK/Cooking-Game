using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*SpawningScript deals with the spawning of ingredients*/
/*Note: spawning gets called once at start with an InvokeRepeating function; changing things during runtime doesn't affect it*/
/*includes a global speed variable that is used throughout the game 
 and also some functionality for preventing the same ingredients from spawning again and again*/
public class SpawningScript : MonoBehaviour
{
    public bool spawnallowed = true;//

    //public GameObject[] ingredients;
    public List<GameObject> ingredients = new List<GameObject>();
    public List<GameObject> ingredients_current = new List<GameObject>();

    public Transform[] spawnpoints;

    public float gamespeed = 2f;
    //public float gamespeed = 4f;

    public GameMasterScript theGameMaster;
    public FurnaceScript theFurnace;

    // Start is called before the first frame update
    void Start()
    {
        //ingredients_current = ingredients;
        foreach (GameObject ingr in ingredients)
            ingredients_current.Add(ingr);
        //spawnallowed = false;//
        if (spawnallowed==true)
            InvokeRepeating("SpawnIngredient", 0, 1*gamespeed);

        theGameMaster = GameObject.Find("GameMaster").GetComponent<GameMasterScript>();
        theFurnace= GameObject.Find("Furnace").GetComponent<FurnaceScript>();
    }
    
    // Update is called once per frame
    /*void Update()
    {
        ingredients = theFurnace.current_recipe;
        
    }*/
    
    void SpawnIngredient()
    {

        int randomingredient;
        if(spawnallowed)
        {
            if (ingredients_current.Count < 1)
            {
                foreach (GameObject ingr in ingredients)
                {
                    //if(ingr.name )
                    foreach(string recing in theFurnace.recipe.neededIngr)
                    {
                        if (recing == ingr.name)
                        {
                            ingredients_current.Add(ingr);
                            randomingredient = Random.Range(0, ingredients.Count);
                            ingredients_current.Add(ingredients[randomingredient]);

                        }
                    }
                    //ingredients_current.Add(ingr);
                }
            }

            randomingredient = Random.Range(0, ingredients_current.Count);
            //Instantiate(ingredients_current[randomingredient], spawnpoints[0].position, Quaternion.identity);
            GameObject spingredient=Instantiate(ingredients_current[randomingredient], spawnpoints[0].position, Quaternion.identity);
            ingredients_current.RemoveAt(randomingredient);

            spingredient.name = spingredient.name.Substring(0, spingredient.name.Length - 7);//remove (clone)
            spingredient.GetComponentInChildren<TextMeshPro>().SetText(theGameMaster.languagehandler[spingredient.name]);
            spingredient.name += "(Clone)";

            //spawnallowed = false; //allow always
            //allow again when destroyed in ingredeient script

        }

    }
}
