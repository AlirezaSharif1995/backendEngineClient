using System.Collections;
using Newtonsoft.Json;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class AppConfig : MonoBehaviour
{
    private string baseUrl;
    public string ID;
    
    [ReadOnly]
    public bool serverConnected = false;

    public void Start()
    {
        baseUrl = "localhost:3030";
        if (baseUrl == null)
        {
            Debug.LogError("Please enter baseURL in Server Manager");
        }
        else
        {
            StartCoroutine(LoginCoroutine(ID));
        }
    }

    private IEnumerator LoginCoroutine(string ID)
    {
        adminPanel admin = new adminPanel();
        admin.ID = ID;
        string json = JsonConvert.SerializeObject(admin);

        using (UnityWebRequest www = new UnityWebRequest(baseUrl + "/loginAdmin/inAppLogin", "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                LoginResponse response = JsonConvert.DeserializeObject<LoginResponse>(www.downloadHandler.text);
                admin = response.user;
                serverConnected = true;
                PlayerPrefs.SetString("schemaID",admin.schemaID);
                PlayerPrefs.SetString("baseURL",admin.baseURL);

                Debug.Log("Login successful. SchemaID: " + admin.schemaID);
                Debug.Log("Login successful. baseURL: " + admin.baseURL);
            }
            else
            {
                Debug.LogError("Login failed: " + www.error);
            }
        }
    }
    
    [System.Serializable]
    public class adminPanel
    {
        public string ID;
        public string schemaID;
        public string baseURL;
    }
    
    [System.Serializable]
    public class LoginResponse
    {
        public adminPanel user;
    }
}
