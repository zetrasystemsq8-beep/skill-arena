#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

public class ClashDashEnvironmentBuilder : EditorWindow
{
    private Material structureMaterial;
    private Material energyMaterial;
    private Material hologramMaterial;

    private int towerCount = 18;
    private int platformCount = 16;
    private int ringCount = 10;

    [MenuItem("CLASHDASH/ZETRA Environment")]
    public static void Open()
    {
        GetWindow<ClashDashEnvironmentBuilder>(
            "CLASHDASH ENVIRONMENT"
        );
    }

    private void OnGUI()
    {
        GUILayout.Label(
            "CLASHDASH // ZETRA WORLD",
            EditorStyles.boldLabel
        );

        GUILayout.Space(10);

        towerCount = EditorGUILayout.IntSlider(
            "Mega Structures",
            towerCount,
            4,
            40
        );

        platformCount = EditorGUILayout.IntSlider(
            "Floating Platforms",
            platformCount,
            4,
            40
        );

        ringCount = EditorGUILayout.IntSlider(
            "Energy Rings",
            ringCount,
            3,
            24
        );

        GUILayout.Space(10);

        structureMaterial =
            (Material)EditorGUILayout.ObjectField(
                "Structure",
                structureMaterial,
                typeof(Material),
                false
            );

        energyMaterial =
            (Material)EditorGUILayout.ObjectField(
                "Energy",
                energyMaterial,
                typeof(Material),
                false
            );

        hologramMaterial =
            (Material)EditorGUILayout.ObjectField(
                "Hologram",
                hologramMaterial,
                typeof(Material),
                false
            );

        GUILayout.Space(15);

        if (GUILayout.Button(
            "GENERATE 5D WORLD",
            GUILayout.Height(50)
        ))
        {
            GenerateWorld();
        }

        if (GUILayout.Button(
            "GENERATE SKY CITY",
            GUILayout.Height(40)
        ))
        {
            GenerateSkyCity();
        }

        if (GUILayout.Button(
            "GENERATE ENERGY NETWORK",
            GUILayout.Height(40)
        ))
        {
            GenerateEnergyNetwork();
        }
    }

    private void GenerateWorld()
    {
        GameObject root =
            CreateRoot("CLASHDASH // 5D WORLD");

        GenerateStructures(root.transform);
        GeneratePlatforms(root.transform);
        GenerateRings(root.transform);
        GenerateSkyBridges(root.transform);

        Debug.Log(
            "CLASHDASH // 5D WORLD GENERATED"
        );
    }

    private void GenerateStructures(
        Transform parent
    )
    {
        for (int i = 0; i < towerCount; i++)
        {
            float angle =
                i * Mathf.PI * 2f / towerCount;

            float radius =
                Random.Range(45f, 75f);

            Vector3 position =
                new Vector3(
                    Mathf.Cos(angle) * radius,
                    Random.Range(5f, 35f),
                    Mathf.Sin(angle) * radius
                );

            GameObject tower =
                GameObject.CreatePrimitive(
                    PrimitiveType.Cube
                );

            tower.name =
                "ZETRA MEGA SPIRE";

            tower.transform.SetParent(parent);

            tower.transform.position =
                position;

            tower.transform.localScale =
                new Vector3(
                    Random.Range(3f, 7f),
                    Random.Range(12f, 45f),
                    Random.Range(3f, 7f)
                );

            ApplyMaterial(
                tower,
                structureMaterial
            );
        }
    }

    private void GeneratePlatforms(
        Transform parent
    )
    {
        for (int i = 0; i < platformCount; i++)
        {
            float angle =
                Random.Range(0f, Mathf.PI * 2f);

            float radius =
                Random.Range(25f, 70f);

            Vector3 position =
                new Vector3(
                    Mathf.Cos(angle) * radius,
                    Random.Range(10f, 50f),
                    Mathf.Sin(angle) * radius
                );

            GameObject platform =
                GameObject.CreatePrimitive(
                    PrimitiveType.Cube
                );

            platform.name =
                "FLOATING TRANSIT PLATFORM";

            platform.transform.SetParent(parent);

            platform.transform.position =
                position;

            platform.transform.localScale =
                new Vector3(
                    Random.Range(5f, 14f),
                    0.6f,
                    Random.Range(5f, 14f)
                );

            ApplyMaterial(
                platform,
                hologramMaterial
            );
        }
    }

    private void GenerateRings(
        Transform parent
    )
    {
        for (int i = 0; i < ringCount; i++)
        {
            float angle =
                i * Mathf.PI * 2f / ringCount;

            float radius =
                35f + i * 2f;

            GameObject ring =
                GameObject.CreatePrimitive(
                    PrimitiveType.Cylinder
                );

            ring.name =
                "DIMENSIONAL RING";

            ring.transform.SetParent(parent);

            ring.transform.position =
                new Vector3(
                    Mathf.Cos(angle) * radius,
                    15f + i * 3f,
                    Mathf.Sin(angle) * radius
                );

            ring.transform.localScale =
                new Vector3(
                    10f,
                    0.12f,
                    10f
                );

            ApplyMaterial(
                ring,
                energyMaterial
            );
        }
    }

    private void GenerateSkyBridges(
        Transform parent
    )
    {
        for (int i = 0; i < 12; i++)
        {
            GameObject bridge =
                GameObject.CreatePrimitive(
                    PrimitiveType.Cube
                );

            bridge.name =
                "ZETRA SKY BRIDGE";

            bridge.transform.SetParent(parent);

            bridge.transform.position =
                new Vector3(
                    Random.Range(-45f, 45f),
                    Random.Range(8f, 42f),
                    Random.Range(-45f, 70f)
                );

            bridge.transform.localScale =
                new Vector3(
                    Random.Range(8f, 22f),
                    0.5f,
                    2f
                );

            bridge.transform.rotation =
                Quaternion.Euler(
                    0f,
                    Random.Range(0f, 360f),
                    Random.Range(-8f, 8f)
                );

            ApplyMaterial(
                bridge,
                energyMaterial
            );
        }
    }

    private GameObject CreateRoot(
        string name
    )
    {
        GameObject root =
            new GameObject(name);

        Undo.RegisterCreatedObjectUndo(
            root,
            "Create CLASHDASH World"
        );

        return root;
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

    private void GenerateSkyCity()
    {
        GenerateStructures(
            CreateRoot(
                "ZETRA SKY CITY"
            ).transform
        );
    }

    private void GenerateEnergyNetwork()
    {
        GenerateRings(
            CreateRoot(
                "ZETRA ENERGY NETWORK"
            ).transform
        );
    }
}

#endif
