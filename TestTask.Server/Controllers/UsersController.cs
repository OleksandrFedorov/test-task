using Microsoft.AspNetCore.Mvc;
using TestTask.Dto.User;
using TestTask.Services.User;

namespace TestTask.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController(UserService userService) : ControllerBase
    {
        private readonly UserService _userService = userService;

        /// <summary>
        /// Returns created user entity.
        /// </summary>
        /// <response code="200">Success creation.</response>
        /// <response code="400">Data is invalid.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAsync([FromForm] string name, [FromForm] string password, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    return BadRequest("User name must be specified");
                }

                if (string.IsNullOrWhiteSpace(password))
                {
                    return BadRequest("User password must be specified");
                }

                var user = new UserDto()
                {
                    Name = name,
                    Password = password
                };

                user = await _userService.AddAsync(user, cancellationToken);
                return user is not null
                    ? Ok(user)
                    : Problem("Internal server error while user creation");
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        /// <summary>
        /// Returns user by specified id.
        /// </summary>
        /// <response code="200">Requested data.</response>
        /// <response code="404">User not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id, cancellationToken);
                return user is null
                    ? NotFound("User not found")
                    : Ok(user);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        /// <summary>
        /// Returns names of all users.
        /// </summary>
        /// <response code="200">Requested data.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllUserNamesAsync(CancellationToken cancellationToken)
        {
            try
            {
                var userNames = await _userService.GetAllUserNamesAsync(cancellationToken);
                return Ok(userNames);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        /// <summary>
        /// Returns session on valid login.
        /// </summary>
        /// <response code="200">User session.</response>
        /// <response code="400">Data is invalid.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> LoginAsync([FromForm] string name, [FromForm] string password, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    return BadRequest("User name must be specified");
                }

                if (string.IsNullOrWhiteSpace(password))
                {
                    return BadRequest("User password must be specified");
                }

                var user = new UserDto() 
                {
                    Name = name,
                    Password = password
                };

                if (await _userService.ValidateLoginAsync(user, cancellationToken))
                {
                    var userSession = await _userService.UserLoginAsync(user, cancellationToken);
                    return Ok(userSession);
                }

                return Unauthorized();

            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        /// <summary>
        /// Returns is user session valid.
        /// </summary>
        /// <response code="200">User session.</response>
        /// <response code="400">Data is invalid.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("session/{sessionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> IsUserSessionValidAsync(Guid sessionId, CancellationToken cancellationToken)
        {
            try
            {
                if (await _userService.ValidateUserSessionAsync(sessionId, cancellationToken))
                {
                    return Ok();
                }

                return Unauthorized();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}
