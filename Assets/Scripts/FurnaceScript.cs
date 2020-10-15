﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceScript : MonoBehaviour
{
    
    public Recipe_SO recipe;

    //more recipes;
    //public Recipe_SO[] next_recipe;
    public List<Recipe_SO> next_recipe;

    //our current, in-game recipe
    public List<string> current_recipe;

    public List<int> current_number_of_ingredients=new List<int>();

    public List<int> usable_number_of_ingredients = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        //recipe.
        //
        int ran = Random.Range(0, next_recipe.Count);
        recipe = next_recipe[ran];
        next_recipe.RemoveAt(ran);
        //

        foreach(int i in recipe.numbOfIng)
        {
            usable_number_of_ingredients.Add(i);
        }

}

    // Update is called once per frame
    void Update()
    {
        
    }
}