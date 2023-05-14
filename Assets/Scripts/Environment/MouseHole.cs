using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHole : MonoBehaviour
{
    private bool playerNearby;

    private GameObject mouseHolePrompt;
    private GameManager gm;

    private Boss boss;
    // Start is called before the first frame update
    private void Start() {
        mouseHolePrompt = GameObject.Find("MouseHolePromptUI");
        boss = GameObject.FindWithTag("Boss").GetComponent<Boss>();
        gm = GameManager.Instance;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!gm.forestLevelSolved && playerNearby) {
            mouseHolePrompt.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E)) {
                SoundManager.Instance.PlayBossFightAmbience();
                boss.Respawn();
            }
        }
        else {
            mouseHolePrompt.SetActive(false);
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
