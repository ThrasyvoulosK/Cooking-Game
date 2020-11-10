using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*FurnaceScript initialises the list of available recipes for the current scene*/
/*its public lists are frequently used by other scripts, such as the IngredientScript*/
public class FurnaceScript : MonoBehaviour
{
    
    public Recipe_SO recipe;

    //more recipes;
    public List<Recipe_SO> next_recipe;

    //our current, in-game recipe
    public List<string> current_recipe;

    public List<int> current_number_of_ingredients=new List<int>();

    public List<int> usable_number_of_ingredients = new List<int>();

    public int numberofrecipesinlevel;
    public int numberofcompletedrecipes = 0;

    public NextRecipeScript theNextRecipe;
    // Start is called before the first frame update
    void Start()
    {

        //choose a random recipe to begin with
        int ran = Random.Range(0, next_recipe.Count);
        recipe = next_recipe[ran];
        next_recipe.RemoveAt(ran);

        // add its ingredients
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
