using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectCollision : MonoBehaviour
{
    public Rigidbody obj;

    float kubbRotation;

    bool hit = false;
    // Start is called before the first frame update
    void Start()
    {
        obj = GetComponent<Rigidbody>();

        obj.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if(hit)
          //  Debug.Log("Euler: "+gameObject.transform.eulerAngles.x);
    }

    void OnCollisionEnter(Collision collider)
    {
        obj.useGravity = true;

        // Fetch rotation of object in Degrees
        kubbRotation = gameObject.transform.eulerAngles.x;

        // Prevent only touched objects to be deleted
        if(kubbRotation < 70)
            Destroy(gameObject, 3);
    }
}
