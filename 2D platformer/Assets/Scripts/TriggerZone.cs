using UnityEngine;

public class TriggerZone : MonoBehaviour, IUpdateable
{
    [SerializeField]
    private Vector3 newCameraPosition;
    [SerializeField]
    private float cameraSize;
    public GameObject Camera
    {
        get;
        private set;
    }

    public void Tick()
    {
        Camera.transform.position = newCameraPosition;
        if (Camera.transform.position == newCameraPosition)
        {
            UpdateManager.UnregisterLogic(this);
        }
    }

    private void Awake()
    {
        Camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UpdateManager.RegisterLogic(this);
        }
        Camera.GetComponent<Camera>().orthographicSize = cameraSize;
    }

}
