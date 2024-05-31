using JWTAuth.WebAPI.Models;

namespace JWTAuth.WebAPI.Interfaces
{
    public interface IUsers
    {
        public List<UserModel> GetUserDetails();
        public UserModel GetUserDetails(int id);
        public void AddUser(UserModel user);
        public void UpdateUser(UserModel user);
        public UserModel DeleteUser(int id);
        public bool CheckUser(int id);
    }
}
