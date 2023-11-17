using System.ComponentModel.DataAnnotations;

namespace KartuliAPI1.Data.Dtos.Wines
    {
        public record CreateWinesDto([Required] string WineName, [Required] string WineDescription);

    }
