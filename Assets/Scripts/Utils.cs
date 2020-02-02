using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    bool interact(GameObject user);
}

public class Timer
{
    public float time { get; private set; }
    private float presetTime;
    public bool Done { get; private set; }

    public Timer(float duration)
    {
        time = 0;
        presetTime = duration;
        Done = false;
    }

    public void Reset()
    {
        //Debug.Log("Reset");

        time = 0;
        Done = false;
    }

    // Call during Start()
    public void Set(float duration)
    {
        presetTime = duration;
        Reset();
    }

    // Call during Update()
    public void Update()
    {
        if (time < presetTime )
        {
            time += Time.deltaTime;
        } else
        {
            time = presetTime;
            Done = true;
        }
    }

    public int GetPercentDone()
    {
        return (int)(100 * time / presetTime);
    }
}