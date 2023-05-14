using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vine : MonoBehaviour
{
    private bool playerNearby = false;

    private GameObject log;
    private GameObject vines;
    // Start is called before the first frame update
    private void Start()
    {
        log = GameObject.Find("Log");
        vines = GameObject.Find("Vines");
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E)) {
            log.SetActive(false);
            vines.SetActive(false);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            playerNearby = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            playerNearby = false;
        }
    }
}
