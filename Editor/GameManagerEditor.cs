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
        string prefabPath = "Assets/Backend Engin/Prefabs/GameManager.prefab";
        
        // Check if the prefab exists in the project
        GameObject existingPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

        if (existingPrefab == null)
        {
            Debug.LogError("Prefab not found at specified path: " + prefabPath);
            return;
        }

        // Check if the prefab is already in the scene
        GameObject prefabInScene = GameObject.Find("GameManager");

        if (prefabInScene == null)
        {
            // Create a new GameObject in the scene
            GameObject prefabInstance = PrefabUtility.InstantiatePrefab(existingPrefab) as GameObject;
            prefabInstance.name = existingPrefab.name;
            Debug.Log("Prefab added to the scene: " + prefabInstance.name);
        }
        else
        {
            Debug.Log("Prefab already exists in the scene: " + prefabInScene.name);
        }

        // Refresh the Asset Database and save scene to apply changes
        AssetDatabase.Refresh();
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
    }
}