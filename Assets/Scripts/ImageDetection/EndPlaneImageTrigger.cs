using Unity.Cinemachine;
using UnityEngine;

public class EndPlaneImageTrigger : AerialObstacle
{
    [SerializeField] private ImageDetectionManager _imgDetectionManager;
    [SerializeField] private Camera _screenshotCamera;
    public override void CollisionEvent(Collision collision)
    {
        base.CollisionEvent(collision);
        
        if (_imgDetectionManager)
        {
            if (_screenshotCamera)
            {
                _screenshotCamera.gameObject.SetActive(true);
            }

            _imgDetectionManager.TakeScreenshotAndCompare();
        }
        else
        {
            Debug.Log("No Image Detection Manager referenced.");
        }
    }
}