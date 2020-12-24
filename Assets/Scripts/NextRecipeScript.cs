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
            gamepause = false;
        }
        else
        {
            Debug.Log("nextrecipescriptgamepause false");
            gamepause = true;
        }
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
            button.interactable = true;
        else
            button.interactable = false;
    }
}
