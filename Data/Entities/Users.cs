namespace KartuliAPI1.Data.Entities
{
    public class Users
    {
        public int UserId {  get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public List<Recipes> Recipes { get; set; }


    }
}
