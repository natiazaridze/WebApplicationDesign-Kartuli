namespace KartuliAPI1.Data.Entities
{
    public class Recipes
    {

        public int Id { get; set; }
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDateUtc { get; set; }


    }
}
