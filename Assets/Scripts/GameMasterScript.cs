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

    void Awake()
    {

        //Debug.Log("current language is: " + language_current);
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

        /*
          //if ((Application.dataPath + "/Resources/scrnsht.png") != null)
        if ((Application.persistentDataPath + "scrnsht.png") != null)
        {
            Debug.Log("screenshot exists");

            //Sprite newsprite;

            GameObject.Find("Continue").GetComponent<Image>().preserveAspect = true;
            //GameObject.Find("Continue").GetComponent<Image>().sprite = Resources.Load<Sprite>("scrnsht");
            GameObject.Find("Continue").GetComponent<Image>().sprite = spriteslayers["Screenshot"];
            //GameObject.Find("Continue").GetComponent<Image>().sprite = co.GetComponent<SpriteRenderer>().sprite;
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        /*if (GameObject.Find("Continue") != null)
        {
            //if ((Application.dataPath + "/Resources/scrnsht.png") != null)
            //GameObject.Find("Continue").GetComponent<Image>().sprite = Resources.Load<Sprite>("scrnsht");
            if ((Application.persistentDataPath + "scrnsht.png") != null)
                GameObject.Find("Continue").GetComponent<Image>().sprite = spriteslayers["Screenshot"];
        }*/

        if (language_current == "English")
        {
            GameObject speechbubble = null;
            if ((speechbubble = GameObject.Find("SpeechBubble")) != null)
                speechbubble.GetComponentInChildren<TextMeshPro>().text = "Thank You!";
            GameObject decdesc = null;
            if ((decdesc = GameObject.Find("DecText")) != null)
            {
                string boght;
                boght = languagehandler[GameObject.Find("ObjectsToBuy").GetComponent<TransactionScript>().objecttobebought];
                decdesc.GetComponent<Text>().text = "Congratulations!\nChoose a new " + boght + " colour to continue!";
            }
        }
        else if (language_current == "Greek")
        {
            GameObject decdesc = null;
            if ((decdesc = GameObject.Find("DecText")) != null)
            {
                string boght;
                boght = languagehandler[GameObject.Find("ObjectsToBuy").GetComponent<TransactionScript>().objecttobebought];
                decdesc.GetComponent<Text>().text = "Συγχαρητήρια!\nΔιάλεξε νέο χρώμα " + boght + " για να συνεχίσεις!";
            }
        }


        GameObject cnt = GameObject.Find("Counter");
        if (cnt != null)
        {
            cnt.GetComponent<SpriteRenderer>().sprite = boughtables["Counter"];
            //tents and walls are on the same scene as the counter (so we can change them)
            GameObject.Find("Wall").GetComponent<SpriteRenderer>().sprite = boughtables["Wall"];
            GameObject.Find("Tent").GetComponent<SpriteRenderer>().sprite = boughtables["Tent"];
            //Debug.Log("boughtablesCounter: " + boughtables["Counter"].name);
        }

        //cheat mode!
        
        theFurnace = null;
        if(GameObject.Find("Furnace")!=null)
        {
            theFurnace = GameObject.Find("Furnace").GetComponent<FurnaceScript>();
            theFurnace.numberofrecipesinlevel = 1;
        }
        



        if (issavedgame)
        {
            Debug.Log("loaded saved game");
            //InitialiseDictionary();

            Save save = new Save();
            XmlDocument xmlDocument = new XmlDocument();
            //xmlDocument.Load(Application.dataPath + "/SavedGames/save1.txt");//
            //xmlDocument.Load(Application.dataPath + "/Resources/save1.txt");//
            xmlDocument.Load(Application.persistentDataPath + "save1.txt");//

            XmlNodeList xmoney = xmlDocument.GetElementsByTagName("Money");
            save.money = int.Parse(xmoney[0].InnerText);
            XmlNodeList xrec = xmlDocument.GetElementsByTagName("CompletedRecipes");
            save.completedrecipes = int.Parse(xrec[0].InnerText);

            XmlNodeList xcurrec = xmlDocument.GetElementsByTagName("CurrentRecipe");
            save.currentrecipe = xcurrec[0].InnerText;

            save.remainingrecipes.Clear();
            XmlNodeList xremrec = xmlDocument.GetElementsByTagName("RemainingRecipes");
            //foreach(XmlNodeList remn in xremrec)
            for (int i = 0; i < xremrec.Count; i++)
            {
                save.remainingrecipes.Add(xremrec[i].InnerText);
            }

            XmlNodeList xcoun = xmlDocument.GetElementsByTagName("Counter");
            save.counter = xcoun[0].InnerText;
            //Debug.Log(xcoun[0].InnerText);
            XmlNodeList xtent = xmlDocument.GetElementsByTagName("Tent");
            save.tent = xtent[0].InnerText;
            XmlNodeList xwall = xmlDocument.GetElementsByTagName("Wall");
            save.wall = xwall[0].InnerText;

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

            //WIP
            //GameObject.Find("Furnace").GetComponent<FurnaceScript>().recipe.name=save.currentrecipe;
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

            //GameObject.Find("Furnace").GetComponent<FurnaceScript>().next_recipe.Clear();
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
                {
                    Debug.Log("recipe " + nrecipe + " doesn't exist here");
                    //Debug.Log("num is: " + num +" nextrecipes are: "+ GameObject.Find("Furnace").GetComponent<FurnaceScript>().next_recipe.Count);
                    //Debug.Log("recipebefore1 " + GameObject.Find("Furnace").GetComponent<FurnaceScript>().next_recipe[23].name);
                    //Debug.Log("will delete recipe " + GameObject.Find("Furnace").GetComponent<FurnaceScript>().next_recipe[num].name);
                    //GameObject.Find("Furnace").GetComponent<FurnaceScript>().next_recipe.RemoveAt(num);
                }
            }
            GameObject.Find("Furnace").GetComponent<FurnaceScript>().next_recipe.Clear();
            GameObject.Find("Furnace").GetComponent<FurnaceScript>().next_recipe = nrsl;


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

            GameObject.Find("Customer").GetComponent<CustomerScript>().customerandom = save.customer;
            GameObject.Find("Customer").GetComponent<CustomerScript>().customercurrentspriterenderer.sprite = GameObject.Find("Customer").GetComponent<CustomerScript>().customerspritelistcurrent[save.customer];
            // GameObject.Find("Customer").GetComponent<CustomerScript>().allowspritetochange=true;
            //GameObject.Find("Customer").GetComponent<CustomerScript>().customerchangesprite();
            if (GameObject.Find("Customer").GetComponent<SpriteRenderer>().sprite == GameObject.Find("Customer").GetComponent<CustomerScript>().customerspritelistcurrent[save.customer])
                Debug.Log("same customer sprite " + save.customer);
            else
                Debug.Log("different customer sprite");
            //GameObject.Find("Customer").GetComponent<CustomerScript>().allowspritetochange = false;

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
        /*languagehandlerprev.Clear();
        for (int i = 0; i < words_en_base.Count; i++)
        {
            languagehandlerprev.Add(words_en_base[i], words_current[i]);
            Debug.Log("added to dictionary: " + words_en_base[i] + words_current[i]);
        }*/
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
            speechbubble.GetComponentInChildren<TextMeshPro>().SetText("Thank You!");// = "Thank You!";



    }

    public void changelanguage_gr()
    {
        /*language_current = "English";
        words_current = words_en;
        //InitialiseLanguage();
        languagehandlerprev.Clear();
        for (int i = 0; i < words_en_base.Count; i++)
        {
            languagehandlerprev.Add(words_en_base[i], words_current[i]);
            Debug.Log("added to dictionary: " + words_en_base[i] + words_current[i]);
        }*/
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
        //words_current = words_en;//
        //words_current = words_gr;//
        /*languagehandlerprev.Clear();
        languagehandlerprev = languagehandler;*/
        languagehandler.Clear();
        for (int i = 0; i < words_en_base.Count; i++)
        {
            languagehandler.Add(words_en_base[i], words_current[i]);
            Debug.Log("added to dictionary: " + words_en_base[i]+words_current[i]);
            /*Debug.Log(languagehandler[words_en_base[i]]);
            Debug.Log(languagehandler[words_current[i]]);*/
        }
        //Debug.Log("sizeof base: " + words_en_base.Count + " Sizeof curr: " + words_current.Count);
        foreach (GameObject men in menuitems)
        {
            men.GetComponentInChildren<Text>().text = languagehandler[men.GetComponentInChildren<Text>().text];
            //preserve image aspect in here as well
            men.GetComponentInChildren<Image>().preserveAspect=true;
        }
        
    }

    public GameObject[] menuitems;
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

    public void SaveGame()
    {
        SaveXML();

        //take a screenshot
        //ScreenCapture.CaptureScreenshot(Application.dataPath + "/SavedGames/scrnsht.png");
        //ScreenCapture.CaptureScreenshot(Application.dataPath + "/Resources/scrnsht.png");
        ScreenCapture.CaptureScreenshot(Application.persistentDataPath + "scrnsht.png");
        //refresh its metadata
        //AssetDatabase.ImportAsset("Assets/Resources/scrnsht.png");
        //AssetDatabase.ImportAsset("Assets/Resources/scrnsht.png.meta");
    }

    

    private void SaveXML()
    {
        Save save = new Save();

        //do not allow saving when we don't have the necessary objects
        GameObject frn=null;
        if((frn=GameObject.Find("Furnace"))==null)
        {
            Debug.Log("save in this state isn't supported!");
            return;
        }

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
                    Debug.Log("getting sprite");
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
                
        XmlElement xcus = xmlDocument.CreateElement("Customer");
        xcus.InnerText = save.customer.ToString();
        root.AppendChild(xcus);

        xmlDocument.AppendChild(root);

        //xmlDocument.Save(Application.dataPath + "/Resources/save1.txt");//
        xmlDocument.Save(Application.persistentDataPath + "save1.txt");//
    }

    //this variable will be called whenever we load a scene
    // to check whether we should load its own specific values
    public static bool issavedgame = false;
    public void LoadGame()
    {
        Debug.Log("current level_id: " + levelid);
        if (levelchanged == false)
        {
            string sav = Application.persistentDataPath + "save1.txt";
            if(System.IO.File.Exists(sav))
                LoadXML();
            //if a saved game dosn't exist, start from level1
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);//
        }
        else
            SceneManager.LoadScene(levelid);
    }

    private void LoadXML()
    {
        Save save = new Save();
        XmlDocument xmlDocument = new XmlDocument();
        //xmlDocument.Load(Application.dataPath + "/Resources/save1.txt");//
        xmlDocument.Load(Application.persistentDataPath + "save1.txt");//

        XmlNodeList xmoney = xmlDocument.GetElementsByTagName("Money");
        save.money = int.Parse(xmoney[0].InnerText);
        XmlNodeList xscene = xmlDocument.GetElementsByTagName("Scene");
        save.scenenum = int.Parse(xscene[0].InnerText);
        XmlNodeList xrec = xmlDocument.GetElementsByTagName("CompletedRecipes");
        save.completedrecipes = int.Parse(xrec[0].InnerText);

        XmlNodeList xcurrec = xmlDocument.GetElementsByTagName("CurrentRecipe");
        save.currentrecipe = xcurrec[0].InnerText;

        save.remainingrecipes.Clear();
        XmlNodeList xremrec = xmlDocument.GetElementsByTagName("RemainingRecipes");
        for(int i=0;i<xremrec.Count;i++)
        {
            save.remainingrecipes.Add(xremrec[i].InnerText);
        }

        XmlNodeList xcoun = xmlDocument.GetElementsByTagName("Counter");
        save.counter = xcoun[0].InnerText;
        XmlNodeList xtent = xmlDocument.GetElementsByTagName("Tent");
        save.tent = xtent[0].InnerText;
        XmlNodeList xwall = xmlDocument.GetElementsByTagName("Wall");
        save.wall = xwall[0].InnerText;

        XmlNodeList xcus = xmlDocument.GetElementsByTagName("Customer");
        save.customer = int.Parse(xcus[0].InnerText);

        issavedgame = true;
        

        SceneManager.LoadScene(save.scenenum);//
    }
}

public class Save
{
    public int scenenum;
    public int money;
    public int completedrecipes;
    //recipes
    public string currentrecipe;
    public List<string> remainingrecipes = new List<string>();
    //graphics
    public string counter;
    public string tent;
    public string wall;
    public int customer;
}
