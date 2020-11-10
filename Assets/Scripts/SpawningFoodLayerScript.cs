﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*SpawningFoodLayerScript is responsible for creating a new layered-recipe (in the middle of the screen) when allowed to do so*/
public class SpawningFoodLayerScript : MonoBehaviour
{

    public bool spawnfoodlayerallowed = false;

    public GameObject foodlayer;

    public float timetodestroy=3f;
    public bool timeron=false;

    public Transform spawnpoint;
    // Start is called before the first frame update
    void Start()
    {
        spawnfoodlayerallowed = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        //timekeeper();
        
    }

    //var instance;
    public void spawnfoodlayer()
    {
        if (spawnfoodlayerallowed)
        {
            Instantiate(foodlayer, spawnpoint.position,Quaternion.identity);
        }
        spawnfoodlayerallowed = false;
        //Debug.Log("spawnfoodlayer");
    }
    /*
    public void destroyfoodlayer()
    {
        timeron = true;


            foreach(SpriteRenderer sr in Instantiate(foodlayer).GetComponent<FoodLayersScript>().renderers)
            {
                sr.sprite = null;
            }
            
            Destroy(Instantiate(foodlayer));
            spawnfoodlayerallowed = true;
            //Debug.Log("destroyfoodlayer");
        
    }
    */
    /*
    public void timekeeper()
    {
        if (timeron)
        {
            if (timetodestroy > 0)
            {
                timetodestroy -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Time has run out!");
                timetodestroy = 0;

                //codefromdestroy
                foreach (SpriteRenderer sr in Instantiate(foodlayer).GetComponent<FoodLayersScript>().renderers)
                {
                    sr.sprite = null;
                }
                Instantiate(foodlayer).GetComponent<FoodLayersScript>().renderers[0].sprite = null;
                Instantiate(foodlayer).GetComponent<FoodLayersScript>().renderers[1].sprite = null;
                Instantiate(foodlayer).GetComponent<FoodLayersScript>().renderers[2].sprite = null;
                Instantiate(foodlayer).GetComponent<FoodLayersScript>().renderers[3].sprite = null;
                Instantiate(foodlayer).GetComponent<FoodLayersScript>().renderers[4].sprite = null;
                Destroy(Instantiate(foodlayer));
                spawnfoodlayerallowed = true;
                Debug.Log("destroyfoodlayer");
                //endofcodefromdestroy
                timeron = false;
            }
        }
    }
    */

}
