using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mustard : Ingredient
{
    [SerializeField] private  LineRenderer line;
    [SerializeField] private Transform spawner;
    [SerializeField] private GameObject _mustardPrefab;
    [HideInInspector] private bool _squeezing = false;
    // Start is called before the first frame update
    void Start()
    {
        line.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_squeezing)
        {
            RaycastHit ray;
            Physics.Raycast(spawner.position, Vector3.down, out ray);

            if (ray.collider != null)
            {
                line.enabled = true;
                line.SetPosition(0, spawner.transform.position);
                line.SetPosition(1, ray.point);

                if (ray.collider.gameObject.layer == LayerMask.NameToLayer("Bread"))
                {
                    Bread bread = ray.collider.GetComponent<Bread>();
                    if(!bread.ingredients.Find(x => x.food == Food.Mustard))
                    {
                        Ingredient must = Instantiate(_mustardPrefab).GetComponent<Ingredient>();
                        bread.AddIngedient(must);
                    }
                    
                }
            }
        }
    }

    public void OnSqueeze()
    {
        Debug.Log("squeezed");
        _squeezing = true;

    }

    public void EndSqueeze()
    {
        line.enabled = false;
        _squeezing = false;
    }

    public void OnGrab()
    {
        transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
    }

}
