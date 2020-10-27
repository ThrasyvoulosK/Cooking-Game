using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerScript : MonoBehaviour
{
    public SpriteRenderer customercurrentspriterenderer;
    //spawn random sprites from a given list
    public List<Sprite> customerspritelist = new List<Sprite>();
    //check when they need to disappear and get new ones if they do
    bool customerisdone=false;
    //the place where the customers appear initially, before leaving
    public Transform[] customerinitialposition;

    public NextRecipeScript theNextRecipe;

    public int customerandom;
    // Start is called before the first frame update
    void Start()
    {
        customercurrentspriterenderer = gameObject.GetComponent<SpriteRenderer>();
        
        customerandom = Random.Range(0, customerspritelist.Count);
        customercurrentspriterenderer.sprite = customerspritelist[customerandom];
        customerisdone = false;

        theNextRecipe = GameObject.Find("NextRecipeButton").GetComponent<NextRecipeScript>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (theNextRecipe.gamepause)
        {
            customerisdone = true;
            customercurrentspriterenderer.sprite = null;

            customerandom = Random.Range(0, customerspritelist.Count);
            //customercurrentspriterenderer.sprite = customerspritelist[customerandom];
        }
        else
        {
            customerisdone = false;
            customercurrentspriterenderer.sprite = customerspritelist[customerandom];

        }

        /*if(customerisdone)
        {
            //customerandom = Random.Range(0, customerspritelist.Count);
            //customercurrentspriterenderer.sprite = customerspritelist[customerandom];

            customerisdone = false;
        }*/
        
        
    }

    public Sprite customerchangesprite()
    {
        return null;
    }
}
