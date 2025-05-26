using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SmoothFollow : MonoBehaviour
{
    [SerializeField] private Transform target; // The target to follow
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 offset; // Offset from the target position
    [SerializeField] private Vector3 rotationOffset; // Offset for the rotation


    // Start is called before the first frame update
    void Start()
    {
        if (target == null)
        {
            Debug.LogError("Target is not assigned in SmoothFollow script.");
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;

        // Calculate the desired position with the offset
        Vector3 desiredPosition = target.position + target.TransformDirection(offset);

        // Smoothly interpolate to the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        // Update the position of this GameObject
        transform.position = smoothedPosition;

        // Also make the rotation follow the target
        if (target.rotation == null) return;
        Quaternion smoothedRotation = Quaternion.Slerp(transform.rotation, target.rotation*Quaternion.Euler(rotationOffset), smoothSpeed * Time.deltaTime);
        transform.rotation = smoothedRotation;
    }

    void OnValidate()
    {
        transform.position = target != null ? target.position + target.TransformDirection(offset) : transform.position;
        transform.rotation = target != null ? target.rotation*Quaternion.Euler(rotationOffset) : transform.rotation;
    }
}
