using KartuliAPI1.Data.Entities;

namespace KartuliAPI1.Data.Repositories
    {
        public interface IRecipesRepository
        {
            Task<IEnumerable<Recipes>> GetAll();
            Task<Recipes> Get(int id);
            Task<Recipes> Create(Recipes recipe);
            Task<Recipes> Patch(Recipes recipe);
            Task Delete(Recipes recipe);

            Task<Users> Authenticate(string username, string password);

    }



    public class RecipesRepository : IRecipesRepository
    {

        private readonly List<Users> _users;

        public async Task<IEnumerable<Recipes>> GetAll()

        {


            return new List<Recipes>

            {
            new Recipes()
            {
                
                Id = 0,
                Name = "Test Name of the Dish",
                Description = "This dish is made of vegetables",   


            },

            new Recipes()

            {

                Id = 1,
                Name = "Test Name of the Dish2",
                Description = "This dish is made of meat",


            }




        };

        }
        public async Task<Recipes> Get(int id)
        {

            return new Recipes()

            {
                Id = 2,
                Name = "Test Name of the Dish2",
                Description = "This dish is made of meat",




            };

        }
        public async Task<Recipes> Create(Recipes recipe)
        {

            return new Recipes()

            {
                Name = "Name of the Dish",
                Description = "This dish is made of vegetables and meat",


            };

        }
        public async Task<Recipes> Patch(Recipes recipe)
        {

            return new Recipes()

            {
                Name = "Name of the Dish",
                Description = "This dish is made of vegetables and meat",



            };

        }
        public async Task Delete(Recipes recipe)
        {
            

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