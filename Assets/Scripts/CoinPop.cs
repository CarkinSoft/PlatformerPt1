using UnityEngine;

public class CoinPop : MonoBehaviour
{
    [SerializeField] private float speed = 2.0f;

    private void Update()
    {
        transform.position += Vector3.up * (speed * Time.deltaTime);
    }
}