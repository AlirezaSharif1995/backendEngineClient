using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using BackendEngine.Models;

namespace BackendEngine
{
    public class RegisterManager : MonoBehaviour, IUserManager
    {
        public InputField emailInput;
        public InputField passwordInput;
        public InputField locationInput;
        public InputField usernameInput;
        public InputField phoneNumberInput;
        public InputField labelInput;
        public InputField tagsInput;

        public void OnRegisterButtonClick()
        {
            string email = emailInput.text;
            string password = passwordInput.text;
            string location = locationInput.text;
            string username = usernameInput.text;
            string phoneNumber = phoneNumberInput.text;
            string label = labelInput.text;
            string tags = tagsInput.text;

            RegisterUser(email, password, location, username, phoneNumber, label, tags);
        }

        public void RegisterUser(string email, string password, string location, string username, string phoneNumber, string label, string tags)
        {
            StartCoroutine(RegisterUserCoroutine(email, password, location, username, phoneNumber, label, tags));
        }

        public void LoginUser(string email, string password)
        {
            // This class does not handle login, so leave this method empty or throw a NotImplementedException
            throw new System.NotImplementedException();
        }

        private IEnumerator RegisterUserCoroutine(string email, string password, string location, string username, string phoneNumber, string label, string tags)
        {
            user userInfo = new user
            {
                email = email,
                password = password,
                location = location,
                username = username,
                phoneNumber = phoneNumber,
                label = label,
                tags = tags,
                schemaID = PlayerPrefs.GetString("schemaID")
            };

            string baseURL = PlayerPrefs.GetString("baseURL");
            string json = JsonConvert.SerializeObject(userInfo);

            using (UnityWebRequest www = new UnityWebRequest(baseURL + "/register", "POST"))
            {
                byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
                www.uploadHandler = new UploadHandlerRaw(bodyRaw);
                www.downloadHandler = new DownloadHandlerBuffer();
                www.SetRequestHeader("Content-Type", "application/json");

                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.Success)
                {
                    user player = JsonConvert.DeserializeObject<user>(www.downloadHandler.text);
                    PlayerPrefs.SetInt("playerID", player.id);
                    Debug.Log("Register successful. Player ID: " + player.id);
                }
                else
                {
                    Debug.LogError("Register failed: " + www.error);
                }
            }
        }
    }
}
