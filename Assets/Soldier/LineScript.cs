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
        if (Input.GetMouseButton(1) && selected) {
            Debug.Log("hi");
            int i = 0;
            int j = 0;
            foreach (SoldierMarker soldierMarker in soldierMarkerList) {
                Debug.Log("moving marker");
                soldierMarker.stm.moving = false;
                soldierMarker.marking = true;
                movement = new Vector3(lineStartposition.x + lineRowDirection.normalized.x * i + lineColumnDirection.normalized.x * j, 19.3f, lineStartposition.z + lineRowDirection.normalized.z * i + lineColumnDirection.normalized.z * j);
                soldierMarker.transform.position = movement;
                i++;
                if (i > unitWidth - 1) {
                    j++;
                    i = 0;
                }
            }
        }
        if (Input.GetMouseButtonUp(1) && selected) {
            int i = 0;
            int j = 0;
            lineStartposition = lr.GetPosition(0);
            lineRowDirection = lr.GetPosition(1) - lr.GetPosition(0);
            lineColumnDirection = new Vector3(lineRowDirection.z * 1, lineRowDirection.y * 1, lineRowDirection.x * -1);
            foreach (SoldierMarker soldierMarker in soldierMarkerList) {
                movement = new Vector3(lineStartposition.x + lineRowDirection.normalized.x * i + lineColumnDirection.normalized.x * j, 19.3f, lineStartposition.z + lineRowDirection.normalized.z * i + lineColumnDirection.normalized.z * j);
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
