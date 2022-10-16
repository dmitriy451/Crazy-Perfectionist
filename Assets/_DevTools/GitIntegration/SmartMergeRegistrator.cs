#if UNITY_EDITOR
using System;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace GitIntegration
{
    [InitializeOnLoad]
    public class SmartMergeRegistrator
    {
        private const string SmartMergeRegistratorEditorPrefsKey = "smart_merge_installed";
        private const int Version = 1;
        private static readonly string VersionKey = $"{Version}_{Application.unityVersion}";

        //Unity calls the static constructor when the engine opens
        static SmartMergeRegistrator()
        {
            var instaledVersionKey = EditorPrefs.GetString(SmartMergeRegistratorEditorPrefsKey);
            if (instaledVersionKey != VersionKey)
                SmartMergeRegister();
        }

        public static string ExecuteGitWithParams(string param)
        {
            var processInfo = new ProcessStartInfo("git");

            processInfo.UseShellExecute = false;
            processInfo.WorkingDirectory = Environment.CurrentDirectory;
            processInfo.RedirectStandardOutput = true;
            processInfo.RedirectStandardError = true;
            processInfo.CreateNoWindow = true;

            var process = new Process();
            process.StartInfo = processInfo;
            process.StartInfo.FileName = "git";
            process.StartInfo.Arguments = param;
            process.Start();
            process.WaitForExit();

            if (process.ExitCode != 0)
                throw new Exception(process.StandardError.ReadLine());

            return process.StandardOutput.ReadLine();
        }

        [MenuItem("Tools/Git/SmartMerge registration")]
        private static void SmartMergeRegister()
        {
            try
            {
                var UnityYAMLMergePath = EditorApplication.applicationContentsPath + "/Tools" + "/UnityYAMLMerge.exe";
                ExecuteGitWithParams("config merge.unityyamlmerge.name \"Unity SmartMerge (UnityYamlMerge)\"");
                ExecuteGitWithParams(
                    $"config merge.unityyamlmerge.driver \"\\\"{UnityYAMLMergePath}\\\" merge -h -p --force --fallback none %O %B %A %A\"");
                ExecuteGitWithParams("config merge.unityyamlmerge.recursive binary");
                EditorPrefs.SetString(SmartMergeRegistratorEditorPrefsKey, VersionKey);
                Debug.Log($"Succesfuly registered UnityYAMLMerge with path {UnityYAMLMergePath}");
            }
            catch (Exception e)
            {
                Debug.Log($"Fail to register UnityYAMLMerge with error: {e}");
            }
        }
    }
}
#endif