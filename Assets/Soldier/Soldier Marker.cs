using UnityEngine;

public class SoldierMarker : MonoBehaviour
{
    public SoldierMoveTomarker stm;
    public LineScript ls;
    public bool marking;
    void Awake(){
        stm = this.transform.parent.GetChild(0).GetComponent<SoldierMoveTomarker>();
        ls = this.transform.parent.parent.GetChild(0).GetComponent<LineScript>();
        ls.AddMarker(this);
    }
    void Update(){
        stm.moving = !marking;
    }
}
