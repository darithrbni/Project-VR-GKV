using UnityEngine;

public class BillboardUI : MonoBehaviour
{
    private Transform cam;

    private void Start()
    {
        cam = Camera.main.transform;
    }

    private void LateUpdate()
    {
        if (cam == null) return;

        transform.LookAt(transform.position + cam.forward);
    }
}