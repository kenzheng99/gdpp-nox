using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour {
    [SerializeField] private GameObject controlsScreen;
    [SerializeField] private GameObject creditsScreen;

    private void Start() {
        controlsScreen.SetActive(false);
        creditsScreen.SetActive(false);
    }

    public void StartGame() {
        SceneManager.LoadScene("HouseLevel");
    }
    
    public void ShowControls() {
        controlsScreen.SetActive(true);
        creditsScreen.SetActive(false);
    }

    public void HideControls() {
        controlsScreen.SetActive(false);
    }

    public void ShowCredits() {
        creditsScreen.SetActive(true);
        controlsScreen.SetActive(false);
    }

    public void HideCredits() {
        creditsScreen.SetActive(false);
    }
    
    public void Quit() {
        Application.Quit();
    }
}
