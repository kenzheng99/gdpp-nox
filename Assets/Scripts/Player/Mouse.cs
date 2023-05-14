using System;
using System.Collections;
using UnityEngine;

public class Mouse : MonoBehaviour {
    [SerializeField] private Transform player;
    [SerializeField] private float alertDistance;
    [SerializeField] private float startRunningDistance;
    [SerializeField] private float jumpForce;
    [SerializeField] private float runForce;
    [SerializeField] private float runDistance;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip squeak1;
    [SerializeField] private AudioClip squeak2;
    [SerializeField] private AudioClip squeak3;
    private bool isPlayingSound;
    private bool isRunning;
    private bool isFacingPlayer;
    private Animator anim;
    
    private void Start() {
        anim = GetComponent<Animator>();
        
    }

    
    private void Update() {
        float distanceFromPlayer = Vector3.Distance(transform.position, player.position);
        if (!isPlayingSound && distanceFromPlayer < 35)
        {
            InvokeRepeating("Squeak", 0, 3);
            isPlayingSound = true;
        }
        if (!isFacingPlayer && distanceFromPlayer <= alertDistance) {
            isFacingPlayer = true;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        if (!isRunning && distanceFromPlayer <= startRunningDistance) {
            StartCoroutine(Run());
        }
    }
    
    private IEnumerator Run() {
        Debug.Log("start run coroutine");
        isRunning = true;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.2f);
        anim.Play("Mouse");
        Vector3 initialPosition = transform.position;
        rb.AddForce(runForce * Vector2.right, ForceMode2D.Impulse);
        float distanceTraveled = transform.position.x - initialPosition.x;
        while (distanceTraveled <= runDistance && rb.velocity.x >= 0.01f) {
            yield return null;
        }
        gameObject.SetActive(false);
    }
    
    private void Squeak()
    {
        PlayMouseSqueakSound(UnityEngine.Random.Range(1, 4));
    }

    private void PlayMouseSqueakSound(int x)
    {
        switch (x)
        {
            case 1:
                audioSource.clip = squeak1;
                break;
            case 2:
                audioSource.clip = squeak2;
                break;
            case 3:
                audioSource.clip = squeak3;
                break;
        }
        audioSource.Play();
    }
}
