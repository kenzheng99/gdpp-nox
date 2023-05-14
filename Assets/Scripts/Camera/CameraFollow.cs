using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [SerializeField] private GameObject objectToFollow;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float followSpeed;
    [SerializeField] private Transform groundBoundary;
    [SerializeField] private float groundOffset = 0;
    [SerializeField] private Transform topBoundary;
    [SerializeField] private Transform leftBoundary;
    [SerializeField] private Transform rightBoundary;

    void Start() {
        Vector3 targetPosition = getTargetPosition();
        transform.position = targetPosition;
    }

    void FixedUpdate() {
        
        Vector3 targetPosition = getTargetPosition();
        float distance = (targetPosition - transform.position).magnitude;
        Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, followSpeed * distance * Time.fixedDeltaTime);
        transform.position = newPosition;
    }

    private Vector3 getTargetPosition() {
        Vector3 targetPosition = objectToFollow.transform.position + offset;

        float cameraVerticalSize = GetComponent<Camera>().orthographicSize;
        float cameraHorizontalSize = cameraVerticalSize * 16f / 9f;
        float minY = groundBoundary ? groundBoundary.position.y - groundOffset + cameraVerticalSize: float.NegativeInfinity;
        float maxY = topBoundary ? topBoundary.position.y - groundOffset - cameraVerticalSize: float.PositiveInfinity;
        float minX = leftBoundary ? leftBoundary.position.x + cameraHorizontalSize : float.NegativeInfinity;
        float maxX = rightBoundary ? rightBoundary.position.x - cameraHorizontalSize : float.PositiveInfinity;

        targetPosition = new Vector3(
            Mathf.Clamp(targetPosition.x, minX, maxX),
            Mathf.Clamp(targetPosition.y, minY, maxY),
            targetPosition.z
        );
        
        return targetPosition;
    }
}