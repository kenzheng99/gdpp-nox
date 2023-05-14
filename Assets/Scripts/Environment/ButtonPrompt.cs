using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonPrompt : MonoBehaviour
{
    [SerializeField] private GameObject promptUI;

    private bool enabled = true;
    private void Start() {
        promptUI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (enabled && other.CompareTag("Player")) {
            promptUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (enabled && other.CompareTag("Player")) {
            promptUI.SetActive(false);
        }
    }

    public void Enable() {
        this.enabled = true;
    }

    public void Disable() {
        this.enabled = false;
        promptUI.SetActive(false);
    }

    private void OnDestroy() {
        Destroy(promptUI);
    }
}
