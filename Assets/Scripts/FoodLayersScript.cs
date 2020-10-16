using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using System.Security.Cryptography;
//using System.Diagnostics;
using UnityEngine;

public class FoodLayersScript : MonoBehaviour
{
    public SpriteRenderer renderer;
    public Sprite oldsprite,newsprite,othersprite;
    public List<SpriteRenderer> renderers = new List<SpriteRenderer>();

    //we need access to the furnace in order to obtain variables like number-of-ingredients
    public FurnaceScript theFurnace;

    public SpawningFoodLayerScript theSPF;

    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        //oldsprite = null;
        //oldsprite = Resources.Load<Sprite>("Triangle");
        //newsprite = Resources.Load<Sprite>("Square");//Resourses.

        //renderer.sprite = oldsprite;

        //renderers[0].sprite

        theSPF = GameObject.Find("SpawningFoodLayer").GetComponent<SpawningFoodLayerScript>();

        target = GameObject.Find("Furnace");
    }

    public bool change = false;
    // Update is called once per frame
    void Update()
    {
        /*
        if (change == false)
        {
            renderers[0].sprite = oldsprite;
            renderers[1].sprite = oldsprite;
            renderers[2].sprite = oldsprite;

        }            
        else
        {
            renderers[0].sprite = newsprite;
            renderers[1].sprite = newsprite;
            renderers[2].sprite = newsprite;

        }*/

        //MoveFoodLayer();
        if (change == true)
        {
            //rename our object so that it doesn't interfere with other similar objects' functionality
            //very buggy in updayte! 
            if (gameObject.name.EndsWith("(Clone)"))
                gameObject.name = gameObject.name.Substring(0, gameObject.name.Length - 7);//remove (clone) from string
            transform.Translate(Vector2.right * Time.deltaTime * 4);
            theSPF.spawnfoodlayer();
            if (gameObject.transform.position.x > 11)
            {
                Destroy(gameObject);
                change = false;
                //theSPF.spawnfoodlayer();
            }
        }
            
    }

    public void changesprite()
    {
        change = true;
        Debug.Log("changesprite");
        renderer.sprite = newsprite;

        //renderer.transform.position.Set(10, 10, 0);
        transform.position = new Vector2(transform.position.x+0,transform.position.y+1);

        //Sprite nextsprite = Instantiate(newsprite,Vector2(0,  1),transform.position(0,0));
    }


    public void AddIngredient(Sprite sprite) {
        change = true;
        renderers[0].sprite = sprite;

        renderers[1].sprite = sprite;

        renderers[2].sprite = sprite;

       // newsprite = sprite;
        Debug.Log("addingredient");

        othersprite = sprite;

    }

    public void ResetIngredient(Sprite sprite)
    {
        change = false;
        renderers[0].sprite = sprite;

        renderers[1].sprite = sprite;

        renderers[2].sprite = sprite;

        // newsprite = sprite;
        Debug.Log("resetingredient");

    }

    public Sprite SpriteHandler(string ingredientname)
    {
        Sprite ingredientsprite;
        if(ingredientname=="Cheese")
        {
            //ingredientsprite= Resources.LoadAll<Sprite>("canteen_toast-01").Single(s => s.name == "toast_cheese");
            ingredientsprite = Resources.LoadAll<Sprite>("canteen_toast-01")[9];
            return ingredientsprite;
        }
        else if (ingredientname == "Ham")
        {
            ingredientsprite = Resources.LoadAll<Sprite>("canteen_toast-01")[10];
            return ingredientsprite;
        }
        else if (ingredientname == "Lettuce")
        {
            ingredientsprite = Resources.LoadAll<Sprite>("canteen_toast-01")[11];
            return ingredientsprite;
        }
        else if (ingredientname == "Tomato")
        {
            ingredientsprite = Resources.LoadAll<Sprite>("canteen_toast-01")[12];
            return ingredientsprite;
        }

        return null;
    }

    Vector2 directiontotarget;
    /*
    void MoveFoodLayer()
    {
        if (target != null)
        {
            directiontotarget = (target.transform.position - transform.position).normalized;
            //GetComponent<Rigidbody2D>().velocity = new Vector2(directiontotarget.x * 8, directiontotarget.y * 8);
            //transform.position = new Vector2(directiontotarget.x * 8, directiontotarget.y * 8);
            //transform.position = new Vector2(transform.position.x+directiontotarget.x * 8, transform.position.y+directiontotarget.y * 8);

            float newpos = Mathf.Repeat(Time.time * 1f, 20);
            transform.position = Vector2.right * newpos;
            ///transform.position = new Vector2(directiontotarget.x * 1, directiontotarget.y * 1);
        }
    }
    */

}
