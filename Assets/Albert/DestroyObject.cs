using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public void DestroyGameObject(int timeToDie)
    {
        Destroy(gameObject, timeToDie);
    }
}
