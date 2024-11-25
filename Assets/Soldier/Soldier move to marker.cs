using System;
using Unity.VisualScripting;
using UnityEngine;

public class SoldierMoveTomarker : MonoBehaviour
{
    SoldierMarker sm;
    Vector3 movement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sm = this.transform.parent.GetChild(1).GetComponent<SoldierMarker>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position = new Vector3(transform.position.x, 20f, transform.position.z);
        if (Math.Abs(sm.transform.position.x - transform.position.x) > 0 || Math.Abs(sm.transform.position.y - transform.position.y) > 0) {
            movement = new Vector3((sm.transform.position.x - this.transform.position.x), 0f, sm.transform.position.z - this.transform.position.z);
            if (movement.magnitude > 0.1) {
                movement = movement.normalized /10;
            }
            else {
                Debug.Log("need more lee way");
            }
            transform.position += movement;
            //Debug.Log(transform.position.magnitude + ", " + sm.transform.position.magnitude);
        }
        else {
        }
        //(new Vector3(sm.transform.position.x, sm.transform.position.y, sm.transform.position.z));
        
    }
}
