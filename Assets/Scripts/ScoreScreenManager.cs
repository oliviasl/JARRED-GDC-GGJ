using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScreenManager : MonoBehaviour
{
    [SerializeField] private Image requestedImage;
    [SerializeField] private Image yourImage;
    [SerializeField] private TextMeshProUGUI similarityText;

    [SerializeField] private SceneController sceneController;

    void Start()
    {
        SetupUI();
    }

    private void SetupUI()
    {
        if (Results.Instance != null)
        {
            similarityText.text = (int)(Results.Instance.similarity * 100f) + "% Match!";
            Texture2D cleanRef = RemoveBlack(Results.Instance.reference);
            Texture2D cleanShot = RemoveBlack(Results.Instance.screenshot);
            requestedImage.sprite = Sprite.Create(cleanRef, new Rect(0, 0, cleanRef.width, cleanRef.height), new Vector2(0.5f, 0.5f));
            yourImage.sprite = Sprite.Create(cleanShot, new Rect(0, 0, cleanShot.width, cleanShot.height), new Vector2(0.5f, 0.5f));
        }
    }

    private Texture2D RemoveBlack(Texture2D source, float threshold = 0.1f)
    {
        Texture2D result = new Texture2D(source.width, source.height, TextureFormat.RGBA32, false);
        Color[] pixels = source.GetPixels();

        for (int i = 0; i < pixels.Length; i++)
        {
            if (pixels[i].r <= threshold && pixels[i].g <= threshold && pixels[i].b <= threshold)
            {
                pixels[i] = Color.clear;
            }
        }

        result.SetPixels(pixels);
        result.Apply();
        return result;
    }

    public void NextScene()
    {
        sceneController.LoadScene();
    }

}
