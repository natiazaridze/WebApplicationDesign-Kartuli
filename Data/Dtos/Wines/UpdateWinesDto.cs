using System.ComponentModel.DataAnnotations;

namespace KartuliAPI1.Data.Dtos.Wines

    {


    public record UpdateWinesDto([Required] string WineName, [Required] string WineDescription); 
}
