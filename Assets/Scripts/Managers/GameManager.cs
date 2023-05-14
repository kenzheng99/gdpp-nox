using UnityEngine;
using UnityEngine.SceneManagement;

public enum Level { INTRO, CITY, FOREST }

public enum TransitionType { NEXT, PREVIOUS, NONE }


public class GameManager : MonoBehaviour {
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    public bool bossStarted;
    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(gameObject);
        } else {
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public TransitionType lastLevelTransition = TransitionType.NONE;
    
    // state variables
    // public bool hasWokenUp { get; set; }
    public bool hasEnteredCity { get; set; }
    public bool hasEnteredForest { get; set; }

    public bool houseLevelSolved { get; set; }
    public bool cityLevelSolved { get; set; }

    public bool forestLevelSolved { get; set; }
    
    private Level currentLevel = Level.INTRO;
    private void Start() {
        Cursor.visible = false;
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene.Equals("CityLevel")) {
            Debug.Log("starting GameManager in city level");
            currentLevel = Level.CITY;
            SoundManager.Instance.PlayCityAmbience();
            SoundManager.Instance.PlayCityMusic();
        } else if (currentScene.Equals("HouseLevel")) {
            Debug.Log("starting GameManager in intro level");
            currentLevel = Level.INTRO;
        } else if (currentScene.Equals("ForestLevel")) {
            Debug.Log("starting GameManager in forest level");
            currentLevel = Level.FOREST;
            SoundManager.Instance.PlayForestAmbience();
            SoundManager.Instance.PlayForestMusic();
        }
    }

    public void NextLevel() {
        if (currentLevel == Level.INTRO) {
            Debug.Log("intro -> city");
            SceneManager.LoadScene("CityLevel");
            lastLevelTransition = TransitionType.NEXT;
            currentLevel = Level.CITY;
            hasEnteredCity = true;
            if (bossStarted == false)
            {
                SoundManager.Instance.PlayCityAmbience();
                SoundManager.Instance.PlayCityMusic();
            }
        } else if (currentLevel == Level.CITY) {
            Debug.Log("city -> forest");
            SceneManager.LoadScene("ForestLevel");
            lastLevelTransition = TransitionType.NEXT;
            currentLevel = Level.FOREST;
            hasEnteredForest = true;
            if (bossStarted == false)
            {
                SoundManager.Instance.PlayForestAmbience();
                SoundManager.Instance.PlayForestMusic();
            }
        } else {
            Debug.Log("no next level");
        }
    }

    public void PreviousLevel() {
        if (currentLevel == Level.FOREST) {
            Debug.Log("forest -> city");
            cityLevelSolved = true;
            SceneManager.LoadScene("CityLevel");
            lastLevelTransition = TransitionType.PREVIOUS;
            currentLevel = Level.CITY;
            if (bossStarted == false)
            {
                SoundManager.Instance.PlayCityAmbience();
                SoundManager.Instance.PlayCityMusic();
            }
        } else if (currentLevel == Level.CITY) {
            Debug.Log("city -> intro");
            SceneManager.LoadScene("HouseLevel");
            lastLevelTransition = TransitionType.PREVIOUS;
            currentLevel = Level.INTRO;
        } else {
            Debug.Log("no previous level");
        }
    }

    public void RestartBossFight() {
        Debug.Log("Returning to end of forest level");
        forestLevelSolved = true;
        SceneManager.LoadScene("ForestLevel");
        lastLevelTransition = TransitionType.PREVIOUS;
        currentLevel = Level.FOREST;
        SoundManager.Instance.PlayBossFightAmbience();
    }

    private void Update() {
        //CheckCheatCode();
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }
    private void CheckCheatCode() {
        if (Input.GetKeyDown(KeyCode.L)) {
            cityLevelSolved = true;
        }
        if (Input.GetKeyDown(KeyCode.P)) {
            cityLevelSolved = true;
            forestLevelSolved = true;
        }
    }
}
