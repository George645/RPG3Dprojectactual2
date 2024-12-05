using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldierattack : MonoBehaviour
{

    SphereCollider attackRange;
    EnemyScript target;
    List<EnemyScript> inRange = new();
    Vector3 closest;
    float damage = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        attackRange = GetComponent<SphereCollider>();
        attackRange.isTrigger = true;
    }
    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<PlayerAndSoldier>() != null) {
            if (target == null) {
                target = other.GetComponent<EnemyScript>();
                AttackDelay(target);
                inRange.Add(target);
            }
            else {
                inRange.Add(other.GetComponent<EnemyScript>());
            }
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.GetComponent<PlayerAndSoldier>() != null) {
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
            else {
                AttackDelay(target);
            }
        }
    }

    private IEnumerator AttackDelay(EnemyScript DealingDamage) {
        while (DealingDamage != null) {
            yield return new WaitForSeconds(3);
            try {
                DealingDamage.TakeDamage(damage);
            }
            catch { }
        }
    }
}
