using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using BackendEngine.Models;

namespace BackendEngine
{
    public class LoginManager : MonoBehaviour, IUserManager
    {
        public void LoginUser(string email, string password)
        {
            StartCoroutine(LoginCoroutine(email, password));
        }

        public void RegisterUser(string email, string password, string location, string username, string phoneNumber, string label, string tags)
        {
            // This class does not handle registration, so leave this method empty or throw a NotImplementedException
            throw new System.NotImplementedException();
        }

        private IEnumerator LoginCoroutine(string email, string password)
        {
            user userInfo = new user
            {
                email = email,
                password = password,
                schemaID = PlayerPrefs.GetString("schemaID")
            };

            string baseURL = PlayerPrefs.GetString("baseURL");
            string json = JsonConvert.SerializeObject(userInfo);

            using (UnityWebRequest www = new UnityWebRequest(baseURL + "/login", "POST"))
            {
                byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
                www.uploadHandler = new UploadHandlerRaw(bodyRaw);
                www.downloadHandler = new DownloadHandlerBuffer();
                www.SetRequestHeader("Content-Type", "application/json");

                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.Success)
                {
                    Response player = JsonConvert.DeserializeObject<Response>(www.downloadHandler.text);
                    Debug.Log("Login successful. Player ID: " + player.user.id);
                    Debug.Log("Login successful. Player email: " + player.user.email);
                    Debug.Log("Login successful. Player location: " + player.user.location);
                    Debug.Log("Login successful. Player schemaID: " + player.user.schemaID);
                    Debug.Log("Login successful. Player phoneNumber: " + player.user.phoneNumber);
                    Debug.Log("Login successful. Player label: " + player.user.label);
                    Debug.Log("Login successful. Player tags: " + player.user.tags);
                    Debug.Log("Login successful. Player Username: " + player.user.username);
                }
                else
                {
                    Debug.LogError("Login failed: " + www.error);
                }
            }
        }
    }
}
