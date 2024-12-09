using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class EnemysAttack : MonoBehaviour{
    SphereCollider attackRange;
    PlayerAndSoldier target;
    [SerializeField]List<PlayerAndSoldier> inRange = new();
    Vector3 closest;
    float damage = 2f;
    float counter = 0;
    void Start(){
        attackRange = GetComponent<SphereCollider>();
        attackRange.isTrigger = true;
    }
    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<PlayerAndSoldier>() != null) {
            if (target == null) {
                target = other.GetComponent<PlayerAndSoldier>();
            }
            inRange.Add(target.GetComponent<PlayerAndSoldier>());
        }
    }
    void TheForLoop() {
        foreach (PlayerAndSoldier friendly in inRange) {
            if (friendly == null) {
                inRange.Remove(friendly);
                TheForLoop();
                break;
            }
            if ((friendly.transform.position - transform.position).magnitude < (closest - transform.position).magnitude) {
                closest = friendly.transform.position;
                target = friendly.GetComponent<PlayerAndSoldier>();
            }
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.GetComponent<PlayerAndSoldier>() != null) {
            inRange.Remove(other.GetComponent<PlayerAndSoldier>());
            closest = transform.position + new Vector3(100, 0, 100);
            TheForLoop();
            if (closest == transform.position + new Vector3(100, 0, 100)) {
                target = null;
            }
        }
    }
    private void FixedUpdate() {
        if (target != null) {
            if (counter <= 0) {
                try {
                    target.GetComponent<SoldierScript>().TakeDamage(damage);
                }
                catch {
                    target.GetComponent<Player>().TakeDamage(damage);
                }
                counter = 3;
            }
            else {
                counter -= Time.deltaTime;
            }
        }
    }
}