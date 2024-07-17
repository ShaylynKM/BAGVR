using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class Bread : MonoBehaviour
{
    [HideInInspector] public List<Ingredient> ingredients = new List<Ingredient>();

    [SerializeField]private float _ingredientOffset = .15f;
    [SerializeField] private GameObject sandwich;

    [SerializeField] private ParticleSystem puffOfAir;
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
        if (!ingredients.Find(x => x.food == ingredient.food))
        {
            ingredients.Add(ingredient);
        }
        else
            return;
        //attach object to this

        ingredient.transform.parent = transform;
        ingredient.transform.rotation = transform.rotation;
        if(ingredients.Count <= 1)
        {
            ingredient.transform.localPosition = new Vector3(0, _ingredientOffset, 0);
        }
        else
        {
            ingredient.transform.localPosition = new Vector3(0, _ingredientOffset, 0); //have the ingredients attach further up
        }
        Destroy(ingredient.GetComponent<XRGrabInteractable>());
        Destroy(ingredient.GetComponent<Rigidbody>());

        ingredient.enabled = false;

        Destroy(ingredient);

        if(ingredients.Count > 2)
        {
            allIngredients = true;
        }

        puffOfAir.Play();
    }

    public void FinishSandwich(GameObject bread)
    {
        Instantiate(sandwich, transform.position, transform.rotation);
        Destroy(bread);
        Destroy(gameObject);
    }

    public void OnDropped()
    {
        if(ingredients.Count == 0) 
        {
            //check if nearby bread has all ingredients. if yes then attach to top
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, .15f, LayerMask.GetMask("Bread"));
            if (hitColliders.Length > 0)
            {
                foreach(Collider collider in hitColliders)
                {
                    if(collider.GetComponent<Bread>().allIngredients)
                    {
                        collider.GetComponent<Bread>().FinishSandwich(gameObject);
                    }
                }
            }
        }
    }
}
