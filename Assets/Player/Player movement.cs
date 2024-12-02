using System;
using UnityEngine;


public class CharMover : MonoBehaviour {
    public CharacterController controller;
    
    private float verticalVelocity, groundedTimer, playerSpeed = 2.0f, jumpHeight = 1.0f, gravityValue = 9.81f;
    [SerializeField] int speed = 5;
    LineScript ol;
    LineRenderer lr;
    Vector3 mousePosStart, mousePosEnd;
    int unitWidth;
    int mask;
    Vector3 previousDirection;
    float turner;

    private void Start() {
        controller = gameObject.GetComponent<CharacterController>();
    }
    private void Update() {
        turner = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && groundedTimer > 0) {
            groundedTimer = 0;
            verticalVelocity += Mathf.Sqrt(jumpHeight * 2 * gravityValue);
        }

        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit) && hit.transform.name.Contains("Soldier")) {
                ol = hit.transform.parent.parent.GetChild(0).GetComponent<LineScript>();
                ol.selected = true;
                lr = hit.transform.parent.parent.GetChild(0).GetComponent<LineRenderer>();
                Debug.Log("hit");
            }
            else{
                if (ol != null) {
                    foreach (SoldierMarker sm in ol.soldierMarkerList) {
                        sm.marking = false;
                        sm.transform.position = new Vector3(sm.stm.endPosition.x, 19, sm.stm.endPosition.z);
                        Debug.Log(sm.transform.position + " " + sm.stm.transform.position);
                    }
                    ol.selected = false;
                    ol = null;
                    lr = null;
                }
            }
        }
        if (ol != null){
            unitWidth = (int)Math.Round(Vector3.Distance(mousePosStart, mousePosEnd), 1);
            unitWidth = Math.Clamp(unitWidth, (int)Math.Sqrt(ol.soldierMarkerList.Count), ol.soldierMarkerList.Count / 3);
            mask = LayerMask.GetMask("Default");
            if (Input.GetMouseButtonDown(1)) {
                RaycastHit hit;
                Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 10000f, mask);
                mousePosStart = hit.point;
                foreach (SoldierMarker sm in ol.soldierMarkerList) {
                    sm.GetComponent<MeshRenderer>().enabled = true;
                    sm.marking = true;
                }
            }
            else if (Input.GetMouseButton(1)) {
                RaycastHit hit;
                Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 10000f, mask);
                mousePosEnd = hit.point;
                ol.unitWidth = unitWidth;
            }
            else if (Input.GetMouseButtonUp(1)) {
                RaycastHit hit;
                Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 10000f, mask);
                mousePosEnd = hit.point;
                if ((int)Math.Round(Vector3.Distance(mousePosStart, mousePosEnd), 1) < (int)Math.Sqrt(ol.soldierMarkerList.Count)) {
                    mousePosStart -= previousDirection / 2;
                    mousePosEnd += previousDirection / 2;
                    unitWidth = (int)Math.Round(Vector3.Distance(mousePosStart, mousePosEnd), 1);
                    unitWidth = Math.Clamp(unitWidth, (int)Math.Sqrt(ol.soldierMarkerList.Count), ol.soldierMarkerList.Count / 3);
                }
                ol.unitWidth = unitWidth;
                previousDirection = mousePosEnd - mousePosStart;
                foreach (SoldierMarker sm in ol.soldierMarkerList) {
                    sm.GetComponent<MeshRenderer>().enabled = false;
                    sm.marking = false;
                }
            }
        }
    }
    void FixedUpdate() {
        if (Input.GetMouseButton(2)) {
            transform.rotation = new Quaternion(0, 0, transform.rotation.z + turner/5, 0);
        }
        try {
            if (mousePosStart != new Vector3() && mousePosEnd != new Vector3()) {
                lr.SetPosition(0, mousePosStart);
                lr.SetPosition(1, mousePosEnd);
            }
        }
        catch  {
        }
        bool groundedPlayer = controller.isGrounded;
        if (groundedPlayer) {
            groundedTimer = 0.2f;
        }
        if (groundedTimer > 0) {
            groundedTimer -= Time.deltaTime;
        }
        if (groundedPlayer && verticalVelocity < 0) {
            verticalVelocity = 0f;
        }
        if (Input.GetButtonDown("Jump") && groundedTimer > 0) {
            groundedTimer = 0;
            verticalVelocity += Mathf.Sqrt(jumpHeight * 2 * gravityValue);
        }
        verticalVelocity -= gravityValue * Time.deltaTime;

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move *= playerSpeed;
        if (move.magnitude > 0.05f) {
            gameObject.transform.forward = move;
        }
        move.y = verticalVelocity;
        controller.Move(move * Time.deltaTime * speed);
    }
}