using System;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class BirdObstacle : AerialObstacle
{
    [SerializeField] private Canvas _planeHUD;
    [SerializeField] private GameObject _splatImgPrefab;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _despawnTime = 10f;

    private BirdSpawner _birdSpawner;
    private float _despawnTimer;

    public void SetBirdSpawner(BirdSpawner spawner)
    {
        _birdSpawner = spawner;
    }

    public void SetPlaneHUD(Canvas hud)
    {
        _planeHUD = hud;
    }

    public override void CollisionEvent(Collision collision)
    {
        base.CollisionEvent(collision);
        Camera mainCamera = collision.gameObject.GetComponentInChildren<Camera>();
        if (mainCamera)
        {
            RectTransform canvasRectTransform = _planeHUD.GetComponent<RectTransform>();
                
            // if off screen don't fire
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(transform.position);
            /*
            if (screenPosition.z < 0f || screenPosition.x < -canvasRectTransform.rect.width
                                      || screenPosition.y < -canvasRectTransform.rect.height 
                                      || screenPosition.x > canvasRectTransform.rect.width
                                      || screenPosition.y > canvasRectTransform.rect.height)
            {
                return;
            }
            */
                
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

    private void Update()
    {
        transform.position = transform.position + Time.deltaTime * _speed * transform.forward;

        _despawnTimer += Time.deltaTime;
        if (_despawnTimer >= _despawnTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (_birdSpawner)
        {
            _birdSpawner.RemoveBird(gameObject);
        }
    }
}
