using UnityEngine;
using UnityEngine.UI;

public class UISpriteSwicher : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float switchInterval = 0.2f;

    private Image image;
    private float timer = 0f;
    private int currentIndex = 0;

    public bool isAnimating = false;

    void Start()
    {
        image = GetComponent<Image>();
        if (sprites.Length > 0)
            image.sprite = sprites[0];
    }

    void Update()
    {
        if (!isAnimating) return;

        timer += Time.deltaTime;
        if (timer >= switchInterval)
        {
            currentIndex = (currentIndex + 1) % sprites.Length;
            image.sprite = sprites[currentIndex];
            timer = 0f;
        }
    }

    public void StartAnimating()
    {
        isAnimating = true;
    }

    public void StopAnimating()
    {
        isAnimating = false;
        currentIndex = 0;
        timer = 0f;
        if (sprites.Length > 0)
            image.sprite = sprites[0];
    }
}