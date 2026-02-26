using UnityEngine;

public class CameraScroller : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime = 0.12f;

    [Header("Clamp X")]
    [SerializeField] private bool clampX = false;
    [SerializeField] private float minX = 0f;
    [SerializeField] private float maxX = 200f;

    private float _xVelocity;

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 pos = transform.position;

        float desiredX = target.position.x;
        float newX = Mathf.SmoothDamp(pos.x, desiredX, ref _xVelocity, smoothTime);

        if (clampX)
            newX = Mathf.Clamp(newX, minX, maxX);

        transform.position = new Vector3(newX, pos.y, pos.z);
    }
}