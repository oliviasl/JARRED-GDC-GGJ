using UnityEngine;
using UnityEngine.UI;

public class FrostOverlay : MonoBehaviour
{
    [SerializeField] private Sprite[] frostSprites; // drag your 6 PNGs here
    [SerializeField] private float frostLevel = 0f; // 0 = none, 6 = full

    [SerializeField] private float lerpSpeed = 5f;

    private Image[] frostImages;
    private Canvas canvas;

    void Start()
    {
        // Create canvas
        GameObject canvasGO = new GameObject("FrostCanvas");
        canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 999;
        canvasGO.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasGO.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1080, 1920);
        canvasGO.AddComponent<GraphicRaycaster>();

        // Create one Image per sprite
        frostImages = new Image[frostSprites.Length];
        for (int i = 0; i < frostSprites.Length; i++)
        {
            GameObject imgGO = new GameObject($"Frost_{i}");
            imgGO.transform.SetParent(canvasGO.transform, false);

            Image img = imgGO.AddComponent<Image>();
            img.sprite = frostSprites[i];
            img.color = new Color(1f, 1f, 1f, 0f); // start invisible
            img.raycastTarget = false;

            // Stretch to fill screen
            RectTransform rt = img.rectTransform;
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;

            frostImages[i] = img;
        }
    }

    void Update()
    {
        for (int i = 0; i < frostImages.Length; i++)
        {
            // Each sprite occupies a band of 1.0 on the slider (0-1, 1-2, 2-3 etc.)
            float bandStart = i;
            float bandEnd = i + 1f;

            // Alpha is 0 before its band, ramps 0->1 through its band, stays 1 after
            float targetAlpha = Mathf.Clamp01(frostLevel - bandStart);

            Color c = frostImages[i].color;
            c.a = Mathf.Lerp(c.a, targetAlpha, Time.deltaTime * lerpSpeed);
            frostImages[i].color = c;
        }
    }

    // Call this from anywhere to set frost level
    public void SetFrostLevel(float level)
    {
        frostLevel = Mathf.Clamp(level, 0f, frostSprites.Length);
    }
}