using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*Initialise a sprite-string dictionary that stores the game's graphics*/
/*This dictionary is defined inside the editor*/
public class GameMasterScript : MonoBehaviour
{
    public Dictionary<string, Sprite> spriteslayers = new Dictionary<string, Sprite>();
    [SerializeField]
    private Sprite[] sprites;
    [SerializeField]
    private string[] spritesnames;

    public static GameMasterScript Instance;

    public List<string> languages = new List<string>() { "Greek", "English" };
    [SerializeField]
    public  string language_current;

    public List<string> words_en_base = new List<string>() { "Cheese", "Ham", "Lettuce", "Tomato", "Coffee", "Ice", "Milk" };

    public List<string> words_gr = new List<string>() { "ΤΥΡΙ","ΖΑΜΠΟΝ","ΜΑΡΟΥΛΙ","ΝΤΟΜΑΤΑ","ΚΑΦΕΣ","ΠΑΓΟΣ","ΓΑΛΑ" };
    public List<string> words_en = new List<string>() { "CHEESE","HAM","LETTUCE","TOMATO","COFFEE","ICE","MILK" };

    [SerializeField]
    public  List<string> words_current;
    public Dictionary<string, string> languagehandler = new Dictionary<string, string>();

    void Awake()
    {
        Debug.Log("current language is: " + language_current);
        //Instance = this;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (language_current == "English")
        {
            Debug.Log("yes, language is english!");
            GameObject speechbubble = null;
            if ((speechbubble = GameObject.Find("SpeechBubble")) != null)
                speechbubble.GetComponentInChildren<TextMeshPro>().text = "Thank You!";
        }
        else
            Debug.Log("current language is: " + language_current);

        DontDestroyOnLoad(this);//
    }
    // Start is called before the first frame update
    void Start()
    {
        InitialiseDictionary();

        //
        changelanguage_en();//
    }

    // Update is called once per frame
    void Update()
    {
        if (language_current == "English")
        {
            GameObject speechbubble = null;
            if ((speechbubble = GameObject.Find("SpeechBubble")) != null)
                speechbubble.GetComponentInChildren<TextMeshPro>().text = "Thank You!";
        }
    }
    private void InitialiseDictionary()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            spriteslayers.Add(spritesnames[i], sprites[i]);
        }
    }

    public void changelanguage_en()
    {
        language_current = "English";
        Debug.Log("current language is: " + language_current);
        words_current = words_en;
        InitialiseLanguage();

        GameObject speechbubble = null;
        if ((speechbubble = GameObject.Find("SpeechBubble")) != null)
            //speechbubble.GetComponentInChildren<TextMeshPro>().text = "Thank You!";
            speechbubble.GetComponentInChildren<TextMeshPro>().SetText("Thank You!");// = "Thank You!";

    }

    public void changelanguage_gr()
    {
        language_current = "Greek";
        words_current = words_gr;
        InitialiseLanguage();

        GameObject[] menuitems = new GameObject[3];
        menuitems[0] = GameObject.Find("Text");
        menuitems[1] = GameObject.Find("LanguagesButton");
        menuitems[2] = GameObject.Find("LevelsButton");
        foreach (GameObject men in menuitems)
        {
            if (men == null)
                break;
            else
            {
                menuitems[0].GetComponentInChildren<Text>().text = "Ας Μαγειρέψουμε!";
                menuitems[1].GetComponentInChildren<Text>().text = "Γλώσσες";
                menuitems[2].GetComponentInChildren<Text>().text = "Επίπεδα";
            }
        }

    }
    private void InitialiseLanguage()
    {
        //words_current = words_en;//
        //words_current = words_gr;//
        languagehandler.Clear();
        for (int i = 0; i < words_en_base.Count; i++)
        {
            languagehandler.Add(words_en_base[i], words_current[i]);
        }
    }
}
