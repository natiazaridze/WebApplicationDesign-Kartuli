using KartuliAPI1.Data.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace KartuliAPI1.Data.Repositories
{


    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    namespace KartuliAPI1.Data.Repositories
    {
        public interface IUsersRepository
        {
            Task<IEnumerable<Users>> GetAll();
            Task<Users> Get(int id);
            Task<Users> Create(Users user);
            Task<Users> Patch(Users user);
            Task Delete(Users user);

            Task<IEnumerable<Recipes>> GetUserRecipes(int userId);

            Task<Users> Authenticate(string username, string password);
        }

        public class UsersRepository : IUsersRepository
        {
            private readonly List<Users> _users;

            public UsersRepository()
            {
                _users = new List<Users>
            {
                new Users
                {
                    UserId = 1,
                    Username = "Test",
                    Email = "test@example.com",
                    Password = "5f4dcc3b5aa765d61d8327deb882cf99",
                    Recipes = new List<Recipes>
                    {
                        new Recipes
                        {
                            RecipeId = 1,
                            Name = "Guruli Gvezeli",
                            Description = "Made of eggs, water and flour"
                        },
                        new Recipes
                        {
                            RecipeId = 2,
                            Name = "Guruli Gvezeli with Cheese",
                            Description = "Made of eggs, water and flour, cheese"
                        }
                    }
                },
                new Users
                {
                    UserId = 2,
                    Username = "Test1",
                    Email = "test1@example.com",
                    Password = "5f4dcc3b5aa765d61d8327deb882cf994", 
                    Recipes = new List<Recipes>
                    {
                        new Recipes
                        {
                            RecipeId = 3,
                            Name = "Another Recipe",
                            Description = "Another description"
                        }
                    }
                }
            };
            }

            public async Task<IEnumerable<Users>> GetAll()
            {
                return _users;
            }

            public async Task<Users> Get(int id)
            {
                return _users.FirstOrDefault(u => u.UserId == id);
            }

            public async Task<Users> Create(Users user)
            {
          
                user.UserId = _users.Count + 1;
                _users.Add(user);
                return user;
            }

            public async Task<Users> Patch(Users user)
            {
                var existingUser = _users.FirstOrDefault(u => u.UserId == user.UserId);
                if (existingUser != null)
                {
                    existingUser.Username = user.Username;
                    existingUser.Email = user.Email;
                }
                return existingUser;
            }

            public async Task Delete(Users user)
            {
                _users.Remove(user);
            }

            public async Task<IEnumerable<Recipes>> GetUserRecipes(int userId)
            {
                var user = _users.FirstOrDefault(u => u.UserId == userId);
                return user?.Recipes ?? Enumerable.Empty<Recipes>();
            }

            public async Task<Users> Authenticate(string username, string password)
            {
                var user = _users.FirstOrDefault(u => u.Username == username && VerifyPassword(password, u.Password));
                return user;
            }

            private bool VerifyPassword(string inputPassword, string storedHashedPassword)
            {
             
                return inputPassword == storedHashedPassword;
            }
        }
    }
}

