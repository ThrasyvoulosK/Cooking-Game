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

                theChangeScene.change_scene();
            }

        }
        
    }
}
