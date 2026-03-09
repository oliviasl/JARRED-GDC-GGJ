using UnityEngine;

public class Results : MonoBehaviour
{
    public static Results Instance;

    public float similarity = 0.0f;

    public Texture2D screenshot;
    public Texture2D reference;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(this);
    }
}
