using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace RH.Utilities.Editor.SimpleCi
{
    internal static class Builder
    {
        private static string[] ActiveScenesPaths =>
            EditorBuildSettings.scenes
            .Where(x => x.enabled)
            .Select(x => x.path)
            .ToArray();

        internal static void ToAndroid()
        {
            BuildPipeline.BuildPlayer(
                new BuildPlayerOptions
                {
                    target = BuildTarget.Android,
                    locationPathName = "Artifacts/Game.apk",
                    scenes = ActiveScenesPaths
                });
        }

        internal static void OpenBuildsFolder() =>
            Process.Start(new ProcessStartInfo() {
                FileName = Path.Combine(Application.dataPath, "..", "Artifacts"),
                UseShellExecute = true,
                Verb = "open"
            });
    }
}