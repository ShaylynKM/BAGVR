using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeDoor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      //  transform.rotation = new Quaternion(0,Quaternion.,0,transform.rotation.w);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.rotation.x != 0)
        {
            transform.rotation = new Quaternion(0,transform.rotation.y, transform.rotation.z, transform.rotation.w);
        }
        if (transform.rotation.z != 0)
        {
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 0, transform.rotation.w);
        }
        if (transform.rotation.y < Mathf.Deg2Rad * -90f)
        {
            transform.rotation = new Quaternion(transform.rotation.x, Mathf.Deg2Rad * -90f, transform.rotation.z, transform.rotation.w);
        }
        else if (transform.rotation.y > 0)
        {
            transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, transform.rotation.w);
        }
    }
}
