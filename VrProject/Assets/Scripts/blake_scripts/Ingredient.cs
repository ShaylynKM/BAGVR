using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public Food food;
    [SerializeField] private float _detectionRadius = .15f;
    public bool _added;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnDropped()
    {
        if(!_added)
        {
            Debug.Log("dropped");
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _detectionRadius, LayerMask.GetMask("Bread"));
            if (hitColliders.Length > 0)
            {
                Bread bread = hitColliders[0].GetComponent<Bread>();
                bread.AddIngedient(this);
            }
        }
        
    }
}

public enum Food
{
    Cheese,
    Ham,
    Mustard
}
