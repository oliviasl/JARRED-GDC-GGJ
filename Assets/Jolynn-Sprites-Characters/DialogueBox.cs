using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueBox : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TextMeshPro dialogueText;
    [SerializeField] private DialogueController dialogueController;

    [Header("Bubble")]
    [SerializeField] private SpriteRenderer bubbleBackground;
    [SerializeField] private Sprite[] bubbleSprites;
    [SerializeField] private float switchInterval = 0.1f;

    [Header("Padding")]
    [SerializeField] private float paddingLeft = 0.5f;
    [SerializeField] private float paddingRight = 0.5f;
    [SerializeField] private float paddingTop = 0.4f;
    [SerializeField] private float paddingBottom = 1f;

    [Header("Sizing")]
    [SerializeField] private float minWidth = 2f;
    [SerializeField] private float maxWidth = 8f;
    [SerializeField] private float minHeight = 1f;
    [SerializeField] private float maxHeight = 6f;
    [SerializeField] private float lerpSpeed = 8f;

    [Header("Typewriter")]
    [SerializeField] private float charDelay = 0.05f;

    private Vector2 targetSize;
    private float timer = 0f;
    private int currentIndex = 0;
    private TMP_Text sourceText;
    private string lastSourceText = "";
    private Coroutine typewriterCoroutine;

    void Start()
    {
        sourceText = dialogueController.GetComponentInChildren<TMP_Text>();
        dialogueText.alignment = TextAlignmentOptions.TopLeft;
    }

    void Update()
    {
        if (sourceText != null && sourceText.text != lastSourceText)
        {
            Debug.Log("New text detected: " + sourceText.text);
            lastSourceText = sourceText.text;

            if (typewriterCoroutine != null)
                StopCoroutine(typewriterCoroutine);

            typewriterCoroutine = StartCoroutine(TypewriterEffect(sourceText.text));
        }

        HandleSpriteAnimation();
        HandleBubbleResize();
    }

    IEnumerator TypewriterEffect(string fullText)
    {
        dialogueText.text = "";
        dialogueText.ForceMeshUpdate();

        foreach (char c in fullText)
        {
            dialogueText.text += c;
            Debug.Log("Typing: " + dialogueText.text);
            yield return new WaitForSeconds(charDelay);
        }
    }

    void HandleSpriteAnimation()
    {
        if (bubbleSprites.Length == 0) return;

        timer += Time.deltaTime;
        if (timer >= switchInterval)
        {
            currentIndex = (currentIndex + 1) % bubbleSprites.Length;
            bubbleBackground.sprite = bubbleSprites[currentIndex];
            timer = 0f;
        }
    }

    void HandleBubbleResize()
    {
        // Tell TMP the max width so it can word-wrap and calculate correct height
        dialogueText.rectTransform.sizeDelta = new Vector2(
            maxWidth - paddingLeft - paddingRight,
            0f // let TMP calculate height freely
        );

        dialogueText.ForceMeshUpdate();
        Vector2 textSize = dialogueText.GetRenderedValues(onlyVisibleCharacters: false);

        float targetWidth = Mathf.Clamp(textSize.x + paddingLeft + paddingRight, minWidth, maxWidth);
        float targetHeight = Mathf.Clamp(textSize.y + paddingTop + paddingBottom, minHeight, maxHeight);

        targetSize = new Vector2(targetWidth, targetHeight);

        bubbleBackground.size = Vector2.Lerp(
            bubbleBackground.size,
            targetSize,
            Time.deltaTime * lerpSpeed
        );

        // Resize text rect to actual bubble width
        dialogueText.rectTransform.sizeDelta = new Vector2(
            targetWidth - paddingLeft - paddingRight,
            targetHeight - paddingTop - paddingBottom
        );

        float verticalOffset = (paddingBottom - paddingTop) / 2f;
        dialogueText.rectTransform.anchoredPosition = new Vector2(
            (paddingLeft - paddingRight) / 2f,
            verticalOffset
        );
    }


}