using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Parallax : MonoBehaviour {
    [SerializeField] private Transform leftBoundary;
    [SerializeField] private Transform rightBoundary;
    [SerializeField] private Transform dreamBackground;
    [SerializeField] private Transform nightmareBackground;
    
    [SerializeField] private float scale;
    private float yOffset;

    public void Start() {
        yOffset = dreamBackground.localPosition.y + transform.position.y;
        
        // set scale based on background image width and camera size to achieve perfect parallax.
        // This means that when the camera is at leftBoundary or rightBoundary
        // we exactly reach the corresponding edge of the background image 
        float backgroundWidth = dreamBackground.GetComponent<SpriteRenderer>().bounds.size.x;
        float cameraVerticalSize = GetComponent<Camera>().orthographicSize * 2;
        float cameraHorizontalSize = cameraVerticalSize * 16f / 9f;
        scale = backgroundWidth - cameraHorizontalSize; 
    }

    public void Update() {
        float leftBoundaryX = leftBoundary.transform.position.x;
        float rightBoundaryX = rightBoundary.transform.position.x;
        
        // set x based on interpolation parameter t
        float t = (transform.position.x - leftBoundaryX) / (rightBoundaryX - leftBoundaryX);
        float newX = (0.5f - t) * scale;
        
        // set y so that the vertical position is fixed in global coordinates
        float newY = yOffset - transform.position.y;
        
        dreamBackground.localPosition = new Vector3(newX, newY, dreamBackground.localPosition.z);
        // assumes backgrounds are the same size
        nightmareBackground.localPosition = new Vector3(newX, newY, nightmareBackground.localPosition.z);
    }
}
