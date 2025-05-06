using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.data;
using api.Extensions;
using api.interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepo;
        private readonly IPorfolioRepository _porfolioRepo;
        public PortfolioController(UserManager<AppUser> userManager, 
            IStockRepository stockRepo,
            IPorfolioRepository porfolioRepo
        )
        {
            _stockRepo = stockRepo;
            _userManager = userManager;
            _porfolioRepo = porfolioRepo;

        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPorfolio()
        {
            var email = User.GetEmail();
            var username = User.GetUsername();
            var AppUser = await _userManager.FindByEmailAsync(email);
            var userPorfolio = await _porfolioRepo.GetUserPortfolio(AppUser);
            if (userPorfolio == null)
            {
                return NotFound("No porfolio found for this user");
            }
            return Ok(userPorfolio);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            var email = User.GetEmail();
            var username = User.GetUsername();
            var AppUser = await _userManager.FindByNameAsync(username);
            if (AppUser == null)
            {
                return NotFound("User not found");
            }
            var stock = await _stockRepo.GetBySymbolAsync(symbol);

            if (stock == null)
            {
                return NotFound("Stock not found");
            }

            var userPortfolio = await _porfolioRepo.GetUserPortfolio(AppUser);
            if (userPortfolio.Any(s => s.Symbol.ToLower() == stock.Symbol.ToLower()))
            {
                return BadRequest("Stock already in portfolio");
            }

            var portfolioModel = new Portfolio
            {
                AppUserId = AppUser.Id,
                StockId = stock.Id,
            };

            await _porfolioRepo.CreateAsync(portfolioModel);
            if(portfolioModel == null)
            {
                return StatusCode(500, "Failed to add stock to portfolio");
            } else
            {
                return Created();
            }
            
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(string symbol)
        {
            var email = User.GetEmail();
            var username = User.GetUsername();
            var AppUser = await _userManager.FindByNameAsync(username);
            if (AppUser == null)
            {
                return NotFound("User not found");
            }

            var userPortfolio = await _porfolioRepo.GetUserPortfolio(AppUser);
            var filteredStock = userPortfolio.Where(s => s.Symbol.ToLower() == symbol.ToLower()).ToList();

            if (filteredStock.Count == 1)
            {
                await _porfolioRepo.DeletePorfolio(AppUser, symbol);
                
            } else {
                return BadRequest("Stock not found in portfolio");
            }
            
            return Ok("Stock removed from portfolio");
        }
        
    }
}