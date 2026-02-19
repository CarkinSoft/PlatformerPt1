using UnityEngine;

public class QuestionBlock : MonoBehaviour, IClickable
{
    [SerializeField] private int coinsPerClick = 1;

    public void HandleClick(RaycastHit hit)
    {
        UIManager.Instance.AddCoins(coinsPerClick);
    }
}