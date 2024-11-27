using System;
using Unity.VisualScripting;
using UnityEngine;

public class SoldierMoveTomarker : MonoBehaviour
{
    SoldierMarker sm;
    Vector3 movement, endPosition;
    public bool moving;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sm = this.transform.parent.GetChild(1).GetComponent<SoldierMarker>();
    }

    // Update is called once per frame
    private void Update() {
        if (moving) {
            Debug.Log(moving);
            endPosition = sm.transform.position;
        }
    }
    void FixedUpdate()
    {
        this.transform.position = new Vector3(transform.position.x, 20f, transform.position.z);
        if (Math.Abs(endPosition.x - transform.position.x) > 0 || Math.Abs(endPosition.y - transform.position.y) > 0) {
            movement = new Vector3((endPosition.x - this.transform.position.x), 0f, endPosition.z - this.transform.position.z);
            if (movement.magnitude > 0.1) {
                movement = movement.normalized /10;
            }
            else {
            }
            transform.position += movement;
            //Debug.Log(transform.position.magnitude + ", " + sm.transform.position.magnitude);
        }
        else {
        }
        //(new Vector3(sm.transform.position.x, sm.transform.position.y, sm.transform.position.z));
        
    }
}
