using UnityEngine;

public class BrickBlock : MonoBehaviour, IClickable
{
    [SerializeField] private GameObject breakEffectPrefab; // particle prefab, optional
    [SerializeField] private AudioClip breakSound;          // optional
    [SerializeField] private float effectLifeSeconds = 2f;

    public void HandleClick(RaycastHit hit)
    {
        Break();
    }

    public void Break()
    {
        if (UIManager.Instance != null)
            UIManager.Instance.AddPoints(100);

        if (breakEffectPrefab != null)
        {
            var fx = Instantiate(breakEffectPrefab, transform.position, Quaternion.identity);
            Destroy(fx, effectLifeSeconds);
        }

        if (breakSound != null)
            AudioSource.PlayClipAtPoint(breakSound, transform.position);

        Destroy(gameObject);
    }
}