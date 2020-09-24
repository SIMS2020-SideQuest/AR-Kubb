using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

//Require object
[RequireComponent(typeof(ARRaycastManager))]

public class ARTapToPlaceObject1 : MonoBehaviour{
    public GameObject gameObjectToInstantiate;

    private GameObject spawnedObject;
    private ARRaycastManager _arRaycastManager;
    private Vector2 touchPosition;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

   
    private void awake(){
        _arRaycastManager = GetComponent<ARRaycastManager>();
    }

    
    bool TryGetTouchPosition(out Vector2 touchPosition){
        if(Input.touchCount > 0){
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return false;
    }

    void Update()
    {
        if(!TryGetTouchPosition(out Vector2 touchPosition))
            return;

        if(_arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon)){
            var hitPost = hits[0].pose;

            if(spawnedObject == null){
                spawnedObject = Instantiate(gameObjectToInstantiate, hitPost.position, hitPost.rotation);
            }
            else{
                spawnedObject.transform.position = hitPost.position;
            }
        }
    }
}
