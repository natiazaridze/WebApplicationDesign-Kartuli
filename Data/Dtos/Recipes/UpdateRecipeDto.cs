using System.ComponentModel.DataAnnotations;

namespace KartuliAPI1.Data.Dtos.Recipes
{
    public record UpdateRecipeDto([Required] string Name, [Required] string Description);


  }
