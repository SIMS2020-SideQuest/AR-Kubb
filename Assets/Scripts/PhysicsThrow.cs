﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

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
    float powerXY = 1f, powerZ = 0.1f, m_ThrowForce = 5f;

    //public Vector3 m_StickCameraOffset = new Vector3(0f, -1.4f, 2f);
    // Time
    float TouchStart, TouchEnd, TouchInterval;

    // Start is called before the first frame update
    void Start(){
        // Get engine physics properties
        obj = GetComponent<Rigidbody>();
        m_SessionOrigin=GameObject.Find("AR Session Origin").GetComponent<ARSessionOrigin>();
        ARCam = m_SessionOrigin.transform.Find("AR Camera").gameObject;
        transform.parent = ARCam.transform;
        //resetStick();
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
            //obj.AddForce(direction.x * powerXY, direction.y * powerXY, powerZ / TouchInterval);
            obj.AddForce(ARCam.transform.forward * m_ThrowForce / TouchInterval + ARCam.transform.up * direction.y * powerXY + ARCam.transform.right * direction.x * powerXY);
            //removes object after 3s
            //Destroy (gameObject, 3f);
        }
    }
/*
    private void resetStick()
    {
       obj.mass = 0;
       obj.useGravity = false;
       obj.velocity = Vector3.zero;
       obj.angularVelocity = Vector3.zero;
       TouchEnd = 0.0f; 

       Vector3 stickPos = ARCam.transform.position + ARCam.transform.forward * m_StickCameraOffset.z + ARCam.transform.up * m_StickCameraOffset.y;
       transform.position = stickPos;
    }
*/
} 