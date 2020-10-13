using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class SpawnThrowObject : MonoBehaviour
{
    [SerializeField]
	GameObject ThrowObject;

    [SerializeField]
    GameObject ARCam;

	[SerializeField]
	ARSessionOrigin m_SessionOrigin;
	
	private Transform oTransform;
    GameObject spawnedObject;

	private GameObject objCamera;
    private Vector3 SpawnPosition;
    private int DistanceToCamera = 1;
    //public Transform creation;

	//float distance = 1f;
	//static float speed = 10f;
  	//float step = speed * Time.deltaTime;
	//public float smoothFactor = 0.5f;
	//bool contentPlaced;

	void Awake () {
		oTransform = transform;
        m_SessionOrigin = GetComponent<ARSessionOrigin>();
        //contentPlaced = false;
		//m_SessionOrigin=GameObject.Find("AR Session Origin").GetComponent<ARSessionOrigin>();
		//ARCam = m_SessionOrigin.transform.Find("AR Camera").gameObject;
        //objCamera = (GameObject) GameObject.FindWithTag("AR Camera");
    }

	void update(){
		//SpawnPosition = objCamera.transform.forward * DistanceToCamera + objCamera.transform.position ;
	}

    public void Spawn(){
		spawnedObject = Instantiate(ThrowObject);
		Vector3 pos = new Vector3(oTransform.position.x, oTransform.position.y - 0.5f, oTransform.position.z + 1);
		m_SessionOrigin.MakeContentAppearAt(spawnedObject.transform, pos, Quaternion.identity);

		/*Transform cameraTransform = Camera.main.transform;

		transform.position = cameraTransform.position + cameraTransform.forward * 10f;
		transform.rotation = cameraTransform.rotation;
		transform.SetParent (imageTarget, true);
		transform.localScale = Vector3.one;
		var targetObject = ThrowObject.transform.GetChild(0);
		var currentPosition = targetObject.position;

		var targetPosition = Camera.main.transform.position 
		// Place it 60cm in front of the camera
		+ Camera.main.transform.forward * 0.6f
		// additionally move it "up" 20cm perpendicular to the view direction
		+ Camera.main.transform.up * 0.2f;

		targetObject.position = Vector3.MoveTowards(currentPosition, targetPosition, step * Time.deltaTime);

		targetObject.position = Vector3.Lerp(currentPosition, targetPosition, smoothFactor);

		//Instantiate(ThrowObject, new Vector3(0f, 0f, 0f), Quaternion.identity);
		//Instantiate(ThrowObject, transform.position + transform.forward, transform.rotation);
		//Instantiate(ThrowObject, transform.FindChild("pos").position, transform.FindChild("pos").rotation);
		//Instantiate(ThrowObject, SpawnPosition, Quaternion.identity);
		//Instantiate(creation, SpawnPosition, Quaternion.identity);
		//Instantiate(ThrowObject, transform.position + transform.forward*distance, transform.rotation);
		*/
		//Instantiate(ThrowObject, SpawnPosition, Quaternion.identity);
	}

}
