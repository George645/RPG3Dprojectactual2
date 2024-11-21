using UnityEngine;

public class Playermovement : MonoBehaviour
{
    int level = 0, experience = 0;
    public Rigidbody rb;
    private Vector3 movement = new();
    Vector3 moveDirection = Vector3.zero;
    float horizontal, vertical, mouseX, mouseY;
    bool jump;
    [SerializeField] int jumpHeight = 300, gravity = 3, speed = 5;
    float sensitivity = 3;
    private void Start() {
        Time.timeScale = 1;
        transform.rotation.Set(0f, 0f, 0f, 0f);
    }
    // Update is called once per frame
    public void Update() {
        transform.rotation.Set(0f, 0f, 0f, 0f);
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        jump = Input.GetButton("Jump");
    }
    public void FixedUpdate() {
        rb.AddForce(transform.forward * vertical * speed);
        rb.AddForce(transform.right * horizontal * speed);
        
        if (jump){
            Debug.Log("jump");
            rb.AddForce(0, jumpHeight, 0);
        }
        transform.TransformDirection(moveDirection);
        float turner = mouseX * sensitivity;
        if (turner != 0){
            transform.eulerAngles += new Vector3(0f, turner, 0f);
        }
        float upDown = mouseY * sensitivity;
        if (upDown != 0){
            transform.eulerAngles += new Vector3(-upDown, 0f, 0f);
        }
        transform.Translate(moveDirection);
    }
}
