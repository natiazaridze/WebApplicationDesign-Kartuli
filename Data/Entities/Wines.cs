namespace KartuliAPI1.Data.Entities
{
    public class Wines
    {
        public int Id { get; set; }
        public int WineId { get; set; }
        public string WineDescription { get; set; }
        public string WineName { get; set; }
        public DateTime CreationDateUtc { get; set; }

    }
}
