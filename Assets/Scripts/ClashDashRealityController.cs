using System.Collections;
using UnityEngine;

public class ClashDashRealityController : MonoBehaviour
{
    [Header("Reality Layers")]
    [SerializeField] private Transform[] dimensionalLayers;
    [SerializeField] private Transform[] floatingStructures;
    [SerializeField] private Transform[] energyRings;

    [Header("Motion")]
    [SerializeField] private float layerSpeed = 0.15f;
    [SerializeField] private float structureSpeed = 8f;
    [SerializeField] private float ringSpeed = 35f;

    [Header("Reality Distortion")]
    [SerializeField] private float distortionAmount = 0.4f;
    [SerializeField] private float distortionSpeed = 2f;

    [Header("World Scale")]
    [SerializeField] private float floatingHeight = 0.35f;
    [SerializeField] private float floatingFrequency = 1.2f;

    private Vector3[] startingPositions;
    private Vector3[] structurePositions;

    private void Start()
    {
        CachePositions();
    }

    private void Update()
    {
        AnimateRealityLayers();
        AnimateFloatingStructures();
        AnimateEnergyRings();
        ApplyRealityMotion();
    }

    private void CachePositions()
    {
        if (dimensionalLayers != null)
        {
            startingPositions =
                new Vector3[dimensionalLayers.Length];

            for (int i = 0; i < dimensionalLayers.Length; i++)
            {
                if (dimensionalLayers[i] != null)
                    startingPositions[i] =
                        dimensionalLayers[i].position;
            }
        }

        if (floatingStructures != null)
        {
            structurePositions =
                new Vector3[floatingStructures.Length];

            for (int i = 0; i < floatingStructures.Length; i++)
            {
                if (floatingStructures[i] != null)
                    structurePositions[i] =
                        floatingStructures[i].position;
            }
        }
    }

    private void AnimateRealityLayers()
    {
        if (dimensionalLayers == null)
            return;

        for (int i = 0; i < dimensionalLayers.Length; i++)
        {
            Transform layer =
                dimensionalLayers[i];

            if (layer == null)
                continue;

            float offset =
                Mathf.Sin(
                    Time.time *
                    layerSpeed +
                    i
                ) * distortionAmount;

            layer.localPosition =
                startingPositions[i] +
                Vector3.forward * offset;
        }
    }

    private void AnimateFloatingStructures()
    {
        if (floatingStructures == null)
            return;

        for (int i = 0; i < floatingStructures.Length; i++)
        {
            Transform structure =
                floatingStructures[i];

            if (structure == null)
                continue;

            float height =
                Mathf.Sin(
                    Time.time *
                    floatingFrequency +
                    i * 0.7f
                ) * floatingHeight;

            structure.position =
                structurePositions[i] +
                Vector3.up * height;

            structure.Rotate(
                Vector3.up,
                structureSpeed *
                Time.deltaTime,
                Space.World
            );
        }
    }

    private void AnimateEnergyRings()
    {
        if (energyRings == null)
            return;

        for (int i = 0; i < energyRings.Length; i++)
        {
            Transform ring =
                energyRings[i];

            if (ring == null)
                continue;

            float direction =
                i % 2 == 0 ? 1f : -1f;

            ring.Rotate(
                Vector3.forward,
                ringSpeed *
                direction *
                Time.deltaTime,
                Space.Self
            );
        }
    }

    private void ApplyRealityMotion()
    {
        float wave =
            Mathf.Sin(
                Time.time *
                distortionSpeed
            );

        transform.localScale =
            Vector3.one *
            (1f + wave * 0.003f);
    }

    public void RealityShift()
    {
        StopAllCoroutines();
        StartCoroutine(RealityShiftSequence());
    }

    private IEnumerator RealityShiftSequence()
    {
        float originalAmount =
            distortionAmount;

        distortionAmount = 2f;

        yield return new WaitForSeconds(0.18f);

        distortionAmount =
            originalAmount;
    }
}
