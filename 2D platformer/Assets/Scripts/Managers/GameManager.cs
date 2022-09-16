using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IUpdateable
{
    [SerializeField]
    private GameObject _camera;
    [SerializeField]
    private Timer timer;
    [SerializeField]
    private float levelTime;

    private void Start()
    {
        _camera.transform.position = new Vector3(0, 0, -10);

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
