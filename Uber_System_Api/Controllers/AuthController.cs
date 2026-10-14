using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Uber_System_Api.DTOs.AuthDTOs;
using Uber_System_Api.Mapping;
using Uber_System_Api.Models;
using Uber_System_Api.Reposatories.Interface;

namespace Uber_System_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unit;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;


        public AuthController(IUnitOfWork unit, IConfiguration configuration)
        {
            _unit = unit;
            _configuration = configuration;
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();
        }

        [HttpPost("Register")]

        public async Task<IActionResult> Register([FromBody] RegisterDTO DTO)
        {

            if (DTO.Role != "Driver" && DTO.Role != "Rider")
                return BadRequest("Invalid role. Role must be either 'Driver' or 'Rider'.");

            var item = _unit.Users.GetUserByUserNameAsync(DTO.UserFullName);
            if (item != null)
                return BadRequest("User already exists.");

            var Entity = _mapper.Map<User>(DTO);

            await _unit.Users.Add(Entity);

            await _unit.SaveAsync();

            return Ok("User registered successfully.");
        }

        [HttpPost("Login")]

        public async Task<IActionResult> Login([FromBody] LoginDTO DTO)
        {
            var user = await _unit.Users.GetUserByUserNameAsync(DTO.UserFullName);
            if (user == null)
                return NotFound("User not found.");


            if (user.Password != DTO.Password)
                return BadRequest("Invalid password.");


            var token = GenerateJwtToken(user);
            return Ok(token);
        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserFullName),
                new Claim(ClaimTypes.Role, user.Role)
            };


            var token = new JwtSecurityToken(

                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
                
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}