﻿using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaneSetupManager : MonoBehaviour
{
    public ARPlaneManager planeManager;
    public Material occlusionMat, planeMat;
    public GameObject planePrefab;

    public void SetOcclusionMaterial(){
        planePrefab.GetComponent<Renderer>().material = occlusionMat;

        foreach(var plane in planeManager.trackables){
            plane.GetComponent<Renderer>().material = occlusionMat;
        }
    }

    public void SetPlaneMaterial(){
        planePrefab.GetComponent<Renderer>().material = planeMat;

        foreach(var plane in planeManager.trackables){
            plane.GetComponent<Renderer>().material = planeMat;
        }
    }
}
