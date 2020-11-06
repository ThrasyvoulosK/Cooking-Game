using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        //find the camera's aspect
        // and change its aspect accordingly
        //GameObject.Find("Canvas").GetComponent<Canvas_Scaler>().scaleFactor = 1;
        GameObject.Find("Canvas").GetComponent<CanvasScaler>().matchWidthOrHeight = 1f;
        if (Camera.main.aspect < 1.26f)//1280/1024
        {
            Debug.Log("5:4");
            //size=7.1
            Camera.main.orthographicSize = 7.1f;
            GameObject.Find("recipe board").GetComponent<RectTransform>().anchoredPosition = new Vector3(69f,-212f, 0f);
            //board.xy=(62,-82)
            //width=487
            //height=232
            GameObject.Find("Board").GetComponent<RectTransform>().anchoredPosition = new Vector3(62f, -82f, 0f);
            GameObject.Find("Board").GetComponent<RectTransform>().sizeDelta = new Vector3(487f, 232f, 0f);
            //recimage.xy=(382,-40)
            //width,height=150,125
            GameObject.Find("RecImage").GetComponent<RectTransform>().anchoredPosition = new Vector3(382f, -40f, 0f);
            GameObject.Find("RecImage").GetComponent<RectTransform>().sizeDelta = new Vector2(150f, 95f);
            //money.xy=(635,185)
            GameObject.Find("Money").GetComponent<RectTransform>().anchoredPosition = new Vector3(635f, 185f, 0f);

        }
        else if (Camera.main.aspect < 1.34f)//640/480
        {
            Debug.Log("4:3");
            //size=6.7
            Camera.main.orthographicSize = 6.7f;
            //recipeboard.xy=(83,-151)
            GameObject.Find("recipe board").GetComponent<RectTransform>().anchoredPosition = new Vector3(83f, -151f, 0f);
            //board.xy=(58,-108)
            GameObject.Find("Board").GetComponent<RectTransform>().anchoredPosition = new Vector3(58f, -108f, 0f);
            GameObject.Find("Board").GetComponent<RectTransform>().sizeDelta = new Vector3(0.75f, 0.75f, 0f);
            //recimage.xy=(400,-65)
            //scale?
            GameObject.Find("RecImage").GetComponent<RectTransform>().anchoredPosition = new Vector3(400f, -70f, 0f);
            GameObject.Find("RecImage").GetComponent<RectTransform>().sizeDelta = new Vector2(190f, 100f);
            //money.xy=(672,194)
            GameObject.Find("Money").GetComponent<RectTransform>().anchoredPosition = new Vector3(672f, 194f, 0f);

            //quit button?

        }
        else if (Camera.main.aspect < 1.48f)//1920/1300
        {
            Debug.Log("<1.48");
            Camera.main.orthographicSize = 6.0f;

            GameObject.Find("recipe board").GetComponent<RectTransform>().anchoredPosition = new Vector3(92f, -114f, 0f);

            GameObject.Find("Board").GetComponent<RectTransform>().anchoredPosition = new Vector3(60f, -120f, 0f);
            GameObject.Find("Board").GetComponent<RectTransform>().sizeDelta = new Vector3(444f, 268f, 0f);

            GameObject.Find("RecImage").GetComponent<RectTransform>().anchoredPosition = new Vector3(442f, -70f, 0f);
            GameObject.Find("RecImage").GetComponent<RectTransform>().sizeDelta = new Vector2(225f, 105f);

            GameObject.Find("Money").GetComponent<RectTransform>().anchoredPosition =new Vector3(760f,212f,0f);
        }
        else if (Camera.main.aspect < 1.57f)
        {
            Debug.Log("800x480");
            //GameObject.Find("RecImage").GetComponent<RectTransform>().anchoredPosition = new Vector3(483f, -47f, 0f);
        }
        else if (Camera.main.aspect < 1.67f)
        {
            Debug.Log("16:10");
            Debug.Log("800x480");
            GameObject.Find("RecImage").GetComponent<RectTransform>().anchoredPosition = new Vector3(483f, -47f, 0f);
        }
        else if (Camera.main.aspect < 1.78f)//1920/1080
        { 
            Debug.Log("16:9");
            //recimage.x=517
            //Money.x=910
            GameObject.Find("Money").GetComponent<RectTransform>().anchoredPosition = new Vector3(910f, 260f, 0f);
            GameObject.Find("RecImage").GetComponent<RectTransform>().anchoredPosition = new Vector3(517f, -47f, 0f);
        }
        else if (Camera.main.aspect == 2f)//2160/1080
        {
            Debug.Log("2:1");
            //recipeboard.x=254
            GameObject.Find("recipe board").GetComponent<RectTransform>().anchoredPosition = new Vector3(254f, -88f, 0f);
            //size=4.5
            Camera.main.orthographicSize = 4.5f;
            //recipeboard.x =171
            GameObject.Find("recipe board").GetComponent<RectTransform>().anchoredPosition = new Vector3(171f, -88f, 0f);
            //board.y=-90
            GameObject.Find("Board").GetComponent<RectTransform>().anchoredPosition = new Vector3(60f, -90f, 0f);
            //board.height=340
            GameObject.Find("Board").GetComponent<RectTransform>().sizeDelta = new Vector2(1.1f,1.1f);
            //recimage.x=579
            //recimage.y=-15
            GameObject.Find("RecImage").GetComponent<RectTransform>().anchoredPosition = new Vector3(579f, -15f, 0f);
            //money.x=1000
            //money.y=285
            GameObject.Find("Money").GetComponent<RectTransform>().anchoredPosition = new Vector3(1000f, 285f, 0f);
        }
        else if (Camera.main.aspect < 2.06f)//2960/1440
        { 
            Debug.Log("<2.06f");
            //size=4.4
            Camera.main.orthographicSize = 4.4f;
            //recipeboard.xy=(180,-90)
            GameObject.Find("recipe board").GetComponent<RectTransform>().anchoredPosition = new Vector3(180f, -90f, 0f);
            //board.xy=(60,-97)
            GameObject.Find("Board").GetComponent<RectTransform>().anchoredPosition = new Vector3(60f, -97f, 0f);
            //board.height=350
            GameObject.Find("Board").GetComponent<RectTransform>().sizeDelta = new Vector2(1.1f, 1.1f);
            //recimage.xy=(578,-13)
            GameObject.Find("RecImage").GetComponent<RectTransform>().anchoredPosition = new Vector3(579f, -13f, 0f);
            //money.xy=(1022,290)
            GameObject.Find("Money").GetComponent<RectTransform>().anchoredPosition = new Vector3(1022f, 290f, 0f);
        }
        Debug.Log("CurrentRatio is: " + Camera.main.aspect);
        //Camera.main.orthographicSize += Camera.main.aspect/2;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
