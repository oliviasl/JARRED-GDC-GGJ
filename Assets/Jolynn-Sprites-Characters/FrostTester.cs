using UnityEngine;

public class FrostTester : MonoBehaviour
{
    [SerializeField] private FrostOverlay frostOverlay;
    [SerializeField] private float speed = 1f; // units per second

    private float frostLevel = 0f;
    private bool goingUp = true;

    void Update()
    {
        frostLevel += (goingUp ? 1f : -1f) * speed * Time.deltaTime;

        if (frostLevel >= 6f)
        {
            frostLevel = 6f;
            goingUp = false;
        }
        else if (frostLevel <= 0f)
        {
            frostLevel = 0f;
            goingUp = true;
        }

        if (frostOverlay)
        {
            frostOverlay.SetFrostLevel(frostLevel);
        }
    }
}