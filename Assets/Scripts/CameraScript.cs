using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/*Find the screeen's resolution and change its ui elements' positions accordingly*/
/*All of the following elements have been calculated by hand*/
public class CameraScript : MonoBehaviour
{
    public GameMasterScript theGameMaster;
    // Start is called before the first frame update
    void Start()
    {
        //call the gamemaster gameobject
        theGameMaster = GameObject.Find("GameMaster").GetComponent<GameMasterScript>();
        
        //match screen to height
        GameObject.Find("Canvas").GetComponent<CanvasScaler>().matchWidthOrHeight = 1f;
        
        //to determine whether our scene is a menu or a game scene,
        // search for the Counter object
        GameObject cntr=null;
        bool isgamescene = false;
        if ((cntr = GameObject.Find("Counter"))!=null)
            isgamescene = true;

        //find the camera's aspect
        // and change it accordingly
        if (Camera.main.aspect < 1.26f)//1280/1024
        {
            Debug.Log("5:4");

            Camera.main.orthographicSize = 7.1f;
            if (isgamescene)
            {
                GameObject.Find("recipe board").GetComponent<RectTransform>().anchoredPosition = new Vector3(69f, -212f, 0f);

                GameObject.Find("Board").GetComponent<RectTransform>().anchoredPosition = new Vector3(62f, -82f, 0f);
                GameObject.Find("Board").GetComponent<RectTransform>().sizeDelta = new Vector3(487f, 232f, 0f);

                GameObject.Find("RecImage").GetComponent<RectTransform>().anchoredPosition = new Vector3(382f, -40f, 0f);
                GameObject.Find("RecImage").GetComponent<RectTransform>().sizeDelta = new Vector2(150f, 95f);

                GameObject.Find("Money").GetComponent<RectTransform>().anchoredPosition = new Vector3(635f, 185f, 0f);
            }
            else if (GameObject.Find("OptionsButton") != null)
            {
                //get 'back' menu button
                theGameMaster.menuitems[3].GetComponent<RectTransform>().anchoredPosition = new Vector3(-425f, 386f, 0);
                theGameMaster.menuitems[3].GetComponent<RectTransform>().localScale = new Vector3(0.75f, 0.75f, 0);
                //'options' button
                theGameMaster.menuitems[1].GetComponent<RectTransform>().anchoredPosition = new Vector3(-110f, -447f, 0);

            }
            else if(GameObject.Find("ContinueButton")!=null)
            {
                GameObject.Find("ContinueButton").GetComponent<RectTransform>().localScale = new Vector3(3.5f, 3.5f, 0);
                GameObject.Find("DecText").GetComponent<RectTransform>().localScale = new Vector3(0.4f, 0.4f, 0);
            }

        }
        else if (Camera.main.aspect < 1.34f)//640/480
        {
            Debug.Log("4:3");
            Camera.main.orthographicSize = 6.7f;

            if (isgamescene)
            {
                GameObject.Find("recipe board").GetComponent<RectTransform>().anchoredPosition = new Vector3(83f, -151f, 0f);

                GameObject.Find("Board").GetComponent<RectTransform>().anchoredPosition = new Vector3(58f, -108f, 0f);
                GameObject.Find("Board").GetComponent<RectTransform>().sizeDelta = new Vector3(0.75f, 0.75f, 0f);

                //scale?
                GameObject.Find("RecImage").GetComponent<RectTransform>().anchoredPosition = new Vector3(400f, -70f, 0f);
                GameObject.Find("RecImage").GetComponent<RectTransform>().sizeDelta = new Vector2(190f, 100f);

                GameObject.Find("Money").GetComponent<RectTransform>().anchoredPosition = new Vector3(672f, 194f, 0f);
            }
            else if (GameObject.Find("OptionsButton") != null)
            {
                //get 'back' menu button
                theGameMaster.menuitems[3].GetComponent<RectTransform>().anchoredPosition = new Vector3(-455f, 386f, 0);
                theGameMaster.menuitems[3].GetComponent<RectTransform>().localScale = new Vector3(0.9f, 0.9f, 0);

                theGameMaster.menuitems[1].GetComponent<RectTransform>().anchoredPosition = new Vector3(-125f, -447f, 0);

            }
            else if (GameObject.Find("ContinueButton") != null)
            {
                GameObject.Find("ContinueButton").GetComponent<RectTransform>().localScale = new Vector3(3.5f, 3.5f, 0);
                GameObject.Find("DecText").GetComponent<RectTransform>().localScale = new Vector3(0.4f, 0.4f, 0);
            }
            //quit button?

        }
        else if (Camera.main.aspect < 1.48f)//1920/1300
        {
            Debug.Log("<1.48");
            Camera.main.orthographicSize = 6.0f;

            if (isgamescene)
            {
                GameObject.Find("recipe board").GetComponent<RectTransform>().anchoredPosition = new Vector3(92f, -114f, 0f);

                GameObject.Find("Board").GetComponent<RectTransform>().anchoredPosition = new Vector3(60f, -120f, 0f);
                GameObject.Find("Board").GetComponent<RectTransform>().sizeDelta = new Vector3(444f, 268f, 0f);

                GameObject.Find("RecImage").GetComponent<RectTransform>().anchoredPosition = new Vector3(442f, -70f, 0f);
                GameObject.Find("RecImage").GetComponent<RectTransform>().sizeDelta = new Vector2(225f, 105f);

                GameObject.Find("Money").GetComponent<RectTransform>().anchoredPosition = new Vector3(760f, 212f, 0f);
            }
            else if (GameObject.Find("OptionsButton") != null)
            {
                theGameMaster.menuitems[3].GetComponent<RectTransform>().anchoredPosition = new Vector3(-505f, 386f, 0);
                //options button
                theGameMaster.menuitems[1].GetComponent<RectTransform>().anchoredPosition = new Vector3(-150f, -447f, 0);

            }
            else if (GameObject.Find("ContinueButton") != null)
            {
                GameObject.Find("ContinueButton").GetComponent<RectTransform>().localScale = new Vector3(3.5f, 3.5f, 0);
                GameObject.Find("DecText").GetComponent<RectTransform>().localScale = new Vector3(0.4f, 0.4f, 0);
            }
        }
        else if (Camera.main.aspect < 1.67f)
        {
            Debug.Log("16:10");
            //Debug.Log("800x480");

            if (isgamescene)
            {
                //GameObject.Find("RecImage").GetComponent<RectTransform>().anchoredPosition = new Vector3(483f, -47f, 0f);
                GameObject.Find("RecImage").GetComponent<RectTransform>().anchoredPosition = new Vector3(460f, -47f, 0f);

                GameObject.Find("Money").GetComponent<RectTransform>().anchoredPosition = new Vector3(870f, 260f, 0f);
            }
            else if (GameObject.Find("OptionsButton") != null)
            {
                theGameMaster.menuitems[3].GetComponent<RectTransform>().anchoredPosition = new Vector3(-550f, 386f, 0);
                //options button
                theGameMaster.menuitems[1].GetComponent<RectTransform>().anchoredPosition = new Vector3(-180f, -447f, 0);

            }
        }
        else if (Camera.main.aspect < 1.78f)//1920/1080
        { 
            Debug.Log("16:9");

            if (isgamescene)
            {
                GameObject.Find("Money").GetComponent<RectTransform>().anchoredPosition = new Vector3(910f, 260f, 0f);
                GameObject.Find("RecImage").GetComponent<RectTransform>().anchoredPosition = new Vector3(517f, -47f, 0f);
            }
        }
        else if (Camera.main.aspect == 2f)//2160/1080
        {
            Debug.Log("2:1");
            //Camera.main.orthographicSize = 4.5f;
            if (isgamescene)
            {
                Debug.Log("2:1");
                GameObject.Find("BackgroundObjects").GetComponent<Transform>().localScale = new Vector3(1.1f, 1f, 1f);

                GameObject.Find("recipe board").GetComponent<RectTransform>().anchoredPosition = new Vector3(180f, -88f, 0f);

                //GameObject.Find("Board").GetComponent<Image>().preserveAspect = false;
                //GameObject.Find("Board").GetComponent<RectTransform>().sizeDelta = new Vector2(1.2f, 1.2f);//
                GameObject.Find("Board").GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.0f, 0);
                GameObject.Find("RecImage").GetComponent<RectTransform>().anchoredPosition = new Vector3(565f, -47f, 0f);

                GameObject.Find("Money").GetComponent<RectTransform>().anchoredPosition = new Vector3(1000f, 260f, 0f);

                GameObject.Find("SpawningPoint").GetComponent<Transform>().position = new Vector3(-12f, -3f, 0f);

            }
            else if(GameObject.Find("OptionsButton")!=null)
            {
                //get 'back' menu button
                theGameMaster.menuitems[3].GetComponent<RectTransform>().anchoredPosition = new Vector3(-680f, 386f, 0);
                //options 
                theGameMaster.menuitems[1].GetComponent<RectTransform>().anchoredPosition = new Vector3(-270f, -447f, 0);
                //info
                theGameMaster.menuitems[8].GetComponent<RectTransform>().anchoredPosition = new Vector3(280f, -447f, 0);
            }
        }
        else if (Camera.main.aspect < 2.06f)//2960/1440
        { 
            Debug.Log("<2.06f");
            Camera.main.orthographicSize = 4.4f;
            if (isgamescene)
            {
                GameObject.Find("recipe board").GetComponent<RectTransform>().anchoredPosition = new Vector3(180f, -90f, 0f);

                GameObject.Find("Board").GetComponent<RectTransform>().anchoredPosition = new Vector3(60f, -97f, 0f);
                GameObject.Find("Board").GetComponent<RectTransform>().sizeDelta = new Vector2(1.1f, 1.1f);

                GameObject.Find("RecImage").GetComponent<RectTransform>().anchoredPosition = new Vector3(579f, -13f, 0f);

                GameObject.Find("Money").GetComponent<RectTransform>().anchoredPosition = new Vector3(1022f, 290f, 0f);
            }
            else if (GameObject.Find("OptionsButton") != null)
            {
                theGameMaster.menuitems[3].GetComponent<RectTransform>().anchoredPosition = new Vector3(-700f, 386f, 0);
                //info
                theGameMaster.menuitems[8].GetComponent<RectTransform>().anchoredPosition +=new Vector2(20f, 0);
                //options
                theGameMaster.menuitems[1].GetComponent<RectTransform>().anchoredPosition +=new Vector2(-30f, 0);
            }
        }
        //Debug.Log("CurrentRatio is: " + Camera.main.aspect);
        //Camera.main.orthographicSize += Camera.main.aspect/2;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void cameracolourdark()
    {
        GetComponent<Camera>().backgroundColor = new Vector4(0, 0, 0, 0);
    }
    public void cameracolourlight()
    {
        GetComponent<Camera>().backgroundColor = new Vector4(255, 255, 255, 0);
    }
}
