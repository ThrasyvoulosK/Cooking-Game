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
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (theNextRecipe.gamepause)
        {
            customerisdone = true;
            /*gameObject.GetComponent<SpriteRenderer>().color = gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().material.color.r, gameObject.GetComponent<SpriteRenderer>().material.color.g, gameObject.GetComponent<SpriteRenderer>().material.color.b, gameObject.GetComponent<SpriteRenderer>().material.color.a * 0.1f);

            GameObject.Find("SpeechBubble").GetComponent<SpriteRenderer>().color=new Color(gameObject.GetComponent<SpriteRenderer>().material.color.r, gameObject.GetComponent<SpriteRenderer>().material.color.g, gameObject.GetComponent<SpriteRenderer>().material.color.b, gameObject.GetComponent<SpriteRenderer>().material.color.a * 0.1f);//
            */

            customerandom = Random.Range(0, customerspritelist.Count);
        }
        else
        {
            //StopAllCoroutines();
            Color32 newColor = new Color32(255, 255, 255, 255);
            gameObject.GetComponent<SpriteRenderer>().color = newColor;
            GameObject.Find("SpeechBubble").GetComponent<SpriteRenderer>().color = newColor;
            
            customerisdone = false;
            customercurrentspriterenderer.sprite = customerspritelist[customerandom];

        }
    }

    public Sprite customerchangesprite()
    {
        return null;
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
