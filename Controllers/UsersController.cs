using AutoMapper;
using KartuliAPI1.Data.Dtos.Recipes;
using KartuliAPI1.Data.Dtos.Users;
using KartuliAPI1.Data.Entities;
using KartuliAPI1.Data.Repositories;
using KartuliAPI1.Data.Repositories.KartuliAPI1.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace KartuliAPI1.Controllers

{
    [Authorize]
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;
        private readonly JwtService _jwtService;
        public UsersController(IUsersRepository usersRepository, IMapper mapper, JwtService jwtService)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
            _jwtService = jwtService;
        }


        [HttpGet]

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            return (await _usersRepository.GetAll()).Select(o => _mapper.Map<UserDto>(o));

        }

        [HttpGet("{UserId}")]
        public async Task<ActionResult<UserDto>> Get(int UserId)
        {

            var user = await _usersRepository.Get(UserId);
            if (user == null) return NotFound($"User with ID '{UserId}' not found.");

            return Ok(_mapper.Map<UserDto>(user));
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginDto loginDto)
        {
            var user = await _usersRepository.Authenticate(loginDto.Username, loginDto.Password);

            if (user == null)
                return Unauthorized();

            var token = _jwtService.GenerateJwtToken(user.Username);

            return Ok(new { Token = token });
        }

        [HttpPost]

        public async Task<ActionResult<UserDto>> Post(CreateUsersDto usersDto, int UserId )
        {

            var user = _mapper.Map<Users>(usersDto);
            await _usersRepository.Create(user);



            if (user == null) return NotFound($"User with ID '{UserId}' not found.");

            return Created($"api/users/{user.UserId}", _mapper.Map<UserDto>(user));
        }


         [HttpPatch("{UserId}")]

        public async Task<ActionResult<UserDto>> Put(int UserId, UpdateUsersDto usersDto)
        {

            var user = await _usersRepository.Get(UserId);
            if (user == null) return NotFound($"User with ID '{UserId}' not found.");

            _mapper.Map(usersDto, user);


            await _usersRepository.Patch(user);

            return Ok(_mapper.Map<UserDto>(user));


        }



        [HttpDelete("{UserId}")]

        public async Task<ActionResult<UserDto>> Delete(int UserId)
        {

            var user = await _usersRepository.Get(UserId);
            if (user == null) return NotFound($"User with ID '{UserId}' not found.");


            await _usersRepository.Delete(user);

            return NoContent();


        }


        [HttpGet("{userId}/recipes")]
        public async Task<ActionResult<IEnumerable<RecipeDto>>> GetUserRecipes(int userId)
        {
            var recipes = await _usersRepository.GetUserRecipes(userId);
            return Ok(recipes.Select(r => _mapper.Map<RecipeDto>(r)));
        }




    }
}
