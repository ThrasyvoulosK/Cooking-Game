using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //find the camera's aspect
        // and change its aspect accordingly
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
        }
        else if (Camera.main.aspect < 1.57f)
        {
            Debug.Log("16:10");
        }
        else if (Camera.main.aspect < 1.78f)//1920/1080
        { 
            Debug.Log("16:9"); 
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
