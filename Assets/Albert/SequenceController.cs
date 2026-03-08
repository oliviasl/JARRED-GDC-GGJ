using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;

public class SequenceController : MonoBehaviour
{
    public static SequenceController instance { get; private set; }

    public bool CurrentlyPlaying => currentDirector.state == PlayState.Paused;

    public PlayableDirector CurrentDirector => instance.currentDirector;
    [SerializeField] private PlayableDirector currentDirector;

    public bool PlayOnAwake => _playOnStart;
    [FormerlySerializedAs("_playOnAwake")][SerializeField] private bool _playOnStart;
    [SerializeField] private bool startCutsceneHidePlayers;
    [SerializeField] private bool startCutsceneFreezePlayers;




    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlayDefaultSequence(PlayableDirector director) => Play(director);


    private void OnTimelineStopped(PlayableDirector director)
    {
        
        
        currentDirector.stopped -= OnTimelineStopped;
    }
    public void Play(PlayableDirector director = null)
    {
        
        if (director)
        {
            //lock movement
            if (CurrentlyPlaying)
            {
                
                // stop current cutscene
                OnTimelineStopped(currentDirector);
            }
            currentDirector = director;
        }

        
        currentDirector.stopped += OnTimelineStopped;
        currentDirector.Play();
    }
}
