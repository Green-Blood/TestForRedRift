using UnityEditor;
using static UnityEditor.AssetDatabase;

namespace JeyTools
{
    public static class ToolsMenu
    {
        [MenuItem("Tools/Setup/Create Default Folders")]
        public static void CreateDefaultFolders()
        {
            Folders.CreateDirectories("",
                "_Scenes", "Scripts", "Textures", "Sprites",
                "Prefabs", "Resources", "Resources/Scriptable objects", "Animations", "Materials", "Physics Materials");
            Refresh();
        }

        [MenuItem("Tools/Setup/Load new Manifest")]
        static async void LoadNewManifest() => await Packages.ReplacePackageFromGist("d2b5cd41601255a7df00ef29974d7bcf");
        
    }
}