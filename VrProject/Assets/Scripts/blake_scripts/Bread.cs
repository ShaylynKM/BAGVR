using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bread : MonoBehaviour
{
    [HideInInspector] public List<Ingredient> ingredients = new List<Ingredient>();

    public bool allIngredients { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per srame
    void Update()
    {
        
    }

    public void AddIngedient(Ingredient ingredient)
    {
        if(!ingredients.Find(x => x.type == ingredient.type))
        {
            ingredients.Add(ingredient);
        }
        //attach object to this

        ingredient.enabled = false;

        if(ingredients.Count >= 2)
        {
            allIngredients = true;
        }
    }

    public void OnDropped()
    {
        if(ingredients.Count == 0) 
        {
            //check if nearby bread has all ingredients. if yes then attach to top
        }
    }
}
