using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldierattack : MonoBehaviour
{
    SphereCollider attackRange;
    EnemyScript target;
    [SerializeField] List<EnemyScript> inRange = new();
    Vector3 closest;
    float damage = 2f;
    float counter = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        attackRange = GetComponent<SphereCollider>();
        attackRange.isTrigger = true;
    }
    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<EnemyScript>() != null) {
            if (target == null) {
                target = other.transform.GetComponent<EnemyScript>();
                Debug.Log("where is my attackdelay");
                inRange.Add(target);
            }
            else {
                inRange.Add(other.GetComponent<EnemyScript>());
            }
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.GetComponent<EnemyScript>() != null) {
            inRange.Remove(other.GetComponent<EnemyScript>());
            closest = transform.position + new Vector3(100, 0, 100);
            foreach (EnemyScript enemy in inRange) {
                if ((enemy.transform.position - transform.position).magnitude < (closest - transform.position).magnitude) {
                    closest = enemy.transform.position;
                    target = enemy;
                }
            }
            if (closest == transform.position + new Vector3(100, 0, 100)) {
                target = null;
            }
        }
    }
    private void FixedUpdate() {
        if (target != null) {
            if (counter <= 0) {
                target.TakeDamage(damage);
                counter = 3;
            }
            else {
                counter -= Time.deltaTime;
            }
        }
    }
}
