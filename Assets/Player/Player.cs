using System;
using Unity.Cinemachine;
using UnityEngine;


public class Player : MonoBehaviour {
    public CharacterController controller;
    public Camera followingCamera;
    
    private float verticalVelocity, groundedTimer, playerSpeed = 2.0f, jumpHeight = 1.0f, gravityValue = 9.81f;
    [SerializeField] int speed = 5;
    LineScript ol;
    LineRenderer lr;
    Vector3 mousePosStart, mousePosEnd;
    int unitWidth, mask, previousUnitWidth = 10;

    float health = 100;
    int maxHealth;
    float healingCooldown = 0f;
    float healingFactor = 0.2f;

    double rotation;
    double startZ = 10f;
    double startX = 0f;
    Vector3 startMousePosition;

    public static Player instance;

    private void Start() {
        if (instance == null) {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else {
            Destroy(this.gameObject);
        }
        controller = gameObject.GetComponent<CharacterController>();
    }
    private void CheckIfClickingOnUnit() {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit) && hit.transform.name.Contains("Soldier")) {
            ol = hit.transform.parent.parent.GetChild(0).GetComponent<LineScript>();
            ol.selected = true;
            previousUnitWidth = ol.unitWidth;
            lr = hit.transform.parent.parent.GetChild(0).GetComponent<LineRenderer>();
        }
        else {
            if (ol != null) {
                foreach (SoldierMarker sm in ol.soldierMarkerList) {
                    sm.marking = false;
                    sm.transform.position = new Vector3(sm.stm.endPosition.x, 19, sm.stm.endPosition.z);
                }
                ol.selected = false;
                ol = null;
                lr = null;
            }
        }
    }
    private void GetUnitWidth() {
        unitWidth = (int)Math.Round(Vector3.Distance(mousePosStart, mousePosEnd), 1);
        if (unitWidth < 3) {
            unitWidth = previousUnitWidth;
        }
        else {
            unitWidth = Math.Clamp(unitWidth, 3, ol.soldierMarkerList.Count / 3);
        }
    }
    private void ShowMarkers(bool setTo) {
        foreach (SoldierMarker soldierMarker in ol.soldierMarkerList) {
            soldierMarker.GetComponent<MeshRenderer>().enabled = setTo;
            soldierMarker.marking = setTo;
        }
    }
    private void Death() {
        if (health < 0) {
            Destroy(this.gameObject);
        }
    }
    public void TakeDamage(float damage) {
        health -= damage;
        healingCooldown = 10;
    }
    private void Update() {
        Death();
        if (Input.GetMouseButtonDown(2)) {
            startMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(2)) {
            rotation = (Input.mousePosition.x - startMousePosition.x) / 10;
            startMousePosition = Input.mousePosition;
        }
        if (Input.GetButtonDown("Jump") && groundedTimer > 0) {
            groundedTimer = 0;
            verticalVelocity += Mathf.Sqrt(jumpHeight * 2 * gravityValue);
        }
        if (Input.GetMouseButtonDown(0)) {
            CheckIfClickingOnUnit();
        }
        if (ol != null){
            GetUnitWidth();
            mask = LayerMask.GetMask("Default");
            if (Input.GetMouseButtonDown(1)) {
                RaycastHit hit;
                Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 10000f, mask);
                mousePosStart = hit.point;
                ShowMarkers(true);
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
                ol.unitWidth = unitWidth;
                mousePosEnd = hit.point;
                previousUnitWidth = unitWidth;
                ShowMarkers(false);
            }
        }
    }
    void FixedUpdate() {
        healingCooldown -= Time.deltaTime;
        if (health < maxHealth && healingCooldown < 0f) {
            health += healingFactor;
        }
        else if (health > maxHealth) {
            health = maxHealth;
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

        Vector3 move = new Vector3((followingCamera.transform.position.x - transform.position.x) * Input.GetAxis("Vertical") + (followingCamera.transform.position.z - transform.position.z) * Input.GetAxis("Horizontal"), 0, (followingCamera.transform.position.z - transform.position.z) * Input.GetAxis("Vertical") + (followingCamera.transform.position.x - transform.position.x) * Input.GetAxis("Horizontal"));
        move = move.normalized;
        move *= playerSpeed;
        if (move.magnitude > 0.05f) {
            gameObject.transform.forward = move;
        }
        move.y = verticalVelocity;
        controller.Move(move * Time.deltaTime * speed);
    }
}