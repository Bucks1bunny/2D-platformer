using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour, IUpdateable
{
    [SerializeField]
    private PlayerHealth playerHealth;
    [SerializeField]
    private Timer timer;
    [SerializeField]
    private GameObject startUI;
    [SerializeField]
    private GameObject deathUI;

    public float levelTime;

    void Awake()
    {
        Time.timeScale = 0;
        startUI.SetActive(true);

        timer.TimeEnded += OnTimeEnded;
        timer.CurrentTime = levelTime;
        timer.StartTimer();

        UpdateManager.RegisterLogic(this);
    }

    private void OnTimeEnded()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Start()
    {
        playerHealth.Died += OnDeath;
        deathUI.SetActive(false);
    }

    private void OnDeath()
    {
        deathUI.SetActive(true);
        timer.StopTimer();
    }

    public void Tick()
    {
        if (Input.anyKey)
        {
            Time.timeScale = 1;
            startUI.SetActive(false);
            UpdateManager.UnregisterLogic(this);
        }
    }

    public void Retry()
    {
        Debug.Log("Click");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void DebugTry()
    {
        Debug.Log("click");
    }
}
