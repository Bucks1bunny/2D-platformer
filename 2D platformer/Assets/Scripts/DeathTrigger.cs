using System;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    public event Action deathtriggered = delegate { };
    private void OnTriggerEnter2D(Collider2D collision)
    {
        deathtriggered();
    }
}
