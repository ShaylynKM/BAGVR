using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class Bread : MonoBehaviour
{
    [HideInInspector] public List<Ingredient> ingredients = new List<Ingredient>();

    private float _ingredientOffset = 0;
    [SerializeField] private GameObject sandwich;

    [SerializeField] private InfoPanelVR infoPanelVR;

    [SerializeField] private ParticleSystem puffOfAir;

    public bool allIngredients { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        _ingredientOffset = GetOffset(GetComponent<MeshFilter>().sharedMesh).z / 2;
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
        _ingredientOffset += GetOffset(ingredient.GetComponent<MeshFilter>().sharedMesh).z;
        if (ingredients.Count <= 1)
        {
            ingredient.transform.localPosition = new Vector3(0, 0, 0);
            ingredient.transform.position = new Vector3(ingredient.transform.position.x, ingredient.transform.position.y + _ingredientOffset , ingredient.transform.position.z);
        }
        else
        {
            
            ingredient.transform.localPosition = new Vector3(0, 0, 0);
            ingredient.transform.position = new Vector3(ingredient.transform.position.x, ingredient.transform.position.y + _ingredientOffset, ingredient.transform.position.z); //have the ingredients attach further up
        }
        Destroy(ingredient.GetComponent<XRGrabInteractable>());
        Destroy(ingredient.GetComponent<Rigidbody>());

        ingredient._added = true;

        if(ingredients.Count > 2)
        {
            allIngredients = true;
        }

        ParticleSystem p = Instantiate(puffOfAir,null);
        p.transform.position = transform.position;
    }

    public void FinishSandwich(GameObject bread)
    {
        Instantiate(sandwich, transform.position, transform.rotation);
        Destroy(bread);
        Destroy(gameObject);

        infoPanelVR.ShowInfo();
    }

    private Vector3 GetOffset(Mesh mesh)
    {
        return mesh.bounds.size;
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
