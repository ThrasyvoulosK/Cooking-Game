using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using System.Xml;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEditor;

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

        if ((Application.dataPath + "/Resources/scrnsht.png") != null)
        {
            Debug.Log("screenshot exists");
            GameObject.Find("Continue").GetComponent<Image>().preserveAspect = true;
            GameObject.Find("Continue").GetComponent<Image>().sprite = Resources.Load<Sprite>("scrnsht");
            
        }
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
        else if(language_current=="Greek")
        {
            GameObject decdesc = null;
            if ((decdesc = GameObject.Find("DecText")) != null)
                decdesc.GetComponent<Text>().text = "Συγχαρητήρια!\nΔιάλεξε νέα διακόσμηση για να συνεχίσεις!";
        }


        GameObject cnt = GameObject.Find("Counter");
        if(cnt!=null)
        {
            cnt.GetComponent<SpriteRenderer>().sprite = boughtables["Counter"];
            //tents and walls are on the same scene as the counter (so we can change them)
            GameObject.Find("Wall").GetComponent<SpriteRenderer>().sprite = boughtables["Wall"];
            GameObject.Find("Tent").GetComponent<SpriteRenderer>().sprite = boughtables["Tent"];
            //Debug.Log("boughtablesCounter: " + boughtables["Counter"].name);
        }

        //cheat mode!
        
        /*theFurnace = null;
        if(GameObject.Find("Furnace")!=null)
        {
            theFurnace = GameObject.Find("Furnace").GetComponent<FurnaceScript>();
            theFurnace.numberofrecipesinlevel = 1;
        }*/
        

        

        if (issavedgame)
        {
            Debug.Log("loaded saved game");
            //InitialiseDictionary();

            Save save = new Save();
            XmlDocument xmlDocument = new XmlDocument();
            //xmlDocument.Load(Application.dataPath + "/SavedGames/save1.txt");//
            xmlDocument.Load(Application.dataPath + "/Resources/save1.txt");//

            XmlNodeList xmoney = xmlDocument.GetElementsByTagName("Money");
            save.money = int.Parse(xmoney[0].InnerText);
            XmlNodeList xrec = xmlDocument.GetElementsByTagName("CompletedRecipes");
            save.completedrecipes = int.Parse(xrec[0].InnerText);

            XmlNodeList xcoun = xmlDocument.GetElementsByTagName("Counter");
            save.counter = xcoun[0].InnerText;
            //Debug.Log(xcoun[0].InnerText);
            XmlNodeList xtent = xmlDocument.GetElementsByTagName("Tent");
            save.tent = xtent[0].InnerText;
            XmlNodeList xwall = xmlDocument.GetElementsByTagName("Wall");
            save.wall = xwall[0].InnerText;

            GameObject.Find("Money").GetComponent<MoneyScript>().money = (float)save.money;
            GameObject.Find("Furnace").GetComponent<FurnaceScript>().numberofcompletedrecipes = save.completedrecipes;

            //GameObject.Find("Counter").GetComponent<SpriteRenderer>().sprite = spriteslayers[save.counter];
            GameObject.Find("Counter").GetComponent<SpriteRenderer>().sprite = spriteslayers[xcoun[0].InnerText];
            boughtables["Counter"] = spriteslayers[xcoun[0].InnerText];
            //Debug.Log("savecounter " + save.counter);
            //Debug.Log("savecountername " + spriteslayers[save.counter]);
            //GameObject.Find("Tent").GetComponent<SpriteRenderer>().sprite = spriteslayers[save.tent];
            GameObject.Find("Tent").GetComponent<SpriteRenderer>().sprite = spriteslayers[xtent[0].InnerText];
            boughtables["Tent"] = spriteslayers[xtent[0].InnerText];
            //GameObject.Find("Wall").GetComponent<SpriteRenderer>().sprite = spriteslayers[save.wall];
            GameObject.Find("Wall").GetComponent<SpriteRenderer>().sprite = spriteslayers[xwall[0].InnerText];
            boughtables["Wall"] = spriteslayers[xwall[0].InnerText];
            issavedgame = false;

        }

        if (Input.GetKeyDown("s"))
            SaveGame();
        if (Input.GetKeyDown("l"))
        {
            issavedgame = true;
            LoadGame();
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
        GameObject[] menuitems = new GameObject[4];
        menuitems[0] = GameObject.Find("TitleText");
        menuitems[1] = GameObject.Find("LanguagesButton");
        menuitems[2] = GameObject.Find("LevelsButton");
        menuitems[3] = GameObject.Find("Continue");
        foreach (GameObject men in menuitems)
        {
            if (men == null)
                break;
            else
            {
                menuitems[0].GetComponentInChildren<Text>().text = languagehandler["Let's Cook!"];
                menuitems[1].GetComponentInChildren<Text>().text = languagehandler["Language"];
                menuitems[2].GetComponentInChildren<Text>().text = languagehandler["Levels"];
                menuitems[3].GetComponentInChildren<Text>().text = languagehandler["Continue"];
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

        //take a screenshot
        //ScreenCapture.CaptureScreenshot(Application.dataPath + "/SavedGames/scrnsht.png");
        ScreenCapture.CaptureScreenshot(Application.dataPath + "/Resources/scrnsht.png");
        //refresh its metadata
        //AssetDatabase.ImportAsset("Assets/Resources/scrnsht.png");
        //AssetDatabase.ImportAsset("Assets/Resources/scrnsht.png.meta");
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

        //find the graphics currently in use
        foreach(string desc in spritesnames)
        {
            if (spriteslayers[desc] == boughtables["Counter"])
            {
                save.counter = desc;

                //Debug.Log("counter is "+boughtables["Counter"].name+" desc "+desc);
                XmlElement xcoun = xmlDocument.CreateElement("Counter");
                xcoun.InnerText = save.counter;
                root.AppendChild(xcoun);
            }
            if (spriteslayers[desc] == boughtables["Tent"])
            {
                save.tent = desc;

                //Debug.Log("counter is "+boughtables["Counter"].name+" desc "+desc);
                XmlElement xtent = xmlDocument.CreateElement("Tent");
                xtent.InnerText = save.tent;
                root.AppendChild(xtent);
            }
            if (spriteslayers[desc] == boughtables["Wall"])
            {
                save.wall = desc;

                //Debug.Log("counter is "+boughtables["Counter"].name+" desc "+desc);
                XmlElement xwall = xmlDocument.CreateElement("Wall");
                xwall.InnerText = save.wall;
                root.AppendChild(xwall);
            }

        }

        xmlDocument.AppendChild(root);

        //xmlDocument.Save(Application.dataPath + "/SavedGames/save1.txt");//
        //xmlDocument.Save(Application.persistentDataPath + "/SavedGames/save1.txt");//
        xmlDocument.Save(Application.dataPath + "/Resources/save1.txt");//
        //xmlDocument.Save(save1.txt);//
    }

    //this variable will be called whenever we load a scene
    // to check whether we should load its own specific values
    public static bool issavedgame = false;
    public void LoadGame()
    {
        LoadXML();
    }

    private void LoadXML()
    {
        Save save = new Save();
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(Application.dataPath + "/Resources/save1.txt");//

        XmlNodeList xmoney = xmlDocument.GetElementsByTagName("Money");
        save.money = int.Parse(xmoney[0].InnerText);
        XmlNodeList xscene = xmlDocument.GetElementsByTagName("Scene");
        save.scenenum = int.Parse(xscene[0].InnerText);
        XmlNodeList xrec = xmlDocument.GetElementsByTagName("CompletedRecipes");
        save.completedrecipes = int.Parse(xrec[0].InnerText);

        XmlNodeList xcoun = xmlDocument.GetElementsByTagName("Counter");
        //save.counter = (xcoun[0].InnerText).ToString();
        save.counter = xcoun[0].InnerText;
        XmlNodeList xtent = xmlDocument.GetElementsByTagName("Tent");
        //save.tent = (xtent[0].InnerText).ToString();
        save.tent = xtent[0].InnerText;
        XmlNodeList xwall = xmlDocument.GetElementsByTagName("Wall");
        //save.wall = (xwall[0].InnerText).ToString();
        save.wall = xwall[0].InnerText;


        //GameObject.Find("Money").GetComponent<MoneyScript>().money = (float)save.money;
        //GameObject.Find("Furnace").GetComponent<FurnaceScript>().numberofcompletedrecipes=save.completedrecipes ;

        //DontDestroyOnLoad(GameObject.Find("Money"));
        //DontDestroyOnLoad(GameObject.Find("Furnace"));

        issavedgame = true;
        

        SceneManager.LoadScene(save.scenenum);//
    }
}

public class Save
{
    public int scenenum;
    public int money;
    public int completedrecipes;
    //graphics?
    public string counter;
    public string tent;
    public string wall;
}
