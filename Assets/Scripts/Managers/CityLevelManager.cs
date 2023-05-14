using UnityEngine;

public class CityLevelManager : MonoBehaviour {
    [SerializeField] private Statue statue1;
    [SerializeField] private Statue statue2;
    [SerializeField] private Statue statue3;
    [SerializeField] private Drawbridge drawbridge;

    [SerializeField] private GameObject tutorialButtonPrompts;
    [SerializeField] private GameObject mouse;

    private GameManager gameManager;
    private GameObject bossObj;

    private void Start() {
        gameManager = GameManager.Instance;
        bossObj = GameObject.FindWithTag("Boss");
        
        if (gameManager.cityLevelSolved) {
            Destroy(tutorialButtonPrompts);
            Destroy(mouse);
        }
        
        if (gameManager.forestLevelSolved) {
            bossObj.SetActive(true);
        }
        else {
            bossObj.SetActive(false);
        }
    }

    private void Update() {
        
        if (!gameManager.cityLevelSolved && statue1.isActive && statue2.isActive && statue3.isActive) {
            Debug.Log("level solved!");
            drawbridge.LowerBridge();
            gameManager.cityLevelSolved = true;
            Destroy(tutorialButtonPrompts);
            Destroy(mouse);
        }

    }
}
