 using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class PhysicsThrow : MonoBehaviour
{
    // Our thrown object
    public Rigidbody obj;

    // Start position Vector
    public Vector2 startPos, endPos, direction;

    // Boolean for moving object
    public bool isMoving;

    // Powers in 3D space
    public float powerXY = 1f,powerZ = 50f;

    // Time
    public float TouchStart, TouchEnd, TouchInterval;

    // Start is called before the first frame update
    void Start()
    {
        // Get engine physics properties
        obj = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Checks whether screen is touched
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            TouchStart = Time.time;
            startPos = Input.GetTouch(0).position;
        }

        // If finger is released
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            // Assigning time where touch/swipe ended
            TouchEnd = Time.time;

            // Get touch interval
            TouchInterval = TouchEnd - TouchStart;

            // get position of finger release
            endPos = Input.GetTouch(0).position;

            // Calculate direction of object in 2D plane
            direction = startPos - endPos;
            obj.isKinematic = false;
            // add force in 3D space
            obj.AddForce(-direction.x * powerXY, -direction.y * powerXY, powerZ / TouchInterval);
        }
    }
} 