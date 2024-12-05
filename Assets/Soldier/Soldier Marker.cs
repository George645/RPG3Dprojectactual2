using UnityEngine;

public class SoldierMarker : MonoBehaviour
{
    public SoldierScript stm;
    public LineScript ls;
    public bool marking;
    void Awake(){
        stm = this.transform.parent.GetChild(0).GetComponent<SoldierScript>();
        ls = this.transform.parent.parent.GetChild(0).GetComponent<LineScript>();
        ls.AddMarker(this);
    }
    void Update(){
        stm.moving = !marking;
    }
}
