using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class ProgressBarScript : MonoBehaviour
{
    public int currentfill;
    int maxfill;

    public Image mask;

    bool buttonsActive = false;
    // Start is called before the first frame update
    void Start()
    {
        maxfill = GameObject.Find("Furnace").GetComponent<FurnaceScript>().next_recipe.Count+1;
        currentfill = 0;

        setText();
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();

        if (gameObject.transform.Find("Button1").gameObject.activeInHierarchy == true && gameObject.transform.Find("Button2").gameObject.activeInHierarchy == true)
            buttonsActive = true;
        else
            buttonsActive = false;
    }

    void GetCurrentFill()
    {
        mask.fillAmount = (float)currentfill / (float)maxfill;
    }

    public void resetBarButtons()
    {
        if(buttonsActive)
        {
            gameObject.transform.Find("Button1").gameObject.SetActive(false);
            gameObject.transform.Find("Button2").gameObject.SetActive(false);
        }
        else
        {
            gameObject.transform.Find("Button1").gameObject.SetActive(true);
            gameObject.transform.Find("Button2").gameObject.SetActive(true);
        }

    }

    public void setText()
    {
        gameObject.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text =GameObject.Find("GameMaster").GetComponent<GameMasterScript>().levelid.ToString();
    }
}
