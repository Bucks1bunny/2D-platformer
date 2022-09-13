using System;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour, IUpdateable
{
    public event Action TimeEnded = delegate { };

    public float CurrentTime
    {
        get;
        set;
    }

    [SerializeField]
    private TextMeshProUGUI timer;

    void IUpdateable.Tick()
    {
        if (CurrentTime > 0)
        {
            CurrentTime -= Time.deltaTime;
            DisplayTime(CurrentTime);
        }
        else
        {
            CurrentTime = 0;
            DisplayTime(CurrentTime);
            TimeEnded();
            UpdateManager.UnregisterLogic(this);
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StartTimer()
    {
        UpdateManager.RegisterLogic(this);
    }

    public void StopTimer()
    {
        UpdateManager.UnregisterLogic(this);
    }
}
