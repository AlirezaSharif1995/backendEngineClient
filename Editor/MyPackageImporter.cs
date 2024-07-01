using UnityEditor;
using UnityEngine;
using System.IO;

public class MyPackageImporter : AssetPostprocessor
{
    private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets,
                                               string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (var assetPath in importedAssets)
        {
            // Check if the imported asset is your package (e.g., a specific file or folder in your package)
              // Define the source and destination paths
                string sourcePath = "Packages/com.asoft.backendengine/Editor/BackendEngin";
                string destinationPath = "Assets/BackendEngin";

                // Copy the folder and its contents
                CopyFolder(sourcePath, destinationPath);
                Debug.Log("Copied folder to: " + destinationPath);
            
        }
    }

    private static void CopyFolder(string sourceFolder, string destinationFolder)
    {
        if (!Directory.Exists(sourceFolder))
        {
            Debug.LogError("Source folder does not exist: " + sourceFolder);
            return;
        }

        // Create destination folder if it doesn't exist
        if (!Directory.Exists(destinationFolder))
        {
            Directory.CreateDirectory(destinationFolder);
        }

        // Copy all files
        foreach (var file in Directory.GetFiles(sourceFolder))
        {
            string destFile = Path.Combine(destinationFolder, Path.GetFileName(file));
            File.Copy(file, destFile, true);
        }

        // Copy all subfolders recursively
        foreach (var subFolder in Directory.GetDirectories(sourceFolder))
        {
            string destSubFolder = Path.Combine(destinationFolder, Path.GetFileName(subFolder));
            CopyFolder(subFolder, destSubFolder);
        }

        // Refresh the asset database to make Unity recognize the new files
        AssetDatabase.Refresh();
    }
}
