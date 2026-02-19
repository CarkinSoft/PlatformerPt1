using UnityEngine;

public class BrickBlock : MonoBehaviour, IClickable
{
    [SerializeField] private GameObject breakEffectPrefab; // particle prefab, optional
    [SerializeField] private AudioClip breakSound;          // optional
    [SerializeField] private float effectLifeSeconds = 2f;

    public void HandleClick(RaycastHit hit)
    {
        Destroy(gameObject);
    }
}