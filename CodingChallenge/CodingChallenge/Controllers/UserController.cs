using CodingChallenge.Exceptions;
using CodingChallenge.Interfaces;
using CodingChallenge.Models.DTOs;
using CodingChallenge.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodingChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("ReactPolicy")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            try
            {
                // Check if user with the same username already exists
                var allUsers = await _userService.GetAllUser();
                var existingUser = allUsers.FirstOrDefault(u => u.UserName == user.UserName);
                if (existingUser != null)
                {
                    return Conflict($"Username '{user.UserName}' is already taken.");
                }

                // Perform additional username validation if needed
                if (!IsValidUsername(user.UserName))
                {
                    return BadRequest($"Invalid username format.");
                }

                // Add user using UserService method
                var addedUser = await _userService.AddUser(user);
                return Ok(addedUser);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, $"Failed to register user: {ex.Message}");
            }
        }

        // Helper method to validate username format (you can customize as per your requirements)
        private bool IsValidUsername(string username)
        {
            // Example: Check if username length is between 5 to 20 characters
            if (username.Length < 5 || username.Length > 20)
            {
                return false;
            }

            // Example: Check if username contains only alphanumeric characters and underscores
            return System.Text.RegularExpressions.Regex.IsMatch(username, @"^[a-zA-Z0-9_]+$");
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login([FromBody] LoginDto loginDTO)
        {
            try
            {
                // Validate request
                if (loginDTO == null || string.IsNullOrEmpty(loginDTO.UserName) || string.IsNullOrEmpty(loginDTO.Password))
                {
                    return BadRequest("Username and password are required.");
                }

                // Fetch all users
                var users = await _userService.GetAllUser();

                // Find the user by username and validate password
                var user = users.FirstOrDefault(u => u.UserName == loginDTO.UserName && u.Password == loginDTO.Password);

                if (user == null)
                {
                    return NotFound("Invalid username or password.");
                }

                // Log successful login attempt
                _logger.LogInformation($"User {user.UserName} logged in successfully.");

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login.");
                return StatusCode(500, "Internal server error occurred.");
            }
        }


        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUser();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all users");
                return StatusCode(500, "Internal server error occurred");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            try
            {
                var user = await _userService.GetUser(id);
                return Ok(user);
            }
            catch (NoSuchUserException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while fetching user with ID {id}");
                return StatusCode(500, "Internal server error occurred");
            }
        }

        [HttpPost]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            try
            {
                var addedUser = await _userService.AddUser(user);
                return CreatedAtAction(nameof(GetUser), new { id = addedUser.UserId }, addedUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new user");
                return StatusCode(500, "Internal server error occurred");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest("User ID in the request path does not match the user ID in the request body");
            }

            try
            {
                var updatedUser = await _userService.UpdateUser(user);
                return Ok(updatedUser);
            }
            catch (NoSuchUserException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating user with ID {id}");
                return StatusCode(500, "Internal server error occurred");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await _userService.GetUser(id);
                if (user == null)
                {
                    return NotFound();
                }

                await _userService.RemoveUser(user);
                return NoContent();
            }
            catch (NoSuchUserException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting user with ID {id}");
                return StatusCode(500, "Internal server error occurred");
            }
        }
    }
}
