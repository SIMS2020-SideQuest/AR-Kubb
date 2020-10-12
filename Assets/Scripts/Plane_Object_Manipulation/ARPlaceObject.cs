using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;

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
        
        private Vector2 touchPosition; //Touch istället för Vector2?

        public GameObject debugText;

        private bool objectPlaced = false;
        //public GameObject placementIndicator;
        //private Pose placementPose;
        //private bool placementPoseIsValid = false;

        private void Awake(){
            _arRaycastManager = GetComponent<ARRaycastManager>();
            //_planeManager = GetComponent<ARPlaneManager>;
            //placementIndicator = GetComponent<>();
        }

        public void DebugTextFunction(string outputtext){
            //Debug.LogErrorFormat("ERROR");
            debugText.GetComponent<Text>().text = outputtext;
        }
        
        void Update(){
            if(!objectPlaced){
                //UpdatePlacementPose();
                //UpdatePlacementIndicator();
                if(!TryGetTouchPosition(out Vector2 touchPosition))
                    return;
            
                if(_arRaycastManager.Raycast(touchPosition, _hits, TrackableType.PlaneWithinPolygon)){
                    var hitPose = _hits[0].pose; //var istället för pose??
                    PlaceObject(hitPose);
                }
            }
        }

        bool TryGetTouchPosition(out Vector2 touchPosition){
            if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began){ //placementPoseIsValid && !IsPointerOverUIObject()
                touchPosition = Input.GetTouch(0).position; //Touch touch = Input.GetTouch(0); //touch.position
                return true;
            }
            touchPosition = default;
            return false;
        }

        private void PlaceObject(Pose hitPose){
            spawnedObject = Instantiate(_PlaceableObject, hitPose.position, hitPose.rotation);
            objectPlaced = true;
            //placementIndicator.SetActive(false);
        }
        /*
        private void UpdatePlacementPose(){
            var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
            var hits = new List<ARRaycastHit>();
            _arRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);// TrackableType.All?
            placementPoseIsValid = hits.Count > 0;
            if (placementPoseIsValid){
                placementPose = hits[0].pose;
                var cameraForward = Camera.current.transform.forward;
                var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
                placementPose.rotation = Quaternion.LookRotation(cameraBearing);
            }
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
        
        private bool IsPointerOverUIObject(){
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
            List<RaycastResult> results = new List<RaycastResult>();

            return results.Count > 0;
        }
        */
    }
}

