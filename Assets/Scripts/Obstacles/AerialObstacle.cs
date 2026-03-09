using UnityEngine;
using UnityEngine.Events;

public class AerialObstacle : MonoBehaviour
{
    public UnityEvent onCollision;
    
    private CapsuleCollider _collider;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
            onCollision.Invoke();
            CollisionEvent(collision);
        }
    }

    public virtual void CollisionEvent(Collision collision)
    {
        // override
    }
}
