using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _camera;

    private void Start()
    {
        _camera.transform.position = new Vector3(0, 0.5f, -10);
        _camera.GetComponent<Camera>().orthographicSize = 5.5f;
    }
}
