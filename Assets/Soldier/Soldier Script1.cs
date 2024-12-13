using UnityEngine;

public class SoldierSpawner : MonoBehaviour{
    float counter = 10f;
    public GameObject prefab;
    public void TakeDamage(float damage) {
        counter = 1f;
    }
    void FixedUpdate(){
        if (counter <= 0) {
            Instantiate(prefab);
            counter = 30f;
        }
        Debug.Log(counter);
        counter -= Time.deltaTime;
    }
}
