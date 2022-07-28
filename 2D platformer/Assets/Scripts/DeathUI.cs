using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathUI : MonoBehaviour
{
    [SerializeField]
    private PlayerHealth playerHealth;
    [SerializeField]
    private GameObject deathUI;

    public void Retry()
    {
        SceneManager.LoadScene(0);
    }

    private void Start()
    {
        playerHealth.Died += OnDeath;
        deathUI.SetActive(false);
    }

    private void OnDeath()
    {
        deathUI.SetActive(true);
    }

}
