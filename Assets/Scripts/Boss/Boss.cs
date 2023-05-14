using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Boss : MonoBehaviour {
    [SerializeField] private float horizontalSpeedMax;
    [SerializeField] private float horizontalSpeedMin;
    [SerializeField] private Transform bossSpawn;
    [SerializeField] private float verticalSpeed;

    private Transform bossTr;
    private Transform mouseTr;
    private Transform playerTr;
    
    private GameManager gm;
    private bool playerSleeping; // set to true when game ends
    private float timeSincePlayerSleep;
    
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip roar1;
    [SerializeField] private AudioClip roar2;
    [SerializeField] private AudioClip roar3;
    [SerializeField] private AudioClip roar4;
    private bool bossAmbiencePlaying;
    
    private void Start()
    {
        bossTr = GameObject.FindWithTag("Boss").transform;
        mouseTr = GameObject.FindWithTag("Mouse").transform;
        playerTr = GameObject.FindWithTag("Player").transform;
        
        gm = GameManager.Instance;
        if (gm.forestLevelSolved) {
            Respawn();
        }
    }

    public void OnPlayerSleep() {
        playerSleeping = true;
        timeSincePlayerSleep = 0;
    }

    private void Update() {
        if (gm.forestLevelSolved) {
            var toPlayerX = bossTr.position.x - playerTr.position.x;
            var speed = Mathf.Lerp(horizontalSpeedMax, horizontalSpeedMin, 1 / toPlayerX);
            if (playerSleeping) {
                speed -= 3 * timeSincePlayerSleep;
                speed = Mathf.Max(speed, 0);
                timeSincePlayerSleep += Time.deltaTime;
                if (toPlayerX < 20) {
                    speed = 0;
                }
            }
            bossTr.Translate(speed * Time.deltaTime * Vector3.left);
        }

        var toPlayerY = new Vector2(0, playerTr.position.y - mouseTr.position.y);
        mouseTr.Translate(verticalSpeed * Time.deltaTime * toPlayerY);
    }

    public void Respawn() {
        Debug.Log("respawn");
        gm.forestLevelSolved = true;
        bossTr.position = bossSpawn.position;
        SoundManager.Instance.StopMusic();
        GameManager.Instance.bossStarted = true;
        SoundManager.Instance.LowerAmbienceVolume();
        SoundManager.Instance.PlayInitialBossSound();
        InvokeRepeating("Roar", 5, 3);
    }
    
    private void Roar()
    {
        PlayBossRoarSound(UnityEngine.Random.Range(1, 5));
    }

    private void PlayBossRoarSound(int x)
    {
        switch (x)
        {
            case 1:
                audioSource.clip = roar1;
                audioSource.PlayOneShot(roar1);
                break;
            case 2:
                audioSource.clip = roar2;
                audioSource.PlayOneShot(roar2);

                break;
            case 3:
                audioSource.clip = roar3;
                audioSource.PlayOneShot(roar3);

                break;
            case 4:
                audioSource.clip = roar4;
                audioSource.PlayOneShot(roar4);
                break;
        }
    }
}
