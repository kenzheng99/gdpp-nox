using UnityEngine;

public class LevelTransition : MonoBehaviour {
    [SerializeField] private TransitionType transitionType = TransitionType.NEXT;
    
    private GameManager gameManager;

    private void Start() {
        gameManager = GameManager.Instance;
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("Player")) {
            if (transitionType == TransitionType.NEXT) {
                gameManager.NextLevel();
            } else if (transitionType == TransitionType.PREVIOUS) {
                gameManager.PreviousLevel();
            } else {
                Debug.Log("invalid transition type");
            }
        }
    }
}
