namespace BackendEngine
{
    public interface IUserManager
    {
        void RegisterUser(string email, string password, string location, string username, string phoneNumber, string label, string tags);
        void LoginUser(string email, string password);
    }
}