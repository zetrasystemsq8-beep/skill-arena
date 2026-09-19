using UnityEngine;

public class ChallengeManager : MonoBehaviour
{
    [Header("Challenge")]
    public string challengeName = "The First Test";
    public int scorePerSuccess = 100;

    private int successfulActions;

    public void StartChallenge()
    {
        successfulActions = 0;

        Debug.Log("Challenge Started: " + challengeName);
    }

    public void SuccessfulAction()
    {
        successfulActions++;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(scorePerSuccess);
        }

        Debug.Log("Successful Action: " + successfulActions);
    }

    public int GetSuccessfulActions()
    {
        return successfulActions;
    }
}
