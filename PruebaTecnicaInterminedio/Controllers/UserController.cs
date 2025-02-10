namespace PruebaTecnicaInterminedio.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.HttpResults;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore.Metadata.Conventions;
    using PruebaTecnicaInterminedio.Models;
    using PruebaTecnicaInterminedio.Models.Dto;
    using PruebaTecnicaInterminedio.Repository.IRepository;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public UserController(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        [HttpPost("userCreate")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> createUserAsync([FromBody] CreateUserDto createUserDto) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (createUserDto == null) 
            {
                return BadRequest(ModelState);
            }
            if (!_usersRepository.IsValidEmail(createUserDto.Email)) { ModelState.AddModelError("", $"Email no valido");  return StatusCode(404, ModelState); }
            TimeSpan newTime = DateTime.Now.Subtract(createUserDto.BurnsDay);
            if (newTime.TotalDays < 6571) 
            {
                ModelState.AddModelError("", $"No se puede registrar un menor de edad{createUserDto.BurnsDay}");
                return StatusCode(404, ModelState);
            }
            var newUser = _mapper.Map<Usuario>(createUserDto);
            newUser.GuidUser = new string(Guid.NewGuid().ToString());
            
            var respuesta = await _usersRepository.createUser(newUser);
            if (!respuesta)
            {
                ModelState.AddModelError("", $"No fue posible crear el usuario{newUser.NameUser}");
                return StatusCode(404, ModelState);
            }
            ModelState.AddModelError("", $"Usuario creado{newUser.NameUser}");
            return Ok(StatusCode(200, ModelState));
        }

        [HttpDelete("userDelete")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteUSer([FromBody] DeleteUserDto deleteUserDto) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (deleteUserDto == null)
            {
                return BadRequest(ModelState);
            }
            var email = _usersRepository.IsValidEmail(deleteUserDto.Email);
            if (!email) { ModelState.AddModelError("", $"Email no valido"); return StatusCode(404, ModelState); }
            var respuesta = await _usersRepository.DeleteUSer(deleteUserDto.Email);
            if (!respuesta)
            {
                ModelState.AddModelError("", $"No fue posible eliminar el usuario{deleteUserDto.Email}");
                return StatusCode(404, ModelState);
            }
            ModelState.AddModelError("", $"Usuario Eliminado");
            return Ok(StatusCode(200, ModelState));
        }

        [HttpGet("userGet")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUsuario() 
        {
            var lista = await _usersRepository.GetUsuario();
            if (lista == null) 
            {
                ModelState.AddModelError("", $"There is not data");
                return StatusCode(404, ModelState);
            }
            return Ok(lista);
        }

        [HttpGet("userGetByEmail")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUserById([FromBody] DeleteUserDto getUserById) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (getUserById == null)
            {
                return BadRequest(ModelState);
            }
            var lista = await _usersRepository.GetUsuarioById(getUserById.Email);
            if (lista == null)
            {
                ModelState.AddModelError("", $"There is not data");
                return StatusCode(404, ModelState);
            }
            return Ok(lista);

        }

        [HttpPost("userUpdate")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUser([FromBody]UpdateUserDto updateUserDto) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (updateUserDto == null)
            {
                return BadRequest(ModelState);
            }
            if (!_usersRepository.IsValidEmail(updateUserDto.OldEmail) || !_usersRepository.IsValidEmail(updateUserDto.Email))
            {
                ModelState.AddModelError("", $"Email no valido"); return StatusCode(404, ModelState);
            }
            var respuesta = await _usersRepository.UpdateUser(updateUserDto);
            if (!respuesta)
            {
                ModelState.AddModelError("", $"We can't update de register");
                return StatusCode(404, ModelState);
            }
            return Ok();
        }
    }
}
