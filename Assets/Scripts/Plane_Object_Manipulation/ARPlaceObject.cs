using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace SIMS.SideQuest.ARKubb
{
    [RequireComponent(typeof(ARRaycastManager))]
    public class ARPlaceObject : MonoBehaviour{
        [SerializeField]
        GameObject _PlaceableObject;
        
        public GameObject placedPrefab{ //The prefab to instantiate on touch.
            get { return _PlaceableObject; }
            set { _PlaceableObject = value; }
        }

        public GameObject spawnedObject { get; private set; }   //The object instantiated as a result of a successful raycast intersection with a plane.        
        private ARRaycastManager _arRaycastManager;

        static List<ARRaycastHit> _hits = new List<ARRaycastHit>();
        
        private Vector2 touchPosition;
        //private List<GameObject> placedPrefabList = new List<GameObject>();
        //private Touch touchPosition1;

        public GameObject debugText;

        private bool objectPlaced = false;
        //public GameObject placementIndicator;
        //private Pose placementPose;
        //private bool placementPoseIsValid = false;
        private bool isObjectPlaced = false;

        private void Awake(){
            _arRaycastManager = GetComponent<ARRaycastManager>();
        }

        public void DebugTextFunction(string outputtext){
            debugText.GetComponent<Text>().text = outputtext;
        }
        
        bool TryGetTouchPosition(out Vector2 touchPosition){
            if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began){
                touchPosition = Input.GetTouch(0).position;
                return true;
            }
            touchPosition = default;
            return false;
        }

        void Update(){
            if(!TryGetTouchPosition(out Vector2 touchPosition))
                return;
            
            if(_arRaycastManager.Raycast(touchPosition, _hits, TrackableType.PlaneWithinPolygon)){
                var hitPose = _hits[0].pose;
                if(!objectPlaced){
                    PlaceObject(hitPose);
                }
            }
        }


        private void PlaceObject(Pose hitPose){
            spawnedObject = Instantiate(_PlaceableObject, hitPose.position, hitPose.rotation);
            objectPlaced = true;
            //placementIndicator.SetActive(false);
            //if(spawnedObject == null){
            //    spawnedObject = Instantiate(_PlaceableObject, hitPose.position, hitPose.rotation);
            //}
            //else{
            //    spawnedObject.transform.position = hitPose.position;
            //}
        }
        
        
        
    





        /*
        void Update(){
            if(!objectPlaced){
                UpdatePlacementPose();
                UpdatePlacementIndicator();
                if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began){
                    PlaceObject();
                }
            }

        }

        private void PlaceObject(){
            spawnedObject = Instantiate(_PlaceableObject, placementPose.position, placementPose.rotation);
            objectPlaced = true;
            placementIndicator.SetActive(false);
        }

        private void UpdatePlacementIndicator(){
            if (placementPoseIsValid){
                placementIndicator.SetActive(true);
                placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
            }
            else{
                placementIndicator.SetActive(false);
            }
        }

        private void UpdatePlacementPose(){
            var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
            var hits = new List<ARRaycastHit>();
            _arRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);
            placementPoseIsValid = hits.Count > 0;
            if (placementPoseIsValid){
                placementPose = hits[0].pose;
                var cameraForward = Camera.current.transform.forward;
                var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
                placementPose.rotation = Quaternion.LookRotation(cameraBearing);
            }
        }
        */





    }
}



/*
//public static event Action onPlacedObject; //Invoked whenever an object is placed in on a plane.
        void Update(){
            if (Input.touchCount > 0){
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began){
                    if (_arRaycastManager.Raycast(touch.position, _hits, TrackableType.PlaneWithinPolygon)){
                    Pose hitPose = _hits[0].pose;
                    spawnedObject = Instantiate(_PlaceableObject, hitPose.position, hitPose.rotation);
                        if(spawnedObject == null){
                            spawnedObject = Instantiate(_PlaceableObject, hitPose.position, hitPose.rotation);
                        }
                        else{
                            spawnedObject.transform.position = hitPose.position;
                        }
                    }
                }
            }
        }

        Debug.LogErrorFormat("PlaceObject");
        Debug.LogErrorFormat("PlaceObjectLog");
*/