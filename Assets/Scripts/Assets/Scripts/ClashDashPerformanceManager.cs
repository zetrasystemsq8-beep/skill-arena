using UnityEngine;

public class ClashDashPerformanceManager : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private int targetFrameRate = 60;

    [Header("Mobile Quality")]
    [SerializeField] private bool optimizeForMobile = true;
    [SerializeField] private int mobileShadowDistance = 35;
    [SerializeField] private int mobilePixelLightCount = 2;

    private void Awake()
    {
        ConfigurePerformance();
    }

    private void ConfigurePerformance()
    {
        Application.targetFrameRate =
            targetFrameRate;

        QualitySettings.vSyncCount = 0;

        if (!optimizeForMobile)
            return;

        QualitySettings.pixelLightCount =
            mobilePixelLightCount;

        QualitySettings.shadowDistance =
            mobileShadowDistance;

        QualitySettings.anisotropicFiltering =
            AnisotropicFiltering.Enable;

        QualitySettings.softParticles = true;

        QualitySettings.realtimeReflectionProbes =
            true;

        QualitySettings.billboardsFaceCameraPosition =
            true;
    }

    public void SetHighQuality()
    {
        QualitySettings.pixelLightCount = 4;
        QualitySettings.shadowDistance = 60f;

        QualitySettings.realtimeReflectionProbes = true;
    }

    public void SetBalancedQuality()
    {
        QualitySettings.pixelLightCount = 2;
        QualitySettings.shadowDistance = 35f;

        QualitySettings.realtimeReflectionProbes = true;
    }

    public void SetPerformanceMode()
    {
        QualitySettings.pixelLightCount = 1;
        QualitySettings.shadowDistance = 20f;

        QualitySettings.realtimeReflectionProbes = false;
    }
}
