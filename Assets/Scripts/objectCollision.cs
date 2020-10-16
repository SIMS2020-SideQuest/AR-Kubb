using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectCollision : MonoBehaviour
{
    public Rigidbody obj;

    Collider objectName;

    bool hit = false;
    // Start is called before the first frame update
    void Start()
    {
        obj = GetComponent<Rigidbody>();

        obj.useGravity = false;

        Debug.Log("Old: "+ Vector3.Dot(transform.up,Vector3.down));
    }

    // Update is called once per frame
    void Update()
    {
        // Prevent only touched objects to be deleted
        if(objectName.name == "Throwingstick") {
            if( Mathf.Abs(Vector3.Dot(transform.up,Vector3.down)) > 0.825f )
                Destroy(gameObject, 3);
        }    
    }
    void OnCollisionEnter(Collision collider)
    {
        obj.useGravity = true;

        hit = true;

        objectName = collider.collider;

        if(objectName.name == "Throwingstick")
            Debug.Log("New: "+ Vector3.Dot(transform.up,Vector3.down));
    }
}
