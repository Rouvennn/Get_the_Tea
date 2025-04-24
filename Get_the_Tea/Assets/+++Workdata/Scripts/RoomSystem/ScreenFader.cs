using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    public static ScreenFader Instance;

    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        DontDestroyOnLoad(gameObject); // Optional: persists between scenes
    }

    public void FadeOut(System.Action onComplete = null)
    {
        StartCoroutine(Fade(1, 0, onComplete));
    }

    public void FadeIn(System.Action onComplete = null)
    {
        StartCoroutine(Fade(0, 1, onComplete));
    }

    private IEnumerator Fade(float start, float end, System.Action onComplete = null)
    {
        float t = 0f;
        Color color = fadeImage.color;
        while (t < 1f)
        {
            t += Time.deltaTime / fadeDuration;
            float a = Mathf.Lerp(start, end, t);
            fadeImage.color = new Color(color.r, color.g, color.b, a);
            yield return null;
        }

        fadeImage.color = new Color(color.r, color.g, color.b, end);
        onComplete?.Invoke();
    }
}
