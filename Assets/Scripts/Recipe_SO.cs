using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Recipe_SO : ScriptableObject
{
    public string nameofrecipe;

    public Sprite recipeSprite;
    public List<string> neededIngr = new List<string>();
    public List<int> numbOfIng = new List<int>();
    public List<Sprite> layeredingredients = new List<Sprite>();
}
