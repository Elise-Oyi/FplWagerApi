using fplWagerApi.Models;
using FplWagerApi.Data;
using FplWagerApi.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FplWagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly FplWagerContext _context;

        public UsersController(FplWagerContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(Users user)
        {

            //// Generate a salt value
            //byte[] salt = new byte[128 / 8];
            //using (var rng = RandomNumberGenerator.Create())
            //{
            //    rng.GetBytes(salt);
            //}

            //// Hash the password using bcrypt
            //string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            //    password: user.Password,
            //    salt: salt,
            //    prf: KeyDerivationPrf.HMACSHA512,
            //    iterationCount: 10000,
            //    numBytesRequested: 256 / 8
            //));

            //// Set the hashed password and salt values
            //user.Password = hashedPassword;
            //user.Salt = Convert.ToBase64String(salt);

            var users = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email || u.FplId == user.FplId);
            if (user != null) return BadRequest(new ProblemDetails { Title = "User with that Email or FplId already exists" });

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task <ActionResult<Users>> Login(Users users)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => (u.Email == users.Email || u.FplId == users.FplId) && u.Password == users.Password);
            if (user == null) return Unauthorized(new ProblemDetails { Title = "No user found" });

            //// Hash the entered password using the stored salt
            //string hashedPassword = HashPassword(user.Password, user.Salt);

            //if (user.Password != hashedPassword)
            //{
            //    return Unauthorized(new ProblemDetails { Title = "invalid password" }); // Invalid password
            //}


             return Ok(user);
        }
        

        //---get wagerList by userId
        [HttpGet("wager-details/{userId}")]
        public async Task<ActionResult<List<WagerList>>> GetWagerByUserId(int userId)
        {
            var wagers = await _context.WagerList.Where(w => w.UserId == userId).ToListAsync();
            if (wagers == null) return BadRequest(new ProblemDetails { Title = "you do not have any wagers" });

            return wagers;
        }


        //---updating users tokens
        [HttpPut("update-tokens")]
        public async Task<ActionResult> UpdateTokens(Users users)
        {
            var user = await _context.Users.FindAsync(users.Id);
            if (user == null)
            {
                return NotFound();
            }

            user.Tokens = users.Tokens; // Update the tokens property

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        // Helper method to hash the password using the salt
        private string HashPassword(string password, string salt)
        {
            // Implement your preferred password hashing algorithm here
            // For example, you can use PBKDF2, bcrypt, or Argon2
            // Here's an example using the built-in SHA256 algorithm

            byte[] saltBytes = Convert.FromBase64String(salt);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            byte[] combinedBytes = new byte[saltBytes.Length + passwordBytes.Length];
            Array.Copy(saltBytes, combinedBytes, saltBytes.Length);
            Array.Copy(passwordBytes, 0, combinedBytes, saltBytes.Length, passwordBytes.Length);

            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(combinedBytes);
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}


//{
//    "FplID":1101,
//    "Fullname":"forward ever",
//    "Email":"forwarjd@gmail.com",
//    "tokens":23,
//    "Password":"123"
//}