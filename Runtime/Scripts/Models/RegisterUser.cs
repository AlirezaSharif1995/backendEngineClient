namespace BackendEngine.Models
{
    [System.Serializable]
    public class Response
    {
        public string message;
        public user user;
    }

    [System.Serializable]
    public class user
    {
        public int id;
        public string username;
        public string email;
        public string password;
        public string location;
        public string schemaID;
        public string phoneNumber;
        public string label;
        public string tags;
    }
}