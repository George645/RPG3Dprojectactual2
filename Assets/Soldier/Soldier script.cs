using System;
using Unity.VisualScripting;
using UnityEngine;

public class SoldierMoveTomarker : MonoBehaviour
{
    SoldierMarker sm;
    Vector3 movement;
    public Vector3 endPosition;
    public bool moving = true;
    bool start = true;
    float health = 100;
    int maxHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start(){
        maxHealth = (int)health;
        sm = this.transform.parent.GetChild(1).GetComponent<SoldierMarker>();
        endPosition = sm.transform.position;
    }
    private void Death() {
        if (health < 0) {
            this.transform.parent.parent.GetChild(0).GetComponent<LineScript>().RemoveSoldier(this.transform.parent.GetComponent<SoldierMarker>());
            Destroy(this.transform.parent);
        }
    }
    public void TakeDamage(float damage) {
        health -= damage;
    }
    // Update is called once per frame
    private void Update() {
        //need something to run after start but before everything else, so used another variable that only lets it run once
        if (moving || start) {
            endPosition = sm.transform.position;
            start = false;
        }
    }
    void FixedUpdate(){
        Death();
        this.transform.position = new Vector3(transform.position.x, 19f, transform.position.z);
        if (Math.Abs(endPosition.x - transform.position.x) > 0 || Math.Abs(endPosition.y - transform.position.y) > 0) {
            movement = new Vector3(endPosition.x - this.transform.position.x, 0f, endPosition.z - this.transform.position.z);
            if (movement.magnitude > 0.1) {
                movement = movement.normalized /10;
            }
            movement *= Time.deltaTime * 60;
            transform.position += movement;
        }
    }
}