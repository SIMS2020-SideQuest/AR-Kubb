using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class PhysicsThrow : MonoBehaviour
{
    // Our thrown object
    public Rigidbody obj;

    // Start position Vector
    public Vector3 startPos;

    // Boolean for moving object
    public bool isMoving;

    // Power
    public float power;

    public float x, y;

    // Start is called before the first frame update
    void Start()
    {
        // Get engine physics properties
        obj = GetComponent<Rigidbody>();

        // Set object starting position
        startPos = transform.position;

        // placeholder power
        power = 1.0f;

        // 
        isMoving = false;

    }

    // Update is called once per frame
    void Update()
    {
        // Check whether the object thrown has stopped traveling
        if (obj.velocity.magnitude == 0 && isMoving)
        {
            // Object not moving anymore
            isMoving = false;

            // Reset Object position to start
            transform.position = startPos;
        }
    }

    // Throw function to be called when throw button is pressed
    public void Throw()
    {
        // Be able to fetch power value from power meter and assign it to float power



        // Enable Unity engine physics
        obj.useGravity = true;

        // Add force in space (x,y,z) to travel 
        // Transform.forward will be multiplied with the powermeter (?) Ex. Transform.forward * power
        obj.AddForce(transform.forward * power, ForceMode.Impulse);

        isMoving = true;
    }
}
