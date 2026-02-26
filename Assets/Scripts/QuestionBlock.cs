using UnityEngine;

public class QuestionBlock : MonoBehaviour
{
    [SerializeField] private int coinsPerHit = 1;
    [SerializeField] private int coinsRemaining = 1;

    [Header("Effects")]
    [SerializeField] private GameObject coinPopPrefab;
    [SerializeField] private float coinPopLifeSeconds = 1.0f;

    public void GiveCoin()
    {
        if (coinsRemaining <= 0) return;

        int amountToGive = Mathf.Min(coinsPerHit, coinsRemaining);
        coinsRemaining -= amountToGive;

        if (UIManager.Instance != null)
            UIManager.Instance.AddCoins(amountToGive);

        if (coinPopPrefab != null)
        {
            Vector3 spawnPos = transform.position + Vector3.up * 0.75f;
            var coin = Instantiate(coinPopPrefab, spawnPos, Quaternion.identity);
            Destroy(coin, coinPopLifeSeconds);
        }
    }
}