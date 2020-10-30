using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextRecipeScript : MonoBehaviour
{
   public bool gamepause=true;
    public Button button=null;

    public CustomerScript theCustomer;

    public static NextRecipeScript Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void allow_next_recipe()
    {
        Debug.Log("nextrecipescript");

        if (gamepause)
        {
            //Time.timeScale = 1;
            gamepause = false;

            //enabled = true;
        }
        else
        {
            //Time.timeScale = 0;
            gamepause = true;

            //enabled = false;

            /*theCustomer.customercurrentspriterenderer = GameObject.Find("Customer").GetComponent<SpriteRenderer>();
            theCustomer.customerandom = Random.Range(0, theCustomer.customerspritelist.Count);
            theCustomer.customercurrentspriterenderer.sprite = theCustomer.customerspritelist[theCustomer.customerandom];
            */
        }

        //isclickable = false;

    }

    void Start()
    {
        //gamepause = true;
        gamepause = false;

        button = GameObject.Find("NextRecipeButton").GetComponent<Button>();

        theCustomer = GameObject.Find("Customer").GetComponent<CustomerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gamepause)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }

    }
}
