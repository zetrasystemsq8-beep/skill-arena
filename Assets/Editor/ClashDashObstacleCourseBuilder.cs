#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

public class ClashDashObstacleCourseBuilder : EditorWindow
{
    private Material obstacleMaterial;
    private Material energyMaterial;

    [MenuItem("CLASHDASH/BUILD OBSTACLE COURSE")]
    public static void Open()
    {
        GetWindow<ClashDashObstacleCourseBuilder>(
            "OBSTACLE COURSE"
        );
    }

    private void OnGUI()
    {
        GUILayout.Label(
            "CLASHDASH // THE TEST",
            EditorStyles.boldLabel
        );

        GUILayout.Space(10);

        obstacleMaterial =
            (Material)EditorGUILayout.ObjectField(
                "Obstacle Material",
                obstacleMaterial,
                typeof(Material),
                false
            );

        energyMaterial =
            (Material)EditorGUILayout.ObjectField(
                "Energy Material",
                energyMaterial,
                typeof(Material),
                false
            );

        GUILayout.Space(15);

        if (GUILayout.Button(
            "BUILD COURSE",
            GUILayout.Height(50)
        ))
        {
            BuildCourse();
        }
    }

    private void BuildCourse()
    {
        GameObject root =
            new GameObject(
                "CLASHDASH // OBSTACLE COURSE"
            );

        CreateMovingWall(
            root.transform,
            new Vector3(0f, 2f, 15f),
            new Vector3(3f, 4f, 1f)
        );

        CreateMovingWall(
            root.transform,
            new Vector3(-12f, 2f, 28f),
            new Vector3(2f, 4f, 5f)
        );

        CreateMovingWall(
            root.transform,
            new Vector3(12f, 2f, 38f),
            new Vector3(2f, 5f, 5f)
        );

        CreateRotatingBar(
            root.transform,
            new Vector3(0f, 2f, 48f)
        );

        CreateMovingWall(
            root.transform,
            new Vector3(-8f, 3f, 58f),
            new Vector3(2f, 6f, 4f)
        );

        CreateRotatingBar(
            root.transform,
            new Vector3(0f, 4f, 68f)
        );

        CreateCheckpoint(
            root.transform,
            new Vector3(0f, 2f, 25f),
            1
        );

        CreateCheckpoint(
            root.transform,
            new Vector3(0f, 2f, 42f),
            2
        );

        CreateCheckpoint(
            root.transform,
            new Vector3(0f, 5f, 58f),
            3
        );

        CreateCheckpoint(
            root.transform,
            new Vector3(0f, 5f, 72f),
            4
        );

        CreateFinish(
            root.transform,
            new Vector3(0f, 6f, 80f)
        );

        Debug.Log(
            "CLASHDASH // OBSTACLE COURSE BUILT"
        );
    }

    private void CreateMovingWall(
        Transform parent,
        Vector3 position,
        Vector3 scale
    )
    {
        GameObject wall =
            GameObject.CreatePrimitive(
                PrimitiveType.Cube
            );

        wall.name =
            "MOVING WALL";

        wall.transform.SetParent(parent);
        wall.transform.position = position;
        wall.transform.localScale = scale;

        ApplyMaterial(
            wall,
            obstacleMaterial
        );

        wall.AddComponent<
            ClashDashObstaclePattern
        >();

        wall.AddComponent<
            ClashDashObstacleImpact
        >();
    }

    private void CreateRotatingBar(
        Transform parent,
        Vector3 position
    )
    {
        GameObject bar =
            GameObject.CreatePrimitive(
                PrimitiveType.Cube
            );

        bar.name =
            "ROTATING DANGER BAR";

        bar.transform.SetParent(parent);
        bar.transform.position = position;

        bar.transform.localScale =
            new Vector3(
                16f,
                0.6f,
                0.8f
            );

        ApplyMaterial(
            bar,
            energyMaterial
        );

        ClashDashObstaclePattern pattern =
            bar.AddComponent<
                ClashDashObstaclePattern
            >();

        bar.AddComponent<
            ClashDashObstacleImpact
        >();
    }

    private void CreateCheckpoint(
        Transform parent,
        Vector3 position,
        int number
    )
    {
        GameObject checkpoint =
            GameObject.CreatePrimitive(
                PrimitiveType.Cylinder
            );

        checkpoint.name =
            "CHECKPOINT " + number;

        checkpoint.transform.SetParent(parent);
        checkpoint.transform.position = position;

        checkpoint.transform.localScale =
            new Vector3(
                3f,
                0.15f,
                3f
            );

        Collider collider =
            checkpoint.GetComponent<Collider>();

        collider.isTrigger = true;

        checkpoint.AddComponent<
            ClashDashCheckpointSystem
        >();
    }

    private void CreateFinish(
        Transform parent,
        Vector3 position
    )
    {
        GameObject finish =
            GameObject.CreatePrimitive(
                PrimitiveType.Cube
            );

        finish.name =
            "CLASHDASH FINISH GATE";

        finish.transform.SetParent(parent);
        finish.transform.position = position;

        finish.transform.localScale =
            new Vector3(
                14f,
                8f,
                1f
            );

        Collider collider =
            finish.GetComponent<Collider>();

        collider.isTrigger = true;

        finish.AddComponent<
            ClashDashFinishSystem
        >();
    }

    private void ApplyMaterial(
        GameObject obj,
        Material material
    )
    {
        if (material == null)
            return;

        Renderer renderer =
            obj.GetComponent<Renderer>();

        if (renderer != null)
            renderer.sharedMaterial =
                material;
    }
}

#endif
