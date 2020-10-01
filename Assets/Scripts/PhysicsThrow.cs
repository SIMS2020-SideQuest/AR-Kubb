using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Debug = UnityEngine.Debug;

[RequireComponent(typeof(Rigidbody))]

public class PhysicsThrow : MonoBehaviour
{
    [SerializeField]
    GameObject ARCam;

    [SerializeField]
    ARSessionOrigin m_SessionOrigin;

    // Our thrown object
    Rigidbody obj;

    // Start position Vector
    Vector2 startPos, endPos, direction;

    // Powers in 3D space
    [SerializeField]
    float powerXY = 0.01f, m_ThrowForce = 3f;

    // Time
    float TouchStart, TouchEnd, TouchInterval;

    // Holding object
    bool holding;

    // Start is called before the first frame update
    void Start(){

        // Get engine physics properties
        obj = GetComponent<Rigidbody>();

        m_SessionOrigin=GameObject.Find("AR Session Origin").GetComponent<ARSessionOrigin>();

        ARCam = m_SessionOrigin.transform.Find("AR Camera").gameObject;
        transform.parent = ARCam.transform;

        holding = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetMouseButtonDown(0)) {
            TouchStart = Time.time;
            startPos = Input.mousePosition;
        }

        if(Input.GetMouseButtonUp(0)) {
            TouchEnd = Time.time;
            TouchInterval = TouchEnd - TouchStart;
            endPos = Input.mousePosition;
            direction = endPos - startPos;
            obj.isKinematic = false;
            Debug.Log("TouchInterval: "+TouchInterval);
            Debug.Log("startPos: "+startPos);
            Debug.Log("endPos: "+endPos);
                        Debug.Log("Direction.X: "+direction.x);
                                    Debug.Log("Direction.Y: "+direction.y);            
            Debug.Log("Direction: "+direction);
            Debug.Log("forward: "+transform.forward);
            //Debug.Log("Force vector3: "+obj.AddForce((transform.forward*m_ThrowForce) + (transform.up*direction.y*powerXY) + (transform.right*direction.x*powerXY)));            
            
            if((endPos - startPos).Equals(Vector2.zero)) {
                m_ThrowForce = 0.0001f;
            }
            // add force in 3D space (z,y,x)
            obj.AddForce((ARCam.transform.forward * m_ThrowForce / TouchInterval) + (ARCam.transform.up * direction.y * powerXY) + (ARCam.transform.right * direction.x * powerXY),ForceMode.Impulse);
        }

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

            // Calculate the speed of object
            CalcObjectSpeed();

            // add force in 3D space (z,y,x)
            obj.AddForce((ARCam.transform.forward * m_ThrowForce) + (ARCam.transform.up * direction.y * powerXY) + (ARCam.transform.right * direction.x * powerXY),ForceMode.Impulse);
        }
    }
    void CalcObjectSpeed()
    {
        float flick = direction.magnitude;
        float ObjectVelocity;
        
        if  ( TouchInterval > 0 && !((endPos - startPos).Equals(Vector2.zero)) )
        {
            ObjectVelocity = flick / ( flick - TouchInterval);
        }    
    }
}