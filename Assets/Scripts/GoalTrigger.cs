using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<CharacterController>() == null) return;

        Debug.Log("Level complete.");
    }
}