using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemysAttack : MonoBehaviour{
    SphereCollider attackRange;
    PlayerAndSoldier target;
    List<PlayerAndSoldier> inRange = new();
    Vector3 closest;
    float damage = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        attackRange = GetComponent<SphereCollider>();
        attackRange.isTrigger = true;
    }
    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<PlayerAndSoldier>() != null) {
            if (target == null) {
                target = other.GetComponent<PlayerAndSoldier>();
                AttackDelay(target);
            }
            inRange.Add(target.GetComponent<PlayerAndSoldier>());
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.GetComponent<PlayerAndSoldier>() != null) {
            inRange.Remove(other.GetComponent<PlayerAndSoldier>());
            closest = transform.position + new Vector3(100, 0, 100);
            foreach (PlayerAndSoldier friendly in inRange) {
                if ((friendly.transform.position - transform.position).magnitude < (closest - transform.position).magnitude) {
                    closest = friendly.transform.position;
                    target = friendly.GetComponent<PlayerAndSoldier>();
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

    private IEnumerator AttackDelay(PlayerAndSoldier DealingDamage) {
        while (DealingDamage != null) {
            yield return new WaitForSeconds(3);
            try {
                DealingDamage.transform.GetComponent<SoldierScript>().TakeDamage(damage);
            }
            catch {
                DealingDamage.transform.GetComponent<Player>().TakeDamage(damage);
            }
        }
    }

    void Update()
    {
        
    }
}
