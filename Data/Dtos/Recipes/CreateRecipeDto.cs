using System.ComponentModel.DataAnnotations;

namespace KartuliAPI1.Data.Dtos.Recipes
{
    public record CreateRecipeDto([Required] string Name, [Required] string Description);

    }
