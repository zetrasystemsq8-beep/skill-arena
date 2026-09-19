using UnityEngine;

public class ClashDashObstacleSpawner : MonoBehaviour
{
    [Header("Obstacle Prefabs")]
    [SerializeField] private GameObject[] obstaclePrefabs;

    [Header("Spawn Area")]
    [SerializeField] private Transform spawnArea;
    [SerializeField] private int obstacleCount = 20;

    [Header("Spacing")]
    [SerializeField] private float minSpacing = 5f;
    [SerializeField] private float heightVariation = 4f;

    [Header("Animation")]
    [SerializeField] private bool animateObstacles = true;

    private void Start()
    {
        SpawnObstacles();
    }

    public void SpawnObstacles()
    {
        if (obstaclePrefabs == null ||
            obstaclePrefabs.Length == 0 ||
            spawnArea == null)
            return;

        for (int i = 0; i < obstacleCount; i++)
        {
            GameObject prefab =
                obstaclePrefabs[
                    Random.Range(
                        0,
                        obstaclePrefabs.Length
                    )
                ];

            Vector3 position =
                GetSpawnPosition(i);

            GameObject obstacle =
                Instantiate(
                    prefab,
                    position,
                    Quaternion.identity,
                    spawnArea
                );

            if (animateObstacles)
            {
                ObstacleController controller =
                    obstacle.GetComponent<
                        ObstacleController
                    >();

                if (controller == null)
                {
                    controller =
                        obstacle.AddComponent<
                            ObstacleController
                        >();
                }
            }
        }
    }

    private Vector3 GetSpawnPosition(int index)
    {
        Bounds bounds =
            GetSpawnBounds();

        float x =
            Random.Range(
                bounds.min.x,
                bounds.max.x
            );

        float z =
            bounds.min.z +
            index * minSpacing;

        z = Mathf.Min(
            z,
            bounds.max.z
        );

        float y =
            bounds.min.y +
            Random.Range(
                0f,
                heightVariation
            );

        return new Vector3(
            x,
            y,
            z
        );
    }

    private Bounds GetSpawnBounds()
    {
        Collider collider =
            spawnArea.GetComponent<Collider>();

        if (collider != null)
            return collider.bounds;

        return new Bounds(
            spawnArea.position,
            new Vector3(
                40f,
                5f,
                80f
            )
        );
    }

    public void ClearObstacles()
    {
        if (spawnArea == null)
            return;

        for (int i =
            spawnArea.childCount - 1;
            i >= 0;
            i--)
        {
            Destroy(
                spawnArea.GetChild(i).gameObject
            );
        }
    }
}
