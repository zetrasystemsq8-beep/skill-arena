#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

public class ClashDashArenaBuilder : EditorWindow
{
    private Material arenaMaterial;
    private Material energyMaterial;
    private Material hologramMaterial;

    private float arenaSize = 70f;
    private float platformHeight = 1f;

    [MenuItem("CLASHDASH/ZETRA Arena Builder")]
    public static void Open()
    {
        GetWindow<ClashDashArenaBuilder>(
            "CLASHDASH BUILDER"
        );
    }

    private void OnGUI()
    {
        GUILayout.Label(
            "CLASHDASH // THE TEST",
            EditorStyles.boldLabel
        );

        GUILayout.Space(10);

        arenaSize = EditorGUILayout.FloatField(
            "Arena Size",
            arenaSize
        );

        platformHeight = EditorGUILayout.FloatField(
            "Platform Height",
            platformHeight
        );

        GUILayout.Space(10);

        arenaMaterial =
            (Material)EditorGUILayout.ObjectField(
                "Arena Material",
                arenaMaterial,
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

        hologramMaterial =
            (Material)EditorGUILayout.ObjectField(
                "Hologram Material",
                hologramMaterial,
                typeof(Material),
                false
            );

        GUILayout.Space(15);

        if (GUILayout.Button(
            "BUILD THE TEST",
            GUILayout.Height(45)
        ))
        {
            BuildArena();
        }

        GUILayout.Space(8);

        if (GUILayout.Button(
            "BUILD ZETRA TOWERS",
            GUILayout.Height(35)
        ))
        {
            BuildTowers();
        }

        if (GUILayout.Button(
            "BUILD ENERGY RINGS",
            GUILayout.Height(35)
        ))
        {
            BuildEnergyRings();
        }

        if (GUILayout.Button(
            "BUILD FLOATING PLATFORMS",
            GUILayout.Height(35)
        ))
        {
            BuildFloatingPlatforms();
        }
    }

    private void BuildArena()
    {
        GameObject root =
            CreateObject(
                "CLASHDASH // THE TEST",
                Vector3.zero
            );

        CreatePlatform(
            root.transform,
            "CORE PLATFORM",
            Vector3.zero,
            new Vector3(
                arenaSize,
                platformHeight,
                arenaSize
            ),
            arenaMaterial
        );

        CreatePlatform(
            root.transform,
            "LEFT PLATFORM",
            new Vector3(-25f, 3f, 12f),
            new Vector3(18f, 1f, 14f),
            arenaMaterial
        );

        CreatePlatform(
            root.transform,
            "RIGHT PLATFORM",
            new Vector3(25f, 5f, 18f),
            new Vector3(16f, 1f, 12f),
            arenaMaterial
        );

        CreatePlatform(
            root.transform,
            "HIGH PLATFORM",
            new Vector3(0f, 9f, 32f),
            new Vector3(20f, 1f, 12f),
            arenaMaterial
        );

        CreatePlatform(
            root.transform,
            "FINAL PLATFORM",
            new Vector3(0f, 4f, 48f),
            new Vector3(24f, 1f, 18f),
            arenaMaterial
        );

        Debug.Log(
            "CLASHDASH ARENA CREATED"
        );
    }

    private void BuildTowers()
    {
        GameObject root =
            CreateObject(
                "ZETRA TOWERS",
                Vector3.zero
            );

        CreateTower(
            root.transform,
            new Vector3(-30f, 7f, 20f),
            14f
        );

        CreateTower(
            root.transform,
            new Vector3(30f, 10f, 28f),
            20f
        );

        CreateTower(
            root.transform,
            new Vector3(-35f, 14f, 48f),
            28f
        );

        CreateTower(
            root.transform,
            new Vector3(35f, 18f, 55f),
            36f
        );
    }

    private void BuildEnergyRings()
    {
        GameObject root =
            CreateObject(
                "DIMENSIONAL ENERGY RINGS",
                Vector3.zero
            );

        for (int i = 0; i < 8; i++)
        {
            float angle =
                i * Mathf.PI * 2f / 8f;

            float radius = 26f;

            Vector3 position =
                new Vector3(
                    Mathf.Cos(angle) * radius,
                    5f + i * 2f,
                    Mathf.Sin(angle) * radius
                );

            GameObject ring =
                GameObject.CreatePrimitive(
                    PrimitiveType.Cylinder
                );

            ring.name =
                "ENERGY RING " + (i + 1);

            ring.transform.SetParent(
                root.transform
            );

            ring.transform.position =
                position;

            ring.transform.localScale =
                new Vector3(
                    7f,
                    0.15f,
                    7f
                );

            if (energyMaterial != null)
            {
                ring.GetComponent<Renderer>()
                    .sharedMaterial =
                    energyMaterial;
            }
        }
    }

    private void BuildFloatingPlatforms()
    {
        GameObject root =
            CreateObject(
                "FLOATING CITY",
                Vector3.zero
            );

        for (int i = 0; i < 12; i++)
        {
            float angle =
                i * Mathf.PI * 2f / 12f;

            float radius =
                35f + (i % 3) * 8f;

            Vector3 position =
                new Vector3(
                    Mathf.Cos(angle) * radius,
                    12f + (i % 4) * 5f,
                    Mathf.Sin(angle) * radius
                );

            CreatePlatform(
                root.transform,
                "FLOATING PLATFORM " +
                (i + 1),
                position,
                new Vector3(
                    8f,
                    1f,
                    8f
                ),
                hologramMaterial
            );
        }
    }

    private void CreateTower(
        Transform parent,
        Vector3 position,
        float height
    )
    {
        GameObject tower =
            GameObject.CreatePrimitive(
                PrimitiveType.Cube
            );

        tower.name = "ZETRA SPIRE";

        tower.transform.SetParent(parent);

        tower.transform.position =
            position;

        tower.transform.localScale =
            new Vector3(
                3f,
                height,
                3f
            );

        if (hologramMaterial != null)
        {
            tower.GetComponent<Renderer>()
                .sharedMaterial =
                hologramMaterial;
        }
    }

    private void CreatePlatform(
        Transform parent,
        string name,
        Vector3 position,
        Vector3 scale,
        Material material
    )
    {
        GameObject platform =
            GameObject.CreatePrimitive(
                PrimitiveType.Cube
            );

        platform.name = name;

        platform.transform.SetParent(parent);

        platform.transform.position =
            position;

        platform.transform.localScale =
            scale;

        if (material != null)
        {
            platform.GetComponent<Renderer>()
                .sharedMaterial =
                material;
        }
    }

    private GameObject CreateObject(
        string name,
        Vector3 position
    )
    {
        GameObject obj =
            new GameObject(name);

        obj.transform.position =
            position;

        return obj;
    }
}

#endif
