using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreadmillScript : MonoBehaviour
{
    float scrollspeed = 2f;
    Vector2 startpos;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {

        float newpos = Mathf.Repeat(Time.time * scrollspeed, 20);
        transform.position = startpos + Vector2.right * newpos;
        
    }
}
