using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

/*MoneyScript is mainly called for displaying the amount of money on the screen (on top of the cash register of the game)*/
/*currently, money can only be added and only from the IngredientScript*/
public class MoneyScript : MonoBehaviour
{
    public Text moneytext;

    public float money = 0;

    public static MoneyScript Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        money = GameObject.Find("GameMaster").GetComponent<GameMasterScript>().money;
        moneytext = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //moneytext.text = "Money: "+money+" €";
        moneytext.text =money+" ";

        GameObject.Find("GameMaster").GetComponent<GameMasterScript>().money = money;
    }
}
