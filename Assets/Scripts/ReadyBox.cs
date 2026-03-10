using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReadyBox : MonoBehaviour
{
    public List<UnityEvent> events;
    public void OnTriggerEnter(Collider other)
    {
        foreach (UnityEvent e in events)
        {
            e.Invoke();
        }
    }
}
