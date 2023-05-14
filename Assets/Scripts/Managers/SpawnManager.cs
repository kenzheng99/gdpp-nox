using UnityEngine;

public class SpawnManager: MonoBehaviour {
    [SerializeField] private Transform startSpawnPoint;
    [SerializeField] private Transform endSpawnPoint;
    [SerializeField] private Transform player;
    
    private void Awake() {
        GameManager gameManager = GameManager.Instance;
        // do this in Awake() to make sure player is positioned correctly before camera Start()
        if (gameManager && gameManager.lastLevelTransition == TransitionType.NEXT) {
            player.position = startSpawnPoint.position;
        } else if (gameManager && gameManager.lastLevelTransition == TransitionType.PREVIOUS) {
            player.position = endSpawnPoint.position;
        }
    }
}
