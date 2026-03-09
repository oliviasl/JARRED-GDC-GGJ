using UnityEngine;

public class Results : MonoBehaviour
{
    public float similarity = 0.0f;

    public Texture2D screenshot;
    public Texture2D reference;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
