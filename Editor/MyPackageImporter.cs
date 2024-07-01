using UnityEditor;
using UnityEngine;

public class MyPackageImporter : AssetPostprocessor
{
    public static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets,
        string[] movedAssets, string[] movedFromAssetPaths)
    {
        Debug.Log("Created folder: ");
        foreach (var assetPath in importedAssets)
        {
            // Check if the imported asset is your package
            if (assetPath.Contains("BackendEngin"))
            {
                // Create a folder if it doesn't exist
                string targetFolder = "Assets/BackendEngin2";
                if (!AssetDatabase.IsValidFolder(targetFolder))
                {
                    AssetDatabase.CreateFolder("Assets", "BackendEngin2");
                    Debug.Log("Created folder: " + targetFolder);
                }
            }
        }
    }
}