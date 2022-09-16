using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour, IUpdateable
{
    [SerializeField]
    private GameObject startUI;

    void Awake()
    {
        Time.timeScale = 0;
        startUI.SetActive(true);

        UpdateManager.RegisterLogic(this);
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

}
