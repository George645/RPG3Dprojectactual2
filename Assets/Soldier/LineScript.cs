using UnityEngine;
using System.Collections.Generic;

public class LineScript : MonoBehaviour
{
    public List<SoldierMarker> soldierMarkerList = new();
    public bool selected = false;
    public int unitWidth;
    LineRenderer lr;
    Vector3 lineStartposition, lineColumnDirection, lineRowDirection, movement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }
    public void AddMarker(SoldierMarker soldier) {
        soldierMarkerList.Add(soldier);
    }

    // Update is called once per frame
    void Update() {
        Debug.Log(unitWidth);
        if (Input.GetMouseButtonUp(1) && selected) {
            int i = 0;
            int j = 0;
            lineStartposition = lr.GetPosition(0);
            lineRowDirection = lr.GetPosition(1) - lr.GetPosition(0);
            lineColumnDirection = new Vector3(lineRowDirection.z * 1, lineRowDirection.y * 1, lineRowDirection.x * -1);
            foreach (SoldierMarker soldierMarker in soldierMarkerList) {
                movement = new Vector3(lineStartposition.x + lineRowDirection.normalized.x * i + lineColumnDirection.normalized.x * j, 21f, lineStartposition.z + lineRowDirection.normalized.z * i + lineColumnDirection.normalized.z * j);
                soldierMarker.transform.position = movement;
                i++;
                if (i > unitWidth - 1) {
                    j++;
                    i = 0;
                }
            }
        }
    }
}
