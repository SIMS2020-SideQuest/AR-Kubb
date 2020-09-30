using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceOnPlane : MonoBehaviour
{

    private ARSessionOrigin sessionOrigin;
    private List<ARRaycastHit> hits;

    public GameObject prefabToPlace;

    // Start is called before the first frame update
    void Start()
    {
        sessionOrigin = GetComponent<ARSessionOrigin>();
        hits = new List<ARRaycastHit>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                if (sessionOrigin.GetComponent<ARRaycastManager>().Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose pose = hits[0].pose;
                    Instantiate(prefabToPlace, pose.position, pose.rotation);
                }
            }

        }
        
    }
}
