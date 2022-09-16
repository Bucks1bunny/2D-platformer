using UnityEngine;

public class TriggerZone : MonoBehaviour, IUpdateable
{
    [SerializeField]
    private Vector3 newCameraPosition;
    private GameObject camera;

    public void Tick()
    {
        camera.transform.position = newCameraPosition;
        if (camera.transform.position == newCameraPosition)
        {
            UpdateManager.UnregisterLogic(this);
        }
    }

    private void Awake()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            UpdateManager.RegisterLogic(this);
        }
    }

}
