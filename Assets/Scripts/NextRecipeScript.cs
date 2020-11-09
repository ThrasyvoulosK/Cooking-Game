using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*NextRecipeScript is called when we need to prevent a new recipe from appearing, as well as allow the next one to do so*/
/*it previously used to be a button, that is currently disabled, with its functionality replaced from within the other scripts*/
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
        if (gamepause)
        {
            Debug.Log("nextrecipescriptgamepause true");
            //Time.timeScale = 1;
            //theCustomer.allowspritetochange = true;
            gamepause = false;

            //enabled = true;
            
        }
        else
        {
            Debug.Log("nextrecipescriptgamepause false");
            //theCustomer.allowspritetochange = true;
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
