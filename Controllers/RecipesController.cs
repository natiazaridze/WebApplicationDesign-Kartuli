using AutoMapper;
using KartuliAPI1.Data.Dtos.Recipes;
using KartuliAPI1.Data.Dtos.Users;
using KartuliAPI1.Data.Entities;
using KartuliAPI1.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KartuliAPI1.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/recipes")]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipesRepository _recipesRepository;
        private readonly IMapper _mapper;
        private readonly JwtService _jwtService;


        public RecipesController(IRecipesRepository recipesRepository, IMapper mapper, JwtService jwtService)
        {
            _mapper = mapper;
            _recipesRepository = recipesRepository;
            _jwtService = jwtService;
        }

        [HttpGet]
        public async Task<IEnumerable<RecipeDto>> GetAll()
        {
            return (await _recipesRepository.GetAll()).Select(o => _mapper.Map<RecipeDto>(o));
        }

        [HttpGet("{recipeId}")]
        public async Task<ActionResult<RecipeDto>> Get(int recipeId)
        {
            var recipe = await _recipesRepository.Get(recipeId);
            if (recipe == null) return NotFound($"Recipe with ID '{recipeId}' not found.");
            return Ok(_mapper.Map<RecipeDto>(recipe));
        }


        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateRecipe([FromBody] LoginDto loginDto)
        {
            var recipe = await _recipesRepository.Authenticate(loginDto.Username, loginDto.Password);

            if (recipe == null)
                return Unauthorized();

            var token = _jwtService.GenerateJwtToken(recipe.Username);

            return Ok(new { Token = token });
        }



        [HttpPost]

        public async Task<ActionResult<RecipeDto>> Post(int recipeId, CreateRecipeDto recipeDto)
        {
            var recipe = _mapper.Map<Recipes>(recipeDto);
            await _recipesRepository.Create(recipe);

            return Created($"api/recipes/{recipe.RecipeId}", _mapper.Map<RecipeDto>(recipe));



    
        }

        [HttpPatch("{recipeId}")]
        public async Task<ActionResult<RecipeDto>> Put(int recipeId, UpdateRecipeDto recipeDto)
        {
            var recipe = await _recipesRepository.Get(recipeId);
            if (recipe == null) return NotFound($"Recipe with ID '{recipeId}' not found.");
          
            _mapper.Map(recipeDto, recipe);

            await _recipesRepository.Patch(recipe);

            return Ok(_mapper.Map<RecipeDto>(recipe));
        }




        [HttpDelete("{recipeId}")]
        public async Task<ActionResult<RecipeDto>> Delete(int recipeId)
        {
            var recipe = await _recipesRepository.Get(recipeId);
            if (recipe == null) return NotFound($"Recipe with ID '{recipeId}' not found.");
            await _recipesRepository.Delete(recipe);
            return NoContent();
        }
    }


}
    