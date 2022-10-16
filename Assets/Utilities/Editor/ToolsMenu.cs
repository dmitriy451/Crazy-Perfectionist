using RH.Utilities.Editor.Setup;
using RH.Utilities.Editor.SimpleCi;
using UnityEditor;
using static RH.Utilities.Editor.ToolsMenuNames;

namespace RH.Utilities.Editor
{
    public static class ToolsMenu
    {
        [MenuItem(TOOLS + "/" + SETUP + "/📄 Replace manifest")]
        private static void ReplaceManifest() => ManifestUpdater.LoadNewManifest();

        [MenuItem(TOOLS + "/" + SETUP + "/📁 Create default folders")]
        private static void CreateDefaultFolders() => DirectoryCreator.CreateDefaultFolders();

        [MenuItem(TOOLS + "/" + BUILD + "/📁 Show build folder")]
        public static void OpenBuildsFolder() => Builder.OpenBuildsFolder();

        [MenuItem(TOOLS + "/" + BUILD + "/Android")]
        public static void BuildAndroid() => Builder.ToAndroid();
    }
}