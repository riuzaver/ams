using ams_finstek_dotnet.Data.Entities;

namespace ams_finstek_dotnet.Data
{
    public interface IAmsRepository
    {
        IEnumerable<User> GetAllUsers(int id = 0, string name = "", string active = "", string email = "", string location = "");
        IEnumerable<Service> GetAllServices();
        List<dynamic> GetUserAccesses(int id = 0);
        bool SaveAll();
        void AddEntity(object model);
        void EditUser(int id, User user);
        void DeleteUser(int id);
        User GetUserById(int id);
    }
}