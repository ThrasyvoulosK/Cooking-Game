using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningScript : MonoBehaviour
{
    public bool spawnallowed = true;//

    public GameObject[] ingredients;

    public Transform[] spawnpoints;

    // Start is called before the first frame update
    void Start()
    {
        //spawnallowed = false;//
        if(spawnallowed==true)
            InvokeRepeating("SpawnIngredient", 0, 2);//
        //InvokeRepeating("SpawnIngredient", 0, 0.1f);//
        //spawnallowed = false;
        Debug.Log("spwnallowed " + spawnallowed);
        
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
            randomingredient = Random.Range(0, ingredients.Length);
            //Instantiate(ingredients[1],spawnpoints[0].position,Quaternion.identity);
            Instantiate(ingredients[randomingredient], spawnpoints[0].position, Quaternion.identity);

            //spawnallowed = false; //allow always
            //allow again when destroyed in ingredeient script

        }

    }
}
