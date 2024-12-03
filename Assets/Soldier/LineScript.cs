using UnityEngine;
using System.Collections.Generic;
using System;

public class LineScript : MonoBehaviour
{
    public List<SoldierMarker> soldierMarkerList = new();
    public bool selected = false;
    public int unitWidth = 10;
    int i = 0, j = 0, previousUnitWidth;
    LineRenderer lr;
    Vector3 lineStartPosition, lineColumnDirection, lineRowDirection, movement, previousLineColumnDirection, previousLineRowDirection;

    void Start() {
        lr = GetComponent<LineRenderer>();
        unitWidth = (int)Math.Round(Vector3.Distance(lr.GetPosition(0), lr.GetPosition(1)), 1);
        unitWidth = Math.Clamp(unitWidth, (int)Math.Sqrt(soldierMarkerList.Count), soldierMarkerList.Count / 3);
        lineStartPosition = lr.GetPosition(0); //between this line and the lines below it, we get where the line starts (this line), then it finds the direction of the rows (the next line) then it gets the direction of the columns. 
        lineRowDirection = lr.GetPosition(1) - lr.GetPosition(0); // Using these, we can multiply the position in the row (i) by the 
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
        previousUnitWidth = unitWidth;
    }
    public void AddMarker(SoldierMarker soldier) {
        soldierMarkerList.Add(soldier);
    }
    private void Update() {
        lineStartPosition = lr.GetPosition(0);
        lineRowDirection = lr.GetPosition(1) - lr.GetPosition(0);
        if ((int)Math.Round(Vector3.Distance(lr.GetPosition(0), lr.GetPosition(1)), 1) < soldierMarkerList.Count / 30 + 2) {
            lineStartPosition = lr.GetPosition(0) - previousLineRowDirection / 2;
            lineColumnDirection = previousLineColumnDirection;
            lineRowDirection = previousLineRowDirection;
            unitWidth = previousUnitWidth;
        }
        Debug.Log(previousLineColumnDirection + " " + previousLineRowDirection);
        if (Input.GetMouseButtonUp(1) && selected) {
            Debug.Log("HI");
            previousLineColumnDirection = lineColumnDirection;
            previousLineRowDirection = lineRowDirection;
            previousUnitWidth = unitWidth;
        }
    }
    void FixedUpdate() {
        i = 0;
        j = 0;
        lineColumnDirection = new Vector3(lineRowDirection.z * 1, lineRowDirection.y * 1, lineRowDirection.x * -1);
        if (Input.GetMouseButton(1) && selected) {
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
            foreach (SoldierMarker soldierMarker in soldierMarkerList) {
                Math.DivRem(j, 2, out int result);
                movement = new Vector3(lineStartPosition.x + lineRowDirection.normalized.x * i + lineColumnDirection.normalized.x * j, 19.1f, lineStartPosition.z + lineRowDirection.normalized.z * i + lineColumnDirection.normalized.z * j + result * 0.5f);
                soldierMarker.transform.position = movement;
                i++;
                if (i > unitWidth - 1) {
                    j++;
                    i = 0;
                }
            }
            previousLineColumnDirection = lineColumnDirection;
            previousLineRowDirection = lineRowDirection;
        }
    }
}
