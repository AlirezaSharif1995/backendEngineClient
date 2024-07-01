using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.SceneManagement;


[InitializeOnLoad]
public class BackendEnginePackageImporter
{
    
    private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        var inPackages = importedAssets.Any(path => path.StartsWith("Packages/")) ||
                         deletedAssets.Any(path => path.StartsWith("Packages/")) ||
                         movedAssets.Any(path => path.StartsWith("Packages/")) ||
                         movedFromAssetPaths.Any(path => path.StartsWith("Packages/"));
 
        if (inPackages)
        {
            InitializeOnLoad();
        }
    }
   
    [InitializeOnLoadMethod]
    private static void InitializeOnLoad()
    {
        var listRequest = Client.List(true);
        while (!listRequest.IsCompleted)
            Thread.Sleep(100);
 
        if (listRequest.Error != null)
        {
            Debug.Log("Error: " + listRequest.Error.message);
            return;
        }
 
        var packages = listRequest.Result;
        var text = new StringBuilder("Packages:\n");
        foreach (var package in packages)
        {
            if (package.source == PackageSource.Registry)
                text.AppendLine($"{package.name}: {package.version} [{package.resolvedPath}]");
        }
       
        Debug.Log(text.ToString());
    }
    
    static BackendEnginePackageImporter()
    {
        EditorApplication.delayCall += CheckAndAddPrefab;
    }

    static void CheckAndAddPrefab()
    {
        // This method is executed when the Editor starts and after a delay call
        string prefabPath = "Assets/Backend Engin/Prefabs/GameManager.prefab";

        // Load the prefab at the specified path
        GameObject existingPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

        if (existingPrefab == null)
        {
            Debug.LogError("Prefab not found at specified path: " + prefabPath);
            return;
        }

        // Check if the prefab is already in the scene
        GameObject prefabInScene = GameObject.Find(existingPrefab.name);

        if (prefabInScene == null)
        {
            // Instantiate the prefab into the scene
            GameObject prefabInstance = PrefabUtility.InstantiatePrefab(existingPrefab) as GameObject;
            prefabInstance.name = existingPrefab.name;
            Debug.Log("Prefab added to the scene: " + prefabInstance.name);

            // Mark the scene as dirty to save changes
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
        else
        {
            Debug.Log("Prefab already exists in the scene: " + prefabInScene.name);
        }

        // Refresh the Asset Database to update changes
        AssetDatabase.Refresh();
    }
}