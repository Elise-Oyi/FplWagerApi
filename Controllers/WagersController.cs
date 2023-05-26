using fplWagerApi.Models;
using FplWagerApi.Data;
using FplWagerApi.Models;
using FplWagerApi.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FplWagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WagersController : ControllerBase
    {
        private readonly FplWagerContext _context;

        public WagersController(FplWagerContext context)
        {
          _context = context;
        }

        //----create wager
        [HttpPost("create-wagers")]
        public async Task<ActionResult> CreateWager(Wagers wager)
        {
            await _context.Wagers.AddAsync(wager);
            await _context.SaveChangesAsync();

            return Ok(wager);
        }

        //----get wager by wagerId
        [HttpGet("get-wager/{wagerId}")]
        public async Task<ActionResult<List<Wagers>>> GetWagerByWagerId(int wagerId)
        {
            var wager = await _context.Wagers.Where(w => w.WagerId == wagerId).ToListAsync();
            if (wager == null) return BadRequest(new ProblemDetails { Title = "wager does not exist" });

            return wager;
        }

      

        //---get wagerList by wagerId
        [HttpGet("wager-lists/{wagerId}")]
        public async Task<ActionResult<List<WagerList>>> GetWagerListByWagerId(int wagerId)
        {
            var wager = await _context.WagerList.Where(w => w.WagerId == wagerId).ToListAsync();
            if(wager == null) return BadRequest(new ProblemDetails { Title = "wager does not exist" });

            return wager;
        }


        //----join wager by wagerId and UserId
        [HttpPost("join-wager")]
        public async Task<ActionResult> JoinWager(WagerListDto wagerListDto)
        {
            
            //---checking if wager exists
           var wager = await _context.Wagers.FindAsync(wagerListDto.WagerId);
            //---checking if user has already joined
            var userExist = await _context.WagerList.FirstOrDefaultAsync(w => w.WagerId == wagerListDto.WagerId && w.UserId == wagerListDto.UserId);

            if (wager == null) return BadRequest(new ProblemDetails { Title = "Wager does not exist" });
            if (wager.IsStarted == true || wager.IsCompleted == true) return BadRequest(new ProblemDetails { Title = "wager has either started or ended" });
            if(userExist != null) return BadRequest(new ProblemDetails { Title = "user is already in the wager" });

            var wagerLists = new WagerList
            {
                WagerId = wagerListDto.WagerId,
                UserId = wagerListDto.UserId,
                FplId = wagerListDto.FplId
            };

            await _context.WagerList.AddAsync(wagerLists);
            await _context.SaveChangesAsync();

            return Ok(wagerListDto);
        }


        //---updating isStarted in wagers using wagerId
        [HttpPut("update-isStarted")]
        public async Task<ActionResult> UpdateIsStarted(Wagers wagers)
        {
            var wager = await _context.Wagers.FindAsync(wagers.WagerId);
            if (wager == null)
            {
                return NotFound();
            }

            wager.IsStarted = wagers.IsStarted; // Update the tokens property

            _context.Wagers.Update(wager);
            await _context.SaveChangesAsync();

            return Ok(wager);
        }


        //---updating isCompleted in wagers using wagerId
        [HttpPut("update-isCompleted")]
        public async Task<ActionResult> UpdateIsCompleted(Wagers wagers)
        {
            var wager = await _context.Wagers.FindAsync(wagers.WagerId);
            if (wager == null)
            {
                return NotFound();
            }

            wager.IsCompleted = wagers.IsCompleted; // Update the tokens property

            _context.Wagers.Update(wager);
            await _context.SaveChangesAsync();

            return Ok(wager);
        }


        //---updating users tokens
        //[HttpPut("update-tokens")]
        //public async Task<ActionResult> UpdateTokens(Users users)
        //{
        //    var user = await _context.Users.FindAsync(users.Id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    user.Tokens = users.Tokens; // Update the tokens property

        //    _context.Users.Update(user);
        //    await _context.SaveChangesAsync();

        //    return Ok(user);
        //}



    }
}

