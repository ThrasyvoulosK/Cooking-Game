using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningScript : MonoBehaviour
{
    public bool spawnallowed = true;//

    //public GameObject[] ingredients;
    public List<GameObject> ingredients = new List<GameObject>();
    public List<GameObject> ingredients_current = new List<GameObject>();

    public Transform[] spawnpoints;

    public float gamespeed = 2f;
    //public float gamespeed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        //ingredients_current = ingredients;
        foreach (GameObject ingr in ingredients)
            ingredients_current.Add(ingr);
        //spawnallowed = false;//
        if (spawnallowed==true)
            InvokeRepeating("SpawnIngredient", 0, 1*gamespeed);        
    }
    /*
    // Update is called once per frame
    void Update()
    {
        
    }
    */
    void SpawnIngredient()
    {

        int randomingredient;
        if(spawnallowed)
        {
            if (ingredients_current.Count <= 1)
            {
                //ingredients_current = ingredients;
                foreach (GameObject ingr in ingredients)
                    ingredients_current.Add(ingr);
            }

            //randomingredient = Random.Range(0, ingredients.Count);
            randomingredient = Random.Range(0, ingredients_current.Count);
            //Instantiate(ingredients[randomingredient], spawnpoints[0].position, Quaternion.identity);
            Instantiate(ingredients_current[randomingredient], spawnpoints[0].position, Quaternion.identity);
            ingredients_current.RemoveAt(randomingredient);

            

            //spawnallowed = false; //allow always
            //allow again when destroyed in ingredeient script

        }

    }
}
