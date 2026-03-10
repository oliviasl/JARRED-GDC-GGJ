using UnityEngine;

public class SpriteSwitcher : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float stretchScaleY = 1.08f;
    [SerializeField] private float stretchScaleX = 1f;
    [SerializeField] private float switchInterval = 0.2f;

    public float lerpSpeed = 12f;

    private SpriteRenderer spriteRenderer;
    private float timer = 0f;
    private int currentIndex = 0;

    private Vector3 normalScale = new Vector3(1f, 1f, 1f);
    private Vector3 targetScale;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (sprites.Length > 0)
            spriteRenderer.sprite = sprites[0];

        // Use the object's actual scale as the base
        normalScale = transform.localScale;
        targetScale = normalScale;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= switchInterval)
        {
            currentIndex = (currentIndex + 1) % sprites.Length;
            spriteRenderer.sprite = sprites[currentIndex];

            // Stretch on every other sprite (odd indices)
            targetScale = (currentIndex % 2 != 0)
                ? new Vector3(stretchScaleX, stretchScaleY, 1f)
                : normalScale;

            timer = 0f;
        }

        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * lerpSpeed);
    }
}