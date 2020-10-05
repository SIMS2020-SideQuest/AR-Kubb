using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
//using UnityEngine.Experimental.XR

//Require object
[RequireComponent(typeof(ARRaycastManager))]
public class ARTapToPlaceObject1 : MonoBehaviour{
    public GameObject debugText;


    [SerializeField]
    public GameObject _PlaceableObject;

    /// The prefab to instantiate on touch.
    public GameObject placedPrefab
    {
        get { return _PlaceableObject; }
        set { _PlaceableObject = value; }
    }

    //private GameObject spawnedObject;
    /// The object instantiated as a result of a successful raycast intersection with a plane.
    public GameObject spawnedObject { get; private set; }

    /// Invoked whenever an object is placed in on a plane.
    //public static event Action onPlacedObject;


    private ARRaycastManager _arRaycastManager;
    private Vector2 touchPosition;

    static List<ARRaycastHit> _hits = new List<ARRaycastHit>();

    private List<GameObject> placedPrefabList = new List<GameObject>();

    private void awake(){
        DebugTextFunction("awake");
        _arRaycastManager = GetComponent<ARRaycastManager>();
    }

    
    bool TryGetTouchPosition(out Vector2 touchPosition){
        
        if(Input.touchCount > 0){
           // DebugTextFunction("touch>0");
            if (Input.GetTouch(0).phase == TouchPhase.Began){
                touchPosition = Input.GetTouch(0).position;
                return true;
            }
        }
        touchPosition = default;
        return false;
    }

    void Update(){
        if(!TryGetTouchPosition(out Vector2 touchPosition))
            return;

        if(_arRaycastManager.Raycast(touchPosition, _hits, TrackableType.PlaneWithinPolygon)){
            var hitPose = _hits[0].pose;
            if(spawnedObject == null){
                spawnedObject = Instantiate(_PlaceableObject, hitPose.position, hitPose.rotation);
            }
            else{
                spawnedObject.transform.position = hitPose.position;
            }
        }
    }

    public void SetPrefabType(GameObject prefabType){
        _PlaceableObject = prefabType;
    }

    public void DebugTextFunction(string outputtext){
        debugText.GetComponent<Text>().text = outputtext;
    }



}


/*
        void Update(){
            if (Input.touchCount > 0){
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began){
                    if (m_RaycastManager.Raycast(touch.position, _Hits, TrackableType.PlaneWithinPolygon)){
                        Pose hitPose = _Hits[0].pose;
                        //spawnedObject = Instantiate(_PlaceableObject, hitPose.position, hitPose.rotation);
                        Instantiate(_PlaceableObject, hitPose.position, hitPose.rotation);
                        if (onPlacedObject != null)
                            onPlacedObject();
                    }
                }
            }
        }
*/

/*
    void Update(){
        if(Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began){
                if (sessionOrigin.GetComponent<ARRaycastManager>().Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon)){
                    Pose pose = _hits[0].pose;
                    Instantiate(_PlaceableObject, pose.position, pose.rotation);
                }
            }

        }
        
    }
*/