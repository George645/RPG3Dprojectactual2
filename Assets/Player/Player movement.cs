using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnityExampleCharMover : MonoBehaviour {
    public CharacterController controller;
    private float verticalVelocity, groundedTimer, playerSpeed = 2.0f, jumpHeight = 1.0f, gravityValue = 9.81f;
    [SerializeField] int speed = 5;
    float turner;

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
            if (Physics.Raycast(Camera.ScreenToWorldPoint(Input.mousePosition), out RaycastHit hit){
                Console.WriteLine("work on it");
            }
        }
    }
    void FixedUpdate() {
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
            Debug.Log("0");
            if (groundedTimer > 0) {
                groundedTimer = 0;
                verticalVelocity += Mathf.Sqrt(jumpHeight * 2 * gravityValue);
            }
        }
        move.y = verticalVelocity;
        controller.Move(move * Time.deltaTime * speed);
    }
}