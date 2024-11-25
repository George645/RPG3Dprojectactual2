using UnityEngine;
using System.Collections.Generic;

public class LineScript : MonoBehaviour
{
    List<SoldierMoveTomarker> soldierList = new();
    public bool selected = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void AddSoldier(SoldierMoveTomarker soldier) {
        soldierList.Add(soldier);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
