using System.ComponentModel.DataAnnotations;

namespace KartuliAPI1.Data.Dtos.Users
{
    public record CreateUsersDto([Required]string Username, [Required] string Password, [Required] string Email);

}
