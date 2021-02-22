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
/*This script also handles language dictionaries and game options*/
/*Includes Save/Load functionality*/
/*Also includes cheat functions, to assist with debbuging*/
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
    public string language_current;

    public List<string> words_en_base = new List<string>() { "Cheese", "Ham", "Lettuce", "Tomato", "Coffee", "Ice", "Milk", "Let's Cook!", "Language", "Levels" };

    public List<string> words_gr = new List<string>() { "ΤΥΡΙ", "ΖΑΜΠΟΝ", "ΜΑΡΟΥΛΙ", "ΝΤΟΜΑΤΑ", "ΚΑΦΕΣ", "ΠΑΓΟΣ", "ΓΑΛΑ", "Ας Μαγειρέψουμε", "Γλώσσα", "Επίπεδα" };
    public List<string> words_en = new List<string>() { "CHEESE", "HAM", "LETTUCE", "TOMATO", "COFFEE", "ICE", "MILK", "Let's Cook!", "Language", "Levels" };

    [SerializeField]
    public List<string> words_current;
    public Dictionary<string, string> languagehandler = new Dictionary<string, string>();
    //keep the following stored in case we want to change language more than once
    public Dictionary<string, string> languagehandlerprev = new Dictionary<string, string>();

    //transactions
    //assign current sprites on screen gameobjects
    public Dictionary<string, Sprite> boughtables = new Dictionary<string, Sprite>();
    [SerializeField]
    private Sprite[] boughtablesprites;
    [SerializeField]
    private string[] boughtablesnames;

    public FurnaceScript theFurnace;

    public float money = 0;

    //keep the selected level number saved
    public int levelid = 1;
    //this variable keeps track of whether we changed the level in the options
    // so that the functionality of 'continue' may change
    public bool levelchanged = false;

    //select character options
    public string option_character = "Character_M";
    //select sound options
    public bool option_sound = true;
    //select music options
    public bool option_music = true;

    //menu items that are disabled at various times can be accessed with this
    public GameObject[] menuitems;

    //pre-configured menu items to be loaded from this array
    //public GameObject[] loaded_menu_items;

    public MenuSettings_SO menuSettings;

    /*public UnityEngine.Video.VideoPlayer videoPlayer;
    public UnityEngine.Video.VideoClip vc = Resources.Load<UnityEngine.Video.VideoClip>("Canteen Intro Shotcut English");*/

    void Awake()
    {
        //Debug.Log("current language is: " + language_current);

        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        //LoadMenuXML();

        //do not delete GameMaster
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        InitialiseDictionary();

        InitialiseBoughtables();

        changelanguage_en();
        //set_menu(menuSettings);

        /*if (menuSettings.character != null)
        {
            Debug.Log("menusettings not null");
            get_menu(menuSettings);
            //initialise the default language
            //the player can change language from the main menu
            if (menuSettings.language == "English")
                changelanguage_en();
            else if (menuSettings.language == "Greek")
                changelanguage_gr();
        }
        else
            Debug.Log("menusettings prefab is null");
        */

        //load the menuitems data
        LoadMenuXML();
        //load individual images and data as well
        if (language_current == "Greek")
        { 
            changelanguage_gr();
            menuitems[4].transform.GetChild(1).GetComponent<Image>().sprite = spriteslayers["Language_El"];
            UnityEngine.Video.VideoPlayer videoPlayer;
             //vc = Resources.Load<UnityEngine.Video.VideoClip>("Canteen Intro Shotcut");
        }
        menuitems[6].transform.GetChild(1).GetComponent<Image>().sprite = spriteslayers[option_character];

        //write a localisation csv
        writetocsv();

    }

    // Update is called once per frame
    void Update()
    {
        //menuSettings

        //load correct video
        /*if (SceneManager.GetActiveScene().buildIndex  == 1)
        
            GameObject cnv = GameObject.Find("Canvas");

            UnityEngine.Video.VideoPlayer videoPlayer;
            UnityEngine.Video.VideoClip vc= Resources.Load<UnityEngine.Video.VideoClip>("Canteen Intro Shotcut English");
            videoPlayer = cnv.GetComponentInChildren<UnityEngine.Video.VideoPlayer>();
            videoPlayer.clip = vc;
            Debug.Log("video ok?");*/
            /*
            if (GameObject.Find("Canvas") != null)
            {
                if (cnv.GetComponentInChildren<UnityEngine.Video.VideoClip>() != null)
                    Debug.Log("vidoe exists");
                else
                    Debug.Log("no video");
                UnityEngine.Video.VideoClip vid = cnv.GetComponentInChildren<UnityEngine.Video.VideoClip>();
                if (language_current == "English")
                    vid = Resources.LoadAll<UnityEngine.Video.VideoClip>("Canteen Intro Shotcut English")[0]; 
                else if (language_current == "Greek")
                    vid = Resources.LoadAll<UnityEngine.Video.VideoClip>("Canteen Intro Shotcut")[0];
            }
            */
            
        

        //handle speechbubble text here
        //wip
        if (language_current == "English")
        {
            GameObject speechbubble = null;
            if ((speechbubble = GameObject.Find("SpeechBubble")) != null)
                speechbubble.GetComponentInChildren<TextMeshPro>().text = "THANK YOU!";
            //speechbubble.GetComponentInChildren<TextMeshPro>().text = "Thank You!";
        }
        else if (language_current == "Greek")
        {
            GameObject speechbubble = null;
            if ((speechbubble = GameObject.Find("SpeechBubble")) != null)
                speechbubble.GetComponentInChildren<TextMeshPro>().text = "ΕΥΧΑΡΙΣΤΩ!";
        }

        DecDescriptionText();

        //assign sprites to the ones we bought
        boughtables_assigner();

        //cheat mode!
        //cheat_one_recipe_only();
        //cheat_only_ingredient_recipes("Club");
        //cheat_one_ingredient_recipe = true;

        LoadCheck();

        SaveOrLoadKeys();
        SaveOrLoadKeysMenu();//

        if (SceneManager.GetActiveScene().buildIndex == 0)
        { 
            set_menu(menuSettings);
            //UnityEditor.EditorUtility.SetDirty(menuSettings);
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
        if (languagehandler.Count>0)
        {
            Debug.Log("languagehandelernot null" + languagehandler.Count);
            for (int i = 0; i < words_en_base.Count; i++)
            {
                foreach (GameObject men in menuitems)
                    if (languagehandler[words_en_base[i]] == men.GetComponentInChildren<Text>().text)
                        men.GetComponentInChildren<Text>().text = words_en_base[i];
            }
        }

        language_current = "English";
        Debug.Log("current language is: " + language_current);
        words_current = words_en;
        InitialiseLanguage();

        GameObject speechbubble = null;
        if ((speechbubble = GameObject.Find("SpeechBubble")) != null)
            //speechbubble.GetComponentInChildren<TextMeshPro>().text = "Thank You!";
            speechbubble.GetComponentInChildren<TextMeshPro>().SetText("THANK YOU!");
            //speechbubble.GetComponentInChildren<TextMeshPro>().SetText("Thank You!");
    }

    public void changelanguage_gr()
    {
        for (int i = 0; i < words_en_base.Count; i++)
        {
            foreach (GameObject men in menuitems)
                if (languagehandler[words_en_base[i]] == men.GetComponentInChildren<Text>().text)
                    men.GetComponentInChildren<Text>().text = words_en_base[i];
        }

        language_current = "Greek";
        words_current = words_gr;
        InitialiseLanguage();
    }
    private void InitialiseLanguage()
    {
        languagehandler.Clear();
        for (int i = 0; i < words_en_base.Count; i++)
        {
            languagehandler.Add(words_en_base[i], words_current[i]);
            //Debug.Log("added to dictionary: " + words_en_base[i]+words_current[i]);
            /*Debug.Log(languagehandler[words_en_base[i]]);
            Debug.Log(languagehandler[words_current[i]]);*/
        }
        //Debug.Log("sizeof base: " + words_en_base.Count + " Sizeof curr: " + words_current.Count);
        foreach (GameObject men in menuitems)
        {
            //Debug.Log("menuitems " + men.GetComponentInChildren<Text>().text);
            men.GetComponentInChildren<Text>().text = languagehandler[men.GetComponentInChildren<Text>().text];
            //preserve image aspect in here as well
            if(men.GetComponentInChildren<Image>()!=null)
                men.GetComponentInChildren<Image>().preserveAspect=true;
        }
        
    }

    
    void menuitemhandler(Text menutext)
    {
        menutext.text = languagehandler[menutext.text];
    }

    private void InitialiseBoughtables()
    {
        for (int i = 0; i < boughtablesprites.Length; i++)
        {
            boughtables.Add(boughtablesnames[i], boughtablesprites[i]);
        }
    }

    public void change_character_m()
    {
        option_character = "Character_M";                    
    }
    public void change_character_f()
    {
        option_character = "Character_F";                    
    }
    public void change_sound()
    {
        if(option_sound==true)
        {
            option_sound = false;
            //change sprite accordingly
        }
        else
        {
            option_sound = true;
            //change sprite again
        }
    }
    public void change_music()
    {
        if (option_music == true)
        {
            option_music = false;
            //change sprite accordingly
        }
        else
        {
            option_music = true;
            //change sprite again
        }
    }

    //if we have bought new objects, change their sprites through this function
    void boughtables_assigner()
    {
        GameObject cnt = GameObject.Find("Counter");
        if (cnt != null)
        {
            cnt.GetComponent<SpriteRenderer>().sprite = boughtables["Counter"];
            //tents and walls are on the same scene as the counter (so we can change them)
            GameObject.Find("Wall").GetComponent<SpriteRenderer>().sprite = boughtables["Wall"];
            GameObject.Find("Tent").GetComponent<SpriteRenderer>().sprite = boughtables["Tent"];
            //Debug.Log("boughtablesCounter: " + boughtables["Counter"].name);
            GameObject.Find("PaperTowels").GetComponent<SpriteRenderer>().sprite = boughtables["Napkins"];

            GameObject.Find("Board").GetComponent<Image>().sprite = boughtables["Board"];
            //change board size to fit better, now that it's changed
            if (SceneManager.GetActiveScene().buildIndex > 9)
            {
                //Debug.Log("SceneManager.GetActiveScene().buildIndex>=9" + levelid);
                GameObject.Find("Board").GetComponent<RectTransform>().sizeDelta = new Vector2(660, 440);
                GameObject.Find("Board").GetComponent<RectTransform>().anchoredPosition = new Vector2(60, -160);

                //change our board to one of the new ones, if we have the original at this point
                if (GameObject.Find("Board").GetComponent<Image>().sprite == spriteslayers["Board_orig"])
                    GameObject.Find("Board").GetComponent<Image>().sprite = spriteslayers["Board_Beige"];

                //change napkins' position too, because they get hidden
                GameObject.Find("PaperTowels").GetComponent<Transform>().position = new Vector3(-6, -2, 0);
            }
            if (GameObject.Find("Table") != null)
            {
                GameObject.Find("Table").GetComponent<SpriteRenderer>().sprite = boughtables["Table"];
                if (GameObject.Find("TableDec") != null)
                    GameObject.Find("TableDec").GetComponent<SpriteRenderer>().sprite = boughtables["Table Decoration"];
            }
            if (GameObject.Find("Umbrella") != null)
                GameObject.Find("Umbrella").GetComponent<SpriteRenderer>().sprite = boughtables["Umbrella"];
            if (GameObject.Find("Logo") != null)
                GameObject.Find("Logo").GetComponent<SpriteRenderer>().sprite = boughtables["Logo"];
        }
    }

    //handle the descriptions of decoration screens here
    //note that this is implemented separately from the gamemaster's dictionary
    void DecDescriptionText()
    {
        GameObject decdesc = null;
        string boght;
        if ((decdesc = GameObject.Find("DecText")) != null)
        {
            boght = languagehandler[GameObject.Find("ObjectsToBuy").GetComponent<TransactionScript>().objecttobebought];
            if (GameObject.Find("ObjectsToBuy").GetComponent<TransactionScript>().is_colour == true)
            {
                /*if (language_current == "English")
                    decdesc.GetComponent<Text>().text = "Choose a new " + boght + " colour \nfor your canteen, to continue!";
                else if (language_current == "Greek")
                    decdesc.GetComponent<Text>().text = "Διάλεξε νέο χρώμα " + boght + "\nγια τη καντίνα σου, για να συνεχίσεις!";*/
                if (language_current == "English")
                    decdesc.GetComponent<Text>().text = "CHOOSE A NEW " + boght + " COLOUR \nFOR YOUR CANTEEN, TO CONTINUE!";
                else if (language_current == "Greek")
                    decdesc.GetComponent<Text>().text = "ΔΙΑΛΕΞΕ ΝΕΟ ΧΡΩΜΑ " + boght + "\nΓΙΑ ΤΗ ΚΑΝΤΙΝΑ ΣΟΥ, ΓΙΑ ΝΑ ΣΥΝΕΧΙΣΕΙΣ!";
            }
            else
            {
                /*if (language_current == "English")
                    decdesc.GetComponent<Text>().text = "Choose a new " + boght + "\nfor your canteen, to continue!";
                else if (language_current == "Greek")
                    decdesc.GetComponent<Text>().text = "Κάνε νέα επιλογή " + boght + "\nγια τη καντίνα σου, για να συνεχίσεις!";*/
                if (language_current == "English")
                    decdesc.GetComponent<Text>().text = "CHOOSE A NEW " + boght + "\nFOR YOUR CANTEEN, TO CONTINUE!";
                else if (language_current == "Greek")
                    decdesc.GetComponent<Text>().text = "ΚΑΝΕ ΝΕΑ ΕΠΙΛΟΓΗ " + boght + "\nΓΙΑ ΤΗ ΚΑΝΤΙΝΑ ΣΟΥ, ΓΙΑ ΝΑ ΣΥΝΕΧΙΣΕΙΣ!";
            }
        }
    }

    public void SaveGame()
    {
        //take a screenshot
        ScreenCapture.CaptureScreenshot(Application.persistentDataPath + "/scrnsht.png");

        //do not allow saving when we don't have the necessary objects
        GameObject frn = null;
        if ((frn = GameObject.Find("Furnace")) == null)
        {
            Debug.Log("save in this state isn't supported!");
            return;
        }
        //call save as xml file function
        SaveXML();        
    }    

    private void SaveXML()
    {
        Save save = new Save();

        GameObject frn = GameObject.Find("Furnace");

        save.scenenum = SceneManager.GetActiveScene().buildIndex;
        save.money = (int)GameObject.Find("Money").GetComponent<MoneyScript>().money;
        //save.completedrecipes = theFurnace.numberofcompletedrecipes;
        save.completedrecipes = frn.GetComponent<FurnaceScript>().numberofcompletedrecipes;

        save.currentrecipe = frn.GetComponent<FurnaceScript>().recipe.name;

        save.remainingrecipes.Clear();
        foreach(Recipe_SO rso in frn.GetComponent<FurnaceScript>().next_recipe)
        {
            save.remainingrecipes.Add(rso.name);
        }

        //foreach(Sprite sprite in GameObject.Find("Customer").GetComponent<CustomerScript>().customerspritelist)
        save.customer = 0;
        for(int i=0;i< GameObject.Find("Customer").GetComponent<CustomerScript>().customerspritelist.Count;i++)
            {
                if(GameObject.Find("Customer").GetComponent<CustomerScript>().customerspritelist[i]== GameObject.Find("Customer").GetComponent<SpriteRenderer>().sprite)
                {
                    //Debug.Log("getting sprite");
                    save.customer = i;
                }
            }
        //save.customer = GameObject.Find("Customer").GetComponent<CustomerScript>().customerandom;

        //create the xml document with the above values
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

        XmlElement xcurrec = xmlDocument.CreateElement("CurrentRecipe");
        xcurrec.InnerText = save.currentrecipe;
        root.AppendChild(xcurrec);

        
        XmlElement xremrec;
        for (int i = 0; i < save.remainingrecipes.Count; i++)
        {
            xremrec = xmlDocument.CreateElement("RemainingRecipes");
            xremrec.InnerText = save.remainingrecipes[i];
            root.AppendChild(xremrec);
        }

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
            else if (spriteslayers[desc] == boughtables["Tent"])
            {
                save.tent = desc;

                //Debug.Log("counter is "+boughtables["Counter"].name+" desc "+desc);
                XmlElement xtent = xmlDocument.CreateElement("Tent");
                xtent.InnerText = save.tent;
                root.AppendChild(xtent);
            }
            else if (spriteslayers[desc] == boughtables["Wall"])
            {
                save.wall = desc;

                //Debug.Log("counter is "+boughtables["Counter"].name+" desc "+desc);
                XmlElement xwall = xmlDocument.CreateElement("Wall");
                xwall.InnerText = save.wall;
                root.AppendChild(xwall);
            }
            else if (spriteslayers[desc] == boughtables["Napkins"])
            {
                save.napkins = desc;

                XmlElement xnap = xmlDocument.CreateElement("Napkins");
                xnap.InnerText = save.napkins;
                root.AppendChild(xnap);
            }
            else if (spriteslayers[desc] == boughtables["Board"])
            {
                save.board = desc;

                XmlElement xboard = xmlDocument.CreateElement("Board");
                xboard.InnerText = save.board;
                root.AppendChild(xboard);
            }
            else if (spriteslayers[desc] == boughtables["Table"])
            {
                save.table = desc;

                XmlElement xtab = xmlDocument.CreateElement("Table");
                xtab.InnerText = save.table;
                root.AppendChild(xtab);
            }
            else if (spriteslayers[desc] == boughtables["Table Decoration"])
            {
                save.table_dec = desc;

                XmlElement xtabled = xmlDocument.CreateElement("Table_Decoration");
                xtabled.InnerText = save.table_dec;
                root.AppendChild(xtabled);
            }
            else if (spriteslayers[desc] == boughtables["Umbrella"])
            {
                save.umbrella = desc;

                XmlElement xum = xmlDocument.CreateElement("Umbrella");
                xum.InnerText = save.umbrella;
                root.AppendChild(xum);
            }
            else if (spriteslayers[desc] == boughtables["Logo"])
            {
                save.logo = desc;

                XmlElement xlog = xmlDocument.CreateElement("Logo");
                xlog.InnerText = save.logo;
                root.AppendChild(xlog);
            }

        }
                
        XmlElement xcus = xmlDocument.CreateElement("Customer");
        xcus.InnerText = save.customer.ToString();
        root.AppendChild(xcus);

        xmlDocument.AppendChild(root);

        //xmlDocument.Save(Application.dataPath + "/Resources/save1.txt");//
        xmlDocument.Save(Application.persistentDataPath + "/save1.txt");//
    }

    //this variable will be called whenever we load a scene
    // to check whether we should load its own specific values
    public static bool issavedgame = false;
    public void LoadGame()
    {
        Debug.Log("current level_id: " + levelid);
        if (levelchanged == false)
        {
            string sav = Application.persistentDataPath + "/save1.txt";
            Debug.Log("persistent data path save: " + sav);
            if (System.IO.File.Exists(sav))
            {
                Debug.Log("loading saved file");
                LoadXML();
                Debug.Log("loading aborted");               
            }
            else
                Debug.Log("Save doesn't exist");
            //if a saved game dosn't exist, start from level1
            /*SceneManager.LoadScene(levelid);
            Debug.Log("starting from level 1");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);//
            */
        }
        else
            SceneManager.LoadScene(levelid);
    }

    private void LoadXML()
    {
        Save save = new Save();
        XmlDocument xmlDocument = new XmlDocument();

        xmlDocument.Load(Application.persistentDataPath + "/save1.txt");//

        XmlNodeList xmoney = xmlDocument.GetElementsByTagName("Money");
        save.money = int.Parse(xmoney[0].InnerText);
        XmlNodeList xscene = xmlDocument.GetElementsByTagName("Scene");
        save.scenenum = int.Parse(xscene[0].InnerText);
        /*edit: keep only scene and change the rest on update. See LoadCheck function*/
        issavedgame = true;
        Debug.Log("LoadXML loads scene " + save.scenenum);
        SceneManager.LoadScene(save.scenenum);
        return;
    }

    //use this function in update, after LoadGame and LoadXML
    void LoadCheck()
    {
        if (issavedgame)
        {
            Debug.Log("loaded saved game");

            Save save = new Save();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(Application.persistentDataPath + "/save1.txt");//

            XmlNodeList xmoney = xmlDocument.GetElementsByTagName("Money");
            save.money = int.Parse(xmoney[0].InnerText);
            XmlNodeList xrec = xmlDocument.GetElementsByTagName("CompletedRecipes");
            save.completedrecipes = int.Parse(xrec[0].InnerText);

            XmlNodeList xcurrec = xmlDocument.GetElementsByTagName("CurrentRecipe");
            save.currentrecipe = xcurrec[0].InnerText;

            save.remainingrecipes.Clear();
            XmlNodeList xremrec = xmlDocument.GetElementsByTagName("RemainingRecipes");

            for (int i = 0; i < xremrec.Count; i++)
                save.remainingrecipes.Add(xremrec[i].InnerText);

            XmlNodeList xcoun = xmlDocument.GetElementsByTagName("Counter");
            save.counter = xcoun[0].InnerText;
            //Debug.Log(xcoun[0].InnerText);
            XmlNodeList xtent = xmlDocument.GetElementsByTagName("Tent");
            save.tent = xtent[0].InnerText;
            XmlNodeList xwall = xmlDocument.GetElementsByTagName("Wall");
            save.wall = xwall[0].InnerText;
            XmlNodeList xnap = xmlDocument.GetElementsByTagName("Napkins");
            save.napkins = xnap[0].InnerText;
            XmlNodeList xboard = xmlDocument.GetElementsByTagName("Board");
            save.board = xboard[0].InnerText;
            XmlNodeList xtab = xmlDocument.GetElementsByTagName("Table");
            save.table = xtab[0].InnerText;
            XmlNodeList xtabled = xmlDocument.GetElementsByTagName("Table_Decoration");
            save.table_dec = xtabled[0].InnerText;
            XmlNodeList xum = xmlDocument.GetElementsByTagName("Umbrella");
            save.umbrella = xum[0].InnerText;
            XmlNodeList xlog = xmlDocument.GetElementsByTagName("Logo");
            save.logo = xlog[0].InnerText;

            XmlNodeList xcus = xmlDocument.GetElementsByTagName("Customer");
            save.customer = int.Parse(xcus[0].InnerText);

            //reload if we cannot find our objects yet
            if (GameObject.Find("Money") == null)
            {
                Debug.Log("cannot find money");
                return;
            }
            GameObject.Find("Money").GetComponent<MoneyScript>().money = save.money;
            GameObject.Find("Furnace").GetComponent<FurnaceScript>().numberofcompletedrecipes = save.completedrecipes;

            //get recipes
            foreach (Recipe_SO rso in GameObject.Find("Furnace").GetComponent<FurnaceScript>().next_recipe)
            {
                if (rso.name == save.currentrecipe)
                {
                    GameObject.Find("Furnace").GetComponent<FurnaceScript>().recipe = null;
                    GameObject.Find("Furnace").GetComponent<FurnaceScript>().recipe = rso;
                    GameObject.Find("Furnace").GetComponent<FurnaceScript>().recipe.neededIngr = rso.neededIngr;
                    GameObject.Find("Furnace").GetComponent<FurnaceScript>().recipe.numbOfIng = rso.numbOfIng;
                    GameObject.Find("Furnace").GetComponent<FurnaceScript>().recipe.recipeSprite = rso.recipeSprite;

                    GameObject.Find("Furnace").GetComponent<FurnaceScript>().usable_number_of_ingredients.Clear();
                    foreach (int j in rso.numbOfIng)
                    {
                        GameObject.Find("Furnace").GetComponent<FurnaceScript>().usable_number_of_ingredients.Add(j);
                        //break;
                    }

                    GameObject.Find("Furnace").GetComponent<FurnaceScript>().next_recipe.Remove(rso);//
                    break;
                }
            }

            List<Recipe_SO> nrsl = new List<Recipe_SO>();
            foreach (string nrecipe in save.remainingrecipes)
            {
                bool recipeexists = false;
                int num = 0;
                foreach (Recipe_SO rso in GameObject.Find("Furnace").GetComponent<FurnaceScript>().next_recipe)
                {
                    if (rso.name == nrecipe)
                    {
                        recipeexists = true;
                        nrsl.Add(rso);
                        break;
                    }
                    num++;
                }
                if (recipeexists == false)
                    Debug.Log("recipe " + nrecipe + " doesn't exist here");
            }
            GameObject.Find("Furnace").GetComponent<FurnaceScript>().next_recipe.Clear();
            GameObject.Find("Furnace").GetComponent<FurnaceScript>().next_recipe = nrsl;

            GameObject.Find("Counter").GetComponent<SpriteRenderer>().sprite = spriteslayers[xcoun[0].InnerText];
            boughtables["Counter"] = spriteslayers[xcoun[0].InnerText];
            //Debug.Log("savecounter " + save.counter);
            //Debug.Log("savecountername " + spriteslayers[save.counter]);
            GameObject.Find("Tent").GetComponent<SpriteRenderer>().sprite = spriteslayers[xtent[0].InnerText];
            boughtables["Tent"] = spriteslayers[xtent[0].InnerText];
            GameObject.Find("Wall").GetComponent<SpriteRenderer>().sprite = spriteslayers[xwall[0].InnerText];
            boughtables["Wall"] = spriteslayers[xwall[0].InnerText];
            GameObject.Find("PaperTowels").GetComponent<SpriteRenderer>().sprite = spriteslayers[xnap[0].InnerText];
            boughtables["Napkins"] = spriteslayers[xnap[0].InnerText];
            GameObject.Find("Board").GetComponent<Image>().sprite = spriteslayers[xboard[0].InnerText];
            boughtables["Board"] = spriteslayers[xboard[0].InnerText];
            if (GameObject.Find("Table") != null)
            {
                GameObject.Find("Table").GetComponent<SpriteRenderer>().sprite = spriteslayers[xtab[0].InnerText];
                boughtables["Table"] = spriteslayers[xtab[0].InnerText];
                if (GameObject.Find("TableDec") != null)
                {
                    GameObject.Find("TableDec").GetComponent<SpriteRenderer>().sprite = spriteslayers[xtabled[0].InnerText];
                    boughtables["Table Decoration"] = spriteslayers[xtabled[0].InnerText];
                }
                if (GameObject.Find("Umbrella") != null)
                {
                    GameObject.Find("Umbrella").GetComponent<SpriteRenderer>().sprite = spriteslayers[xum[0].InnerText];
                    boughtables["Umbrella"] = spriteslayers[xum[0].InnerText];
                }
                if (GameObject.Find("Logo") != null)
                {
                    GameObject.Find("Logo").GetComponent<SpriteRenderer>().sprite = spriteslayers[xlog[0].InnerText];
                    boughtables["Logo"] = spriteslayers[xlog[0].InnerText];
                }
            }

            GameObject.Find("Customer").GetComponent<CustomerScript>().customerandom = save.customer;
            GameObject.Find("Customer").GetComponent<CustomerScript>().customercurrentspriterenderer.sprite = GameObject.Find("Customer").GetComponent<CustomerScript>().customerspritelistcurrent[save.customer];

            if (GameObject.Find("Customer").GetComponent<SpriteRenderer>().sprite == GameObject.Find("Customer").GetComponent<CustomerScript>().customerspritelistcurrent[save.customer])
                Debug.Log("same customer sprite " + save.customer);
            else
                Debug.Log("different customer sprite");

            issavedgame = false;
        }
    }

    
    //save menu items
    //WIP
    void SaveMenuXML()
    {
        SaveMenu saveme=new SaveMenu();

        saveme.language_current = language_current;
        saveme.levelid = levelid;
        //save.scenenum = SceneManager.GetActiveScene().buildIndex;
        saveme.option_character = option_character;

        //create the xml document with the above values
        XmlDocument xmlDocument = new XmlDocument();

        XmlElement root = xmlDocument.CreateElement("SaveMenu");

        XmlElement xlang = xmlDocument.CreateElement("Language");
        xlang.InnerText = saveme.language_current.ToString();
        root.AppendChild(xlang);

        XmlElement xlev = xmlDocument.CreateElement("Level");
        xlev.InnerText = saveme.levelid.ToString();
        root.AppendChild(xlev);

        XmlElement xchar = xmlDocument.CreateElement("Character");
        xchar.InnerText = saveme.option_character.ToString();
        root.AppendChild(xchar);


        xmlDocument.AppendChild(root);
        xmlDocument.Save(Application.persistentDataPath + "/savemenu.txt");//
        string sav = Application.persistentDataPath + "/savemenu.txt";
        Debug.Log("persistent data path savemenu: " + sav);


    }
    
    //load menu items properly
    //WIP
    void LoadMenuXML()
    {
        string sav = Application.persistentDataPath + "/savemenu.txt";
        if (System.IO.File.Exists(sav))
        {
            Debug.Log("loading menu data from saved file");

            SaveMenu saveme = new SaveMenu();
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(Application.persistentDataPath + "/savemenu.txt");

            XmlNodeList xscene = xmlDocument.GetElementsByTagName("Level");
            levelid = int.Parse(xscene[0].InnerText);
            //levelchanged = true;
            //menuitems[5].GetComponentInChildren<Image>().sprite=

            XmlNodeList xlang = xmlDocument.GetElementsByTagName("Language");
            language_current = xlang[0].InnerText;
            //

            XmlNodeList xchar = xmlDocument.GetElementsByTagName("Character");
            option_character = xchar[0].InnerText;
            //
        }
        else
            Debug.Log("Save menu data doesn't exist");
    }
    /**/

    //assign menusettings
    void set_menu(MenuSettings_SO mso)
    {
        //get sprites from current settings
        //(the second child of each of the folllowing gameobject is an Image)//
        mso.CharacterSprite = menuitems[6].transform.GetChild(1).GetComponent<Image>().sprite;
        mso.LevelSprite=menuitems[5].transform.GetChild(1).GetComponent<Image>().sprite;
        mso.LanguageSprite=menuitems[4].transform.GetChild(1).GetComponent<Image>().sprite;

        mso.language= language_current;
        mso.level = levelid;
        mso.character = option_character;
    }

    //after setting the above correctly, 
    // then we can get saved values from this SO
    void get_menu(MenuSettings_SO mso)
    {

        menuitems[6].transform.GetChild(1).GetComponent<Image>().sprite=mso.CharacterSprite  ;
        menuitems[5].transform.GetChild(1).GetComponent<Image>().sprite=mso.LevelSprite  ;
        menuitems[4].transform.GetChild(1).GetComponent<Image>().sprite=mso.LanguageSprite  ;

        language_current=mso.language  ;
        levelid=mso.level  ;
        option_character=mso.character  ;
    }

    //allow the game to be saved or loaded during Update by pressing keyboard keys
    void SaveOrLoadKeys()
    {
        if (Input.GetKeyDown("s"))
            SaveGame();
        if (Input.GetKeyDown("l"))
        {
            issavedgame = true;
            LoadGame();
        }
    }

    void SaveOrLoadKeysMenu()
    {
        if (Input.GetKeyDown("d"))
            SaveMenuXML();
        if (Input.GetKeyDown("k"))
        {
            //issavedgame = true;
            LoadMenuXML();
        }
    }

    //misc
    //write csv
    void writetocsv()
    {
        string firstrow = "id;English;Greek;Polish;Portuguese;Romanian;Type;Notes";
        using(StreamWriter sw =new StreamWriter(Application.persistentDataPath + "/localisation.csv"))
        {
            sw.WriteLine(firstrow);
            for (int i = 0; i < words_en_base.Count; i++)
            {
                //append each couple of words to this
                sw.WriteLine(i+";"+words_en[i] + ";" + words_gr[i]);
            }
        }
        Debug.Log("written csv at " + Application.persistentDataPath);
    }

    //Cheats!
    //Experimental. Game experience may vary. 

    //win the level by finishing only one recipe
    void cheat_one_recipe_only()
    {
        theFurnace = null;
        if (GameObject.Find("Furnace") != null)
        {
            theFurnace = GameObject.Find("Furnace").GetComponent<FurnaceScript>();
            theFurnace.numberofrecipesinlevel = 1;
        }
    }

    //the game will only use recipes that include the given string in their names
    //can be used for specific ingredients eg 'tomato' or recipe types such as 'sandwich', 'salad' etc
    void cheat_only_ingredient_recipes(string ingredient)
    {
        theFurnace = null;
        if (GameObject.Find("Furnace") != null)
        {
            theFurnace = GameObject.Find("Furnace").GetComponent<FurnaceScript>();
            foreach(Recipe_SO rec in theFurnace.next_recipe)
            {
                if(rec.name.Contains(ingredient))
                {
                    Debug.Log("this recipe includes our ingredient of choice"+rec.name);
                }
                else
                {
                    Debug.Log("we do not need this recipe" + rec.name);
                    theFurnace.next_recipe.Remove(rec);
                }
            }
        }
    }

    //allow recipes to be completed with only one correct ingredient
    public bool cheat_one_ingredient_recipe = false;
}

public class Save
{
    public int scenenum;
    public int money;
    public int completedrecipes;
    //recipes
    public string currentrecipe;
    public List<string> remainingrecipes = new List<string>();
    //decoration graphics
    public string counter;
    public string tent;
    public string wall;
    public string napkins;
    public string board;
    public string table;
    public string table_dec;
    public string umbrella;
    public string logo;
    //misc graphics
    public int customer;
}

public class SaveMenu
{
    public string language_current;
    public int levelid;
    public string option_character;
}
