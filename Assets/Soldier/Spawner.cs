using UnityEngine;

public class SoldierScript1 : MonoBehaviour{
    float counter = 60f;
    void Start(){
        
    }

    // Update is called once per frame
    void FixedUpdate(){
        if (counter == 0) {
            counter = 60f;
        }
        counter -= Time.deltaTime;
    }
}
