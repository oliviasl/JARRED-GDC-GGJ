using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Events;

public class PlaneTask : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private UnityEvent onStart;
    [SerializeField] private UnityEvent onComplete;

    [Header("Feedbacks")]
    [SerializeField] private MMF_Player onStartFeedbacks;
    [SerializeField] private MMF_Player onCompleteFeedbacks;

    private void Start()
    {
        onStart.Invoke();
        if(onStartFeedbacks != null)
        {
            onStartFeedbacks?.PlayFeedbacks();
        }
        
    }

    protected virtual void TaskCompleted()
    {
        onComplete.Invoke();
        if(onCompleteFeedbacks != null)
        {
            onCompleteFeedbacks?.PlayFeedbacks();
        }
        Debug.Log("OnCompleteOccured");
        
    }
}
