using UnityEngine;

public class ClashDashIdentity : MonoBehaviour
{
    [Header("Game Identity")]
    [SerializeField] private string gameName = "CLASHDASH";
    [SerializeField] private string studioName = "ZETRA";
    [SerializeField] private string creatorName = "CONNECT";

    [Header("World Identity")]
    [SerializeField] private string arenaName = "THE TEST";
    [SerializeField] private string seasonName = "ZERO";

    [Header("Hidden World References")]
    [SerializeField] private string[] worldReferences =
    {
        "ZETRA",
        "CONNECT",
        "ZetraMail",
        "Nigergram",
        "NaijaLearn",
        "ZETRA STORE",
        "CRUCIBLE",
        "TRIBUNAL",
        "NAI",
        "TOLUWANI",
        "TOFUMI",
        "FOLAKEMI",
        "MARVELLOUS"
    };

    private void Awake()
    {
        Application.targetFrameRate = 60;

        Debug.Log(
            gameName +
            " // " +
            studioName +
            " // " +
            creatorName
        );
    }

    public string GetGameName()
    {
        return gameName;
    }

    public string GetStudioName()
    {
        return studioName;
    }

    public string GetCreatorName()
    {
        return creatorName;
    }

    public string GetArenaName()
    {
        return arenaName;
    }

    public string GetSeasonName()
    {
        return seasonName;
    }

    public string[] GetWorldReferences()
    {
        return worldReferences;
    }
}
