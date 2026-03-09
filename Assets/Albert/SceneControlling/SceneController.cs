using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    [SerializeField] string SceneToLoad;

    void Awake()
    {
        Instance = this;
    }

   public void LoadScene()
    {
        SceneManager.LoadScene(SceneToLoad);
    }

    public void SetSceneToLoad(string newLevel)
    {
        SceneToLoad = newLevel;
    }
}