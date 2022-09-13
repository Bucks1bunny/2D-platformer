using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IUpdateable
{
    [SerializeField]
    private Timer timer;
    [SerializeField]
    private float levelTime;
    private HashSet<GameObject> enemies = new HashSet<GameObject>();

    private void Start()
    {
        timer.TimeEnded += OnTimeEnded;

        timer.CurrentTime = levelTime;
        timer.StartTimer();

    }

    private void OnTimeEnded()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Tick()
    {
    }
}
