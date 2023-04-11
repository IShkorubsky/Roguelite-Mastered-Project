using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera _myCamera;
    public Transform _centerOfMap;
    private void Start()
    {
        _myCamera = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        _myCamera.transform.LookAt(_centerOfMap);
    }
}
