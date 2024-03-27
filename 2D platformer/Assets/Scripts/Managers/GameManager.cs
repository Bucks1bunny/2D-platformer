using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _camera;
    [SerializeField]
    private Vector3 startPosition;
    [SerializeField]
    private float cameraSize;


    private void Start()
    {
        _camera.transform.position = startPosition;
        _camera.GetComponent<Camera>().orthographicSize = cameraSize;
    }
}
