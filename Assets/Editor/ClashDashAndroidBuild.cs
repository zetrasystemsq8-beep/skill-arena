#if UNITY_EDITOR

using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

namespace Zetra.ClashDash
{
    public static class ClashDashAndroidBuild
    {
        public static void BuildAndroid()
        {
            string buildFolder = "build";
            string apkPath = Path.Combine(buildFolder, "CLASHDASH.apk");

            if (!Directory.Exists(buildFolder))
                Directory.CreateDirectory(buildFolder);

            List<string> scenePaths = new List<string>();
            foreach (var scene in EditorBuildSettings.scenes)
            {
                if (scene.enabled)
                    scenePaths.Add(scene.path);
            }

            if (scenePaths.Count == 0)
            {
                Debug.LogError("CLASHDASH: No scenes found in Build Settings.");
                return;
            }

            BuildPlayerOptions options = new BuildPlayerOptions
            {
                scenes = scenePaths.ToArray(),
                locationPathName = apkPath,
                target = BuildTarget.Android,
                options = BuildOptions.None
            };

            BuildReport report = BuildPipeline.BuildPlayer(options);

            if (report.summary.result == BuildResult.Succeeded)
            {
                Debug.Log("CLASHDASH APK BUILD SUCCESS: " + apkPath);
            }
            else
            {
                Debug.LogError("CLASHDASH APK BUILD FAILED: " + report.summary.result);
            }
        }
    }
}

#endif
