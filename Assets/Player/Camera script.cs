using Unity.Cinemachine;
using UnityEngine;
using System;

public class Camerascript : MonoBehaviour
{
    double rotation;
    double startZ = 10f;
    double startX = 0f;
    CinemachineFollow HopefullICanUseThisToTurnTheCamera;
    Vector3 startMousePosition;
    void Start()
    {
        HopefullICanUseThisToTurnTheCamera = GetComponent<CinemachineFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        HopefullICanUseThisToTurnTheCamera.FollowOffset.x = (float)((float)(Math.Sin(rotation * Math.PI / 180)) * startZ) + (float)((float)(Math.Cos(rotation * Math.PI / 180)) * startX);
        HopefullICanUseThisToTurnTheCamera.FollowOffset.z = (float)((float)(Math.Cos(rotation * Math.PI / 180)) * startZ) + (float)((float)(-Math.Sin(rotation * Math.PI / 180)) * startX);
        if (Input.GetMouseButtonDown(2)) {
            startMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(2)) {
            rotation = (Input.mousePosition.x - startMousePosition.x)/10;
            startZ = HopefullICanUseThisToTurnTheCamera.FollowOffset.z;
            startX = HopefullICanUseThisToTurnTheCamera.FollowOffset.x;
            startMousePosition = Input.mousePosition;
        }
    }
}
