using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        //
        changelanguage_gr();//
    }

    // Update is called once per frame
    void Update()
    {
        
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
        words_current = words_en;
        InitialiseLanguage();
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
        }
    }
}
