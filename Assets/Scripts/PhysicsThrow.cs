using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Debug = UnityEngine.Debug;

public class PhysicsThrow : MonoBehaviour
{
    [SerializeField]
    GameObject ARCam;

    GameObject stick;

    public GameObject debugText;

    [SerializeField]
    ARSessionOrigin m_SessionOrigin;

    // Our thrown object
    Rigidbody obj;

    // Start position Vector
    Vector2 startPos, endPos, direction;

    // Powers in 3D space
    [SerializeField]
    float powerX = 3f, powerY = 2f, m_ThrowForce = 2f;

    // Average flick speed
    float flickSpeed = 0.8f;

    // Time
    float TouchStart, TouchEnd, TouchInterval, TouchTemp;

    float ObjectVelocity = 0;

    Vector3 spawnPos;

    Quaternion startRot;

    // Start is called before the first frame update
    void Start(){

        // Get engine physics properties
        obj = GetComponent<Rigidbody>();

        m_SessionOrigin=GameObject.Find("AR Session Origin").GetComponent<ARSessionOrigin>();

        ARCam = m_SessionOrigin.transform.Find("AR Camera").gameObject;

        spawnPos = transform.position;

        startRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //DebugTextFunction("ARCAM" + ARCam.transform.position);
        if(Input.GetMouseButtonDown(0)) {

            TouchStart = Time.time;

            startPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }

        if(Input.GetMouseButtonUp(0)) {
            
            // Assigning time where touch/swipe ended
            TouchEnd = Time.time;

            // Get touch interval
            TouchInterval = TouchEnd - TouchStart;

            // get position of finger release
            //endPos = Input.GetTouch(0).position;
            endPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            // Calculate direction of object in 2D plane
            //direction = endPos - startPos;
            direction = startPos - endPos;
            
            obj.isKinematic = false;
            

           // If subtraction equals empty Vector2(0f,0f,0f) don't perform force
            if( !(endPos - startPos).Equals(Vector2.zero) )
            {
                // Calculate Object speed
                CalcObjectSpeed();

                // Add rotation to object
                objectRotation();

                // Add force in 3D space (z,y,x)     
                obj.AddForce((ARCam.transform.forward * m_ThrowForce * ObjectVelocity) + (ARCam.transform.up * -direction.y * powerY) + 
                (ARCam.transform.right * -direction.x * powerX), ForceMode.Impulse);

                obj.useGravity = true;               

                // Reset object
                Invoke("resetObject", time: 3f);    
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
        /*if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            TouchStart = Time.time;
            //startPos = Input.GetTouch(0).position;
            startPos = Camera.main.ScreenToViewportPoint(Input.GetTouch(0).position);
        }

        // If finger is released
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            // Assigning time where touch/swipe ended
            TouchEnd = Time.time;

            // Get touch interval
            TouchInterval = TouchEnd - TouchStart;

            // get position of finger release
            //endPos = Input.GetTouch(0).position;
            endPos = Camera.main.ScreenToViewportPoint(Input.GetTouch(0).position);

            // Calculate direction of object in 2D plane
            //direction = endPos - startPos;
            direction = startPos - endPos;
            
            obj.isKinematic = false;

           // If subtraction equals empty Vector2(0f,0f,0f) don't perform force
            if( !(endPos - startPos).Equals(Vector2.zero) )
            {
                // Calculate Object speed
                CalcObjectSpeed();

                // Add force in 3D space (z,y,x)     
                obj.AddForce((ARCam.transform.forward * m_ThrowForce) + (ARCam.transform.up * -direction.y * powerY) + 
                (ARCam.transform.right * -direction.x * powerX), ForceMode.Impulse);               

                // Add rotation to object
                objectRotation();

                //Destroy (obj, 3f);
            }

            // Get temporary touch time
            if(TouchStart > 0)
                TouchTemp = Time.time - TouchStart;

            // If suspecting holding, reset start time and position
            if(TouchTemp > flickSpeed) {
                TouchStart = Time.time;
                startPos = Camera.main.ScreenToViewportPoint(Input.GetTouch(0).position);
                DebugTextFunction("TouchTemp: " + TouchTemp);
            }
        }
        */    
    }    
   void CalcObjectSpeed()
    {
        // Flick length
        float flick = direction.magnitude;
        
        if  ( TouchInterval > 0 )
            ObjectVelocity = flick / TouchInterval;
    }

    void objectRotation()
    {
        var rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(-direction),Time.deltaTime*m_ThrowForce);
        transform.rotation = rotation;
    }

    private void resetObject()
    {
        //Vector3 SpawnPos = new Vector3(oTransform.position.x, oTransform.position.y, oTransform.position.z + 0.0178f);
        Debug.Log("Z: "+ARCam.transform.position.z);
        Vector3 resetPos = new Vector3(ARCam.transform.position.x,ARCam.transform.position.y/6,ARCam.transform.position.z + 0.1422f);

        obj.useGravity = false;

        obj.velocity = Vector3.zero;

        transform.position = resetPos;

        transform.rotation = startRot;
    }
}