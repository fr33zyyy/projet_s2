using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class CamerController : MonoBehaviour
{
    [SerializeField] Transform followTarget;
    [SerializeField] float distance = 5;
    [SerializeField] float rotationSpeed = 2f;
    [SerializeField] float minVerticalAngle = -45;
    [SerializeField] float maxVerticalAngle = 45;

    [SerializeField] Vector2 framingOffset;
    [SerializeField] bool InvertX;
    [SerializeField] bool InvertY;

    float rotationX;
    float rotationY;

    float intervertXVal;
    float intervertYVal;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        intervertXVal = (InvertX) ? -1 : 1;
        intervertYVal = (InvertY) ? -1 : 1;

        rotationX += Input.GetAxis("Mouse Y") * intervertYVal * rotationSpeed;
        rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);

        rotationY += Input.GetAxis("Mouse X") * intervertXVal * rotationSpeed;

        var targetRotation = Quaternion.Euler(rotationY,rotationY,0);

        var focusPosition = followTarget.position + new Vector3(framingOffset.x,framingOffset.y);

        transform.position = focusPosition - targetRotation * new Vector3(0, 0, distance);
        transform.rotation = targetRotation;

    }
}
