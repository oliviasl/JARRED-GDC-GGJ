using UnityEngine;

public class FailPlaneObstable : AerialObstacle
{
    [SerializeField] private HealthController _healthController;
    
    public override void CollisionEvent(Collision collision)
    {
        base.CollisionEvent(collision);
        
        _healthController.AutoKill();
    }
}
