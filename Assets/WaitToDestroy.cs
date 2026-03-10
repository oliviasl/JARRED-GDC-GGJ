using MoreMountains.Feedbacks;
using System.Collections;
using UnityEngine;

public class WaitToDestroy : MonoBehaviour
{
    [SerializeField] private int delayTime;
    [SerializeField] MMF_Player player;
    public void DestroyThatHoe()
    {
        StartCoroutine(WaitToDestroyPlane(delayTime));
    }

    IEnumerator WaitToDestroyPlane(int delay)
    {
        yield return new WaitForSeconds(delay);
        player.PlayFeedbacks();
    }
}
