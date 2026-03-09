using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScreenManager : MonoBehaviour
{
    [SerializeField] private Image requestedImage;
    [SerializeField] private Image yourImage;
    [SerializeField] private TextMeshProUGUI similarityText;

    void Start()
    {
        SetupUI();
    }

    private void SetupUI()
    {
        similarityText.text = (int)(Results.Instance.similarity * 100f) + "% Match!";
        requestedImage.sprite = Sprite.Create(Results.Instance.reference, new Rect(0, 0, Results.Instance.reference.width, Results.Instance.reference.height), new Vector2(0.5f, 0.5f));
        yourImage.sprite = Sprite.Create(Results.Instance.screenshot, new Rect(0, 0, Results.Instance.screenshot.width, Results.Instance.screenshot.height), new Vector2(0.5f, 0.5f));
    }

}
