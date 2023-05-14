using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour {
    [SerializeField] private GameObject signActive;

    private GameManager gm;
    private bool pressured;
    private GameObject forestTallTree;
    private void Start() {
        pressured = false;
        forestTallTree = GameObject.Find("ForestTallTree");
        gm = GameManager.Instance;
    }

    private void Update() {
        if (pressured || gm.forestLevelSolved) {
            forestTallTree.SetActive(false);
            signActive.SetActive(true);
        }
        else {
            signActive.SetActive(false);
            forestTallTree.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("MovableObject")) {
            pressured = true;
            SoundManager.Instance.PlayLeverSound();
            Debug.Log("pressured");
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("MovableObject")) {
            pressured = false;
            Debug.Log("not pressured");
        }
    }
}
