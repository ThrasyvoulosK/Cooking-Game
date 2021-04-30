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
        //Debug.Log("Tutorial Started!");
        theGameMaster = GameObject.Find("GameMaster").GetComponent<GameMasterScript>();
        
        //instantiate the first screen if we haven't started yet
        if(currentScreen==0&&gameObject.transform.parent!=null)
            if(gameObject.transform.parent.name == "Canvas")
                nextscreen(gameObject);
        
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
            newgameobject.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
            newgameobject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = theGameMaster.languagehandler[newText];

        //assign button text
        newgameobject.transform.GetChild(3).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = theGameMaster.languagehandler["Continue"];

        //destroy previous screen
        Destroy(gameObject);

        //handle transition from tutorial screens to gameplay
        //Debug.Log("current tutorial screen: " + currentScreen);
        if (currentScreen >= tutorialSprites.Count - 1)
        {
            GameObject.Find("Canvas").transform.Find("Image").gameObject.SetActive(false);
            GameObject.Find("Canvas").transform.Find("ProgressBar").gameObject.SetActive(true);
            GameObject.Find("SpawningPoint").GetComponent<SpawningScript>().enabled = true;

            //reset tutorial, so that it replays later, if needed
            currentScreen = 0;
        }
    }
}
