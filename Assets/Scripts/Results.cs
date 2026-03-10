using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Results : MonoBehaviour
{
    public static Results Instance;

    public float similarity = 0.0f;

    public Texture2D screenshot;
    public Texture2D reference;

    public string prevSceneName;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(this);

        prevSceneName = SceneManager.GetActiveScene().name;
    }
}