using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*MenuSettings_SO saves settings and their corresponding graphics, in order to be loaded properly between games*/
/*WIP*/
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
