using UnityEngine;
using System.Collections.Generic;
using System;

public class LineScript : MonoBehaviour
{
    public List<SoldierMarker> soldierMarkerList = new();
    public bool selected = false;
    public int unitWidth = 10;
    int i = 0, j = 0;
    LineRenderer lr;
    Vector3 lineStartPosition, lineColumnDirection, lineRowDirection, movement, previousLineStartPosition, previousLineColumnDirection, previousLineRowDirection;

    void Start(){
        lr = GetComponent<LineRenderer>();
        lineStartPosition = lr.GetPosition(0);
        lineRowDirection = lr.GetPosition(1) - lr.GetPosition(0);
        lineColumnDirection = new Vector3(lineRowDirection.z * 1, lineRowDirection.y * 1, lineRowDirection.x * -1);
        foreach (SoldierMarker soldierMarker in soldierMarkerList) {
            soldierMarker.marking = true;
            movement = new Vector3(lineStartPosition.x + lineRowDirection.normalized.x * i + lineColumnDirection.normalized.x * j, 19.1f, lineStartPosition.z + lineRowDirection.normalized.z * i + lineColumnDirection.normalized.z * j);
            soldierMarker.transform.position = movement;
            i++;
            if (i > unitWidth - 1) {
                j++;
                i = 0;
            }
        }
    }
    public void AddMarker(SoldierMarker soldier) {
        soldierMarkerList.Add(soldier);
    }
    void FixedUpdate() {
        i = 0;
        j = 0;
        lineStartPosition = lr.GetPosition(0);
        lineRowDirection = lr.GetPosition(1) - lr.GetPosition(0);
        lineColumnDirection = new Vector3(lineRowDirection.z * 1, lineRowDirection.y * 1, lineRowDirection.x * -1);
        if (Input.GetMouseButton(1) && selected) {
            if ((int)Math.Round(Vector3.Distance(lr.GetPosition(0), lr.GetPosition(1)), 1) < soldierMarkerList.Count / 30 + 2) {
                Debug.Log("hi");
                lineStartPosition = lr.GetPosition(0) - previousLineRowDirection / 2;
                lineColumnDirection = previousLineColumnDirection;
                lineRowDirection = previousLineRowDirection;
            }
            foreach (SoldierMarker soldierMarker in soldierMarkerList) {
                soldierMarker.marking = true;
                movement = new Vector3(lineStartPosition.x + lineRowDirection.normalized.x * i + lineColumnDirection.normalized.x * j, 19.1f, lineStartPosition.z + lineRowDirection.normalized.z * i + lineColumnDirection.normalized.z * j);
                soldierMarker.transform.position = movement;
                i++;
                if (i > unitWidth - 1) {
                    j++;
                    i = 0;
                }
            }
        }
        if (Input.GetMouseButtonUp(1) && selected) {
            if ((int)Math.Round(Vector3.Distance(lr.GetPosition(0), lr.GetPosition(1)), 1) < soldierMarkerList.Count / 30 + 2) {
                Debug.Log("hi");
                lineStartPosition = lr.GetPosition(0) - previousLineRowDirection / 2;
                lineColumnDirection = previousLineColumnDirection;
                lineRowDirection = previousLineRowDirection;
            }
            foreach (SoldierMarker soldierMarker in soldierMarkerList) {
                movement = new Vector3(lineStartPosition.x + lineRowDirection.normalized.x * i + lineColumnDirection.normalized.x * j, 19.1f, lineStartPosition.z + lineRowDirection.normalized.z * i + lineColumnDirection.normalized.z * j);
                soldierMarker.transform.position = movement;
                i++;
                if (i > unitWidth - 1) {
                    j++;
                    i = 0;
                }
            }
            previousLineStartPosition = lineStartPosition;
            previousLineColumnDirection = lineColumnDirection;
            previousLineRowDirection = lineRowDirection;
        }
    }
}
