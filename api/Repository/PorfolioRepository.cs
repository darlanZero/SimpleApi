using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.data;
using api.interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PorfolioRepository : IPorfolioRepository
    {
        private readonly ApplicationDBContext _context;
        public PorfolioRepository(ApplicationDBContext context)
        {
            _context = context;
        }
       
        public async Task<List<StockModel>> GetUserPortfolio(AppUser user)
        {
            return await _context.Portfolios
                .Where(p => p.AppUserId == user.Id)
                .Select(stock => new StockModel
                {
                    Id = stock.StockId,
                    Symbol = stock.Stock.Symbol,
                   CompanyName = stock.Stock.CompanyName,
                   Purchase = stock.Stock.Purchase,
                   LastDiv = stock.Stock.LastDiv,
                   Industry = stock.Stock.Industry,
                    MarketCap = stock.Stock.MarketCap,
                  
                })
                .ToListAsync();
        }
    }
}