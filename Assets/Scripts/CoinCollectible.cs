using UnityEngine;

public class CoinCollectible : MonoBehaviour
{
    [SerializeField] private int coinValue = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<CharacterController>() == null) return;

        if (UIManager.Instance != null)
            UIManager.Instance.AddCoins(coinValue);

        Destroy(gameObject);
    }
}