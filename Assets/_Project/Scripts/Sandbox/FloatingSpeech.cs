using UnityEngine;

public class FloatingSpeech : MonoBehaviour
{
    private Camera _camera;

    void Awake()
    {
        _camera = Camera.main;
    }

    void LateUpdate()
    {
        transform.rotation = _camera.transform.rotation;
    }
}
