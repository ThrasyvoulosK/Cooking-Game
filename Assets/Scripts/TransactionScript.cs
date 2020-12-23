using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class TransactionScript : MonoBehaviour
{
    public string objecttobebought;
    public Sprite[] spritetobebought;

    public GameMasterScript theGameMaster;
    public ChangeSceneScript theChangeScene;

    public GameObject[] screenGameObjects;

    public bool is_colour = true;
    // Start is called before the first frame update
    void Start()
    {
        theGameMaster = GameObject.Find("GameMaster").GetComponent<GameMasterScript>();
        theChangeScene = GameObject.Find("Canvas").GetComponent<ChangeSceneScript>();

        GameObject.Find("Object1").GetComponent<SpriteRenderer>().sprite = spritetobebought[0];
        GameObject.Find("Object2").GetComponent<SpriteRenderer>().sprite = spritetobebought[1];
        if(spritetobebought.Length>=3)
            GameObject.Find("Object3").GetComponent<SpriteRenderer>().sprite = spritetobebought[2];

        GameObject.Find("ChooseButton").GetComponentInChildren<Text>().text = theGameMaster.languagehandler["Choose"];

        //
        GameObject.Find("ContinueButton").GetComponentInChildren<Text>().text = theGameMaster.languagehandler["Continue"];
        GameObject.Find("Congratulations").GetComponent<Text>().text = theGameMaster.languagehandler["Congratulations"];
        Debug.Log("option character" + theGameMaster.option_character);
        GameObject.Find("CharacterImage").GetComponent<Image>().sprite = theGameMaster.spriteslayers[theGameMaster.option_character];
        //GameObject.Find("CharacterImage").GetComponent<Image>().sprite = theGameMaster.spriteslayers["Character_M"];

        //hide all these until we press coninue
        foreach(GameObject sgameObject in screenGameObjects)
        {
            sgameObject.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit2D=Physics2D.Raycast(worldPoint,Vector2.zero);
            if (hit2D.collider!=null)
            {
                Debug.Log("raycast hits "+hit2D.collider.name);
                theGameMaster.boughtables[objecttobebought] = hit2D.collider.GetComponent<SpriteRenderer>().sprite;
                Debug.Log(theGameMaster.boughtables[objecttobebought].name);

                Transform transform = hit2D.collider.transform;

                greenrectangle(transform);

                //allow 'choice' button
                GameObject.Find("ChooseButton").GetComponent<Button>().interactable = true;
                //theChangeScene.change_scene();
            }

        }
        
    }

    //this function creates a green rectangle around an object, when we click on it
    void greenrectangle(Transform transform)
    {
        GameObject gr=new GameObject();
        gr.SetActive(false);
        //SpriteRenderer grspr = new SpriteRenderer();
        //grspr.sprite = theGameMaster.spriteslayers["Rectangle_Green_Full"];
        gr.AddComponent<SpriteRenderer>();
        gr.GetComponent<SpriteRenderer>().sprite= theGameMaster.spriteslayers["Rectangle_Green_Full"];

        GameObject otherobject;
        if ((otherobject = GameObject.Find("Selected")) != null)
            Destroy(otherobject);
        gr.name = "Selected";
        //gr = Instantiate(gr,transform);
        gr.GetComponent<Transform>().position = transform.position;
        gr.GetComponent<Transform>().position -= new Vector3(0,transform.position.y,0);
        gr.GetComponent<Transform>().localScale= new Vector3(0.65f,0.65f,0f);

        gr.GetComponent<SpriteRenderer>().sortingOrder = -2;

        gr.SetActive(true);

    }

    public void showscreen()
    {
        foreach (GameObject sgameObject in screenGameObjects)
        {
            Debug.Log("showscreen " + sgameObject.name);
            sgameObject.SetActive(true);
        }
    }
}
