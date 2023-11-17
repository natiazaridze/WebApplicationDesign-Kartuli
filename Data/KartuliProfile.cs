using AutoMapper;
using KartuliAPI1.Data.Dtos.Recipes;
using KartuliAPI1.Data.Dtos.Users;
using KartuliAPI1.Data.Dtos.Wines;
using KartuliAPI1.Data.Entities;

namespace KartuliAPI1.Data
{
    public class KartuliProfile : Profile
    {

        public KartuliProfile() 
        
        {
            CreateMap<Users, UserDto>();
            CreateMap<CreateUsersDto, Users>();
            CreateMap<UpdateUsersDto, Users>();
            CreateMap<Recipes, RecipeDto>();
            CreateMap<CreateRecipeDto, Recipes>();
            CreateMap<UpdateRecipeDto, Recipes>();
            CreateMap<Wines, WineDto>();
            CreateMap<CreateWinesDto,Wines>();
            CreateMap<UpdateWinesDto, Wines>();



        }

    }
}
