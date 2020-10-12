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
    float powerX = 0.015f, powerY = 0.001f, m_ThrowForce = 0.006f;


    // Average flick speed
    float flickSpeed = 3.2f;

    // Time
    float TouchStart, TouchEnd, TouchInterval, TouchTemp;

    // Start is called before the first frame update
    void Start(){

        // Get engine physics properties
        obj = GetComponent<Rigidbody>();

        m_SessionOrigin=GameObject.Find("AR Session Origin").GetComponent<ARSessionOrigin>();

        ARCam = m_SessionOrigin.transform.Find("AR Camera").gameObject;

        //transform.position = ARCam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetMouseButtonDown(0)) {

            // Record start time of swipe
            TouchStart = Time.time;

            // Get mouse position of swipe
            startPos = Input.mousePosition;
        }

        if(Input.GetMouseButtonUp(0)) {
            
            // Record end time of swipe ended
            TouchEnd = Time.time;
            
            // Swipe interval
            TouchInterval = TouchEnd - TouchStart;

            // Mouse position of end swipe
            endPos = Input.mousePosition;

            // Get 2D direction of swipe
            direction = endPos - startPos;

            obj.isKinematic = false;
            
            // If subtraction equals empty Vector2(0f,0f,0f) don't perform force
            if( !(endPos - startPos).Equals(Vector2.zero) )
            {
                // Calculate Object speed
                CalcObjectSpeed();

                obj.AddForce((ARCam.transform.forward * m_ThrowForce) + (ARCam.transform.up * direction.y * powerY) + 
                (ARCam.transform.right * direction.x * powerX), ForceMode.Impulse); 
                               
            }

            // Get temporary touch time
            if(TouchStart > 0)
                TouchTemp = Time.time - TouchStart;

            // If suspecting holding, reset start time and position
            if(TouchTemp > flickSpeed) {
                TouchStart = Time.time;
                startPos = Input.mousePosition;
            }    
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
            direction = endPos - startPos;
            obj.isKinematic = false;

           // If subtraction equals empty Vector2(0f,0f,0f) don't perform force
            if( !(endPos - startPos).Equals(Vector2.zero) )
            {
                // Calculate Object speed
                CalcObjectSpeed();

                // Add force in 3D space (z,y,x)
                obj.AddForce((ARCam.transform.forward * m_ThrowForce) + (ARCam.transform.up * direction.y * powerY) + 
                (ARCam.transform.right * direction.x * powerX), ForceMode.Impulse);                
            }

            // Get temporary touch time
            if(TouchStart > 0)
                TouchTemp = Time.time - TouchStart;

            // If suspecting holding, reset start time and position
            if(TouchTemp > flickSpeed) {
                TouchStart = Time.time;
                startPos = Input.GetTouch(0).position;
            }
        }
    }
    void CalcObjectSpeed()
    {
        // Flick length
        float flick = direction.magnitude;

        // Objects velocity
        float ObjectVelocity = 0;
        
        if  ( TouchInterval > 0 )
            ObjectVelocity = flick / TouchInterval;
        

        /*Debug.Log("Interval: "+TouchInterval);
        Debug.Log("Velocity: "+ObjectVelocity);
        Debug.Log("FOrce: "+m_ThrowForce);*/   
        m_ThrowForce *= ObjectVelocity;
    }
}