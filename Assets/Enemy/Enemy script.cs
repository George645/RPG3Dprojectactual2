using UnityEngine;
using System.Collections.Generic;

public class EnemyScript : MonoBehaviour{
    public List<CapsuleCollider> inRangeList = new();
    [SerializeField] float health = 100;
    float healingFactor = 0.1f;
    float healingCooldown = 0f;
    [SerializeField] int maxHealth;
    Vector3 target;
    Rigidbody rb;
    void Start() {
        rb = GetComponent<Rigidbody>();
        maxHealth = (int)health;
    }
    void Death() {
        if (health <= 0) {
            Destroy(this.gameObject);
        }
    }
    public void TakeDamage(float damage) {
        health -= damage;
        healingCooldown = 10f;
    }
    void Update() {
        Death();
        if (inRangeList.Count > 1) {
            target = new Vector3(transform.position.x + 1000, transform.position.y, transform.position.z + 1000);
            foreach (Collider c in inRangeList) {
                if ((this.transform.position - c.transform.position).magnitude < (this.transform.position - target).magnitude) {
                    target = c.transform.position;
                    Debug.Log(c.name);
                    Debug.Log(inRangeList[0].name);
                    Debug.Log(inRangeList[1].name);
                }
            }
        }
        else if (inRangeList.Count == 1) {
            try {
                target = inRangeList[0].transform.position;
            }
            catch { }
        }
        else {
            target = this.transform.position;
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
        rb.AddForce((target - this.transform.position).normalized * Time.deltaTime * 300);
    }
}
