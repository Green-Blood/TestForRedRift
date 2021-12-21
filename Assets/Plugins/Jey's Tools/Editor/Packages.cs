using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

namespace JeyTools
{
    public static class Packages
    {
        public static async Task ReplacePackageFromGist(string id, string user = "Green-Blood")
        {
            var url = GetGistUrl(id, user);
            var contents = await GetContent(url);
            ReplacePackageFile(contents);
        }
        public static string GetGistUrl(string id, string user = "Green-Blood") =>
            $"https://gist.githubusercontent.com/{user}/{id}/raw";

        public static async Task<string> GetContent(string url)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public static void ReplacePackageFile(string contents)
        {
            var existing = Path.Combine(Application.dataPath, "../Packages/manifest.json");
            File.WriteAllText(existing, contents);
            UnityEditor.PackageManager.Client.Resolve();
        }
    }
}