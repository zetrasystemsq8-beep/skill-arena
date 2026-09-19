#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class ClashDashArenaSceneBuilder : EditorWindow
{
    [MenuItem("CLASHDASH/BUILD ARENA 01")]
    public static void Open()
    {
        GetWindow<ClashDashArenaSceneBuilder>(
            "ARENA 01 BUILDER"
        );
    }

    private void OnGUI()
    {
        GUILayout.Label(
            "CLASHDASH // ARENA 01",
            EditorStyles.boldLabel
        );

        GUILayout.Space(10);

        if (GUILayout.Button(
            "CREATE PLAYABLE ARENA",
            GUILayout.Height(55)
        ))
        {
            BuildArena();
        }
    }

    private void BuildArena()
    {
        Scene scene =
            EditorSceneManager.NewScene(
                NewSceneSetup.EmptyScene,
                NewSceneMode.Single
            );

        scene.name = "Arena_01";

        CreateArena();
        CreatePlayer();
        CreateCamera();
        CreateLighting();
        CreateSystems();

        EditorSceneManager.SaveScene(
            scene,
            "Assets/Scenes/Arena_01.unity"
        );

        Debug.Log(
            "CLASHDASH // ARENA_01 READY"
        );
    }

    private void CreateArena()
    {
        GameObject floor =
            GameObject.CreatePrimitive(
                PrimitiveType.Cube
            );

        floor.name = "ARENA CORE";

        floor.transform.position =
            new Vector3(0f, -0.5f, 30f);

        floor.transform.localScale =
            new Vector3(70f, 1f, 100f);

        CreatePlatform(
            "PLATFORM LEFT",
            new Vector3(-22f, 2f, 18f),
            new Vector3(16f, 1f, 14f)
        );

        CreatePlatform(
            "PLATFORM RIGHT",
            new Vector3(22f, 4f, 30f),
            new Vector3(16f, 1f, 14f)
        );

        CreatePlatform(
            "FINAL PLATFORM",
            new Vector3(0f, 5f, 75f),
            new Vector3(24f, 1f, 18f)
        );
    }

    private void CreatePlatform(
        string name,
        Vector3 position,
        Vector3 scale
    )
    {
        GameObject platform =
            GameObject.CreatePrimitive(
                PrimitiveType.Cube
            );

        platform.name = name;
        platform.transform.position = position;
        platform.transform.localScale = scale;
    }

    private void CreatePlayer()
    {
        GameObject player =
            new GameObject("PLAYER // CONNECT");

        player.tag = "Player";

        CharacterController controller =
            player.AddComponent<
                CharacterController
            >();

        controller.height = 2f;
        controller.radius = 0.45f;
        controller.center =
            new Vector3(0f, 1f, 0f);

        player.transform.position =
            new Vector3(0f, 1f, 0f);

        player.AddComponent<
            PlayerController
        >();

        player.AddComponent<
            ClashDashPlayerImpact
        >();
    }

    private void CreateCamera()
    {
        GameObject cameraObject =
            new GameObject("CLASHDASH CAMERA");

        Camera camera =
            cameraObject.AddComponent<Camera>();

        cameraObject.AddComponent<
            MobileCameraController
        >();

        camera.fieldOfView = 65f;
    }

    private void CreateLighting()
    {
        GameObject lightObject =
            new GameObject("ZETRA CORE LIGHT");

        Light light =
            lightObject.AddComponent<Light>();

        light.type =
            LightType.Directional;

        light.intensity = 1.2f;

        lightObject.transform.rotation =
            Quaternion.Euler(
                45f,
                -30f,
                0f
            );
    }

    private void CreateSystems()
    {
        GameObject arena =
            new GameObject(
                "CLASHDASH ARENA CORE"
            );

        arena.AddComponent<
            ArenaCoreController
        >();

        arena.AddComponent<
            ClashDashArenaDirector
        >();

        arena.AddComponent<
            ClashDashEventController
        >();

        GameObject world =
            new GameObject(
                "ZETRA WORLD SYSTEM"
            );

        world.AddComponent<
            ClashDashWorldDirector
        >();

        world.AddComponent<
            ClashDashRealityController
        >();

        GameObject identity =
            new GameObject(
                "CLASHDASH IDENTITY"
            );

        identity.AddComponent<
            ClashDashIdentity
        >();

        GameObject performance =
            new GameObject(
                "PERFORMANCE SYSTEM"
            );

        performance.AddComponent<
            ClashDashPerformanceManager
        >();
    }
}

#endif
