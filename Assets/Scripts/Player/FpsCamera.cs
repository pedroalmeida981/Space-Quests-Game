using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsCamera : MonoBehaviour
{
    [Range(0, 90)] [SerializeField] private float limitRot_Up = 85;
    [Range(0, 90)] [SerializeField] private float limitRot_Down = 85;
    private float currentRotX = 0;

    [HideInInspector] public Transform targetEject = null;
    [HideInInspector] public Transform armHolder = null;

    // Initialize targets to eject and arm to hold the items
    public void Init()
    {
        targetEject = transform.Find("Targets/TargetEject");
        armHolder = transform.Find("ArmHolder");
    }

    // Camera rotation
    public void CameraRotation(float inpRotV)
    {
        currentRotX -= inpRotV;
        currentRotX = Mathf.Clamp(currentRotX, -limitRot_Up, limitRot_Down);

        transform.localRotation = Quaternion.Euler(currentRotX, 0, 0);
    }
}
