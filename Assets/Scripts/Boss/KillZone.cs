using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    private GameManager gm;
    private Boss boss;

    // Start is called before the first frame update
    private void Start()
    {
        gm = GameManager.Instance;
        boss = GameObject.FindWithTag("Boss").GetComponent<Boss>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            Debug.Log("Killed by boss");
            gm.RestartBossFight(); // resets player
        }
    }
}
