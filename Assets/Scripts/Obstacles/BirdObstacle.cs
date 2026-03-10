using UnityEngine;

public class BirdObstacle : AerialObstacle
{
    [SerializeField] private GameObject windShield;
    [SerializeField] private GameObject _splatPrefab;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _despawnTime = 10f;

    private BirdSpawner _birdSpawner;
    private float _despawnTimer;

    public void SetBirdSpawner(BirdSpawner spawner)
    {
        _birdSpawner = spawner;
    }

    public void SetPlaneWindshield(GameObject plane)
    {
        windShield = plane;
    }

    public override void CollisionEvent(Collision collision)
    {
        base.CollisionEvent(collision);
        Camera mainCamera = collision.gameObject.GetComponentInChildren<Camera>();
        if (mainCamera)
        {
            Ray ray = new Ray(transform.position, (collision.transform.position - transform.position).normalized);
            int layerMask = LayerMask.GetMask("Windshield");
            RaycastHit hitData;
            
            // Cast the ray and check for a hit
            if (Physics.Raycast(ray, out hitData, 100f, layerMask))
            {
                GameObject newSplat = Instantiate(_splatPrefab);
                newSplat.transform.position = hitData.point;
                // transform.Rotate(0, -90, 0, Space.World); 
                newSplat.transform.SetParent(windShield.transform, true);

                Destroy(gameObject);
            }
            
            // if off screen don't fire
            /*
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(transform.position);
            if (screenPosition.z < 0f || screenPosition.x < -canvasRectTransform.rect.width
                                      || screenPosition.y < -canvasRectTransform.rect.height 
                                      || screenPosition.x > canvasRectTransform.rect.width
                                      || screenPosition.y > canvasRectTransform.rect.height)
            {
                return;
            }
            
                
            // create image based on screen projection
            GameObject newImage = Instantiate(_splatPrefab) as GameObject;
            newImage.transform.SetParent(_planeHUD.transform, false);
            RectTransform rectTransform = newImage.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0f, 0f);
            rectTransform.anchorMax = new Vector2(0f, 0f);

            RectTransform hudRectTransform = _planeHUD.GetComponent<RectTransform>();
            float screenPosX = Mathf.Clamp(screenPosition.x / _planeHUD.scaleFactor, 0f, hudRectTransform.rect.width - rectTransform.rect.width);
            float screenPosY = Mathf.Clamp(screenPosition.y / _planeHUD.scaleFactor, 0f, hudRectTransform.rect.height - rectTransform.rect.height);
            rectTransform.anchoredPosition = new Vector2(screenPosX, screenPosY);
            */
        }
    }

    private void Update()
    {
        transform.position += Time.deltaTime * _speed * transform.forward;

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
