using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*SpawningFoodLayerScript is responsible for creating a new layered-recipe (in the middle of the screen) when allowed to do so*/
public class SpawningFoodLayerScript : MonoBehaviour
{

    public bool spawnfoodlayerallowed = false;

    public GameObject foodlayer;

    public float timetodestroy=3f;
    public bool timeron=false;

    public Transform spawnpoint;
    // Start is called before the first frame update
    void Start()
    {
        spawnfoodlayerallowed = true;        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void spawnfoodlayer()
    {
        if (spawnfoodlayerallowed)
            Instantiate(foodlayer, spawnpoint.position,Quaternion.identity);

        spawnfoodlayerallowed = false;
        //Debug.Log("spawnfoodlayer");
    }   
}
