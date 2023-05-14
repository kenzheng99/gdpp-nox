using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawbridge : MonoBehaviour {

    [SerializeField] private float lowerSpeed = 0.01f;
    [SerializeField] private GameObject barrier;
    private Quaternion initialRotation;
    private readonly Quaternion finalRotation = Quaternion.identity;

    private float t = 0;
    private bool isLowering = false;

    private void Start() {
        initialRotation = transform.rotation;
        
        GameManager gameManager = GameManager.Instance;
        if (gameManager && gameManager.cityLevelSolved) {
            transform.rotation = finalRotation;
            barrier.SetActive(false);
        }
    }

    private void Update() {
        // if (Input.GetKeyDown(KeyCode.L)) { // TODO for testing only, remove this later
        //     LowerBridge();
        // }
        if (isLowering && t <= 1.0f) {
            transform.rotation = Quaternion.Lerp(initialRotation, finalRotation, t);
            t += lowerSpeed * Time.deltaTime;
        }

        if (t >= 1.0f) {
            transform.rotation = finalRotation;
            barrier.SetActive(false);
        }
    }
    
    public void LowerBridge() {
        isLowering = true;
        SoundManager.Instance.PlayDrawBridgeSound();
    }

}
