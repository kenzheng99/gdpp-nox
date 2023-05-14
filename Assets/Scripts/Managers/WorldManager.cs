using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class WorldManager: MonoBehaviour
{
    // public static WorldManager Instance {
    //     get {
    //         return _instance;
    //     }
    // }
    //
    // private static WorldManager _instance;

    public static event Action<World> OnWorldChange;
    private World currentWorld;
    
    // void Awake() {
    //     if (_instance == null) {
    //         _instance = this;
    //         DontDestroyOnLoad(_instance);
    //     } else {
    //         Destroy (gameObject);
    //     }
    // }

    void Start() {
        currentWorld = World.MAIN;
        OnWorldChange?.Invoke(currentWorld);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            ToggleWorld();
            SoundManager.Instance.PlayMeowSound(Random.Range(1,4));
            SoundManager.Instance.PlayWorldSwapSound();
        }
    }

    private void ToggleWorld() {
        currentWorld = currentWorld == World.MAIN ? World.OTHER : World.MAIN;
        OnWorldChange?.Invoke(currentWorld);
        if (currentWorld == World.OTHER)
        {
            SoundManager.Instance.ApplyNightmareFilter();
        }
        else
        {
            SoundManager.Instance.RemoveNightMareFilter();
        }
    }
}
