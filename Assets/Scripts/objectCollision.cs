using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectCollision : MonoBehaviour
{
    Rigidbody obj;

    // Start is called before the first frame update
    void Start()
    {
        obj = GetComponent<Rigidbody>();

        obj.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collosion)
    {
        
    }
}
