using UnityEngine;

public class WaterHazard : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<CharacterController>() == null) return;

        Debug.Log("Hit hazard.");
    }
}