using UnityEngine;
using UnityEngine.InputSystem;

public class TaskTest : PlaneTask
{

    private void Update()
    {
        if(Time.time >= 10f)
        {
            TaskCompleted();
        }
    }

    protected override void TaskCompleted()
    {
        base.TaskCompleted();
        Debug.Log("Done with task");
    }
}
