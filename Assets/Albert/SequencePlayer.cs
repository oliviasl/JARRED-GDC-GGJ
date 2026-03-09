using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class SequencePlayer : MonoBehaviour
{
    [SerializeField] private PlayableDirector boundDirector;
    [SerializeField] private UnityEvent onSequenceFinishedPlaying;

    private void Awake()
    {
        boundDirector.stopped += FinishedPlaying;
    }

    private void OnDestroy()
    {
        boundDirector.stopped -= FinishedPlaying;
    }

    private void FinishedPlaying(PlayableDirector director)
    {
        onSequenceFinishedPlaying.Invoke();
    }

    
    public void PlayDefaultSequence() {
        Debug.Log(SequenceController.instance);
        SequenceController.instance.Play(boundDirector);
            }

    private void Reset()
    {
        if (!boundDirector)
        {
            boundDirector = GetComponent<PlayableDirector>();
        }
    }
}

