using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Statue : MonoBehaviour {
    [SerializeField] private GameObject statueInactive;
    [SerializeField] private GameObject statueActive;
    [SerializeField] private Transform lever;
    [SerializeField] private float leverSpeed = 1f;
    [SerializeField] private Quaternion leverFinalRotation;
    
    public bool isActive { get; private set; }

    private Quaternion leverInitialRotation;
    private bool playerNearby = false;
    private void Start()
    {
        GameManager gameManager = GameManager.Instance;
        if (gameManager && gameManager.cityLevelSolved) {
            ActivateStatue();
            // StartCoroutine(ActivateCoroutine());
        } else {
            statueInactive.SetActive(true);
            statueActive.SetActive(false);
        }

        leverInitialRotation = lever.rotation;
    }

    private void Update() {
        if (playerNearby && !isActive && Input.GetKeyDown(KeyCode.E)) {
            SoundManager.Instance.PlayLeverSound();
            StartCoroutine(ActivateAnimation());
        }
    }

    private IEnumerator ActivateAnimation() {
        float t = 0;
        while (t <= 1) {
            t += leverSpeed * Time.deltaTime;
            Quaternion newRotation = Quaternion.Slerp(leverInitialRotation, leverFinalRotation, t);
            lever.rotation = newRotation;
            yield return null;
        }
        ActivateStatue();
    }

    private void ActivateStatue() {
        statueActive.SetActive(true);
        statueInactive.SetActive(false);
        lever.rotation = leverFinalRotation;
        isActive = true;

        ButtonPrompt buttonPrompt = GetComponent<ButtonPrompt>();
        if (buttonPrompt) {
            buttonPrompt.Disable();
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
