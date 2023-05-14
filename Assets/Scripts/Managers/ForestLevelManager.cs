using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestLevelManager : MonoBehaviour {
    [SerializeField] private GameObject mice;
    void Start() {
        if (GameManager.Instance.forestLevelSolved) {
            Destroy(mice);
        }
    }
}
