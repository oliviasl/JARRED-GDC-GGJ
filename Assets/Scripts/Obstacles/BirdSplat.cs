using UnityEngine;
using UnityEngine.UI;

public class BirdSplat : MonoBehaviour
{
    private int _health = 3;
    private Image _image;
    private QTEMinigame _qteMinigame;

    void Start()
    {
        _image = GetComponent<Image>();
        _qteMinigame = FindAnyObjectByType<QTEMinigame>();
        if (_qteMinigame)
        {
            _qteMinigame.onSuccess.AddListener(WipeBird);
        }
    }

    private void OnDisable()
    {
        if (_qteMinigame)
        {
            _qteMinigame.onSuccess.RemoveListener(WipeBird);
        }
    }

    void WipeBird()
    {
        --_health;

        Color tempColor = _image.color;
        tempColor.a -= 0.33f;
        _image.color = tempColor;
        
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
