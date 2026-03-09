using UnityEngine;
using UnityEngine.Events;

public class AerialObstacle : MonoBehaviour
{
    public UnityEvent onCollision;
    
    [SerializeField] private bool _isFireOnce = false;
    private bool _hasFired = false;
    private CapsuleCollider _collider;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!_isFireOnce || _isFireOnce && !_hasFired)
            {
                onCollision.Invoke();
                CollisionEvent(collision);

                _hasFired = true;
            }
        }
    }

    public virtual void CollisionEvent(Collision collision)
    {
        // override
    }
}
