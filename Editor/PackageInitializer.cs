using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace BackendEngine.Editor
{
    [InitializeOnLoad]
    public class PackageInitializer
    {
        static PackageInitializer()
        {
            EditorApplication.update += CreateGameObject;
        }

        private static void CreateGameObject()
        {
            EditorApplication.update -= CreateGameObject;

            // Check if the GameObject already exists to prevent duplicates
            if (GameObject.FindObjectOfType<AppConfig>() == null)
            {
                GameObject gameObject = new GameObject("AppConfig");
                gameObject.AddComponent<AppConfig>();
                // Mark the scene as dirty to ensure the new GameObject is saved
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            }
            else
            {
                Debug.Log("GameManager GameObject already exists.");
            }
        }
    }
}