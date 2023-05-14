using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overlay : MonoBehaviour {
    [SerializeField] private float fadeSpeed = 1;
    [SerializeField] private Color initialColor;

    public bool doneFading { get; private set; }
        
    private SpriteRenderer renderer;
    void Awake() {
        renderer = GetComponent<SpriteRenderer>();
        renderer.color = initialColor;
    }

    void Update() {
        // if (Input.GetKeyDown(KeyCode.E)) {
        //     StartCoroutine(FadeOut());
        // }
    }

    public void StartFadeOut() {
        doneFading = false;
        StartCoroutine(FadeOut());
    }

    public void StartFadeToWhite() {
        StartCoroutine(FadeToWhite());
        doneFading = false;
    }

    public void FadeOutInstantly() {
        renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0);
    }

    private IEnumerator FadeOut() {
        float t = 1;
        while (t > 0) {
            t -= fadeSpeed * Time.deltaTime;
            t = Mathf.Max(t, 0f);
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, t);
            yield return null;
        }

        doneFading = true;
    }
    
    private IEnumerator FadeToWhite() {
        float t = 0;
        while (t < 1) {
            t += fadeSpeed * Time.deltaTime;
            t = Mathf.Min(t, 1f);
            renderer.color = new Color(1f, 1f, 1f, t);
            yield return null;
        }

        doneFading = true;
    }
}
