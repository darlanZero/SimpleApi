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
        
    }
}