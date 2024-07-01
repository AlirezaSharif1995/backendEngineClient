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
            if (GameObject.FindObjectOfType<GameManager>() == null)
            {
                GameObject gameObject = new GameObject("GameManager");
                AppConfig config = new AppConfig();
                gameObject.AddComponent<GameManager>();
                gameObject.AddComponent<AppConfig>();

                // Mark the scene as dirty to ensure the new GameObject is saved
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            }
        }
    }
}