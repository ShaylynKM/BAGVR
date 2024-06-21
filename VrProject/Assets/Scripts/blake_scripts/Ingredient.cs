using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public Food type;
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
        Debug.Log("dropped");
        //check for nearby bread

    }
}

public enum Food
{
    Cheese,
    Ham
}
