using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ThreadController : MonoBehaviour
{
    List<Action> tasksToRunInMainThread;
    void Start()
    {
        tasksToRunInMainThread = new List<Action>();
    }

    public bool allTaskDone;
    private void Update()
    {
        allTaskDone = tasksToRunInMainThread.Count == 0;
        while (tasksToRunInMainThread.Count > 0)
        {
            Action someTask = tasksToRunInMainThread[0];
            tasksToRunInMainThread.RemoveAt(0);

            someTask();
        }
    }

    public void startThreadedTask(Action someTask)
    {
        Thread t = new Thread(new ThreadStart(someTask));
        t.Start();
    }
    public void queneMainThreadTask( Action someTask)
    {
        tasksToRunInMainThread.Add(someTask);
    }

    /*
    Syntax for function:

    startThreadedTask(() => slowJob() );
    void slowJob()
    {
        Action aTask = () =>
        {

        };
        queneMainThreadTask(aTask);
    }
    */





}
