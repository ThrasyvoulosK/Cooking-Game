using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ProgressBarScript : MonoBehaviour
{
    public int currentfill;
    int maxfill;

    public Image mask;
    // Start is called before the first frame update
    void Start()
    {
        maxfill = GameObject.Find("Furnace").GetComponent<FurnaceScript>().next_recipe.Count -1;
        currentfill = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        mask.fillAmount = (float)currentfill / (float)maxfill;
    }
}
