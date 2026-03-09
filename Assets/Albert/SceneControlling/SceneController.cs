using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] string SceneToLoad;
   public void LoadScene()
    {
        SceneManager.LoadScene(SceneToLoad);
    }
}
