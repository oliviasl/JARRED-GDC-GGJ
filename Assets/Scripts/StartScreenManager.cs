using NaughtyAttributes;
using UnityEngine;

public class StartScreenManager : MonoBehaviour
{
    [SerializeField] private SceneController sceneControlller;
    [SerializeField, Scene] private string firstLevel;
    public void StartGame()
    {
        sceneControlller.SetSceneToLoad(firstLevel);
        sceneControlller.LoadScene();
    }

    public void LevelSelect()
    {
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
