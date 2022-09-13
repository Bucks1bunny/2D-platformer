using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour, IUpdateable
{
    [SerializeField]
    private Transform player;
    private float offset = -10;
    private float minX, maxX, minY, maxY;

    private void Awake()
    {
        UpdateManager.RegisterLogic(this);
    }

    public void Tick()
    {
        if (player == null)
        {
            UpdateManager.UnregisterLogic(this);
        }
    }
}
