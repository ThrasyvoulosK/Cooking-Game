using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransactionScript : MonoBehaviour
{
    public string objecttobebought;
    public Sprite[] spritetobebought;

    public GameMasterScript theGameMaster;
    public ChangeSceneScript theChangeScene;
    // Start is called before the first frame update
    void Start()
    {
        theGameMaster = GameObject.Find("GameMaster").GetComponent<GameMasterScript>();
        theChangeScene = GameObject.Find("Canvas").GetComponent<ChangeSceneScript>();

        GameObject.Find("Object1").GetComponent<SpriteRenderer>().sprite = spritetobebought[0];
        GameObject.Find("Object2").GetComponent<SpriteRenderer>().sprite = spritetobebought[1];
        GameObject.Find("Object3").GetComponent<SpriteRenderer>().sprite = spritetobebought[2];
        
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
        gr.GetComponent<Transform>().localScale= new Vector3(0.6f,0.6f,0f);

        gr.GetComponent<SpriteRenderer>().sortingOrder = -2;

        gr.SetActive(true);

    }
}
