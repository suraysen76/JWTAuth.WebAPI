using JWTAuth.WebAPI.Interfaces;
using JWTAuth.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JWTAuth.WebAPI.Repositories
{
    public class UserRepository:IUsers
    {
        readonly DatabaseContext _dbContext = new();

        public UserRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<UserModel> GetUserDetails()
        {
            try
            {
                return _dbContext.Users.ToList();
            }
            catch
            {
                throw;
            }
        }

        public UserModel GetUserDetails(int id)
        {
            try
            {
                UserModel? user = _dbContext.Users.Where(u => u.UserId == id).FirstOrDefault();
                if (user != null)
                {
                    return user;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }

        public void AddUser(UserModel user)
        {
            try
            {
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void UpdateUser(UserModel user)
        {
            try
            {
                _dbContext.Entry(user).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public UserModel DeleteUser(int id)
        {
            try
            {
                UserModel? user = _dbContext.Users.Where(ee => ee.UserId == id).FirstOrDefault();

                if (user != null)
                {
                    _dbContext.Users.Remove(user);
                    _dbContext.SaveChanges();
                    return user;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }

        public bool CheckUser(int id)
        {
            return _dbContext.Users.Any(e => e.UserId == id);
        }

        
    }
}
