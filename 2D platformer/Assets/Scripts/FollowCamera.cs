using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour, IUpdateable
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Vector3 startPosition;
    [SerializeField]
    private float cameraSize;

    private void Awake()
    {
        UpdateManager.RegisterLogic(this);
    }

    private void Start()
    {
        transform.position = startPosition;
        GetComponent<Camera>().orthographicSize = cameraSize;
    }

    public void Tick()
    {
        if (player == null)
        {
            UpdateManager.UnregisterLogic(this);
        }
    }
}
