using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialScript : MonoBehaviour
{
    public List<Sprite> tutorialSprites;

    static int currentScreen=0;

    GameMasterScript theGameMaster;
    // Start is called before the first frame update
    void Start()
    {
        theGameMaster = GameObject.Find("GameMaster").GetComponent<GameMasterScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextscreen(GameObject nextGameObject)
    {
        currentScreen++;

        GameObject newgameobject = Instantiate(nextGameObject);

        //assign texts and translations
        string newText;
        newText = newgameobject.name.Remove(12);//"(Clone)"
        //if we have a description, assing the text on top, otherwise it will go to the speech bubble
        if (newgameobject.transform.GetChild(1).gameObject.activeInHierarchy==true)
        {
            newgameobject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = theGameMaster.languagehandler[newText];
        }
        else
            newgameobject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = theGameMaster.languagehandler[newText];

        Destroy(gameObject);
    }
}
