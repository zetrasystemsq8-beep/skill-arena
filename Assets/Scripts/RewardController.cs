using UnityEngine;

public class RewardController : MonoBehaviour
{
    [Header("Rewards")]
    [SerializeField] private int xpReward = 100;
    [SerializeField] private int coinReward = 50;

    public void GiveReward()
    {
        if (ProgressionManager.Instance == null)
            return;

        ProgressionManager.Instance.AddXP(xpReward);
        ProgressionManager.Instance.AddCoins(coinReward);

        Debug.Log(
            "REWARD  +" + xpReward +
            " XP  +" + coinReward + " COINS"
        );
    }
}
