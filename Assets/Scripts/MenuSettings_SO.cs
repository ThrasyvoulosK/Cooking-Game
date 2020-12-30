using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*MenuSettings_SO*/
[CreateAssetMenu]
public class MenuSettings_SO : ScriptableObject
{
    //sprites
    public Sprite LevelSprite;
    public Sprite CharacterSprite;
    public Sprite LanguageSprite;

    //values
    public int level;
    public string character;
    public string language;
}
