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
        if (Camera.main.aspect < 1.26f)
        {
            Debug.Log("5:4");
        }
        else if (Camera.main.aspect < 1.34f)
        {
            Debug.Log("4:3");
        }
        else if (Camera.main.aspect < 1.48f)//1920/1300
        {
            Debug.Log("<1.48");
            //money.x=800
            GameObject.Find("Money").GetComponent<RectTransform>().anchoredPosition =new Vector3(800f,260f,0f);
            //recimage.x=426
            GameObject.Find("RecImage").GetComponent<RectTransform>().anchoredPosition = new Vector3(426f, -47f, 0f);
        }
        else if (Camera.main.aspect < 1.57f)
        {
            Debug.Log("16:10");
        }
        else if (Camera.main.aspect < 1.78f)//1920/1080
        { 
            Debug.Log("16:9");
            //recimage.x=517
            //Money.x=910
            GameObject.Find("Money").GetComponent<RectTransform>().anchoredPosition = new Vector3(910f, 260f, 0f);
            GameObject.Find("RecImage").GetComponent<RectTransform>().anchoredPosition = new Vector3(517f, -47f, 0f);
        }
        else if (Camera.main.aspect < 2.06f)//2960/1440
        { 
            Debug.Log("<2.06f"); 
        }
        Debug.Log("CurrentRatio is: " + Camera.main.aspect);
        //Camera.main.orthographicSize += Camera.main.aspect/2;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
