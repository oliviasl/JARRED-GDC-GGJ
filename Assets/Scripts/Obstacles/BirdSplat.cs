using UnityEngine;
using UnityEngine.UI;

public class BirdSplat : MonoBehaviour
{
    [SerializeField] private Material matOneWipe;
    [SerializeField] private Material matTwoWipe;
    [SerializeField] private SkinnedMeshRenderer mesh;

    private int _health = 3;
    private QTEMinigame _qteMinigame;

    void Start()
    {
        if (!mesh)
        {
            mesh = GetComponentInChildren<SkinnedMeshRenderer>();
        }

        _qteMinigame = FindAnyObjectByType<QTEMinigame>();
        if (_qteMinigame)
        {
            _qteMinigame.onSuccess.AddListener(WipeBird);
        }
    }

    private void OnDisable()
    {
        if (_qteMinigame)
        {
            _qteMinigame.onSuccess.RemoveListener(WipeBird);
        }
    }

    void WipeBird()
    {
        --_health;
        
        if (_health == 2)
        {
            mesh.material = matOneWipe;
        }
        else if (_health == 1)
        {
            mesh.material = matTwoWipe;
        }
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
