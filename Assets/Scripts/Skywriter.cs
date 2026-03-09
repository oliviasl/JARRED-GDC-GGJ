using UnityEngine;

public class Skywriter : MonoBehaviour
{
    //TODO: Change
    [SerializeField] private GameObject smokePrefab;
    [SerializeField] private Transform smokeSpawnPoint;


    [SerializeField] private GameObject minimapVisual;
    private CharacterInput characterInput;

    void Awake()
    {
        characterInput = GetComponent<CharacterInput>();

        Instantiate(minimapVisual, this.transform);
    }

    void Update()
    {
        if (characterInput.GetInteractInput())
        {
            Instantiate(smokePrefab, smokeSpawnPoint.position, smokeSpawnPoint.rotation);
        }
    }
}
