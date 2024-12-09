using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class Soldierattack : MonoBehaviour
{
    SphereCollider attackRange;
    EnemyScript target;
    [SerializeField] List<EnemyScript> inRange = new();
    Vector3 closest;
    float damage = 10f;
    float counter = 0;
    void Start() {
        attackRange = GetComponent<SphereCollider>();
        attackRange.isTrigger = true;
    }
    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<EnemyScript>() != null) {
            if (target == null) {
                target = other.transform.GetComponent<EnemyScript>();
                inRange.Add(target);
            }
            else {
                inRange.Add(other.GetComponent<EnemyScript>());
            }
        }
    }
    private void theForLoop() {
        foreach (EnemyScript enemy in inRange) {
            if (enemy == null) {
                inRange.Remove(enemy);
                theForLoop();
                break;
            }
            if ((enemy.transform.position - transform.position).magnitude < (closest - transform.position).magnitude) {
                closest = enemy.transform.position;
                target = enemy;
            }
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.GetComponent<EnemyScript>() != null) {
            inRange.Remove(other.GetComponent<EnemyScript>());
            closest = transform.position + new Vector3(100, 0, 100);
            try {
                theForLoop();
            }
            catch (MissingReferenceException e) {
                Debug.Log(e);
            }
            if (closest == transform.position + new Vector3(100, 0, 100)) {
                target = null;
            }
        }
    }
    private void FixedUpdate() {
        if (target != null) {
            var lookPos = target.transform.position - transform.parent.position;
            lookPos = new Vector3(-lookPos.x, 0, -lookPos.z);
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.parent.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);
            if (counter <= 0) {
                target.TakeDamage(damage);
                counter = 3;
            }
            else {
                counter -= Time.deltaTime;
            }
        }
        else{
            var lookPos = transform.parent.parent.parent.GetChild(0).GetComponent<LineScript>().previousLineColumnDirection;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.parent.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);
        }
    }
}
