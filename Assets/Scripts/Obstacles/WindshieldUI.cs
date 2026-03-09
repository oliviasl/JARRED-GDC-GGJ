using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WindshieldUI : MonoBehaviour
{
    [SerializeField] private Image _backgroundBar;
    [SerializeField] private Image _markerBar;
    [SerializeField] private Image _middleBar;
    [SerializeField] private QTEMinigame _qteMinigame;

    private RectTransform _markerRectTransform;
    private RectTransform _backgroundRectTransform;
    
    void Start()
    {
        _markerRectTransform = _markerBar.GetComponent<RectTransform>();
        _backgroundRectTransform = _backgroundBar.GetComponent<RectTransform>();
        _qteMinigame.onSuccess.AddListener(Success);
        _qteMinigame.onUpdateMarker.AddListener(UpdateMarker);
    }
    
    private void OnDisable()
    {
        if (_qteMinigame)
        {
            _qteMinigame.onSuccess.RemoveListener(Success);
            _qteMinigame.onUpdateMarker.RemoveListener(UpdateMarker);
        }
    }

    void UpdateMarker(float markerPercent)
    {
        float width = _backgroundRectTransform.rect.width;
        float posX = width * markerPercent - width;
        _markerRectTransform.anchoredPosition = new Vector2(posX, _markerRectTransform.anchoredPosition.y);
    }

    void Success()
    {
        StartCoroutine(SuccessFlash());
    }

    IEnumerator SuccessFlash()
    {
        _middleBar.material.color = Color.green;
        yield return new WaitForSeconds(0.5f);
        _middleBar.material.color = Color.red;
    }
}
