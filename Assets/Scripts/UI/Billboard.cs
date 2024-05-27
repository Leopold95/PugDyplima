using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] Transform _camera;

    private void LateUpdate()
    {
        transform.LookAt(transform.position + _camera.forward);
    }
}
