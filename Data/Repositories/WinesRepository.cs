using KartuliAPI1.Data.Entities;
using Microsoft.IdentityModel.Tokens;

namespace KartuliAPI1.Data.Repositories
{

    public interface IWinesRepository
    {
        Task<IEnumerable<Wines>> GetAll();
        Task<Wines> Get(int WineId);
        Task<Wines> Create(Wines wine);
        Task<Wines> Patch(Wines wine);
        Task Delete(Wines wine);

        Task<Users> Authenticate(string username, string password);
    }

    public class WinesRepository : IWinesRepository 
    
    {
        private readonly List<Users> _users;


        public async Task<IEnumerable<Wines>> GetAll()
        { 

            return new List<Wines>

            {

            new Wines()
            {

            WineId = 0,
            WineName = "Kindzmarauli",
            WineDescription = "Best Wine",


            },

        new Wines()

            {

            WineId = 1,
            WineName = "Saperavi",
            WineDescription = "Best Wine",

            }





            };
    }
    public async Task<Wines> Get(int WineId)
    {

        return new Wines()

        {

            WineId = 0,
            WineName = "Kindzmarauli",
            WineDescription = "Best Wine",



            // return 404 if different id


        };

    }
    public async Task<Wines> Create(Wines wine)
    {

        return new Wines()

        {
            WineName = "Kindzmarauli",
            WineDescription = "Best Wine",


        };

    }
    public async Task<Wines> Patch(Wines wine)
    {

        return new Wines()

        {
            WineName = "Kindzmarauli",
            WineDescription = "Best Wine",



        };

    }
        public async Task Delete(Wines wine)
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

