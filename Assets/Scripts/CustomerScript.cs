﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*CustomerScript deals with the appearance (and disappearance) of the customers.*/
/*A customer is basically a sprite that changes after their demands are satisfied*/
/*Functions to ensure randomness of appearance and fading from view are included*/
public class CustomerScript : MonoBehaviour
{
    public SpriteRenderer customercurrentspriterenderer;
    //spawn random sprites from a given list
    public List<Sprite> customerspritelist = new List<Sprite>();
    public List<Sprite> customerspritelistcurrent = new List<Sprite>();
    //check when they need to disappear and get new ones if they do
    bool customerisdone=false;
    //the place where the customers appear initially, before leaving
    public Transform[] customerinitialposition;
    public static CustomerScript Instance;

    public NextRecipeScript theNextRecipe;

    public int customerandom;
    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

  

    void Start()
    {
        customercurrentspriterenderer = gameObject.GetComponent<SpriteRenderer>();
        
        customerandom = Random.Range(0, customerspritelist.Count);
        customercurrentspriterenderer.sprite = customerspritelist[customerandom];
        customerisdone = false;

        theNextRecipe = GameObject.Find("NextRecipeButton").GetComponent<NextRecipeScript>();

        foreach (Sprite cust in customerspritelist)
            customerspritelistcurrent.Add(cust);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (theNextRecipe.gamepause)
        {
            customerisdone = true;

            customercurrentspriterenderer.enabled = false;
            //customerandom = Random.Range(0, customerspritelist.Count);

           /*if (customerspritelistcurrent.Count <= 0)
            {
                foreach (Sprite cust in customerspritelist)
                    customerspritelistcurrent.Add(cust);
            }
            customerandom = Random.Range(0, customerspritelistcurrent.Count);*/

            //customerspritelistcurrent.RemoveAt(customerandom);
            customercurrentspriterenderer.enabled = true;

            //customerspritelistcurrent.RemoveAt(customerandom);

            //Debug.Log("thenextrecipegamepause");
            allowspritetochange = true;
        }
        else
        {
            if (allowspritetochange)
                customerchangesprite();
            //customercurrentspriterenderer.sprite = customerspritelist[customerandom];
            //customercurrentspriterenderer.sprite = customerspritelistcurrent[customerandom];

            //StopAllCoroutines();
            Color32 newColor = new Color32(255, 255, 255, 255);
            gameObject.GetComponent<SpriteRenderer>().color = newColor;
            GameObject.Find("SpeechBubble").GetComponent<SpriteRenderer>().color = newColor;

            /*customercurrentspriterenderer.sprite = customerspritelistcurrent[customerandom];
            if (customerisdone)
                customerspritelistcurrent.RemoveAt(customerandom);*/
            customerisdone = false;
            //customercurrentspriterenderer.sprite = customerspritelist[customerandom];
            //Debug.Log("thenextrecipegamepausefalse");
        }
    }

    //call the following function only once during the update
    public bool allowspritetochange = false;
    public void customerchangesprite()
    {
        //
        Debug.Log("customerchangesprite");

        customercurrentspriterenderer.sprite = null;
        customerspritelistcurrent.RemoveAt(customerandom);

        customerandom = Random.Range(0, customerspritelistcurrent.Count);

        if (customerspritelistcurrent.Count <= 0)
        {
            foreach (Sprite cust in customerspritelist)
                customerspritelistcurrent.Add(cust);
        }
        //customerandom = Random.Range(0, customerspritelistcurrent.Count);
        customercurrentspriterenderer.sprite = customerspritelistcurrent[customerandom];
        //customercurrentspriterenderer.sprite = customerspritelist[customerandom];

        //customerspritelistcurrent.RemoveAt(customerandom);

        allowspritetochange = false;
        //return null;
    }

    public void tesrFunction()
    {
        StartCoroutine("FadeO");
    }

    IEnumerator FadeO()
    {
       
        for (float ft = 1f; ft > 0f; ft -= 0.1f)
        {
            Color c = gameObject.GetComponent<SpriteRenderer>().color;
            c.a = ft;
            gameObject.GetComponent<SpriteRenderer>().color = c;
            GameObject.Find("SpeechBubble").GetComponent<SpriteRenderer>().color = c;
            yield return new WaitForSeconds(.025f);
        }

        Debug.Log("coroutinefade");
        theNextRecipe.gamepause = false;
    }
}
