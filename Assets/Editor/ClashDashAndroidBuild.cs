#if UNITY_EDITOR

using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

namespace Zetra.ClashDash
{
    public static class ClashDashAndroidBuild
    {
        public static void BuildAndroid()
        {
            string buildFolder = "build";
            string apkPath =
                Path.Combine(
                    buildFolder,
                    "CLASHDASH.apk"
                );

            if (!Directory.Exists(buildFolder))
                Directory.CreateDirectory(buildFolder);

            string[] scenes =
            {
                SceneManager.GetActiveScene().path
            };

            if (string.IsNullOrEmpty(scenes[0]))
            {
                Debug.LogError(
                    "CLASHDASH: No active scene found."
                );

                return;
            }

            BuildPlayerOptions options =
                new BuildPlayerOptions
                {
                    scenes = scenes,
                    locationPathName = apkPath,
                    target =
                        BuildTarget.Android,
                    options =
                        BuildOptions.None
                };

            BuildReport report =
                BuildPipeline.BuildPlayer(options);

            if (report.summary.result ==
                BuildResult.Succeeded)
            {
                Debug.Log(
                    "CLASHDASH APK BUILD SUCCESS: " +
                    apkPath
                );
            }
            else
            {
                Debug.LogError(
                    "CLASHDASH APK BUILD FAILED: " +
                    report.summary.result
                );
            }
        }
    }
}

#endif
