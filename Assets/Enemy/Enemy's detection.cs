using UnityEngine;

public class detectionofenemyscript : MonoBehaviour{
    EnemyScript thisEnemy;
    SphereCollider detectionRange;
    void Start(){
        thisEnemy = transform.parent.GetComponent<EnemyScript>();
        detectionRange = GetComponent<SphereCollider>();
        detectionRange.isTrigger = true;
    }
    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<PlayerAndSoldier>() != null) {
            if (other is CapsuleCollider) {
                CapsuleCollider other1 = (CapsuleCollider)other;
                thisEnemy.inRangeList.Add(other1);
            }
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.GetComponent <PlayerAndSoldier>() != null) {
            if (other is CapsuleCollider) {
                CapsuleCollider other1 = (CapsuleCollider)other;
                thisEnemy.inRangeList.Remove(other1);
            }
        }
    }
    void Update(){
        
    }
}
