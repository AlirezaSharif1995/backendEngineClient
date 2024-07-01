using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public class BackendEnginePackageImporter
{
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