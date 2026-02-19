using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera targetCamera;
    [SerializeField] private LayerMask clickableMask = ~0;

    private void Awake()
    {
        if (targetCamera == null) targetCamera = Camera.main;
    }

    private void Update()
    {
        // New Input System mouse click
        if (Mouse.current == null) return;
        if (!Mouse.current.leftButton.wasPressedThisFrame) return;

        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = targetCamera.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hit, 500f, clickableMask))
        {
            var clickable = hit.collider.GetComponentInParent<IClickable>();
            if (clickable != null)
                clickable.HandleClick(hit);
        }
    }
}