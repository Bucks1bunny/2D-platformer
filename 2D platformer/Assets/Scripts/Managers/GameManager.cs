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
        _camera.transform.position = new Vector3(0, 0.5f, -10);
        _camera.GetComponent<Camera>().orthographicSize = 5.5f;

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
