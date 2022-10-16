using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

namespace RH.Utilities.Editor.Setup
{
    internal static class ManifestUpdater
    {
        internal static async void LoadNewManifest()
        {
            var url = GetGistUrl("e40685b54eb4bda33e853a5274768b73");
            var content = await GetGistContent(url);
            ReplaceManifest(content);
        }
        
        private static string GetGistUrl(string id) =>
            $"https://gist.githubusercontent.com/redHurt96/{id}/raw";

        private static async Task<string> GetGistContent(string url)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                string content = await response.Content.ReadAsStringAsync();

                return content;
            }
        }

        private static void ReplaceManifest(string to)
        {
            var manifest = Path.Combine(Application.dataPath, "../Packages/manifest.json");
            File.WriteAllText(manifest, to);
            UnityEditor.PackageManager.Client.Resolve();
        }
    }
}