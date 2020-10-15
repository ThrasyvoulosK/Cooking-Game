using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

using UnityEngine.UI;

public class MoneyScript : MonoBehaviour
{
    public Text moneytext;

    public float money = 0;
    // Start is called before the first frame update
    void Start()
    {
        moneytext = GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        moneytext.text = "Money: "+money+" $";

        
    }
}
