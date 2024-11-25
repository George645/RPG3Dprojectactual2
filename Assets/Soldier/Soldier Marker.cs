using UnityEngine;

public class SoldierMarker : MonoBehaviour
{
    private SoldierMoveTomarker stm;
    LineScript ls;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stm = this.transform.parent.GetChild(0).GetComponent<SoldierMoveTomarker>();
        ls = this.transform.parent.parent.GetChild(0).GetComponent<LineScript>();
        ls.AddMarker(this);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
