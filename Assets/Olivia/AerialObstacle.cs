using UnityEngine;
using UnityEngine.Events;

public class AerialObstacle : MonoBehaviour
{
    public UnityEvent onCollision;
    
    [SerializeField] private Canvas _planeHUD;
    [SerializeField] private GameObject _splatImgPrefab;
    private CapsuleCollider _collider;

    private void OnCollisionEnter(Collision collision)
    {
        onCollision.Invoke();
        
        if (collision.gameObject.CompareTag("Player"))
        { 
            Camera mainCamera = collision.gameObject.GetComponentInChildren<Camera>();
            if (mainCamera)
            {
                RectTransform canvasRectTransform = _planeHUD.GetComponent<RectTransform>();
                
                // if off screen don't fire
                Vector3 screenPosition = mainCamera.WorldToScreenPoint(transform.position);
                if (screenPosition.z < 0f || screenPosition.x < -canvasRectTransform.rect.width
                    || screenPosition.y < -canvasRectTransform.rect.height || screenPosition.x > canvasRectTransform.rect.width
                    || screenPosition.y > canvasRectTransform.rect.height)
                {
                    return;
                }
                
                // create image based on screen projection
                GameObject newImage = Instantiate(_splatImgPrefab) as GameObject;
                newImage.transform.SetParent(_planeHUD.transform, false);
                RectTransform rectTransform = newImage.GetComponent<RectTransform>();
                rectTransform.anchorMin = new Vector2(0f, 0f);
                rectTransform.anchorMax = new Vector2(0f, 0f);
                rectTransform.anchoredPosition = new Vector2(screenPosition.x / _planeHUD.scaleFactor, screenPosition.y / _planeHUD.scaleFactor);
                
                Destroy(gameObject);
            }
        }
    }
}
