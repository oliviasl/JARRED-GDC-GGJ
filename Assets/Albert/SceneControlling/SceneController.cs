using UnityEngine;
using UnityEngine.InputSystem;
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

    void Update()
    {
        if (Keyboard.current.rKey.isPressed && Keyboard.current.digit0Key.isPressed)
        {
            EmergencyExitToMenu();
        }
    }

    void EmergencyExitToMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}