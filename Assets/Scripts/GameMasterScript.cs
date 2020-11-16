using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using System.Xml;
using UnityEngine.SceneManagement;

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

    public List<string> words_en_base = new List<string>() { "Cheese", "Ham", "Lettuce", "Tomato", "Coffee", "Ice", "Milk","Let's Cook!","Language","Levels" };

    public List<string> words_gr = new List<string>() { "ΤΥΡΙ","ΖΑΜΠΟΝ","ΜΑΡΟΥΛΙ","ΝΤΟΜΑΤΑ","ΚΑΦΕΣ","ΠΑΓΟΣ","ΓΑΛΑ","Ας Μαγειρέψουμε","Γλώσσα","Επίπεδα" };
    public List<string> words_en = new List<string>() { "CHEESE","HAM","LETTUCE","TOMATO","COFFEE","ICE","MILK", "Let's Cook!", "Language", "Levels" };

    [SerializeField]
    public  List<string> words_current;
    public Dictionary<string, string> languagehandler = new Dictionary<string, string>();

    //transactions
    //assign current sprites on screen gameobjects
    public Dictionary<string, Sprite> boughtables = new Dictionary<string, Sprite>();
    [SerializeField]
    private Sprite[] boughtablesprites;
    [SerializeField]
    private string[] boughtablesnames;

    public FurnaceScript theFurnace;

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

        /*
        if (language_current == "English")
        {
            Debug.Log("yes, language is english!");
            GameObject speechbubble = null;
            if ((speechbubble = GameObject.Find("SpeechBubble")) != null)
                speechbubble.GetComponentInChildren<TextMeshPro>().text = "Thank You!";
        }
        else
            Debug.Log("current language is: " + language_current);
        */

        DontDestroyOnLoad(this);//
    }
    // Start is called before the first frame update
    void Start()
    {
        InitialiseDictionary();

        InitialiseBoughtables();

        //initialise English as the default language
        //the player can change language from the main menu
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

        GameObject cnt = GameObject.Find("Counter");
        if(cnt!=null)
        {
            cnt.GetComponent<SpriteRenderer>().sprite = boughtables["Counter"];
            //tents and walls are on the same scene as the counter (so we can change them)
            GameObject.Find("Wall").GetComponent<SpriteRenderer>().sprite = boughtables["Wall"];
            GameObject.Find("Tent").GetComponent<SpriteRenderer>().sprite = boughtables["Tent"];
        }

        //cheat mode!
        
        /*theFurnace = null;
        if(GameObject.Find("Furnace")!=null)
        {
            theFurnace = GameObject.Find("Furnace").GetComponent<FurnaceScript>();
            theFurnace.numberofrecipesinlevel = 1;
        }*/
        

        if (Input.GetKeyDown("s"))
            SaveGame();
        if (Input.GetKeyDown("l"))
            LoadGame();

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

    }
    private void InitialiseLanguage()
    {
        //words_current = words_en;//
        //words_current = words_gr;//
        languagehandler.Clear();
        for (int i = 0; i < words_en_base.Count; i++)
        {
            languagehandler.Add(words_en_base[i], words_current[i]);
            //Debug.Log("added to dictionary: " + words_en_base[i]);
        }
        //Debug.Log("sizeof base: " + words_en_base.Count + " Sizeof curr: " + words_current.Count);
        //Debug.Log(languagehandler["LetsCook!"]);
        //Debug.Log(languagehandler["Cheese"]);
        //Change menu items as well
        GameObject[] menuitems = new GameObject[3];
        menuitems[0] = GameObject.Find("TitleText");
        menuitems[1] = GameObject.Find("LanguagesButton");
        menuitems[2] = GameObject.Find("LevelsButton");
        foreach (GameObject men in menuitems)
        {
            if (men == null)
                break;
            else
            {
                menuitems[0].GetComponentInChildren<Text>().text = languagehandler["Let's Cook!"];
                menuitems[1].GetComponentInChildren<Text>().text = languagehandler["Language"];
                menuitems[2].GetComponentInChildren<Text>().text = languagehandler["Levels"];
            }
        }
    }

    private void InitialiseBoughtables()
    {
        for (int i = 0; i < boughtablesprites.Length; i++)
        {
            boughtables.Add(boughtablesnames[i], boughtablesprites[i]);
        }
    }

    public void SaveGame()
    {
        SaveXML();
    }

    

    private void SaveXML()
    {
        Save save = new Save();

        save.scenenum = SceneManager.GetActiveScene().buildIndex;
        save.money = (int)GameObject.Find("Money").GetComponent<MoneyScript>().money;
        //save.completedrecipes = theFurnace.numberofcompletedrecipes;
        save.completedrecipes = GameObject.Find("Furnace").GetComponent<FurnaceScript>().numberofcompletedrecipes;

        XmlDocument xmlDocument = new XmlDocument();

        XmlElement root = xmlDocument.CreateElement("Save");

        XmlElement xmoney = xmlDocument.CreateElement("Money");
        xmoney.InnerText = save.money.ToString();
        root.AppendChild(xmoney);

        XmlElement xscene = xmlDocument.CreateElement("Scene");
        xscene.InnerText = save.scenenum.ToString();
        root.AppendChild(xscene);

        XmlElement xrec = xmlDocument.CreateElement("CompletedRecipes");
        xrec.InnerText = save.completedrecipes.ToString();
        root.AppendChild(xrec);

        xmlDocument.AppendChild(root);

        xmlDocument.Save(Application.dataPath + "/SavedGames/save1.txt");//
    }

    public void LoadGame()
    {
        LoadXML();
    }

    private void LoadXML()
    {
        Save save = new Save();
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(Application.dataPath + "/SavedGames/save1.txt");//

        XmlNodeList xmoney = xmlDocument.GetElementsByTagName("Money");
        save.money = int.Parse(xmoney[0].InnerText);
        XmlNodeList xscene = xmlDocument.GetElementsByTagName("Scene");
        save.scenenum = int.Parse(xscene[0].InnerText);
        XmlNodeList xrec = xmlDocument.GetElementsByTagName("CompletedRecipes");
        save.completedrecipes = int.Parse(xrec[0].InnerText);

        //SceneManager.LoadScene(save.scenenum);//
        GameObject.Find("Money").GetComponent<MoneyScript>().money = (float)save.money;
        //theFurnace.numberofcompletedrecipes=save.completedrecipes  ;
        GameObject.Find("Furnace").GetComponent<FurnaceScript>().numberofcompletedrecipes=save.completedrecipes ;

    }
}

public class Save
{
    public int scenenum;
    public int money;
    public int completedrecipes;
    //graphics?
}
