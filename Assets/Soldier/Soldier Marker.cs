using UnityEngine;

public class SoldierMarker : MonoBehaviour
{
    public SoldierMoveTomarker stm;
    bool selected = false;
    LineScript ol;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ol = this.transform.parent.parent.GetChild(0).GetComponent<LineScript>();
    }

    // Update is called once per frame
    void Update()
    {
        selected = ol.selected;
    }
}
