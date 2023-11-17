using AutoMapper;
using KartuliAPI1.Data.Dtos.Recipes;
using KartuliAPI1.Data.Dtos.Users;
using KartuliAPI1.Data.Dtos.Wines;
using KartuliAPI1.Data.Entities;
using KartuliAPI1.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KartuliAPI1.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/wines")]
    public class WineController : ControllerBase
    {
        private readonly IWinesRepository _wineRepository;
        private readonly IMapper _mapper;
        private readonly JwtService _jwtService;

        public WineController(IWinesRepository wineRepository, IMapper mapper,JwtService jwtService)
        {
            _mapper = mapper;
            _wineRepository = wineRepository;
            _jwtService = jwtService;

        }

        [HttpGet]
        public async Task<IEnumerable<WineDto>> GetAll()
        {
            return (await _wineRepository.GetAll()).Select(o => _mapper.Map<WineDto>(o));
        }

        [HttpGet("{WineId}")]
        public async Task<ActionResult<WineDto>> Get(int WineId)
        {
            var wine = await _wineRepository.Get(WineId);
            if (wine == null) return NotFound($"Wine with ID '{WineId}' not found.");
            return Ok(_mapper.Map<WineDto>(wine));
        }



        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateRecipe([FromBody] LoginDto loginDto)
        {
            var wine = await _wineRepository.Authenticate(loginDto.Username, loginDto.Password);

            if (wine == null)
                return Unauthorized();

            var token = _jwtService.GenerateJwtToken(wine.Username);

            return Ok(new { Token = token });
        }


        [HttpPost]

        public async Task<ActionResult<WineDto>> Post(int WineId, CreateWinesDto winesDto)
        {
            var wine = _mapper.Map<Wines>(winesDto);
            await _wineRepository.Create(wine);

            return Created($"api/wines/{wine.WineId}", _mapper.Map<WineDto>(wine));




        }

        [HttpPatch("{WineId}")]
        public async Task<ActionResult<WineDto>> Put(int WineId, UpdateWinesDto winesDto)
        {
            var wine = await _wineRepository.Get(WineId);
            if (wine == null) return NotFound($"Wine with ID '{WineId}' not found.");

            _mapper.Map(winesDto, wine);

            await _wineRepository.Patch(wine);

            return Ok(_mapper.Map<WineDto>(wine));
        }




        [HttpDelete("{WineId}")]
        public async Task<ActionResult<WineDto>> Delete(int WineId)
        {
            var wine = await _wineRepository.Get(WineId);
            if (wine == null) return NotFound($"Wine with ID '{WineId}' not found.");
            await _wineRepository.Delete(wine);
            return NoContent();
        }
    }


}
