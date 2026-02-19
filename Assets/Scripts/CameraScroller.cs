using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScroller : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float minX = 0f;
    [SerializeField] private float maxX = 200f;

    private void Update()
    {
        float dx = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.leftArrowKey.isPressed) dx -= 1f;
            if (Keyboard.current.rightArrowKey.isPressed) dx += 1f;
        }

        if (dx == 0f) return;

        var pos = transform.position;
        pos.x = Mathf.Clamp(pos.x + dx * speed * Time.deltaTime, minX, maxX);
        transform.position = pos;
    }
}