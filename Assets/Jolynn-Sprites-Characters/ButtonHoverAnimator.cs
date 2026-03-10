using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private UISpriteSwicher spriteSwitcher;

    public void OnPointerEnter(PointerEventData eventData)
    {
        spriteSwitcher.StartAnimating();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        spriteSwitcher.StopAnimating();
    }
}