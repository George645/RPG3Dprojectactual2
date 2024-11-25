using System;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;


public class CharMover : MonoBehaviour {
    public CharacterController controller;
    
    private float verticalVelocity, groundedTimer, playerSpeed = 2.0f, jumpHeight = 1.0f, gravityValue = 9.81f;
    [SerializeField] int speed = 5;
    float turner;
    LineScript ol;
    LineRenderer lr;
    Vector3 mousePosStart, mousePosEnd;
    int unitWidth;


    private void Start() {
        controller = gameObject.GetComponent<CharacterController>();
    }
    private void Update() {
        turner = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump")) {
            if (groundedTimer > 0) {
                groundedTimer = 0;
                verticalVelocity += Mathf.Sqrt(jumpHeight * 2 * gravityValue);
            }
        }
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) {
                if (hit.transform.name.Contains("Soldier")) {
                    ol = hit.transform.parent.parent.GetChild(0).GetComponent<LineScript>();
                    ol.selected = true;
                    lr = hit.transform.parent.parent.GetChild(0).GetComponent<LineRenderer>();
                }
                else {
                    if (ol != null) {
                        ol.selected = false;
                        ol = null;
                    }
                }
            }
            else{
                if (ol != null) {
                    ol.selected = false;
                    ol = null;
                    lr = null;
                }
            }
        }
        Debug.Log(Math.Abs(mousePosStart.magnitude - mousePosEnd.magnitude));
        if (Math.Abs(mousePosStart.magnitude - mousePosEnd.magnitude) <= 3) {
            unitWidth = 3;
        }
        else if (Math.Abs(mousePosStart.magnitude -  mousePosEnd.magnitude) <= 10){
            unitWidth = (int)Math.Abs(mousePosStart.magnitude - mousePosEnd.magnitude);
        }
        else if (Math.Abs(mousePosStart.magnitude - mousePosEnd.magnitude) >= 10) {
            unitWidth = 10;
        }
        Debug.Log(unitWidth);
        if (Input.GetMouseButtonDown(1) && ol != null) {
            RaycastHit hit;
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);
            mousePosStart = hit.point;
            foreach(SoldierMarker sm in ol.soldierMarkerList) {
                sm.GetComponent<MeshRenderer>().enabled = true;
            }
        }
        else if (Input.GetMouseButton(1) && ol != null) {
            RaycastHit hit;
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);
            mousePosEnd = hit.point;
        }
        else if (Input.GetMouseButtonUp(1) && ol != null) {
            RaycastHit hit;
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);
            mousePosEnd = hit.point;
            ol.unitWidth = unitWidth;
            foreach(SoldierMarker sm in ol.soldierMarkerList) {
                sm.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
    void FixedUpdate() {
        if (mousePosStart != new Vector3() && mousePosEnd != new Vector3()) {
            lr.SetPosition(0, mousePosStart);
            lr.SetPosition(1, mousePosEnd);
        }
        transform.eulerAngles += new Vector3(0, turner * 1.5f, 0);
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
        verticalVelocity -= gravityValue * Time.deltaTime;
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move *= playerSpeed;
        if (move.magnitude > 0.05f) {
            gameObject.transform.forward = move;
        }
        if (Input.GetButtonDown("Jump")) {
            if (groundedTimer > 0) {
                groundedTimer = 0;
                verticalVelocity += Mathf.Sqrt(jumpHeight * 2 * gravityValue);
            }
        }
        move.y = verticalVelocity;
        controller.Move(move * Time.deltaTime * speed);
    }
}