using UnityEngine;
using UnityEngine.Events;

public class HeatController : MonoBehaviour
{
    [SerializeField] private int currentHeat;
    [SerializeField] private int maxHeat;
    [SerializeField] private int minHeat;

    [SerializeField] private bool isOverheating;
    [SerializeField] private bool isFreezing;

    public event UnityAction<int> onHeatChanged;

    private void OnEnable()
    {
        onHeatChanged += HandeleHeatChanged;
    }

    private void OnDisable()
    {
        onHeatChanged -= HandeleHeatChanged;
    }

    public void HandeleHeatChanged(int temp)
    {
        //heat starts at 10 and goes up or down with +/-
        //colder means -1 or whatever and hotter means +1
        //overheat will be 16 and freezing will be 4
        currentHeat += temp;
        CheckTempeture();
    }
    private void CheckTempeture()
    {
        if(currentHeat >= maxHeat)
        {
            isOverheating = true;
        }
        else if(currentHeat <= minHeat)
        {
            isFreezing = true;
        }
    }
}
