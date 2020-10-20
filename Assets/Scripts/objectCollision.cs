using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectCollision : MonoBehaviour
{
    public Rigidbody obj;

    Collider objectCollider;

    // Start is called before the first frame update
    void Start()
    {
        obj = GetComponent<Rigidbody>();

        obj.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Prevent only touched objects to be deleted
        if(objectCollider.name == "Throwingstick") {
            if( Mathf.Abs(Vector3.Dot(transform.up,Vector3.down)) > 0.825f )
                Destroy(this.gameObject, 3);
        }    
    }
    void OnCollisionEnter(Collision collider)
    {
        obj.useGravity = true;

        objectCollider = collider.collider;
    }
}
