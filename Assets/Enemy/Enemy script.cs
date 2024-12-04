using UnityEngine;
using System.Collections.Generic;

public class EnemyScript : MonoBehaviour{
    SphereCollider detectionSphere;
    SphereCollider attackRangeSphere;
    public List<CapsuleCollider> inRangeList = new();
    float health = 100;
    int maxHealth;
    Vector3 target;
    [SerializeField]Rigidbody rb;
    void Start() {
        rb = GetComponent<Rigidbody>();
        detectionSphere = transform.GetChild(0).GetComponent<SphereCollider>();
        attackRangeSphere = transform.GetChild(1).GetComponent<SphereCollider>();
    }
    void Death() {
        if (health <= 0) {
            Destroy(this.gameObject);
        }
    }
    void Update(){
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
        else if (inRangeList.Count == 1){
            try {
                target = inRangeList[0].transform.position;
            }
            catch { }
        }
        else {
            target = this.transform.position;
        }
        rb.AddForce((target - this.transform.position).normalized * Time.deltaTime * 300);
        Debug.Log(inRangeList.Count);
    }
}
