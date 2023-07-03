using UnityEngine;

public class RotateAround : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float rotationSpeed;

    private void Update()
    {
        transform.RotateAround(target.transform.position,Vector3.up,rotationSpeed * Time.deltaTime);
    }
}
