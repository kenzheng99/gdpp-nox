using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseLevelManager : MonoBehaviour {
    [SerializeField] private Mouse mouse;
    [SerializeField] private Overlay sleepOverlay;
    [SerializeField] private Overlay theEndOverlay;
    [SerializeField] private GameObject wakeUpButtonPrompt;
    [SerializeField] private GameObject moveButtonPrompt;
    [SerializeField] private GameObject sleepButtonPrompt;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bossObj;
    [SerializeField] private GameObject sleepParticles;
    
    private GameManager gameManager;
    private bool awake;
    void Start() {
        gameManager = GameManager.Instance;
        // theEndOverlay.StartFadeToWhite();

        if (gameManager.forestLevelSolved) { // boss fight
            bossObj.SetActive(true);
            sleepParticles.SetActive(true);
            Destroy(moveButtonPrompt);
            Destroy(wakeUpButtonPrompt);
            Destroy(mouse.gameObject);
            sleepOverlay.FadeOutInstantly();
            sleepButtonPrompt.SetActive(true);
            awake = true;
        } else if (gameManager.hasEnteredCity) {  // if backtracking from city before boss
            awake = true;
            sleepOverlay.FadeOutInstantly();
            bossObj.SetActive(false);
            Destroy(mouse.gameObject);
            sleepParticles.SetActive(false);
        } else { // beginning of game
            player.GetComponent<Animator>().Play("Sleep");
            player.GetComponent<PlayerMovement>().enabled = false;
            sleepParticles.SetActive(false);
            awake = false;
        }
    }

    void Update() {
        if (!awake) {
            if (Input.GetKeyDown(KeyCode.E)) {
                StartCoroutine(WakeUp());
            }
        }

        if (gameManager.forestLevelSolved && sleepButtonPrompt && player.GetComponent<Collider2D>().OverlapPoint(sleepButtonPrompt.transform.position)) {
            Debug.Log("can sleep");
            if (Input.GetKeyDown(KeyCode.E)) {
                StartCoroutine(Sleep());
            }
        }
    }

    private IEnumerator WakeUp() {
        Destroy(wakeUpButtonPrompt);
        sleepOverlay.StartFadeOut();
        player.GetComponent<Animator>().Play("Wake");
        while (!sleepOverlay.doneFading) {
            yield return null;
        }
        player.GetComponent<Animator>().Play("Idle");
        player.GetComponent<PlayerMovement>().enabled = true;
        awake = true;
        moveButtonPrompt.SetActive(true);
    }

    private IEnumerator Sleep() {
        Destroy(sleepButtonPrompt);
        bossObj.GetComponent<Boss>().OnPlayerSleep();
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<Animator>().Play("Sleep");
        sleepOverlay.StartFadeToWhite();
        SoundManager.Instance.FadeSounds();
        while (!sleepOverlay.doneFading) {
            yield return null;
        }
        theEndOverlay.StartFadeToWhite();
        Destroy(bossObj);
        while (!theEndOverlay.doneFading) {
            yield return null;
        }
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("StartScreen");
    }

}
